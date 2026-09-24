public class NodoLibro
{
    public int ISBN { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Categoria { get; set; }
   
    // Punteros para el Árbol Binario
    public NodoLibro Izquierdo { get; set; }
    public NodoLibro Derecho { get; set; }

    public NodoLibro(int isbn, string titulo, string autor, string categoria)
    {
        ISBN = isbn;
        Titulo = titulo;
        Autor = autor;
        Categoria = categoria;
        Izquierdo = null;
        Derecho = null;
    }
}