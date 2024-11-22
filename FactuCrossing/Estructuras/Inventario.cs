using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactuCrossing.Estructuras
{
    public class Producto (int _id, string _nombre, string _descripcion,
        decimal _precio, int _stock, string _proveedor, DateTime _fechaIngreso)
    {
        public int Id { get; set; } = _id;
        public string Nombre { get; set; } = _nombre;
        public string Descripcion { get; set; } = _descripcion;
        public decimal Precio { get; set; } = _precio;
        public int CantidadEnStock { get; set; } = _stock;
        public string Proveedor { get; set; } = _proveedor;
        public DateTime FechaIngreso { get; set; } = _fechaIngreso;
    }

    public class Descuento
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
