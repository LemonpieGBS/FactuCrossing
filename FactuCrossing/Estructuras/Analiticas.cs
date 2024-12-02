using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactuCrossing.Estructuras
{
    // Enumerador que define los tipos de acceso posibles [DEPRECADO]
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

        // Propiedad que almacena el tipo de acceso (entrada o salida) [DEPRECADO]
        public TipoDeAcceso Tipo { get; set; }

        /// <summary>
        /// Constructor principal que inicializa las propiedades y el tipo de acceso [DEPRECADO]
        /// </summary>
        /// <param name="idDeCuenta">Id de la Cuenta que hizo el acceso</param>
        /// <param name="tiempoDeAcceso">Tiempo donde se dio el acceso</param>
        /// <param name="tipo">Tipo de Acceso [DEPRECADO]</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Acceso(int idDeCuenta, DateTime tiempoDeAcceso, TipoDeAcceso tipo)
        {
            IdDeCuenta = (idDeCuenta >= 0) ? idDeCuenta : throw new ArgumentOutOfRangeException(nameof(idDeCuenta));
            TiempoDeAcceso = tiempoDeAcceso;
            Tipo = tipo;
        }

        /// <summary>
        /// Constructor principal que inicializa las propiedades
        /// </summary>
        /// <param name="idDeCuenta">Id de la Cuenta que hizo el acceso</param>
        /// <param name="tiempoDeAcceso">Tiempo donde se dio el acceso</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Acceso(int idDeCuenta, DateTime tiempoDeAcceso)
        {
            IdDeCuenta = (idDeCuenta >= 0) ? idDeCuenta : throw new ArgumentOutOfRangeException(nameof(idDeCuenta));
            TiempoDeAcceso = tiempoDeAcceso;
            Tipo = TipoDeAcceso.ENTRADA;
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
        /// Constructor vacío de la clase
        /// </summary>
        public TiempoSesion()
        {
            tiempoPorDia = new Dictionary<DateTime, double>();
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
            if (tiempoPorDia.ContainsKey(fecha0))
            {
                // Añadimos los segundos
                tiempoPorDia[fecha0] += segundos;
                // Retornamos
                return;
            }
            else
            {
                // Si llegamos aca es porque no se encontró una fecha
                KeyValuePair<DateTime, double> nuevoValor = new KeyValuePair<DateTime, double>
                    (sesionIniciada.Date, segundos);
                // Agregamos a la lista
                tiempoPorDia.Add(nuevoValor.Key, nuevoValor.Value);
            }
        }

        /// <summary>
        /// Función para obtener el tiempo total de acceso
        /// </summary>
        /// <returns>La cantidad de tiempo en segundos (double)</returns>
        public double ObtenerTiempo()
        {
            double segundos = 0;
            foreach (KeyValuePair<DateTime, double> keyValuePair in tiempoPorDia)
            { segundos += keyValuePair.Value; }

            return segundos;
        }

        /// <summary>
        /// Función para obtener el tiempo de acceso en una fecha específica
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns>La cantidad de tiempo en segundos (double)</returns>
        public double ObtenerTiempo(DateTime fecha)
        {
            // El diccionario solo funciona con fechas en 00:00
            DateTime fecha0 = fecha.Date;

            // Valor
            double value = 0;

            // Buscamos si ya existe el tuple con la fecha
            if (tiempoPorDia.TryGetValue(fecha0, out value)) return value;
            else return value;
        }

        /// <summary>
        /// Función para obtener el tiempo de acceso entre dos fechas
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="final"></param>
        /// <returns></returns>
        public double ObtenerTiempo(DateTime inicio, DateTime final)
        {
            // Segundos
            double segundos = 0;

            // La función traduce automáticamente las fechas a 00:00
            foreach (DateTime fecha
                in DateHelper.GetDates(inicio, final))
            {
                // Si se encuentra
                if (tiempoPorDia.TryGetValue(fecha, out double value))
                {
                    // Sumamos el valor encontrado a los segundos totales
                    segundos += value;
                }
            }

            // Retornamos los segundos
            return segundos;
        }

        /// <summary>
        /// Overload para obtener el tiempo de acceso en un mes específico
        /// </summary>
        /// <param name="año"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        public double ObtenerTiempo(int año, int mes)
        {
            // Segundos
            double segundos = 0;

            // La función traduce automáticamente las fechas a 00:00
            foreach (DateTime fecha
                in DateHelper.GetDates(año, mes))
            {
                // Si se encuentra
                if (tiempoPorDia.TryGetValue(fecha, out double value))
                {
                    // Sumamos el valor encontrado a los segundos totales
                    segundos += value;
                }
            }

            // Retornamos los segundos
            return segundos;
        }
    }

    /// <summary>
    /// Clae evento genérico para unificar a la hora de ordenarlas cronológicamente
    /// </summary>
    public class EventoGenerico
    {
        public EventoGenerico(string accionario, DateTime tiempo, string tipo, string mensaje, string tiemposesion)
        {
            Tiempo = tiempo;
            Tipo = tipo;
            Mensaje = mensaje;
            Accionario = accionario;
            TiempoSesion = tiemposesion;
        }

        public string TiempoSesion { get; set; }
        public DateTime Tiempo { get; set; }
        public string Tipo { get; set; }
        public string Mensaje { get; set; }
        public string Accionario { get; set; }
    }
}
