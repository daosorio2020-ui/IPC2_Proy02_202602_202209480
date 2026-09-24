public class ArbolLibros
{
    public NodoLibro Raiz;

    public ArbolLibros()
    {
        Raiz = null;
    }

    public void Insertar(int isbn, string titulo, string autor, string categoria)
    {
        Raiz = InsertarRecursivo(Raiz, isbn, titulo, autor, categoria);
    }

    private NodoLibro InsertarRecursivo(NodoLibro nodo, int isbn, string titulo, string autor, string cat)
    {
        if (nodo == null)
            return new NodoLibro(isbn, titulo, autor, cat);

        if (isbn < nodo.ISBN)
            nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, isbn, titulo, autor, cat);
        else if (isbn > nodo.ISBN)
            nodo.Derecho = InsertarRecursivo(nodo.Derecho, isbn, titulo, autor, cat);

        return nodo; // Si es igual, no se inserta (ISBN único)
    }

    public NodoLibro Buscar(int isbn)
    {
        return BuscarRecursivo(Raiz, isbn);
    }

    private NodoLibro BuscarRecursivo(NodoLibro nodo, int isbn)
    {
        if (nodo == null || nodo.ISBN == isbn)
            return nodo;

        if (isbn < nodo.ISBN)
            return BuscarRecursivo(nodo.Izquierdo, isbn);
       
        return BuscarRecursivo(nodo.Derecho, isbn);
    }
}