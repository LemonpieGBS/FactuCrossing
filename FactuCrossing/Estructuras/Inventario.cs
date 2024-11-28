using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactuCrossing.Estructuras
{
    // La clase donde está toda la información de los productos
    public class Producto
    {
        // Propiedad de ID, la llave primaria, siempre bueno tener una
        [Key]
        public int Id { get; set; }

        // Nombre del producto
        public string Nombre { get; set; }

        // Descripción del producto
        public string Descripcion { get; set; }

        // Precio del producto
        public decimal Precio { get; set; }

        // Cantidad de producto en stock
        public int CantidadEnStock { get; set; }

        // Proveedor del producto
        public string Proveedor { get; set; }

        // Fecha de ingreso del producto
        public DateTime FechaIngreso { get; set; }

        // Constructor principal y único (por ahora)
        public Producto(int _id, string _nombre, string _descripcion,
                        decimal _precio, int _stock, string _proveedor, DateTime _fechaIngreso)
        {
            // Validaciones de los argumentos
            if (_id < 0)
                throw new ArgumentException("ID no puede ser negativo.", nameof(_id));
            if (string.IsNullOrEmpty(_nombre))
                throw new ArgumentException("Nombre no puede ser nulo o vacío.", nameof(_nombre));
            if (string.IsNullOrEmpty(_descripcion))
                throw new ArgumentException("Descripción no puede ser nula o vacía.", nameof(_descripcion));
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
        }
    }

    public class Descuento
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
