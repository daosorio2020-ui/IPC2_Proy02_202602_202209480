public class ListaCategorias
{
    public NodoCategoria Cabeza;

    public ListaCategorias()
    {
        Cabeza = null;
    }

    public void InsertarOrdenado(string nombre)
    {
        NodoCategoria nuevo = new NodoCategoria(nombre);

        // Caso 1: Lista vacía o el nuevo va antes de la cabeza
        if (Cabeza == null || string.Compare(nombre, Cabeza.Nombre, System.StringComparison.OrdinalIgnoreCase) < 0)
        {
            nuevo.Siguiente = Cabeza;
            Cabeza = nuevo;
            return;
        }

        // Caso 2: Buscar la posición correcta
        NodoCategoria actual = Cabeza;
        while (actual.Siguiente != null && string.Compare(actual.Siguiente.Nombre, nombre, System.StringComparison.OrdinalIgnoreCase) < 0)
        {
            actual = actual.Siguiente;
        }

        // Evitar duplicados
        if (actual.Nombre == nombre || (actual.Siguiente != null && actual.Siguiente.Nombre == nombre))
            return;

        nuevo.Siguiente = actual.Siguiente;
        actual.Siguiente = nuevo;
    }

    public NodoCategoria Buscar(string nombre)
    {
        NodoCategoria actual = Cabeza;
        while (actual != null)
        {
            if (actual.Nombre == nombre) return actual;
            actual = actual.Siguiente;
        }
        return null;
    }
}