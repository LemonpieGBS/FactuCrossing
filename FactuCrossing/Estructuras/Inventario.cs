using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactuCrossing.Estructuras
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int CantidadEnStock { get; set; }
        public string Proveedor { get; set; }
        public DateTime FechaIngreso { get; set; }


        public Producto(int _id, string _nombre, string _descripcion,
        decimal _precio, int _stock, string _proveedor, DateTime _fechaIngreso)
        {
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
