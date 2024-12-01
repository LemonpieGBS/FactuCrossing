using FactuCrossing.Estructuras;
using FactuCrossing.Servicios;

namespace FactuCrossing
{
    // La clase del sistema central yay :3333333
    // Aqui se guarda toda la información de manera centralizada
    // Decidi hacer la clase estática porque tener una instancia me es horripilante

    /// <summary>
    /// Clase del sistema central donde se almacenan todos lo datos
    /// </summary>
    static class SistemaCentral
    {
        /// <summary>
        /// Módulo del sistema central donde se almacena y maneja todo lo relacionado con cuentas
        /// </summary>
        public static class Cuentas
        {
            /// <summary> Tiempo en el que se inició la sesión </summary>
            public static DateTime sesionIniciada { get; private set; }
            /// <summary> Tiempo de ultima calculación </summary>
            private static DateTime? _IultimaRefrezcada = null;
            /// <summary> Cuenta en sesión a cargo del programa </summary>
            public static Cuenta? cuentaEnSesion = null;
            /// <summary> Lista de cuentas cargadas en memoria </summary>
            private static List<Cuenta> _IcuentasEnMemoria = new();
            /// <summary> Una función solo puede leer las cuentas, para cambiar la lista tendrá que usar una de las funciones de la clase </summary>
            public static IReadOnlyList<Cuenta> cuentasEnMemoria { get { return _IcuentasEnMemoria; } }
            /// <summary> String que contiene la ruta de archivo en donde se almacenan las cuentas </summary>
            public static string archivoCuentas = FileHelper.SaveDataPath + "informacionDeRegistro.bin";

            /// <summary>
            ///  Función para establecer una cuenta en memoria
            /// </summary>
            public static void EstablecerIndice(int indice, Cuenta nuevaCuenta)
            {
                // Si el índice o el nombre de usuario ya existen en la lista, retornar falso
                if (cuentasEnMemoria.Any(c => c.Id == nuevaCuenta.Id || c.NombreUsuario == nuevaCuenta.NombreUsuario))
                {
                    // No se añade nada a la lista y tira un error
                    throw new Exception("El ID o el nombre de usuario ya existen en la lista de cuentas");
                }

                // Si el índice esta fuera de rango, se tira otro error
                if (indice < 0 || indice > _IcuentasEnMemoria.Count)
                {
                    // No se añade nada a la lista y tira un error
                    throw new Exception("El índice está fuera de rango");
                }

                // Si no, se establece
                _IcuentasEnMemoria[indice] = nuevaCuenta;
            }

            /// <summary>
            /// Función para añadir una cuenta a memoria
            /// </summary>
            public static void AñadirCuenta(Cuenta nuevaCuenta)
            {
                // Si el índice o el nombre de usuario ya existen en la lista, retornar falso
                if (cuentasEnMemoria.Any(c => c.Id == nuevaCuenta.Id || c.NombreUsuario == nuevaCuenta.NombreUsuario))
                {
                    // No se añade nada a la lista y tira un error
                    throw new Exception("El ID o el nombre de usuario ya existen en la lista de cuentas");
                }
                // Si no, se añade
                _IcuentasEnMemoria.Add(nuevaCuenta);
            }

            /// <summary>
            /// Refrezcar cuenta en memoria
            /// </summary>
            public static void RefrezcarCuenta(Cuenta cuenta)
            {
                // Se busca el índice de la cuenta
                int indice = CuentaEnMemoria(cuenta);
                // Si no se encuentra, tira un error
                if (indice == -1) throw new Exception("La cuenta no se encuentra en memoria");
                // Si se encuentra, se establece
                else _IcuentasEnMemoria[indice] = cuenta;
            }

            /// <summary>
            /// Refrezcar Inicio de Sesión
            /// </summary>
            public static void RefrezcarInicioDeSesion()
            {
                // Log
                Program.Log("Se refrezcó la sesión");
                // Refrezcamos cuando se inició sesión
                sesionIniciada = DateTime.Now;
            }

