using FactuCrossing.Estructuras;
using FactuCrossing.Servicios;

namespace FactuCrossing;
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
        private static List<Producto> _IproductosEnMemoria = new();
        /// <summary> Lista PÚBLICA de productos en memoria </summary>
        public static IReadOnlyList<Producto> productosEnMemoria => _IproductosEnMemoria.AsReadOnly();
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
            sai.EscribirProductos(_IproductosEnMemoria, archivoProductos);
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
                _IproductosEnMemoria = productosCargados;
            else
                Program.Log($"El archivo {archivoProductos} no contiene datos o no se pudo leer, se iniciará sin productos previos.");
        }

        /// <summary>
        /// Refrezcar cuenta en memoria
        /// </summary>
        public static void RefrezcarProducto(Producto producto)
        {
            // Se busca el índice de la cuenta
            int indice = ProductoEnMemoria(producto);
            // Si no se encuentra, tira un error
            if (indice == -1) throw new Exception("La cuenta no se encuentra en memoria");
            // Si se encuentra, se establece
            else _IproductosEnMemoria[indice] = producto;
        }

        /// <summary>
        /// Función para establecer un producto en memoria en un índice específico
        /// </summary>
        /// <param name="indice">Índice en el que se establecerá el producto</param>
        /// <param name="nuevoProducto">Nuevo producto a establecer</param>
        public static void EstablecerIndice(int indice, Producto nuevoProducto)
        {
            // Si el ID o el nombre del producto ya existen en la lista, lanzar una excepción
            if (_IproductosEnMemoria.Any(p => p.Id == nuevoProducto.Id || p.Nombre == nuevoProducto.Nombre))
            {
                throw new Exception("El ID o el nombre del producto ya existen en la lista de productos");
            }

            // Si el índice está fuera de rango, lanzar una excepción
            if (indice < 0 || indice >= _IproductosEnMemoria.Count)
            {
                throw new Exception("El índice está fuera de rango");
            }

            // Establecer el producto en el índice especificado
            _IproductosEnMemoria[indice] = nuevoProducto;
        }

        /// <summary>
        /// Función para añadir un producto a memoria
        /// </summary>
        /// <param name="nuevoProducto">Nuevo producto a añadir</param>
        public static void AñadirProducto(Producto nuevoProducto)
        {
            // Si el ID o el nombre del producto ya existen en la lista, lanzar una excepción
            if (_IproductosEnMemoria.Any(p => p.Id == nuevoProducto.Id || p.Nombre == nuevoProducto.Nombre))
            {
                throw new Exception("El ID o el nombre del producto ya existen en la lista de productos");
            }

            // Añadir el producto a la lista
            _IproductosEnMemoria.Add(nuevoProducto);
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
            for (int i = 0; i < _IproductosEnMemoria.Count; i++)
            {
                // Si el id de alguno coincide, se retorna el índice actual
                if (_IproductosEnMemoria[i].Id == _id) return i;
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
            for (int i = 0; i < _IproductosEnMemoria.Count; i++)
            {
                // Si el id de alguno coincide, se retorna el producto actual
                if (_IproductosEnMemoria[i].Id == _id) return _IproductosEnMemoria[i];
            }
            // Si no se encuentra, se retorna null
            return null;
        }

        /// <summary>
        /// Función para obtener un producto en memoria dado su nombre,
        /// </summary>
        /// <returns>Producto encontrado o nulo</returns>
        public static Producto? EncontrarProductoPorNombre(string nombre)
        {
            return _IproductosEnMemoria.Find(p => p.Nombre == nombre);
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

    /// <summary>
    /// Módulo del sistema central donde se almacena y maneja todo lo relacionado con las acciones
    /// </summary>
    public class Acciones
    {
        /// <summary> Lista de acciones en memoria </summary>
        public static List<Accion> accionesEnMemoria = new();
        /// <summary> String que contiene la ruta de archivo en donde se almacenan las acciones </summary>
        public static string archivoAcciones = FileHelper.SaveDataPath + "todasAcciones.bin";

        /// <summary>
        /// Función para guardar las acciones en memoria en archivoAcciones
        /// </summary>
        public static void GuardarAcciones()
        {
            // Se crea una instancia del servicio de archivo para acciones
            ServicioArchivoAcciones saa = new ServicioArchivoAcciones();
            // Se llama la función para guardar
            saa.EscribirAcciones(accionesEnMemoria, archivoAcciones);
        }

        /// <summary>
        /// Función para cargar las acciones a memoria del archivo archivoAcciones
        /// </summary>
        public static void CargarAcciones()
        {
            // Si el archivo no existe, se retorna
            if (!File.Exists(archivoAcciones))
            {
                // Mensaje de log
                Program.Log($"El archivo {archivoAcciones} no existe, se iniciará sin acciones previas.");
                return;
            }
            // Se crea una instancia del servicio de archivo para acciones
            ServicioArchivoAcciones saa = new ServicioArchivoAcciones();
            // Se llama la función para cargar
            List<Accion> accionesCargadas = saa.LeerAcciones(archivoAcciones);
            // Si no se cargaron acciones se mantiene la lista original vacía
            if (accionesCargadas.Count != 0)
                accionesEnMemoria = accionesCargadas;
            else
                Program.Log($"El archivo {archivoAcciones} no contiene datos o no se pudo leer, se iniciará sin acciones previas.");
        }
    }

    /// <summary>
    /// Módulo del sistema central donde se almacena y maneja todo lo relacionado con los descuentos
    /// </summary>
    public static class Descuentos
    {
        /// <summary> Lista de descuentos en memoria </summary>
        public static List<Descuento> descuentosEnMemoria = new();
        /// <summary> String que contiene la ruta de archivo en donde se almacenan los descuentos </summary>
        public static string archivoDescuentos = FileHelper.SaveDataPath + "todosDescuentos.bin";

        /// <summary>
        /// Función para guardar los descuentos en memoria en archivoDescuentos
        /// </summary>
        public static void GuardarDescuentos()
        {
            // Se crea una instancia del servicio de archivo para descuentos
            ServicioArchivoDescuentos sad = new ServicioArchivoDescuentos();
            // Se llama la función para guardar
            sad.EscribirDescuentos(descuentosEnMemoria, archivoDescuentos);
        }

        /// <summary>
        /// Función para cargar los descuentos a memoria del archivo archivoDescuentos
        /// </summary>
        public static void CargarDescuentos()
        {
            // Si el archivo no existe, se retorna
            if (!File.Exists(archivoDescuentos))
            {
                // Mensaje de log
                Program.Log($"El archivo {archivoDescuentos} no existe, se iniciará sin descuentos previos.");
                return;
            }
            // Se crea una instancia del servicio de archivo para descuentos
            ServicioArchivoDescuentos sad = new ServicioArchivoDescuentos();
            // Se llama la función para cargar
            List<Descuento> descuentosCargados = sad.LeerDescuentos(archivoDescuentos);
            // Si no se cargaron descuentos se mantiene la lista original vacía
            if (descuentosCargados.Count != 0)
                descuentosEnMemoria = descuentosCargados;
            else
                Program.Log($"El archivo {archivoDescuentos} no contiene datos o no se pudo leer, se iniciará sin descuentos previos.");
        }
    }

    /// <summary>
    /// Módulo del sistema central donde se almacena y maneja todo lo relacionado con facturas
    /// </summary>
    public static class Facturas
    {
        /// <summary> Lista de facturas cargadas en memoria </summary>
        private static List<Factura> _facturasEnMemoria = new();
        /// <summary> Una función solo puede leer las facturas, para cambiar la lista tendrá que usar una de las funciones de la clase </summary>
        public static IReadOnlyList<Factura> facturasEnMemoria { get { return _facturasEnMemoria; } }
        /// <summary> String que contiene la ruta de archivo en donde se almacenan las facturas </summary>
        public static string archivoFacturas = FileHelper.SaveDataPath + "facturas.bin";

        /// <summary>
        /// Función para añadir una factura a memoria
        /// </summary>
        public static void AñadirFactura(Factura nuevaFactura)
        {
            // Si el número de factura ya existe en la lista, retornar falso
            if (facturasEnMemoria.Any(f => f.NumFactura == nuevaFactura.NumFactura))
            {
                // No se añade nada a la lista y tira un error
                throw new Exception("El número de factura ya existe en la lista de facturas");
            }
            // Si no, se añade
            _facturasEnMemoria.Add(nuevaFactura);
        }

        /// <summary>
        /// Función para establecer una factura en memoria
        /// </summary>
        public static void EstablecerIndice(int indice, Factura nuevaFactura)
        {
            // Si el número de factura ya existe en la lista, retornar falso
            if (facturasEnMemoria.Any(f => f.NumFactura == nuevaFactura.NumFactura))
            {
                // No se añade nada a la lista y tira un error
                throw new Exception("El número de factura ya existe en la lista de facturas");
            }

            // Si el índice esta fuera de rango, se tira otro error
            if (indice < 0 || indice > _facturasEnMemoria.Count)
            {
                // No se añade nada a la lista y tira un error
                throw new Exception("El índice está fuera de rango");
            }

            // Si no, se establece
            _facturasEnMemoria[indice] = nuevaFactura;
        }

        /// <summary>
        /// Función para remover una factura de memoria
        /// </summary>
        public static void RemoverFactura(int numFactura)
        {
            // Validar que el número de factura no sea negativo
            if (numFactura < 0)
            {
                // Lanzar excepción si el número de factura es negativo
                throw new ArgumentOutOfRangeException(nameof(numFactura), "El número de factura no puede ser negativo");
            }
            // Validar que la factura esté en la lista
            if (!facturasEnMemoria.Any(f => f.NumFactura == numFactura))
            {
                // Si la factura no está en la lista, lanzar excepción
                throw new ArgumentException("La factura no está en la lista");
            }
            // Remover la factura de la lista
            _facturasEnMemoria.Remove(facturasEnMemoria.First(f => f.NumFactura == numFactura));
        }

        /// <summary>
        /// Función para guardar las facturas en memoria en archivoFacturas
        /// </summary>
        public static void GuardarFacturas()
        {
            // Se crea una instancia del servicio de archivo para facturas
            ServicioArchivoFacturas saf = new ServicioArchivoFacturas();
            // Se llama la función para guardar
            saf.EscribirFacturas(_facturasEnMemoria, archivoFacturas);
        }

        /// <summary>
        /// Función para cargar las facturas a memoria del archivo archivoFacturas
        /// </summary>
        public static void CargarFacturas()
        {
            // Si el archivo no existe, se retorna
            if (!File.Exists(archivoFacturas))
            {
                // Mensaje de log
                Program.Log($"El archivo {archivoFacturas} no existe, se iniciará sin facturas previas.");
                return;
            }
            // Se crea una instancia del servicio de archivo para facturas
            ServicioArchivoFacturas saf = new ServicioArchivoFacturas();
            // Se llama la función para cargar
            List<Factura> facturasCargadas = saf.LeerFacturas(archivoFacturas);
            // Si no se cargaron facturas se mantiene la lista original vacía
            if (facturasCargadas.Count != 0)
                _facturasEnMemoria = facturasCargadas;
            else
                Program.Log($"El archivo {archivoFacturas} no contiene datos o no se pudo leer, se iniciará sin facturas previas.");
        }

        /// <summary>
        /// Función para obtener el índice en memoria de una factura dada su número,
        /// los números de factura son únicos así que esta función es primordial para acceder a facturas específicas
        /// </summary>
        /// <param name="numFactura">Número de la factura a buscar</param>
        /// <returns> El índice de la factura en memoria o -1 si no se encontró </returns>
        public static int FacturaEnMemoria(int numFactura)
        {
            // Se busca en un for todas las facturas en memoria
            for (int i = 0; i < _facturasEnMemoria.Count; i++)
            {
                // Si el número de factura de alguna coincide, se retorna el índice actual
                if (_facturasEnMemoria[i].NumFactura == numFactura) return i;
            }
            // Si no se encuentra, se retorna -1
            return -1;
        }

        /// <summary>
        /// Función para obtener una factura en memoria dada su número,
        /// los números de factura son únicos así que esta función es primordial para acceder a facturas específicas
        /// </summary>
        /// <param name="numFactura">Número de la factura a buscar</param>
        /// <returns> La factura en memoria o null si no se encontró </returns>
        public static Factura? ObtenerFacturaPorNumero(int numFactura)
        {
            // Se busca en un for todas las facturas en memoria
            for (int i = 0; i < _facturasEnMemoria.Count; i++)
            {
                // Si el número de factura de alguna coincide, se retorna la factura actual
                if (_facturasEnMemoria[i].NumFactura == numFactura) return _facturasEnMemoria[i];
            }
            // Si no se encuentra, se retorna null
            return null;
        }

        /// <summary>
        /// Función para generar un ID único para una factura
        /// </summary>
        /// <returns>Id de la nueva factura</returns>
        public static int GenerarId()
        {
            int cnt = 0;
            // Se revisan todos los IDs de las facturas en memoria
            for (cnt = 0; facturasEnMemoria.Any(f => f.NumFactura == cnt); cnt++) { }
            return cnt;
        }
    }
}