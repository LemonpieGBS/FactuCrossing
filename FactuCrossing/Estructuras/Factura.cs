using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactuCrossing.Estructuras
{
    internal class ProductoFacturado
    {
        string Nombre { get; set; }
        double Precio { get; set; }
        int Cantidad { get; set; }
    }

    internal class Factura
    {
        int NumFactura { get; set; }
        string Facturista { get; set; }
        string Sucursal { get; set; }
        Tuple<string, string, double, int> ProductosFacturados { get; set; }
    }
}
