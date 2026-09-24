public class NodoCategoria
{
    public string Nombre { get; set; }
   
    // TDA interno para las subcategorías (Lista enlazada)
    public ListaCategorias Subcategorias { get; set; }
   
    // TDA interno para los libros de ESTA categoría (Árbol BST)
    public ArbolLibros Libros { get; set; }
   
    // Puntero al siguiente hermano en el mismo nivel
    public NodoCategoria Siguiente { get; set; }

    public NodoCategoria(string nombre)
    {
        Nombre = nombre;
        Subcategorias = new ListaCategorias();
        Libros = new ArbolLibros();
        Siguiente = null;
    }
}
