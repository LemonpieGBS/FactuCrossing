using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactuCrossing.Estructuras
{
    public class Producto
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public decimal Precio { get; set; }
        public int CantidadEnStock { get; set; }
    }

    public class Descuento
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
