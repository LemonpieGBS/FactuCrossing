using FactuCrossing.Estructuras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactuCrossing.Servicios
{
    /// <summary>
    /// Clase responsable de todos los métodos de la clase <seealso cref="Producto"/> 
    /// para manejo de archivos
    /// </summary>
    public class ServicioArchivoInventario
    {
        /// <summary>
        /// Sello para comprobar si un archivo es perteneciente a uno de inventario
        /// </summary>
        private const string _sello = "FACTUCROSSING - ARCHIVO DE INVENTARIO";

        /// <summary>
        /// Enumerador responsable de identificar los atributos de la clase <seealso cref="Producto"/> 
        /// a la hora de guardar y cargar
        /// </summary>
        private enum IDAtributos
        {
            ATRIBUTOFIN,
            ID,
            NOMBRE,
            DESCRIPCION,
            PRECIO,
            CANTIDADENSTOCK,
            PROVEEDOR,
            FECHAINGRESO,
            DESCONTINUADO
        }

        /// <summary> Función para guardar una lista de productos (<paramref name="productos"/>) en un archivo (<paramref name="rutaArchivo"/>) 
        /// <para>
        /// Esta función usa <seealso cref="BinaryWriter"/> para escribir una lista de productos (clase <seealso cref="Producto"/>) a un archivo binario<br/>
        /// Se guarda un sello (<seealso cref="_sello"/>) para a la hora de cargar un archivo comprobar que es un archivo perteneciente a esta función<br/><br/>
        /// Para cargar los productos de un archivo, usa <seealso cref="LeerProductos(string)"/>
        /// </para>
        /// </summary>
        /// <param name="productos">Lista de productos a guardar</param>
        /// <param name="rutaArchivo">Ruta en donde guardar los productos como archivo binario</param>
        public void EscribirProductos(List<Producto> productos, string rutaArchivo)
        {
            // Loggeamos que la función fue llamada
            Program.Log($"FUNCION GUARDAR PRODUCTOS LLAMADA CON RUTA: {rutaArchivo}");
            // Un try por si algo sale mal
            try
            {
                // Creamos el filestream para el archivo
                using FileStream fStream = new(rutaArchivo, FileMode.Create);
                // Creamos el escritor binario
                using BinaryWriter bWriter = new(fStream);
                // Usamos el escritor binario para 'sellar' el archivo
                bWriter.Write(_sello);
                // Loggeamos la cantidad de productos que se guardarán
                Program.Log($"Productos Totales: {productos.Count}");
                // Usamos el escritor binario para guardar la cantidad de productos
                // Esto será útil para cargarlos después
                bWriter.Write(productos.Count);
                // Iteramos por todos los productos para guardar cada uno
                for (int i = 0; i < productos.Count; i++)
                {
                    // Loggeamos el progreso del programa
                    Program.Log($"------------- Escribiendo producto #{i + 1} / {productos.Count} -------------");
                    // Escribimos el producto
                    EscribirProducto(productos[i], bWriter);
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

        /// <summary> Función privada para guardar un solo producto (<paramref name="producto"/>) dado un escritor binario (<paramref name="bWriter"/>) 
        /// <para>
        /// Usada en la función publica <seealso cref="EscribirProductos(List{Producto}, string)"/><br/>
        /// Escribe un producto (clase <seealso cref="Producto"/>) a un escritor binario
        /// </para>
        /// </summary>
        /// <param name="producto">Producto a guardar</param>
        /// <param name="bWriter">Escritor binario</param>
        private void EscribirProducto(Producto producto, BinaryWriter bWriter)
        {
            // Escribir el atributo ID
            bWriter.Write((Int32)IDAtributos.ID);
            bWriter.Write((Int32)producto.Id);
            Program.Log($"-- Atributo Escrito [ID]: {producto.Id}");

            // Escribir el atributo NOMBRE
            bWriter.Write((Int32)IDAtributos.NOMBRE);
            bWriter.Write(producto.Nombre);
            Program.Log($"-- Atributo Escrito [NOMBRE]: {producto.Nombre}");

            // Escribir el atributo DESCRIPCION
            bWriter.Write((Int32)IDAtributos.DESCRIPCION);
            bWriter.Write(producto.Descripcion);
            Program.Log($"-- Atributo Escrito [DESCRIPCION]: {producto.Descripcion}");

            // Escribir el atributo PRECIO
            bWriter.Write((Int32)IDAtributos.PRECIO);
            bWriter.Write(producto.Precio);
            Program.Log($"-- Atributo Escrito [PRECIO]: {producto.Precio}");

            // Escribir el atributo CANTIDAD EN STOCK
            bWriter.Write((Int32)IDAtributos.CANTIDADENSTOCK);
            bWriter.Write(producto.CantidadEnStock);
            Program.Log($"-- Atributo Escrito [CANTIDADENSTOCK]: {producto.CantidadEnStock}");

            // Escribir el atributo PROVEEDOR
            bWriter.Write((Int32)IDAtributos.PROVEEDOR);
            bWriter.Write(producto.Proveedor);
            Program.Log($"-- Atributo Escrito [PROVEEDOR]: {producto.Proveedor}");

            // Escribir el atributo FECHA DE INGRESO
            bWriter.Write((Int32)IDAtributos.FECHAINGRESO);
            bWriter.Write(producto.FechaIngreso.ToBinary());
            Program.Log($"-- Atributo Escrito [FECHAINGRESO]: {producto.FechaIngreso}");

            // Escribir el atributo FECHA DE INGRESO
            bWriter.Write((Int32)IDAtributos.DESCONTINUADO);
            bWriter.Write(producto.Descontinuado);
            Program.Log($"-- Atributo Escrito [DESCONTINUADO]: {producto.Descontinuado}");

            // Escribimos el atributo FIN para marcar el fin de la escritura
            bWriter.Write((Int32)IDAtributos.ATRIBUTOFIN);
            Program.Log($"-- Atributo Escrito [ATRIBUTOFIN]");
        }

        /// <summary> Función para cargar una lista de productos en un archivo (<paramref name="rutaArchivo"/>) 
        /// <para>
        /// Esta función usa <seealso cref="BinaryReader"/> para cargar una lista de productos (clase <seealso cref="Producto"/>) de un archivo binario<br/>
        /// Se carga un sello (<seealso cref="_sello"/>) para comprobar que es un archivo perteneciente a esta función<br/><br/>
        /// Para guardar los productos a un archivo, usa <seealso cref="EscribirProductos(List{Producto}, string)"/>
        /// </para>
        /// </summary>
        /// <param name="rutaArchivo">Ruta en donde se encuentra el archivo binario para cargar los productos</param>
        /// <returns>Lista de productos cargados</returns>
        public List<Producto> LeerProductos(string rutaArchivo)
        {
            // Loggeamos que la función fue llamada
            Program.Log($"FUNCION CARGARPRODUCTOS LLAMADA CON RUTA: {rutaArchivo}");
            // Creamos la lista de retorno
            List<Producto> listaRetorno = new();
            // Un try por si algo sale mal
            try
            {
                // Creamos el filestream para el archivo
                using FileStream fStream = new(rutaArchivo, FileMode.Open, FileAccess.Read);
                // Creamos el lector binario
                using BinaryReader bReader = new(fStream);
                // Si el sello no está presente tiramos una excepción (esto lo atrapa el catch de todas maneras)
                if (bReader.ReadString() != _sello)
                    throw new FileFormatException("El archivo cargado no pertenece a un archivo de inventario FactuCrossing");
                // Leemos la cantidad de productos a cargar
                int cantidadProductos = bReader.ReadInt32();
                // Loggeamos la cantidad de productos
                Program.Log($"- Cantidad de Productos a leer: {cantidadProductos}");
                // Loggeamos los bytes en el archivo
                Program.Log($"- Bytes en el archivo: {fStream.Length}");
                // Iteramos por la cantidad de productos que vamos a cargar
                for (int i = 0; i < cantidadProductos; i++)
                {
                    // Loggeamos nuestro progreso
                    Program.Log($"------------- Leyendo producto #{i + 1} / {cantidadProductos} -------------");
                    // Leemos el producto y lo agregamos a nuestra lista de retorno
                    listaRetorno.Add(LeerProducto(bReader));
                    // Loggeamos el éxito
                    Program.Log($"-- Producto creado y añadido con éxito --");
                }
            }
            // Un catch para avisar del error al usuario
            catch (Exception ex)
            {
                // Mostramos el mensaje al usuario para que sepa que salió mal
                MessageBox.Show($"Error cargando el archivo: {ex}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Retornamos los productos que logramos cargar
            return listaRetorno;
        }

        /// <summary> Función privada para cargar un solo producto dado un lector binario (<paramref name="bReader"/>) 
        /// <para>
        /// Usada en la función publica <seealso cref="LeerProductos(string)"/><br/>
        /// Lee un producto (clase <seealso cref="Producto"/>) dado un lector binario
        /// </para>
        /// </summary>
        /// <param name="bReader">Lector binario</param>
        /// <returns>Producto cargado del archivo</returns>
        private Producto LeerProducto(BinaryReader bReader)
        {
            // Declaramos la variable para almacenar el atributo leído
            IDAtributos atributoLeido;
            // Inicializamos las variables para almacenar los valores de los atributos del producto
            int? idLeido = null;
            string? nombre = null;
            string? descripcion = null;
            decimal? precio = null;
            int? cantidadEnStock = null;
            string? proveedor = null;
            DateTime? fechaIngreso = null;
            bool? descontinuado = null;
            // Iniciamos un bucle para leer los atributos del producto hasta encontrar el atributo de fin
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
                    // Caso para el atributo NOMBRE
                    case IDAtributos.NOMBRE:
                        {
                            // Leemos el valor del nombre y lo almacenamos
                            string buffer = bReader.ReadString();
                            Program.Log($"-- Nombre leido: {buffer}");
                            nombre = buffer;
                            break;
                        }
                    // Caso para el atributo DESCRIPCION
                    case IDAtributos.DESCRIPCION:
                        {
                            // Leemos el valor de la descripción y lo almacenamos
                            string buffer = bReader.ReadString();
                            Program.Log($"-- Descripción leida: {buffer}");
                            descripcion = buffer;
                            break;
                        }
                    // Caso para el atributo PRECIO
                    case IDAtributos.PRECIO:
                        {
                            // Leemos el valor del precio y lo almacenamos
                            decimal buffer = bReader.ReadDecimal();
                            Program.Log($"-- Precio leido: {buffer}");
                            precio = buffer;
                            break;
                        }
                    // Caso para el atributo CANTIDAD EN STOCK
                    case IDAtributos.CANTIDADENSTOCK:
                        {
                            // Leemos el valor de la cantidad en stock y lo almacenamos
                            int buffer = bReader.ReadInt32();
                            Program.Log($"-- Cantidad en Stock leida: {buffer}");
                            cantidadEnStock = buffer;
                            break;
                        }
                    // Caso para el atributo PROVEEDOR
                    case IDAtributos.PROVEEDOR:
                        {
                            // Leemos el valor del proveedor y lo almacenamos
                            string buffer = bReader.ReadString();
                            Program.Log($"-- Proveedor leido: {buffer}");
                            proveedor = buffer;
                            break;
                        }
                    // Caso para el atributo FECHA DE INGRESO
                    case IDAtributos.FECHAINGRESO:
                        {
                            // Leemos el valor de la fecha de ingreso y lo almacenamos
                            DateTime buffer = DateTime.FromBinary(bReader.ReadInt64());
                            Program.Log($"-- Fecha de Ingreso leida: {buffer}");
                            fechaIngreso = buffer;
                            break;
                        }
                    // Caso para el atributo DESCONTINUADO
                    case IDAtributos.DESCONTINUADO:
                        {
                            // Leemos el valor de descontinuado y lo almacenamos
                            bool buffer = bReader.ReadBoolean();
                            Program.Log($"-- Descontinuado leido: {buffer}");
                            descontinuado = buffer;
                            break;
                        }
                }
                // Continuamos el bucle hasta encontrar el atributo de fin
            } while (atributoLeido != IDAtributos.ATRIBUTOFIN);

            // Hacemos comprobaciones para advertir en el caso que algun parametro opcional falte
            // Advertimos si nombre es nulo o vacío
            if (string.IsNullOrEmpty(nombre))
            {
                // Mandamos un mensajito para que el usuario sepa que pasó
                MessageBox.Show("El parametro nombre no se encontró, se reemplazará por 'Desconocido'", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nombre = "Desconocido";
            }
            // Advertimos si descripcion es nulo o vacío
            /*if (string.IsNullOrEmpty(descripcion))
            {
                // Mandamos un mensajito para que el usuario sepa que pasó
                MessageBox.Show("El parametro descripcion no se encontró, se reemplazará por 'Sin descripción'", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                descripcion = "Sin descripción";
            }*/
            // Advertimos si proveedor es nulo o vacío
            if (string.IsNullOrEmpty(proveedor))
            {
                // Mandamos un mensajito para que el usuario sepa que pasó
                MessageBox.Show("El parametro proveedor no se encontró, se reemplazará por 'Desconocido'", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                proveedor = "Desconocido";
            }
            // Advertimos si fechaIngreso es nulo
            if (fechaIngreso is null)
            {
                // Mandamos un mensajito para que el usuario sepa que pasó
                MessageBox.Show("El parametro fechaIngreso no se encontró, se reemplazará por la fecha actual", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fechaIngreso = DateTime.Now;
            }

            // Creamos nuestra variable para almacenar el producto
            Producto nuevoProducto;
            // Creamos el nuevo producto con los datos
            nuevoProducto = new(
                // Si el idLeido es nulo, tirar una excepción ya que es un parametro obligatorio
                _id: idLeido ?? throw new ArgumentNullException("[requerido] El parametro ID no se encontró"),
                // Si el nombre es nulo, asignarlo a 'Desconocido' (parametro opcional)
                _nombre: nombre,
                // Si la descripcion es nulo, asignarlo a 'Sin descripción' (parametro opcional)
                _descripcion: descripcion ?? "",
                // Si el precio es nulo, tirar una excepción ya que es un parametro obligatorio
                _precio: precio ?? throw new ArgumentNullException("[requerido] El parametro Precio no se encontró"),
                // Si la cantidad en stock es nulo, tirar una excepción ya que es un parametro obligatorio
                _stock: cantidadEnStock ?? throw new ArgumentNullException("[requerido] El parametro CantidadEnStock no se encontró"),
                // Si el proveedor es nulo, asignarlo a 'Desconocido' (parametro opcional)
                _proveedor: proveedor,
                // Si la fecha de ingreso es nulo, asignarlo a la fecha actual (parametro opcional)
                _fechaIngreso: fechaIngreso ?? DateTime.Now
                );
            // Si descontinuado es nulo, asignarlo a false (parametro opcional)
            nuevoProducto.MarcarDescontinuado(descontinuado ?? false);
            // Retornamos el producto creado
            return nuevoProducto;
        }
    }
}
