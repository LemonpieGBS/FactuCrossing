using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactuCrossing.Estructuras
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Clase base para los descuentos
    /// </summary>
    public class Descuento
    {
        public Descuento(int id, string nombre, double porcentaje, DateTime fechaInicio, DateTime fechaFin, int productoAplicable)
        {
            Id = id;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            FechaInicio = fechaInicio.Date;
            FechaFin = fechaFin.Date;
            ProductoAplicable = productoAplicable;
            Porcentaje = porcentaje;
        }

        // Propiedades de la clase Descuento
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Porcentaje { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int ProductoAplicable { get; set; } // -1 significa aplicable a todos los productos


        /// <summary>
        /// Método para verificar si el descuento está vigente
        /// </summary>
        /// <returns>True si el descuento está vigente, False en caso contrario</returns>
        public bool EstaVigente()
        {
            DateTime ahora = DateTime.Now;
            return ahora.Date >= FechaInicio && ahora.Date <= FechaFin;
        }
    }
}
