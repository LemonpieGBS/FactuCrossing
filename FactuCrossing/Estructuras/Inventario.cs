using System.ComponentModel.DataAnnotations;

namespace FactuCrossing.Estructuras
{
    // La clase donde está toda la información de los productos
    public class Producto
    {
        // Propiedad de ID, la llave primaria, siempre bueno tener una
        [Key]
        public int Id { get; init; }

        // Nombre del producto
        public string Nombre { get; private set; }

        // Descripción del producto
        public string Descripcion { get; private set; }

        // Precio del producto
        public decimal Precio { get; private set; }

        // Cantidad de producto en stock
        public int CantidadEnStock { get; set; }

        // Proveedor del producto
        public string Proveedor { get; private set; }

        // Fecha de ingreso del producto
        public DateTime FechaIngreso { get; private set; }

        // Atributo de Descontinuado
        public bool Descontinuado { get; private set; }

        // Constructor principal y único (por ahora)
        public Producto(int _id, string _nombre, string _descripcion,
                        decimal _precio, int _stock, string _proveedor, DateTime _fechaIngreso)
        {
            // Validaciones de los argumentos
            if (_id < 0)
                throw new ArgumentException("ID no puede ser negativo.", nameof(_id));
            if (string.IsNullOrEmpty(_nombre))
                throw new ArgumentException("Nombre no puede ser nulo o vacío.", nameof(_nombre));
            if (_precio < 0)
                throw new ArgumentException("Precio no puede ser negativo.", nameof(_precio));
            if (_stock < 0)
                throw new ArgumentException("Cantidad en stock no puede ser negativa.", nameof(_stock));
            if (string.IsNullOrEmpty(_proveedor))
                throw new ArgumentException("Proveedor no puede ser nulo o vacío.", nameof(_proveedor));
            if (_fechaIngreso > DateTime.Now)
                throw new ArgumentException("Fecha de ingreso no puede ser en el futuro.", nameof(_fechaIngreso));

            // Inicializamos las propiedades con los valores validados
            Id = _id;
            Nombre = _nombre;
            Descripcion = _descripcion;
            Precio = _precio;
            CantidadEnStock = _stock;
            Proveedor = _proveedor;
            FechaIngreso = _fechaIngreso;
            Descontinuado = false;
        }

        // Función para descontinuar un producto
        public void MarcarDescontinuado(bool t)
        {
            Descontinuado = t;
        }
    }

    /// <summary>
    /// Clase que representa una venta
    /// </summary>
    public class Venta
    {
        /// <summary>
        /// Constructor para la clase Venta
        /// </summary>
        /// <param name="mensaje">Mensaje de la venta</param>
        /// <param name="cantidadDinero">Cantidad de dinero que se vendió</param>
        /// <param name="fechaVenta">Fecha en que se efectuó la venta</param>
        public Venta(int idFacturador, string mensaje, double cantidadDinero, DateTime fechaVenta)
        {
            Mensaje = mensaje ?? throw new ArgumentNullException(nameof(mensaje));
            CantidadDinero = cantidadDinero >= 0 ? cantidadDinero : throw new ArgumentOutOfRangeException(nameof(cantidadDinero));
            FechaVenta = fechaVenta;
        }

        // Propiedades de la clase Venta
        public string Mensaje { get; set; }
        public double CantidadDinero { get; set; }
        public DateTime FechaVenta { get; set; }

        /// <summary>
        /// Método para obtener una representación en cadena de la venta
        /// </summary>
        /// <returns>Cadena que representa la venta</returns>
        public override string ToString()
        {
            return $"Venta: {Mensaje}, Cantidad: {CantidadDinero:C}, Fecha: {FechaVenta:yyyy-MM-dd}";
        }
    }
}
