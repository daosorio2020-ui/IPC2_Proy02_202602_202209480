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

    public NodoLibro ObtenerMinimo()
{
    if (Raiz == null) return null;
    NodoLibro actual = Raiz;
    while (actual.Izquierdo != null)
        actual = actual.Izquierdo;
    return actual;
}

public NodoLibro ObtenerMaximo()
{
    if (Raiz == null) return null;
    NodoLibro actual = Raiz;
    while (actual.Derecho != null)
        actual = actual.Derecho;
    return actual;
}

// Generar código Graphviz de los libros en orden ascendente (Recorrido In-Orden)
public string GenerarGraphvizAscendente()
{
    if (Raiz == null) return "digraph G { nodo [label=\"Vacio\"]; }";

    string dot = "digraph G {\n rankdir=LR;\n node [shape=box, style=filled, color=lightblue];\n";
    string conexiones = "";
    NodoLibro anterior = null;

    // Función local para hacer el recorrido in-orden
    void RecorridoInOrden(NodoLibro nodo)
    {
        if (nodo == null) return;
       
        RecorridoInOrden(nodo.Izquierdo);
       
        // Declarar el nodo
        dot += $"n{nodo.ISBN} [label=\"ISBN: {nodo.ISBN}\\n{nodo.Titulo}\\n{nodo.Autor}\"];\n";
       
        // Conectar con el anterior para mostrar el orden ascendente
        if (anterior != null)
        {
            conexiones += $"n{anterior.ISBN} -> n{nodo.ISBN};\n";
        }
        anterior = nodo;
       
        RecorridoInOrden(nodo.Derecho);
    }

    RecorridoInOrden(Raiz);
    dot += conexiones + "}\n";
   
    return dot;
}

}