            /// <summary>
            /// Calcular el tiempo que el usuario ha estado en sesión desde la última vez
            /// que se llamó la función
            /// </summary>
            public static void CalcularTiempoDeSesion()
            {
                // Si no hay cuenta en sesión, lanzamos una excepción
                if (cuentaEnSesion is null)
                    throw new ArgumentNullException(nameof(cuentaEnSesion));
                // Obtenemos la hora actual
                DateTime ahora = DateTime.Now;
                // Si no hubo un último uso de la función, usamos sesiónIniciada
                if (_IultimaRefrezcada is null)
                {
                    // Calculamos la cantidad de segundos desde que se inició la sesión hasta ahora
                    double segundos = (ahora - sesionIniciada).TotalSeconds;
                    // Verificamos que los segundos sean un valor razonable
                    if (segundos < 0 || segundos > 86400) // 86400 segundos = 24 horas
                    {
                        Program.Log($"Valor de segundos no válido: {segundos}");
                        return;
                    }
                    // Loggeamos el tiempo añadido
                    Program.Log($"Se añadieron {segundos} segundo(s) al tiempo de sesión");
                    // Agregamos el tiempo a la cuenta
                    ((Cuenta)cuentaEnSesion).TiempoSesion.AgregarTiempo(sesionIniciada, segundos);
                }
                else
                {
                    // Calculamos el tiempo en segundos entre el último uso de la función y ahora
                    double segundos = (ahora - (DateTime)_IultimaRefrezcada).TotalSeconds;
                    // Verificamos que los segundos sean un valor razonable
                    if (segundos < 0 || segundos > 86400) // 86400 segundos = 24 horas
                    {
                        Program.Log($"Valor de segundos no válido: {segundos}");
                        return;
                    }
                    // Loggeamos el tiempo añadido
                    Program.Log($"Se añadieron {segundos} segundo(s) al tiempo de sesión");
                    // Agregamos el tiempo a la cuenta
                    ((Cuenta)cuentaEnSesion).TiempoSesion.AgregarTiempo((DateTime)_IultimaRefrezcada, segundos);
                }
                // Actualizamos el último uso de la función
                _IultimaRefrezcada = ahora;
                // Guardamos cuentas yay
                GuardarCuentas();
            }

            /// <summary>
            ///  Función para guardar las cuentas en memoria en archivoCuentas
            /// </summary>
            public static void GuardarCuentas()
            {
                // Se crea una instancia del servicio de archivo para cuentas
                ServicioArchivoCuentas sac = new ServicioArchivoCuentas();
                // Dar una lista con solo las cuentas que YA tienen su sesión iniciada
                List<Cuenta> listaVerdadera = new List<Cuenta>() { };
                // Iterar por cada cuenta en memoria para ver cuales tienen su sesión iniciada
                foreach (Cuenta cuenta in _IcuentasEnMemoria)
                    // Si la cuenta tiene su sesión iniciada, añadirla a la lista
                    if (cuenta.SesionIniciada) listaVerdadera.Add(cuenta);
                // Se llama la función para guardar
                sac.EscribirCuentas(listaVerdadera, archivoCuentas);
            }

            /// <summary>
            ///  Función para cargar las cuentas a memoria del archivo archivoCuentas
            /// </summary>
            public static void CargarCuentas()
            {
                // Si el archivo no existe, se retorna
                if (!File.Exists(archivoCuentas))
                {
                    // Mensaje de log
                    Program.Log($"El archivo {archivoCuentas} no existe, se iniciará con la cuenta default de administrador (admin, 1234)");
                    return;
                }
                // Se crea una instancia del servicio de archivo para cuentas
                ServicioArchivoCuentas sac = new ServicioArchivoCuentas();
                // Se llama la función para cargar
                List<Cuenta> cuentasCargadas = sac.LeerCuentas(archivoCuentas);
                // Si no se cargaron cuentas se mantiene la lista original (admin, 1234)
                if (cuentasCargadas.Count != 0)
                    _IcuentasEnMemoria = cuentasCargadas;
                else
                    Program.Log($"El archivo {archivoCuentas} no contiene datos o no se pudo leer, se iniciará con la cuenta default de administrador (admin, 1234)");
            }

