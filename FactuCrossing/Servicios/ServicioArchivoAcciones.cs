using FactuCrossing.Estructuras;

namespace FactuCrossing.Servicios
{
    /// <summary>
    /// Clase responsable de todos los métodos de la clase <seealso cref="Accion"/> 
    /// para manejo de archivos
    /// </summary>
    public class ServicioArchivoAcciones
    {
        /// <summary>
        /// Sello para comprobar si un archivo es perteneciente a uno de acciones
        /// </summary>
        private const string _sello = "FACTUCROSSING - ARCHIVO DE ACCIONES";

        /// <summary>
        /// Enumerador responsable de identificar los atributos de la clase <seealso cref="Accion"/> 
        /// a la hora de guardar y cargar
        /// </summary>
        private enum IDAtributos
        {
            ATRIBUTOFIN,
            CUENTAID,
            MENSAJE,
            TIEMPODEACCION
        }

        /// <summary> 
        /// Función para guardar una lista de acciones (<paramref name="acciones"/>) en un archivo (<paramref name="rutaArchivo"/>) 
        /// <para>
        /// Esta función usa <seealso cref="BinaryWriter"/> para escribir una lista de acciones (clase <seealso cref="Accion"/>) a un archivo binario<br/>
        /// Se guarda un sello (<seealso cref="_sello"/>) para a la hora de cargar un archivo comprobar que es un archivo perteneciente a esta función<br/><br/>
        /// Para cargar las acciones de un archivo, usa <seealso cref="LeerAcciones(string)"/>
        /// </para>
        /// </summary>
        /// <param name="acciones">Lista de acciones a guardar</param>
        /// <param name="rutaArchivo">Ruta en donde guardar las acciones como archivo binario</param>
        public void EscribirAcciones(List<Accion> acciones, string rutaArchivo)
        {
            // Loggeamos que la función fue llamada
            Program.Log($"FUNCION GUARDAR ACCIONES LLAMADA CON RUTA: {rutaArchivo}");
            // Un try por si algo sale mal
            try
            {
                // Creamos el filestream para el archivo
                using FileStream fStream = new(rutaArchivo, FileMode.Create);
                // Creamos el escritor binario
                using BinaryWriter bWriter = new(fStream);
                // Usamos el escritor binario para 'sellar' el archivo
                bWriter.Write(_sello);
                // Loggeamos la cantidad de acciones que se guardarán
                Program.Log($"Acciones Totales: {acciones.Count}");
                // Usamos el escritor binario para guardar la cantidad de acciones
                // Esto será útil para cargarlas después
                bWriter.Write(acciones.Count);
                // Iteramos por todas las acciones para guardar cada una
                for (int i = 0; i < acciones.Count; i++)
                {
                    // Loggeamos el progreso del programa
                    Program.Log($"------------- Escribiendo acción #{i + 1} / {acciones.Count} -------------");
                    // Escribimos la acción
                    EscribirAccion(acciones[i], bWriter);
                }
            }
            // Un catch para avisar del error al usuario
            catch (Exception ex)
            {
                // Mostramos un mensajito para el usuario
                MessageBox.Show($"Error guardando el archivo: {ex}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary> 
        /// Función privada para guardar una sola acción (<paramref name="accion"/>) dado un escritor binario (<paramref name="bWriter"/>) 
        /// <para>
        /// Usada en la función publica <seealso cref="EscribirAcciones(List{Accion}, string)"/><br/>
        /// Escribe una acción (clase <seealso cref="Accion"/>) a un escritor binario
        /// </para>
        /// </summary>
        /// <param name="accion">Acción a guardar</param>
        /// <param name="bWriter">Escritor binario</param>
        private void EscribirAccion(Accion accion, BinaryWriter bWriter)
        {
            // Escribir el atributo CUENTAID
            bWriter.Write((Int32)IDAtributos.CUENTAID);
            bWriter.Write((Int32)accion.IdDeCuenta);
            Program.Log($"-- Atributo Escrito [CUENTAID]: {accion.IdDeCuenta}");

            // Escribir el atributo MENSAJE
            bWriter.Write((Int32)IDAtributos.MENSAJE);
            bWriter.Write(accion.Mensaje);
            Program.Log($"-- Atributo Escrito [MENSAJE]: {accion.Mensaje}");

            // Escribir el atributo TIEMPO DE ACCION
            bWriter.Write((Int32)IDAtributos.TIEMPODEACCION);
            bWriter.Write(accion.TiempoDeAccion.ToBinary());
            Program.Log($"-- Atributo Escrito [TIEMPODEACCION]: {accion.TiempoDeAccion}");

            // Escribimos el atributo FIN para marcar el fin de la escritura
            bWriter.Write((Int32)IDAtributos.ATRIBUTOFIN);
            Program.Log($"-- Atributo Escrito [ATRIBUTOFIN]");
        }

        /// <summary> 
        /// Función para cargar una lista de acciones en un archivo (<paramref name="rutaArchivo"/>) 
        /// <para>
        /// Esta función usa <seealso cref="BinaryReader"/> para cargar una lista de acciones (clase <seealso cref="Accion"/>) de un archivo binario<br/>
        /// Se carga un sello (<seealso cref="_sello"/>) para comprobar que es un archivo perteneciente a esta función<br/><br/>
        /// Para guardar las acciones a un archivo, usa <seealso cref="EscribirAcciones(List{Accion}, string)"/>
        /// </para>
        /// </summary>
        /// <param name="rutaArchivo">Ruta en donde se encuentra el archivo binario para cargar las acciones</param>
        /// <returns>Lista de acciones cargadas</returns>
        public List<Accion> LeerAcciones(string rutaArchivo)
        {
            // Loggeamos que la función fue llamada
            Program.Log($"FUNCION CARGARACCIONES LLAMADA CON RUTA: {rutaArchivo}");
            // Creamos la lista de retorno
            List<Accion> listaRetorno = new();
            // Un try por si algo sale mal
            try
            {
                // Creamos el filestream para el archivo
                using FileStream fStream = new(rutaArchivo, FileMode.Open, FileAccess.Read);
                // Creamos el lector binario
                using BinaryReader bReader = new(fStream);
                // Si el sello no está presente tiramos una excepción (esto lo atrapa el catch de todas maneras)
                if (bReader.ReadString() != _sello)
                    throw new FileFormatException("El archivo cargado no pertenece a un archivo de acciones FactuCrossing");
                // Leemos la cantidad de acciones a cargar
                int cantidadAcciones = bReader.ReadInt32();
                // Loggeamos la cantidad de acciones
                Program.Log($"- Cantidad de Acciones a leer: {cantidadAcciones}");
                // Loggeamos los bytes en el archivo
                Program.Log($"- Bytes en el archivo: {fStream.Length}");
                // Iteramos por la cantidad de acciones que vamos a cargar
                for (int i = 0; i < cantidadAcciones; i++)
                {
                    // Loggeamos nuestro progreso
                    Program.Log($"------------- Leyendo acción #{i + 1} / {cantidadAcciones} -------------");
                    // Leemos la acción y la agregamos a nuestra lista de retorno
                    listaRetorno.Add(LeerAccion(bReader));
                    // Loggeamos el éxito
                    Program.Log($"-- Acción creada y añadida con éxito --");
                }
            }
            // Un catch para avisar del error al usuario
            catch (Exception ex)
            {
                // Mostramos el mensaje al usuario para que sepa que salió mal
                MessageBox.Show($"Error cargando el archivo: {ex}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Retornamos las acciones que logramos cargar
            return listaRetorno;
        }

        /// <summary> 
        /// Función privada para cargar una sola acción dada un lector binario (<paramref name="bReader"/>) 
        /// <para>
        /// Usada en la función publica <seealso cref="LeerAcciones(string)"/><br/>
        /// Lee una acción (clase <seealso cref="Accion"/>) dado un lector binario
        /// </para>
        /// </summary>
        /// <param name="bReader">Lector binario</param>
        /// <returns>Acción cargada del archivo</returns>
        private Accion LeerAccion(BinaryReader bReader)
        {
            // Declaramos la variable para almacenar el atributo leído
            IDAtributos atributoLeido;
            // Inicializamos las variables para almacenar los valores de los atributos de la acción
            int? cuentaIdLeido = null;
            string? mensajeLeido = null;
            DateTime? tiempoDeAccionLeido = null;
            // Iniciamos un bucle para leer los atributos de la acción hasta encontrar el atributo de fin
            do
            {
                // Leemos el siguiente atributo del archivo
                Int32 atributoBuffer = bReader.ReadInt32();
                // Verificamos si el valor leído corresponde a un atributo válido
                if (Enum.IsDefined(typeof(IDAtributos), atributoBuffer))
                    atributoLeido = (IDAtributos)atributoBuffer;
                else
                    throw new ArgumentOutOfRangeException($"No se reconoce el buffer {atributoBuffer} como un atributo válido");
                // Usamos un switch para manejar cada tipo de atributo
                switch (atributoLeido)
                {
                    // Caso para el atributo de fin, no hacemos nada y salimos del bucle
                    case IDAtributos.ATRIBUTOFIN: { break; }
                    // Caso para el atributo CUENTAID
                    case IDAtributos.CUENTAID:
                        {
                            // Leemos el valor del ID de la cuenta y lo almacenamos
                            Int32 buffer = bReader.ReadInt32();
                            Program.Log($"-- cuentaId leido: {buffer}");
                            cuentaIdLeido = buffer;
                            break;
                        }
                    // Caso para el atributo MENSAJE
                    case IDAtributos.MENSAJE:
                        {
                            // Leemos el valor del mensaje y lo almacenamos
                            string buffer = bReader.ReadString();
                            Program.Log($"-- Mensaje leido: {buffer}");
                            mensajeLeido = buffer;
                            break;
                        }
                    // Caso para el atributo TIEMPO DE ACCION
                    case IDAtributos.TIEMPODEACCION:
                        {
                            // Leemos el valor del tiempo de acción y lo almacenamos
                            DateTime buffer = DateTime.FromBinary(bReader.ReadInt64());
                            Program.Log($"-- Tiempo de Acción leido: {buffer}");
                            tiempoDeAccionLeido = buffer;
                            break;
                        }
                }
                // Continuamos el bucle hasta encontrar el atributo de fin
            } while (atributoLeido != IDAtributos.ATRIBUTOFIN);

            // Creamos nuestra variable para almacenar la acción
            Accion nuevaAccion;
            // Creamos la nueva acción con los datos
            nuevaAccion = new(
                // Si el cuentaIdLeido es nulo, tirar una excepción ya que es un parametro obligatorio
                idDeCuenta: cuentaIdLeido ?? throw new ArgumentNullException("[requerido] El parametro cuentaId no se encontró"),
                // Si el mensaje es nulo, tirar una excepción ya que es un parametro obligatorio
                mensaje: mensajeLeido ?? throw new ArgumentNullException("[requerido] El parametro mensaje no se encontró"),
                // Si el tiempo de acción es nulo, tirar una excepción ya que es un parametro obligatorio
                tiempoDeAccion: tiempoDeAccionLeido ?? throw new ArgumentNullException("[requerido] El parametro tiempoDeAccion no se encontró")
                );
            // Retornamos la acción creada
            return nuevaAccion;
        }
    }
}