using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Proyecto2_IPC2.Models;

namespace Proyecto2_IPC2.Controllers
{
    public class HomeController : Controller
    {
        // Instancia estática para mantener los árboles vivos en memoria
        public static GestorXML Gestor = new GestorXML();

        public IActionResult Index()
        {
            // Enviamos el gestor a la vista para poder leer si hay datos cargados
            return View(Gestor);
        }

        // Requisito a. Inicialización - Para que el sistema pueda inicializarse sin ninguna información previa
        [HttpPost]
        public IActionResult InicializarSistema()
        {
            Gestor = new GestorXML(); // Sobreescribe la instancia con árboles limpios
            TempData["Mensaje"] = "Sistema inicializado. El catálogo está vacío.";
            return RedirectToAction("Index");
        }

        // Requisito b. Cargar un archivo XML de entrada
        [HttpPost]
        public IActionResult CargarXML(IFormFile archivoXML)
        {
            if (archivoXML != null && archivoXML.Length > 0)
            {
                // Guardamos el archivo temporalmente en el servidor para que el XmlDocument lo lea
                string rutaTemp = Path.GetTempFileName();
                using (var stream = new FileStream(rutaTemp, FileMode.Create))
                {
                    archivoXML.CopyTo(stream);
                }

                // Usamos nuestro GestorXML para poblar los TDAs
                Gestor.CargarDatos(rutaTemp);

                // Limpiamos el archivo temporal
                System.IO.File.Delete(rutaTemp);
               
                TempData["Mensaje"] = "Archivo XML cargado e integrado al catálogo exitosamente.";
            }
            else
            {
                TempData["Error"] = "Por favor seleccione un archivo XML válido.";
            }

            return RedirectToAction("Index");
        }

        // Requisito e. Ayuda - Mostrar la información del estudiante y link a documentación
        public IActionResult Ayuda()
        {
            return View();
        }

[HttpPost]
public IActionResult BuscarLibro(int isbn)
{
    NodoLibro encontrado = Gestor.CatalogoGlobal.Buscar(isbn);
    if (encontrado != null)
    {
        TempData["Mensaje"] = $"Libro encontrado: {encontrado.Titulo} por {encontrado.Autor} (Categoría: {encontrado.Categoria})";
    }
    else
    {
        TempData["Error"] = $"No se encontró ningún libro con el ISBN {isbn}.";
    }
    return RedirectToAction("Index");
}

// Requisito d. Mostrar libro con el menor y mayor ISBN
public IActionResult ExtremosISBN()
{
    NodoLibro min = Gestor.CatalogoGlobal.ObtenerMinimo();
    NodoLibro max = Gestor.CatalogoGlobal.ObtenerMaximo();

    if (min != null && max != null)
    {
        TempData["Mensaje"] = $"Menor ISBN: {min.ISBN} ({min.Titulo}) | Mayor ISBN: {max.ISBN} ({max.Titulo})";
    }
    else
    {
        TempData["Error"] = "El catálogo está vacío.";
    }
    return RedirectToAction("Index");
}

// Requisito c. Mostrar gráficamente los libros de una categoría utilizando Graphviz
[HttpPost]
public IActionResult GenerarGrafoCategoria(string nombreCategoria)
{
    // Usaremos un método de búsqueda en tu lista enlazada (debes implementarlo o usar uno global si lo prefieres)
    // Por simplicidad en este paso, si pasas "Global", grafica todo el árbol.
    string dotCode = Gestor.CatalogoGlobal.GenerarGraphvizAscendente();
    ViewBag.DotCode = dotCode;
    return View("Grafo"); // Crearemos esta vista en el siguiente paso
}

    }



    
}