            /// <summary>
            /// Función para obtener el índice en memoria de una cuenta dada su ID,
            /// los IDs son únicos así que esta función es primordial para acceder a cuentas específicas
            /// </summary>
            /// <param name="_id">Id de la cuenta a buscar</param>
            /// <returns> El id de la cuenta en memoria o -1 si no se encontró </returns>
            public static int CuentaEnMemoria(int _id)
            {
                // Se busca en un for todas las cuentas en memoria
                for (int i = 0; i < _IcuentasEnMemoria.Count; i++)
                {
                    // Si el id de alguna coincide, se retorna el índice actual
                    if (_IcuentasEnMemoria[i].Id == _id) return i;
                }
                // Si no se encuentra, se retorna -1
                return -1;
            }

            /// <summary>
            /// Función para obtener el índice en memoria de una cuenta,
            /// se encuentra usando su ID y <seealso cref="IndiceEnMemoria(int)"/>
            /// </summary>
            /// <param name="_cuenta">Objeto de la cuenta a buscar</param>
            /// <returns> El id de la cuenta en memoria o -1 si no se encontró </returns>
            public static int CuentaEnMemoria(Cuenta _cuenta)
            {
                // Se llama a IndiceEnMemoria con el id de la cuenta
                return CuentaEnMemoria(_cuenta.Id);
            }

            /// <summary>
            /// Función para obtener una cuenta en memoria dada su ID,
            /// los IDs son únicos así que esta función es primordial para acceder a cuentas específicas
            /// </summary>
            /// <param name="_id">Id de la cuenta a buscar</param>
            /// <returns> La cuenta en memoria o null si no se encontró </returns>
            public static Cuenta? ObtenerCuentaPorId(int _id)
            {
                // Se busca en un for todas las cuentas en memoria
                for (int i = 0; i < _IcuentasEnMemoria.Count; i++)
                {
                    // Si el id de alguna coincide, se retorna la cuenta actual
                    if (_IcuentasEnMemoria[i].Id == _id) return _IcuentasEnMemoria[i];
                }
                // Si no se encuentra, se retorna null
                return null;
            }
        }

        /// <summary>
        /// Módulo del sistema central donde se almacena y maneja todo lo relacionado con el inventario
        /// </summary>
        public static class Inventario
        {
            /// <summary> Lista de productos en memoria </summary>
            public static List<Producto> productosEnMemoria = new();
            /// <summary> String que contiene la ruta de archivo en donde se almacenan los productos </summary>
            public static string archivoProductos = FileHelper.SaveDataPath + "productosInventario.bin";

            /// <summary>
            /// Función para guardar los productos en memoria en archivoProductos
            /// </summary>
            public static void GuardarProductos()
            {
                // Se crea una instancia del servicio de archivo para productos
                ServicioArchivoInventario sai = new ServicioArchivoInventario();
                // Se llama la función para guardar
                sai.EscribirProductos(productosEnMemoria, archivoProductos);
            }

            /// <summary>
            /// Función para cargar los productos a memoria del archivo archivoProductos
            /// </summary>
            public static void CargarProductos()
            {
                // Si el archivo no existe, se retorna
                if (!File.Exists(archivoProductos))
                {
                    // Mensaje de log
                    Program.Log($"El archivo {archivoProductos} no existe, se iniciará sin productos previos.");
                    return;
                }
                // Se crea una instancia del servicio de archivo para productos
                ServicioArchivoInventario sai = new ServicioArchivoInventario();
                // Se llama la función para cargar
                List<Producto> productosCargados = sai.LeerProductos(archivoProductos);
                // Si no se cargaron productos se mantiene la lista original vacía
                if (productosCargados.Count != 0)
                    productosEnMemoria = productosCargados;
                else
                    Program.Log($"El archivo {archivoProductos} no contiene datos o no se pudo leer, se iniciará sin productos previos.");
            }

