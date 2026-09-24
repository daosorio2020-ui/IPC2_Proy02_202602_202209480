using System;
using System.Xml;

namespace Proyecto2_IPC2.Models
{
    public class GestorXML
    {
        // El sistema requiere buscar libros a nivel general y por categoría[span_2](start_span)[span_2](end_span)
        // Mantendremos una lista de categorías raíz y un árbol global para búsquedas rápidas de ISBN.
        public ListaCategorias CategoriasRaiz { get; set; }
        public ArbolLibros CatalogoGlobal { get; set; }

        public GestorXML()
        {
            CategoriasRaiz = new ListaCategorias();
            CatalogoGlobal = new ArbolLibros();
        }

        public void CargarDatos(string rutaArchivo)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(rutaArchivo);

            ProcesarCategorias(doc);
            ProcesarLibros(doc);
        }

        private void ProcesarCategorias(XmlDocument doc)
        {
            // Extrae los elementos <categoria> basándose en la estructura de entrada[span_3](start_span)[span_3](end_span)
            XmlNodeList nodosCategoria = doc.SelectNodes("/config/listaCategorias/categoria");
            if (nodosCategoria == null) return;

            foreach (XmlNode nodo in nodosCategoria)
            {
                string nombre = nodo.InnerText.Trim();
                // El atributo padre es opcional, ya que la categoría principal no posee padre[span_4](start_span)[span_4](end_span)
                string padre = nodo.Attributes["padre"]?.Value;

                if (string.IsNullOrEmpty(padre))
                {
                    CategoriasRaiz.InsertarOrdenado(nombre);
                }
                else
                {
                    // Buscar la categoría padre usando un método de búsqueda en profundidad
                    NodoCategoria nodoPadre = BuscarCategoriaRecursivo(CategoriasRaiz.Cabeza, padre);
                    if (nodoPadre != null)
                    {
                        nodoPadre.Subcategorias.InsertarOrdenado(nombre);
                    }
                }
            }
        }

        private void ProcesarLibros(XmlDocument doc)
        {
            // Extrae los elementos <libro>[span_5](start_span)[span_5](end_span)
            XmlNodeList nodosLibro = doc.SelectNodes("/config/listaLibros/libro");
            if (nodosLibro == null) return;

            foreach (XmlNode nodo in nodosLibro)
            {
                int isbn = int.Parse(nodo["ISBN"].InnerText);
                string titulo = nodo["titulo"].InnerText;
                string autor = nodo["autor"].InnerText;
                string categoria = nodo["categoria"].InnerText;

                // 1. Insertar en el árbol global para cumplir con la búsqueda por ISBN[span_6](start_span)[span_6](end_span)
                CatalogoGlobal.Insertar(isbn, titulo, autor, categoria);

                // 2. Insertar en el árbol específico de su categoría para la visualización gráfica[span_7](start_span)[span_7](end_span)
                NodoCategoria nodoCat = BuscarCategoriaRecursivo(CategoriasRaiz.Cabeza, categoria);
                if (nodoCat != null)
                {
                    nodoCat.Libros.Insertar(isbn, titulo, autor, categoria);
                }
            }
        }

        private NodoCategoria BuscarCategoriaRecursivo(NodoCategoria actual, string nombreBuscado)
        {
            while (actual != null)
            {
                if (actual.Nombre == nombreBuscado) return actual;
               
                // Buscar en las subcategorías
                NodoCategoria sub = BuscarCategoriaRecursivo(actual.Subcategorias.Cabeza, nombreBuscado);
                if (sub != null) return sub;

                actual = actual.Siguiente;
            }
            return null;
        }
    }
}
