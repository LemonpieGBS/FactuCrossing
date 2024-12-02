using FactuCrossing.Estructuras;

namespace FactuCrossing.Servicios
{
    /// <summary>
    /// Clase responsable de todos los métodos de la clase <seealso cref="Acceso"/> 
    /// para manejo de archivos
    /// </summary>
    public class ServicioArchivoAccesos
    {
        /// <summary>
        /// Sello para comprobar si un archivo es perteneciente a uno de accesos
        /// </summary>
        private const string _sello = "FACTUCROSSING - ARCHIVO DE ACCESOS";

        /// <summary>
        /// Enumerador responsable de identificar los atributos de la clase <seealso cref="Acceso"/> 
        /// a la hora de guardar y cargar
        /// </summary>
        private enum IDAtributos
        {
            ATRIBUTOFIN,
            CUENTAID,
            TIEMPODEACCESO,
            TIPO
        }

        /// <summary> Función para guardar una lista de accesos (<paramref name="accesos"/>) en un archivo (<paramref name="rutaArchivo"/>) 
        /// <para>
        /// Esta función usa <seealso cref="BinaryWriter"/> para escribir una lista de accesos (clase <seealso cref="Acceso"/>) a un archivo binario<br/>
        /// Se guarda un sello (<seealso cref="_sello"/>) para a la hora de cargar un archivo comprobar que es un archivo perteneciente a esta función<br/><br/>
        /// Para cargar los accesos de un archivo, usa <seealso cref="LeerAccesos(string)"/>
        /// </para>
        /// </summary>
        /// <param name="accesos">Lista de accesos a guardar</param>
        /// <param name="rutaArchivo">Ruta en donde guardar los accesos como archivo binario</param>
        public void EscribirAccesos(List<Acceso> accesos, string rutaArchivo)
        {
            // Loggeamos que la función fue llamada
            Program.Log($"FUNCION GUARDAR ACCESOS LLAMADA CON RUTA: {rutaArchivo}");
            // Un try por si algo sale mal
            try
            {
                // Creamos el filestream para el archivo
                using FileStream fStream = new(rutaArchivo, FileMode.Create);
                // Creamos el escritor binario
                using BinaryWriter bWriter = new(fStream);
                // Usamos el escritor binario para 'sellar' el archivo
                bWriter.Write(_sello);
                // Loggeamos la cantidad de accesos que se guardarán
                Program.Log($"Accesos Totales: {accesos.Count}");
                // Usamos el escritor binario para guardar la cantidad de accesos
                // Esto será útil para cargarlos después
                bWriter.Write(accesos.Count);
                // Iteramos por todos los accesos para guardar cada uno
                for (int i = 0; i < accesos.Count; i++)
                {
                    // Loggeamos el progreso del programa
                    Program.Log($"------------- Escribiendo acceso #{i + 1} / {accesos.Count} -------------");
                    // Escribimos el acceso
                    EscribirAcceso(accesos[i], bWriter);
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

        /// <summary> Función privada para guardar un solo acceso (<paramref name="acceso"/>) dado un escritor binario (<paramref name="bWriter"/>) 
        /// <para>
        /// Usada en la función publica <seealso cref="EscribirAccesos(List{Acceso}, string)"/><br/>
        /// Escribe un acceso (clase <seealso cref="Acceso"/>) a un escritor binario
        /// </para>
        /// </summary>
        /// <param name="acceso">Acceso a guardar</param>
        /// <param name="bWriter">Escritor binario</param>
        private void EscribirAcceso(Acceso acceso, BinaryWriter bWriter)
        {
            // Escribir el atributo CUENTAID
            bWriter.Write((Int32)IDAtributos.CUENTAID);
            bWriter.Write((Int32)acceso.IdDeCuenta);
            Program.Log($"-- Atributo Escrito [CUENTAID]: {acceso.IdDeCuenta}");

            // Escribir el atributo TIEMPO DE ACCESO
            bWriter.Write((Int32)IDAtributos.TIEMPODEACCESO);
            bWriter.Write(acceso.TiempoDeAcceso.ToBinary());
            Program.Log($"-- Atributo Escrito [TIEMPODEACCESO]: {acceso.TiempoDeAcceso}");

            // Escribir el atributo TIPO
            bWriter.Write((Int32)IDAtributos.TIPO);
            bWriter.Write((Int32)acceso.Tipo);
            Program.Log($"-- Atributo Escrito [TIPO]: {acceso.Tipo}");

            // Escribimos el atributo FIN para marcar el fin de la escritura
            bWriter.Write((Int32)IDAtributos.ATRIBUTOFIN);
            Program.Log($"-- Atributo Escrito [ATRIBUTOFIN]");
        }

        /// <summary> Función para cargar una lista de accesos en un archivo (<paramref name="rutaArchivo"/>) 
        /// <para>
        /// Esta función usa <seealso cref="BinaryReader"/> para cargar una lista de accesos (clase <seealso cref="Acceso"/>) de un archivo binario<br/>
        /// Se carga un sello (<seealso cref="_sello"/>) para comprobar que es un archivo perteneciente a esta función<br/><br/>
        /// Para guardar los accesos a un archivo, usa <seealso cref="EscribirAccesos(List{Acceso}, string)"/>
        /// </para>
        /// </summary>
        /// <param name="rutaArchivo">Ruta en donde se encuentra el archivo binario para cargar los accesos</param>
        /// <returns>Lista de accesos cargados</returns>
        public List<Acceso> LeerAccesos(string rutaArchivo)
        {
            // Loggeamos que la función fue llamada
            Program.Log($"FUNCION CARGARACCESOS LLAMADA CON RUTA: {rutaArchivo}");
            // Creamos la lista de retorno
            List<Acceso> listaRetorno = new();
            // Un try por si algo sale mal
            try
            {
                // Creamos el filestream para el archivo
                using FileStream fStream = new(rutaArchivo, FileMode.Open, FileAccess.Read);
                // Creamos el lector binario
                using BinaryReader bReader = new(fStream);
                // Si el sello no está presente tiramos una excepción (esto lo atrapa el catch de todas maneras)
                if (bReader.ReadString() != _sello)
                    throw new FileFormatException("El archivo cargado no pertenece a un archivo de accesos FactuCrossing");
                // Leemos la cantidad de accesos a cargar
                int cantidadAccesos = bReader.ReadInt32();
                // Loggeamos la cantidad de accesos
                Program.Log($"- Cantidad de Accesos a leer: {cantidadAccesos}");
                // Loggeamos los bytes en el archivo
                Program.Log($"- Bytes en el archivo: {fStream.Length}");
                // Iteramos por la cantidad de accesos que vamos a cargar
                for (int i = 0; i < cantidadAccesos; i++)
                {
                    // Loggeamos nuestro progreso
                    Program.Log($"------------- Leyendo acceso #{i + 1} / {cantidadAccesos} -------------");
                    // Leemos el acceso y lo agregamos a nuestra lista de retorno
                    listaRetorno.Add(LeerAcceso(bReader));
                    // Loggeamos el éxito
                    Program.Log($"-- Acceso creado y añadido con éxito --");
                }
            }
            // Un catch para avisar del error al usuario
            catch (Exception ex)
            {
                // Mostramos el mensaje al usuario para que sepa que salió mal
                MessageBox.Show($"Error cargando el archivo: {ex}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Retornamos los accesos que logramos cargar
            return listaRetorno;
        }

        /// <summary> Función privada para cargar un solo acceso dado un lector binario (<paramref name="bReader"/>) 
        /// <para>
        /// Usada en la función publica <seealso cref="LeerAccesos(string)"/><br/>
        /// Lee un acceso (clase <seealso cref="Acceso"/>) dado un lector binario
        /// </para>
        /// </summary>
        /// <param name="bReader">Lector binario</param>
        /// <returns>Acceso cargado del archivo</returns>
        private Acceso LeerAcceso(BinaryReader bReader)
        {
            // Declaramos la variable para almacenar el atributo leído
            IDAtributos atributoLeido;
            // Inicializamos las variables para almacenar los valores de los atributos del acceso
            int? cuentaIdLeido = null;
            DateTime? tiempoDeAccesoLeido = null;
            TipoDeAcceso? tipoLeido = null;
            // Iniciamos un bucle para leer los atributos del acceso hasta encontrar el atributo de fin
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
                    // Caso para el atributo TIEMPO DE ACCESO
                    case IDAtributos.TIEMPODEACCESO:
                        {
                            // Leemos el valor del tiempo de acceso y lo almacenamos
                            DateTime buffer = DateTime.FromBinary(bReader.ReadInt64());
                            Program.Log($"-- Tiempo de Acceso leido: {buffer}");
                            tiempoDeAccesoLeido = buffer;
                            break;
                        }
                    // Caso para el atributo TIPO
                    case IDAtributos.TIPO:
                        {
                            // Leemos el valor del tipo de acceso y lo almacenamos
                            Int32 buffer = bReader.ReadInt32();
                            if (!Enum.IsDefined(typeof(TipoDeAcceso), buffer))
                                throw new ArgumentOutOfRangeException($"No se reconoce el buffer {buffer} como un tipo de acceso válido");
                            Program.Log($"-- Tipo leido: {buffer}");
                            tipoLeido = (TipoDeAcceso)buffer;
                            break;
                        }
                }
                // Continuamos el bucle hasta encontrar el atributo de fin
            } while (atributoLeido != IDAtributos.ATRIBUTOFIN);

            // Creamos nuestra variable para almacenar el acceso
            Acceso nuevoAcceso;
            // Creamos el nuevo acceso con los datos
            nuevoAcceso = new(
                // Si el cuentaIdLeido es nulo, tirar una excepción ya que es un parametro obligatorio
                idDeCuenta: cuentaIdLeido ?? throw new ArgumentNullException("[requerido] El parametro cuentaId no se encontró"),
                // Si el tiempo de acceso es nulo, tirar una excepción ya que es un parametro obligatorio
                tiempoDeAcceso: tiempoDeAccesoLeido ?? throw new ArgumentNullException("[requerido] El parametro tiempoDeAcceso no se encontró"),
                // Si el tipo de acceso es nulo, tirar una excepción ya que es un parametro obligatorio
                tipo: tipoLeido ?? throw new ArgumentNullException("[requerido] El parametro tipo no se encontró")
                );
            // Retornamos el acceso creado
            return nuevoAcceso;
        }
    }
}