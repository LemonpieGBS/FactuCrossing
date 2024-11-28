using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactuCrossing.Estructuras
{
    // Enumerador que define los tipos de acceso posibles
    public enum TipoDeAcceso
    {
        ENTRADA, // Representa un acceso de entrada
        SALIDA   // Representa un acceso de salida
    }

    // Clase que representa un acceso de una cuenta
    public class Acceso
    {
        // Propiedad que almacena la cuenta asociada al acceso
        public int IdDeCuenta { get; set; }

        // Propiedad que almacena el tiempo del acceso
        public DateTime TiempoDeAcceso { get; set; }

        // Propiedad que almacena el tipo de acceso (entrada o salida)
        public TipoDeAcceso Tipo { get; set; }

        // Constructor principal que inicializa las propiedades
        public Acceso(int idDeCuenta, DateTime tiempoDeAcceso, TipoDeAcceso tipo)
        {
            if(idDeCuenta < 0)
                throw new ArgumentException("El ID de la cuenta no puede ser negativo.", nameof(idDeCuenta));
            IdDeCuenta = idDeCuenta;
            TiempoDeAcceso = tiempoDeAcceso;
            Tipo = tipo;
        }
    }
}
