using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactuCrossing.Estructuras
{
    // Enumerador que define los tipos de acceso posibles
    public enum TipoDeAcceso
    {
        ENTRADA, // Representa un acceso de entrada
        //SALIDA   // Representa un acceso de salida [DEPRECADO]
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
            IdDeCuenta = (idDeCuenta >= 0) ? idDeCuenta : throw new ArgumentOutOfRangeException(nameof(idDeCuenta));
            TiempoDeAcceso = tiempoDeAcceso;
            Tipo = tipo;
        }
    }

    /// <summary>
    /// Clase de acciones de los usuarios
    /// </summary>
    public class Accion
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="idDeCuenta">Id de la cuenta que hizo la acción</param>
        /// <param name="mensaje">Mensaje de la acción</param>
        /// <param name="tiempoDeAccion">Tiempo en el que sucedió</param>
        /// <exception cref="ArgumentNullException">(Si mensaje es nulo o si Id es negativo)</exception>
        public Accion(int idDeCuenta, string mensaje, DateTime tiempoDeAccion)
        {
            IdDeCuenta = (idDeCuenta >= 0) ? idDeCuenta : throw new ArgumentOutOfRangeException(nameof(idDeCuenta));
            Mensaje = mensaje ?? throw new ArgumentNullException(nameof(mensaje));
            TiempoDeAccion = tiempoDeAccion;
        }

        public int IdDeCuenta { get; set; }
        public string Mensaje { get; set; }
        public DateTime TiempoDeAccion { get; set; }


    }

    /// <summary>
    /// Clase del tiempo de sesion para una cuenta
    /// </summary>
    public class TiempoSesion
    {
        /// <summary>
        /// Tiempo de la Cuenta usada por mes
        /// </summary>
        public Dictionary<DateTime, double> tiempoPorDia { get; init; }

        /// <summary>
        /// Constructor principal de la clase
        /// </summary>
        /// <param name="tiempoPorMes">Tiempo por mes</param>
        /// <param name="accesosDelUsuario">Accesos del Usuario</param>
        /// <param name="acciones">Acciones</param>
        /// <exception cref="ArgumentNullException"></exception>
        public TiempoSesion(Dictionary<DateTime, double> tiempoPorDia)
        {
            this.tiempoPorDia = tiempoPorDia ?? throw new ArgumentNullException(nameof(tiempoPorDia));
        }

        /// <summary>
        /// Función para agregar tiempo al usuario
        /// </summary>
        /// <param name="sesionIniciada"></param>
        public void AgregarTiempo(DateTime sesionIniciada, double segundos)
        {
            // El diccionario solo funciona con fechas en 00:00
            DateTime fecha0 = sesionIniciada.Date;

            // Buscamos si ya existe el tuple con la fecha
            if(tiempoPorDia.ContainsKey(fecha0))
            {
                // Añadimos los segundos
                tiempoPorDia[fecha0] += segundos;
                // Retornamos
                return;
            } else
            {
                // Si llegamos aca es porque no se encontró una fecha
                KeyValuePair<DateTime, double> nuevoValor = new KeyValuePair<DateTime, double>
                    (sesionIniciada.Date, segundos);
                // Agregamos a la lista
                tiempoPorDia.Add(nuevoValor.Key, nuevoValor.Value);
            }
        }
    }
}
