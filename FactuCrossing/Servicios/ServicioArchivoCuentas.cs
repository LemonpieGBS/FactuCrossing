using FactuCrossing.Estructuras;

namespace FactuCrossing.Servicios
{
    /// <summary>
    /// Clase responsable de todos los métodos de la clase <seealso cref="Estructuras.Cuenta"/> 
    /// para manejo de archivos
    /// </summary>
    public class ServicioArchivoCuentas
    {
        /// <summary>
        /// Sello para comprobar si un archivo es perteneciente a uno de cuentas
        /// </summary>
        private const string _sello = "FACTUCROSSING - ARCHIVO DE CUENTAS";

        /// <summary>
        /// Enumerador responsable de identificar los atributos de la clase <seealso cref="Estructuras.Cuenta"/> 
        /// a la hora de guardar y cargar
        /// </summary>
        private enum IDAtributos
        {
            ATRIBUTOFIN = 0,
            ID = 1,
            NOMBREUSUARIO = 2,
            NOMBREDISPLAY = 3,
            SALT = 4,
            HASH = 5,
            HABILITADA = 6,
            CONTRASENATEMPORAL = 7,
            ROL = 8,
            TIEMPOSESION = 9
        }

        /// <summary> Función para guardar una lista de cuentas (<paramref name="cuentas"/>) en un archivo (<paramref name="rutaArchivo"/>) 
        /// <para>
        /// Esta función usa <seealso cref="BinaryWriter"/> para escribir una lista de cuentas (clase <seealso cref="Estructuras.Cuenta"/>) a un archivo binario<br/>
        /// Se guarda un sello (<seealso cref="_sello"/>) para a la hora de cargar un archivo comprobar que es un archivo perteneciente a esta función<br/><br/>
        /// Para cargar las cuentas de un archivo, usa <seealso cref="LeerCuentas(string)"/>
        /// </para>
        /// </summary>
        /// <param name="cuentas">Lista de cuentas a guardar</param>
        /// <param name="rutaArchivo">Ruta en donde guardar las cuentas como archivo binario</param>
        public void EscribirCuentas(List<Cuenta> cuentas, string rutaArchivo)
        {
            // Loggeamos que la función fue llamada
            Program.Log($"FUNCION GUARDAR CUENTAS LLAMADA CON RUTA: {rutaArchivo}");
            // Un try por si algo sale mal
            try
            {
                // Creamos el filestream para el archivo
                using FileStream fStream = new(rutaArchivo, FileMode.Create);
                // Creamos el escritor binario
                using BinaryWriter bWriter = new(fStream);
                // Usamos el escritor binario para 'sellar' el archivo
                bWriter.Write(_sello);
                // Loggeamos la cantidad de cuentas que se guardarán
                Program.Log($"Cuentas Totales: {cuentas.Count}");
                // Usamos el escritor binario para guardar la cantidad de cuentas
                // Esto será útil para cargarlas después
                bWriter.Write(cuentas.Count);
                // Iteramos por todas las cuentas para guardar cada una
                for (int i = 0; i < cuentas.Count; i++)
                {
                    // Loggeamos el progreso del programa
                    Program.Log($"------------- Escribiendo cuenta #{i + 1} / {cuentas.Count} -------------");
                    // Si la cuenta no esta iniciada, se ignora
                    if (!cuentas[i].SesionIniciada) continue;
                    // En el caso que si, escribimos la cuenta
                    EscribirCuenta(cuentas[i], bWriter);
                }
            }
            // Un catch para avisar del eror al usuario
            catch (Exception ex)
            {
                // Mostramos un mensajito para el usuario
                MessageBox.Show($"Error guardando el archivo: {ex}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary> Función privada para guardar una sola cuenta (<paramref name="cuenta"/>) dada un escritor binario (<paramref name="bWriter"/>) 
        /// <para>
        /// Usada en la función publica <seealso cref="EscribirCuentas(List{Cuenta}, string)"/><br/>
        /// Escribe una cuenta (clase <seealso cref="Estructuras.Cuenta"/>) a un escritor binario
        /// </para>
        /// </summary>
        /// <param name="cuenta">Cuenta a guardar</param>
        /// <param name="bWriter">Escritor binario</param>
        private void EscribirCuenta(Cuenta cuenta, BinaryWriter bWriter)
        {
            // Escribir el atributo ID
            bWriter.Write((Int32)IDAtributos.ID);
            bWriter.Write((Int32)cuenta.Id);
            Program.Log($"-- Atributo Escrito [ID]: {cuenta.Id}");

            // Escribir el atributo NOMBRE DISPLAY
            bWriter.Write((Int32)IDAtributos.NOMBREDISPLAY);
            bWriter.Write(cuenta.NombreDisplay);
            Program.Log($"-- Atributo Escrito [NOMBREDISPLAY: {cuenta.NombreDisplay}");

            // Escribir el atributo NOMBRE USUARIO
            bWriter.Write((Int32)IDAtributos.NOMBREUSUARIO);
            bWriter.Write(cuenta.NombreUsuario);
            Program.Log($"-- Atributo Escrito [NOMBREUSUARIO]: {cuenta.NombreUsuario}");

            // Escribir el atributo ROL
            bWriter.Write((Int32)IDAtributos.ROL);
            bWriter.Write((Int32)cuenta.Rol);
            Program.Log($"-- Atributo Escrito [ROL]: {cuenta.Rol}");

            // Escribir el atributo CUENTA HABILITADA
            bWriter.Write((Int32)IDAtributos.HABILITADA);
            bWriter.Write(cuenta.Habilitada);
            Program.Log($"-- Atributo Escrito [HABILITADA]: {cuenta.Habilitada}");

            // Escribir el atributo CONTRASEÑA TEMPORAL
            bWriter.Write((Int32)IDAtributos.CONTRASENATEMPORAL);
            bWriter.Write(cuenta.ContraseñaTemporal);
            Program.Log($"-- Atributo Escrito [CONTRASENATEMPORAL]: {cuenta.ContraseñaTemporal}");

            // Escribir el atributo SALT
            bWriter.Write((Int32)IDAtributos.SALT);
            bWriter.Write(cuenta.Contraseña.Salt);
            Program.Log($"-- Atributo Escrito [SALT]: {cuenta.Contraseña.Salt}");

            // Escribir el atributo HASH
            bWriter.Write((Int32)IDAtributos.HASH);
            // Escribimos el tamaño del hash para guiarnos a la hora de cargar
            bWriter.Write((Int32)cuenta.Contraseña.Hash.Length);
            // Usamos un iterador para escribir cada byte
            foreach (Byte b in cuenta.Contraseña.Hash) bWriter.Write(b);
            Program.Log($"-- Atributo Escrito [HASH] con tamaño {cuenta.Contraseña.Hash.Length}");

            // Escribir el atributo TIEMPO DE SESIÓN
            bWriter.Write((Int32)IDAtributos.TIEMPOSESION);
            bWriter.Write(cuenta.TiempoSesion.tiempoPorDia.Count);
            foreach (var kvp in cuenta.TiempoSesion.tiempoPorDia)
            {
                bWriter.Write(kvp.Key.ToBinary());
                bWriter.Write(kvp.Value);
            }
            Program.Log($"-- Atributo Escrito [TIEMPOSESION] con {cuenta.TiempoSesion.tiempoPorDia.Count} entradas");

            // Escribimos el atributo FIN para marcar el fin de la escritura
            bWriter.Write((Int32)IDAtributos.ATRIBUTOFIN);
            Program.Log($"-- Atributo Escrito [ATRIBUTOFIN]");
        }

        /// <summary> Función para cargar una lista de cuentas en un archivo (<paramref name="rutaArchivo"/>) 
        /// <para>
        /// Esta función usa <seealso cref="BinaryReader"/> para cargar una lista de cuentas (clase <seealso cref="Estructuras.Cuenta"/>) de un archivo binario<br/>
        /// Se carga un sello (<seealso cref="_sello"/>) para comprobar que es un archivo perteneciente a esta función<br/><br/>
        /// Para guardar las cuentas a un archivo, usa <seealso cref="EscribirCuentas(List{Cuenta}, string)"/>
        /// </para>
        /// </summary>
        /// <param name="rutaArchivo">Ruta en donde se encuentra el archivo binario para cargar las cuentas</param>
        /// <returns>Lista de cuentas cargadas</returns>
        public List<Cuenta> LeerCuentas(string rutaArchivo)
        {
            // Loggeamos que la función fue llamada
            Program.Log($"FUNCION CARGARCUENTAS LLAMADA CON RUTA: {rutaArchivo}");
            // Creamos la lista de retorno
            List<Cuenta> listaRetorno = new();
            // Un try por si algo sale mal
            try
            {
                // Creamos el filestream para el archivo
                using FileStream fStream = new(rutaArchivo, FileMode.Open, FileAccess.Read);
                // Creamos el lector binario
                using BinaryReader bReader = new(fStream);
                // Si el sello no está presente tiramos una excepción (esto lo atrapa el catch de todas maneras)
                if (bReader.ReadString() != _sello)
                    throw new FileFormatException("El archivo cargado no pertenece a un archivo de cuentas FactuCrossing");
                // Leemos la cantidad de cuentas a cargar
                int cantidadCuentas = bReader.ReadInt32();
                // Loggeamos la cantidad de cuentas
                Program.Log($"- Cantidad de Cuentas a leer: {cantidadCuentas}");
                // Loggeamos los bytes en el archivo
                Program.Log($"- Bytes en el archivo: {fStream.Length}");
                // Iteramos por la cantidad de cuentas que vamos a cargar
                for (int i = 0; i < cantidadCuentas; i++)
                {
                    // Loggeamos nuestro progreso
                    Program.Log($"------------- Leyendo cuenta #{i + 1} / {cantidadCuentas} -------------");
                    // Leemos la cuenta y la agregamos a nuestra lista de retorno
                    listaRetorno.Add(LeerCuenta(bReader));
                    // Loggeamos el éxito
                    Program.Log($"-- Cuenta creada y añadida con éxito --");
                }
            }
            // Un catch para avisar del eror al usuario
            catch (Exception ex)
            {
                // Mostramos el mensaje al usuario para que sepa que salió mal
                MessageBox.Show($"Error cargando el archivo: {ex}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Retornamos las cuentas que logramos cargar
            return listaRetorno;
        }

        /// <summary> Función privada para cargar una sola cuenta dada un lector binario (<paramref name="bReader"/>) 
        /// <para>
        /// Usada en la función publica <seealso cref="LeerCuentas(string)"/><br/>
        /// Lee una cuenta (clase <seealso cref="Estructuras.Cuenta"/>) dado un lector binario
        /// </para>
        /// </summary>
        /// <param name="bReader">Lector binario</param>
        /// <returns>Cuenta cargada del archivo</returns>
        private Cuenta LeerCuenta(BinaryReader bReader)
        {
            // Declaramos la variable para almacenar el atributo leído
            IDAtributos atributoLeido;
            // Inicializamos las variables para almacenar los valores de los atributos de la cuenta
            int? idLeido = null;
            string? nombreUsuario = null;
            string? nombreDisplay = null;
            Roles? rolLeido = null;
            bool? habilitada = null;
            bool? contrasenaTemporal = null;
            string? saltLeido = null;
            byte[]? hashLeido = null;
            Dictionary<DateTime, double>? tiempoSesionLeido = null;
            // Iniciamos un bucle para leer los atributos de la cuenta hasta encontrar el atributo de fin
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
                    // Caso para el atributo ID
                    case IDAtributos.ID:
                        {
                            // Leemos el valor del ID y lo almacenamos
                            Int32 buffer = bReader.ReadInt32();
                            Program.Log($"-- id leido: {buffer}");
                            idLeido = buffer;
                            break;
                        }
                    // Caso para el atributo NOMBREUSUARIO
                    case IDAtributos.NOMBREUSUARIO:
                        {
                            // Leemos el valor del nombre de usuario y lo almacenamos
                            string buffer = bReader.ReadString();
                            Program.Log($"-- Nombre Display leido: {buffer}");
                            nombreUsuario = buffer;
                            break;
                        }
                    // Caso para el atributo NOMBREDISPLAY
                    case IDAtributos.NOMBREDISPLAY:
                        {
                            // Leemos el valor del nombre de display y lo almacenamos
                            string buffer = bReader.ReadString();
                            Program.Log($"-- Nombre de Usuario leido: {buffer}");
                            nombreDisplay = buffer;
                            break;
                        }
                    // Caso para el atributo ROL
                    case IDAtributos.ROL:
                        {
                            // Leemos el valor del rol y verificamos si es válido
                            Int32 buffer = bReader.ReadInt32();
                            if (!Enum.IsDefined(typeof(Roles), buffer))
                                throw new ArgumentOutOfRangeException($"No se reconoce el buffer {buffer} como un rol válido");
                            Program.Log($"-- Rol leido: {buffer}");
                            rolLeido = (Roles)buffer;
                            break;
                        }
                    // Caso para el atributo HABILITADA
                    case IDAtributos.HABILITADA:
                        {
                            // Leemos el valor de habilitada y lo almacenamos
                            bool buffer = bReader.ReadBoolean();
                            Program.Log($"-- Cuenta Habilitada?: {buffer}");
                            habilitada = buffer;
                            break;
                        }
                    // Caso para el atributo CONTRASENATEMPORAL
                    case IDAtributos.CONTRASENATEMPORAL:
                        {
                            // Leemos el valor de contraseña temporal y lo almacenamos
                            bool buffer = bReader.ReadBoolean();
                            Program.Log($"-- Contraseña Temporal?: {buffer}");
                            contrasenaTemporal = buffer;
                            break;
                        }
                    // Caso para el atributo SALT
                    case IDAtributos.SALT:
                        {
                            // Leemos el valor del salt y lo almacenamos
                            string buffer = bReader.ReadString();
                            Program.Log($"-- Salt leido: {buffer}");
                            saltLeido = buffer;
                            break;
                        }
                    // Caso para el atributo HASH
                    case IDAtributos.HASH:
                        {
                            // Leemos el tamaño del hash
                            int bufferSize = bReader.ReadInt32();
                            Program.Log($"-- Lectura de Hash con tamaño {bufferSize}");

                            // Leemos el hash byte por byte y lo almacenamos en un array
                            byte[] buffer = new byte[bufferSize];
                            for (int j = 0; j < bufferSize; j++)
                            {
                                byte b = bReader.ReadByte();
                                buffer[j] = b;
                            }
                            Program.Log($"-- Hash leído correctamente");
                            hashLeido = buffer;
                            break;
                        }
                    // Caso para el atributo TIEMPO DE SESIÓN
                    case IDAtributos.TIEMPOSESION:
                        {
                            // Leemos la cantidad de entradas de tiempo de sesión
                            int count = bReader.ReadInt32();
                            tiempoSesionLeido = new Dictionary<DateTime, double>();
                            for (int j = 0; j < count; j++)
                            {
                                DateTime key = DateTime.FromBinary(bReader.ReadInt64());
                                double value = bReader.ReadDouble();
                                tiempoSesionLeido.Add(key, value);
                            }
                            Program.Log($"-- Tiempo de sesión leído con {count} entradas");
                            break;
                        }
                }
            // Continuamos el bucle hasta encontrar el atributo de fin
            } while (atributoLeido != IDAtributos.ATRIBUTOFIN);

            // Hacemos comprobaciones para advertir en el caso que algun parametro opcional falte
            // Advertimos si nombreDisplay es nulo o vacío
            if (string.IsNullOrEmpty(nombreDisplay))
            {
                Program.Log("El parametro NombreDisplay no se encontró, se reemplazará por el nombre de usuario");
            }
            // Advertimos si 'habilitada' es nulo o vacío
            if (habilitada is null)
            {
                Program.Log("El parametro Habilitada no se encontró, se reemplazará por true");
            }
            // Advertimos si 'contraseñaTemporal' es nulo o vacío
            if (contrasenaTemporal is null)
            {
                Program.Log("El parametro contraseñaTemporal no se encontró, se reemplazará por false");
            }
            // Advertimos si 'tiempoSesion' es nulo o vacío
            if (contrasenaTemporal is null)
            {
                Program.Log("El parametro tiempoSesion no se encontró, se reemplazará por una lista vacía");
            }

            // Creamos nuestra variable para almacenar la cuenta
            Cuenta nuevaCuenta;
            // Creamos la nueva cuenta con los datos
            nuevaCuenta = new(
                // Si el idLeido es nulo, tirar una excepción ya que es un parametro obligatorio
                _id: idLeido ?? throw new ArgumentNullException("[requerido] El parametro ID no se encontró"),
                // Si el nombre de usuario es nulo, tirar una excepción ya que es un parametro obligatorio
                _nombre: nombreUsuario ?? throw new ArgumentNullException("[requerido] El parametro nombreUsuario no se encontró"),
                // Si el nombre de display es nulo, asignarlo al nombre de usuario (parametro opcional)
                _nombredisplay: nombreDisplay ?? nombreUsuario,
                // Si el rol de usuario es nulo, tirar una excepción ya que es un parametro obligatorio
                _rol: rolLeido ?? throw new ArgumentNullException("[requerido] El parametro Rol no se encontró"),
                // En la creación de contraseña, necesitamos el hash y el salt
                _contraseña: new HashSalt(
                    // Si el hash es nulo, tirar una excepción ya que es un parametro obligatorio
                    hashLeido ?? throw new ArgumentNullException("[requerido] El parametro Hash no se encontró"),
                    // Si el salt es nulo, tirar una excepción ya que es un parametro obligatorio
                    saltLeido ?? throw new ArgumentNullException("[requerido] El parametro Salt no se encontró")
                    ),
                // Si el tiempo de sesion no se leyó, creamos una lista vacía
                _tiempoSesion: new TiempoSesion(tiempoSesionLeido ?? new Dictionary<DateTime, double>())
                );
            // Ahora asignamos las propiedades que no se asignan en el constructor
            // Si habilitada es nulo, asignamos el valor por default (true)
            nuevaCuenta.Habilitada = (bool)(habilitada ?? true);
            // Si contraseñaTemporal es nulo, asignamos el valor por default (false)
            nuevaCuenta.ContraseñaTemporal = (bool)(contrasenaTemporal ?? false);
            // Retornamos la cuenta creada
            return nuevaCuenta;
        }
    }


}