            /// <summary>
            /// Función para obtener el índice en memoria de un producto dado su ID,
            /// los IDs son únicos así que esta función es primordial para acceder a productos específicos
            /// </summary>
            /// <param name="_id">Id del producto a buscar</param>
            /// <returns> El índice del producto en memoria o -1 si no se encontró </returns>
            public static int ProductoEnMemoria(int _id)
            {
                // Se busca en un for todos los productos en memoria
                for (int i = 0; i < productosEnMemoria.Count; i++)
                {
                    // Si el id de alguno coincide, se retorna el índice actual
                    if (productosEnMemoria[i].Id == _id) return i;
                }
                // Si no se encuentra, se retorna -1
                return -1;
            }

            /// <summary>
            /// Función para obtener el índice en memoria de un producto,
            /// se encuentra usando su ID y <seealso cref="IndiceEnMemoria(int)"/>
            /// </summary>
            /// <param name="_producto">Objeto del producto a buscar</param>
            /// <returns> El índice del producto en memoria o -1 si no se encontró </returns>
            public static int ProductoEnMemoria(Producto _producto)
            {
                // Se llama a IndiceEnMemoria con el id del producto
                return ProductoEnMemoria(_producto.Id);
            }

            /// <summary>
            /// Función para obtener un producto en memoria dado su ID,
            /// los IDs son únicos así que esta función es primordial para acceder a productos específicos
            /// </summary>
            /// <param name="_id">Id del producto a buscar</param>
            /// <returns> El producto en memoria o null si no se encontró </returns>
            public static Producto? ObtenerProductoPorId(int _id)
            {
                // Se busca en un for todos los productos en memoria
                for (int i = 0; i < productosEnMemoria.Count; i++)
                {
                    // Si el id de alguno coincide, se retorna el producto actual
                    if (productosEnMemoria[i].Id == _id) return productosEnMemoria[i];
                }
                // Si no se encuentra, se retorna null
                return null;
            }
        }

        /// <summary>
        /// Módulo del sistema central donde se almacena y maneja todo lo relacionado con los accesos
        /// </summary>
        public class Accesos
        {
            /// <summary> Lista de accesos en memoria </summary>
            public static List<Acceso> accesosEnMemoria = new();
            /// <summary> String que contiene la ruta de archivo en donde se almacenan los productos </summary>
            public static string archivoAccesos = FileHelper.SaveDataPath + "todosAccesos.bin";

            /// <summary>
            /// Función para guardar los accesos en memoria en archivoAccesos
            /// </summary>
            public static void GuardarAccesos()
            {
                // Se crea una instancia del servicio de archivo para accesos
                ServicioArchivoAccesos saa = new ServicioArchivoAccesos();
                // Se llama la función para guardar
                saa.EscribirAccesos(accesosEnMemoria, archivoAccesos);
            }

            /// <summary>
            /// Función para cargar los accesos a memoria del archivo archivoAccesos
            /// </summary>
            public static void CargarAccesos()
            {
                // Si el archivo no existe, se retorna
                if (!File.Exists(archivoAccesos))
                {
                    // Mensaje de log
                    Program.Log($"El archivo {archivoAccesos} no existe, se iniciará sin accesos previos.");
                    return;
                }
                // Se crea una instancia del servicio de archivo para accesos
                ServicioArchivoAccesos saa = new ServicioArchivoAccesos();
                // Se llama la función para cargar
                List<Acceso> accesosCargados = saa.LeerAccesos(archivoAccesos);
                // Si no se cargaron accesos se mantiene la lista original vacía
                if (accesosCargados.Count != 0)
                    accesosEnMemoria = accesosCargados;
                else
                    Program.Log($"El archivo {archivoAccesos} no contiene datos o no se pudo leer, se iniciará sin accesos previos.");
            }
        }
    }
}