using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FactuCrossing.Estructuras;

namespace FactuCrossing.Servicios
{
    /// <summary>
    /// Clase responsable de todos los métodos de la clase <seealso cref="ProductoFacturado"/> 
    /// para manejo de archivos
    /// </summary>
    public class ServicioArchivoProductosFacturados
    {
        /// <summary>
        /// Enumerador responsable de identificar los atributos de la clase <seealso cref="ProductoFacturado"/> 
        /// a la hora de guardar y cargar
        /// </summary>
        private enum IDAtributosProductoFacturado
        {
            ATRIBUTOFIN,
            ID,
            NOMBRE,
            PROVEEDOR,
            PRECIO,
            CANTIDAD,
            DESCUENTOPORCENTAJE,
            DESCUENTONOMBRE
        }

        /// <summary> 
        /// Función para guardar una lista de productos facturados (<paramref name="productos"/>) en un archivo (<paramref name="rutaArchivo"/>) 
        /// <para>
        /// Esta función usa <seealso cref="BinaryWriter"/> para escribir una lista de productos facturados (clase <seealso cref="ProductoFacturado"/>) a un archivo binario<br/>
        /// </para>
        /// </summary>
        /// <param name="productos">Lista de productos facturados a guardar</param>
        /// <param name="bWriter">Escritor binario</param>
        public void EscribirProductosFacturados(List<ProductoFacturado> productos, BinaryWriter bWriter)
        {
            // Escribimos la cantidad de productos facturados
            bWriter.Write(productos.Count);
            // Iteramos por todos los productos facturados para guardar cada uno
            foreach (var producto in productos)
            {
                EscribirProductoFacturado(producto, bWriter);
            }
        }

        /// <summary> 
        /// Función privada para guardar un solo producto facturado (<paramref name="producto"/>) dado un escritor binario (<paramref name="bWriter"/>) 
        /// <para>
        /// Usada en la función publica <seealso cref="EscribirProductosFacturados(List{ProductoFacturado}, BinaryWriter)"/><br/>
        /// Escribe un producto facturado (clase <seealso cref="ProductoFacturado"/>) a un escritor binario
        /// </para>
        /// </summary>
        /// <param name="producto">Producto facturado a guardar</param>
        /// <param name="bWriter">Escritor binario</param>
        public void EscribirProductoFacturado(ProductoFacturado producto, BinaryWriter bWriter)
        {
            // Escribir el atributo ID
            bWriter.Write((Int32)IDAtributosProductoFacturado.ID);
            bWriter.Write(producto.IDenInventario);

            // Escribir el atributo NOMBRE
            bWriter.Write((Int32)IDAtributosProductoFacturado.NOMBRE);
            bWriter.Write(producto.Nombre);

            // Escribir el atributo PROVEEDOR
            bWriter.Write((Int32)IDAtributosProductoFacturado.PROVEEDOR);
            bWriter.Write(producto.Proveedor);

            // Escribir el atributo PRECIO
            bWriter.Write((Int32)IDAtributosProductoFacturado.PRECIO);
            bWriter.Write(producto.Precio);

            // Escribir el atributo CANTIDAD
            bWriter.Write((Int32)IDAtributosProductoFacturado.CANTIDAD);
            bWriter.Write(producto.Cantidad);

            // Escribir el atributo DESCUENTOPORCENTAJE
            bWriter.Write((Int32)IDAtributosProductoFacturado.DESCUENTOPORCENTAJE);
            bWriter.Write(producto.DescuentoPorcentaje);

            // Escribir el atributo DESCUENTONOMBRE
            bWriter.Write((Int32)IDAtributosProductoFacturado.DESCUENTONOMBRE);
            bWriter.Write(producto.DescuentoNombre);

            // Escribimos el atributo FIN para marcar el fin de la escritura
            bWriter.Write((Int32)IDAtributosProductoFacturado.ATRIBUTOFIN);
        }

        /// <summary> 
        /// Función para cargar una lista de productos facturados desde un archivo binario (<paramref name="bReader"/>) 
        /// <para>
        /// Esta función usa <seealso cref="BinaryReader"/> para cargar una lista de productos facturados (clase <seealso cref="ProductoFacturado"/>) de un archivo binario<br/>
        /// </para>
        /// </summary>
        /// <param name="bReader">Lector binario</param>
        /// <returns>Lista de productos facturados cargados</returns>
        public List<ProductoFacturado> LeerProductosFacturados(BinaryReader bReader)
        {
            // Leemos la cantidad de productos facturados
            int cantidadProductos = bReader.ReadInt32();
            // Creamos la lista de retorno
            List<ProductoFacturado> listaRetorno = new();
            // Iteramos por la cantidad de productos que vamos a cargar
            for (int i = 0; i < cantidadProductos; i++)
            {
                listaRetorno.Add(LeerProductoFacturado(bReader));
            }
            // Retornamos los productos facturados que logramos cargar
            return listaRetorno;
        }

        /// <summary> 
        /// Función privada para cargar un solo producto facturado dado un lector binario (<paramref name="bReader"/>) 
        /// <para>
        /// Usada en la función publica <seealso cref="LeerProductosFacturados(BinaryReader)"/><br/>
        /// Lee un producto facturado (clase <seealso cref="ProductoFacturado"/>) dado un lector binario
        /// </para>
        /// </summary>
        /// <param name="bReader">Lector binario</param>
        /// <returns>Producto facturado cargado del archivo</returns>
        public ProductoFacturado LeerProductoFacturado(BinaryReader bReader)
        {
            // Declaramos la variable para almacenar el atributo leído
            IDAtributosProductoFacturado atributoLeido;
            // Inicializamos las variables para almacenar los valores de los atributos del producto facturado
            int? id = null;
            string? nombre = null;
            string? proveedor = null;
            double? precio = null;
            int? cantidad = null;
            double? descuentoPorcentaje = null;
            string? descuentoNombre = null;

            // Iniciamos un bucle para leer los atributos del producto facturado hasta encontrar el atributo de fin
            do
            {
                // Leemos el siguiente atributo del archivo
                Int32 atributoBuffer = bReader.ReadInt32();
                // Verificamos si el valor leído corresponde a un atributo válido
                if (Enum.IsDefined(typeof(IDAtributosProductoFacturado), atributoBuffer))
                    atributoLeido = (IDAtributosProductoFacturado)atributoBuffer;
                else
                    throw new ArgumentOutOfRangeException($"No se reconoce el buffer {atributoBuffer} como un atributo válido");
                // Usamos un switch para manejar cada tipo de atributo
                switch (atributoLeido)
                {
                    // Caso para el atributo de fin, no hacemos nada y salimos del bucle
                    case IDAtributosProductoFacturado.ATRIBUTOFIN: { break; }
                    // Caso para el atributo ID
                    case IDAtributosProductoFacturado.ID:
                        {
                            // Leemos el valor del ID y lo almacenamos
                            int buffer = bReader.ReadInt32();
                            id = buffer;
                            break;
                        }
                    // Caso para el atributo NOMBRE
                    case IDAtributosProductoFacturado.NOMBRE:
                        {
                            // Leemos el valor del nombre y lo almacenamos
                            string buffer = bReader.ReadString();
                            nombre = buffer;
                            break;
                        }
                    // Caso para el atributo PROVEEDOR
                    case IDAtributosProductoFacturado.PROVEEDOR:
                        {
                            // Leemos el valor del proveedor y lo almacenamos
                            string buffer = bReader.ReadString();
                            proveedor = buffer;
                            break;
                        }
                    // Caso para el atributo PRECIO
                    case IDAtributosProductoFacturado.PRECIO:
                        {
                            // Leemos el valor del precio y lo almacenamos
                            double buffer = bReader.ReadDouble();
                            precio = buffer;
                            break;
                        }
                    // Caso para el atributo CANTIDAD
                    case IDAtributosProductoFacturado.CANTIDAD:
                        {
                            // Leemos el valor de la cantidad y lo almacenamos
                            int buffer = bReader.ReadInt32();
                            cantidad = buffer;
                            break;
                        }
                    // Caso para el atributo DESCUENTOPORCENTAJE
                    case IDAtributosProductoFacturado.DESCUENTOPORCENTAJE:
                        {
                            // Leemos el valor del descuento porcentaje y lo almacenamos
                            double buffer = bReader.ReadDouble();
                            descuentoPorcentaje = buffer;
                            break;
                        }
                    // Caso para el atributo DESCUENTONOMBRE
                    case IDAtributosProductoFacturado.DESCUENTONOMBRE:
                        {
                            // Leemos el valor del descuento nombre y lo almacenamos
                            string buffer = bReader.ReadString();
                            descuentoNombre = buffer;
                            break;
                        }
                }
                // Continuamos el bucle hasta encontrar el atributo de fin
            } while (atributoLeido != IDAtributosProductoFacturado.ATRIBUTOFIN);

            // Creamos nuestra variable para almacenar el producto facturado
            ProductoFacturado nuevoProductoFacturado;
            // Creamos el nuevo producto facturado con los datos
            nuevoProductoFacturado = new(
                id: id ?? throw new ArgumentNullException("[requerido] El parametro id no se encontró"),
                nombre: nombre ?? throw new ArgumentNullException("[requerido] El parametro nombre no se encontró"),
                proveedor: proveedor ?? throw new ArgumentNullException("[requerido] El parametro proveedor no se encontró"),
                precio: precio ?? throw new ArgumentNullException("[requerido] El parametro precio no se encontró"),
                cantidad: cantidad ?? throw new ArgumentNullException("[requerido] El parametro cantidad no se encontró"),
                descPorcentaje: descuentoPorcentaje ?? 0,
                descNombre: descuentoNombre ?? ""
                );

            // Retornamos el producto facturado creado
            return nuevoProductoFacturado;
        }
    }

    /// <summary>
    /// Clase responsable de todos los métodos de la clase <seealso cref="Factura"/> 
    /// para manejo de archivos
    /// </summary>
    public class ServicioArchivoFacturas
    {
        /// <summary>
        /// Sello para comprobar si un archivo es perteneciente a uno de facturas
        /// </summary>
        private const string _sello = "FACTUCROSSING - ARCHIVO DE FACTURAS";

        /// <summary>
        /// Enumerador responsable de identificar los atributos de la clase <seealso cref="Factura"/> 
        /// a la hora de guardar y cargar
        /// </summary>
        private enum IDAtributos
        {
            ATRIBUTOFIN,
            NUMFACTURA,
            NUMFACTURISTA,
            FACTURISTA,
            NOMBREFACURA,
            SUCURSAL,
            PRODUCTOSFACTURADOS,
            DESCUENTOSAPLICADOS,
            DESCUENTOGLOBAL,
            FECHAFACTURA,
            TOTAL,
            SUBTOTAL,
            DESCUENTO
        }

        /// <summary>
        /// Enumerador responsable de identificar los atributos de la clase <seealso cref="ProductoFacturado"/> 
        /// a la hora de guardar y cargar
        /// </summary>
        private enum IDAtributosProducto
        {
            ATRIBUTOFIN,
            ID,
            NOMBRE,
            PROVEEDOR,
            PRECIO,
            CANTIDAD,
            DESCUENTOPORCENTAJE,
            DESCUENTONOMBRE
        }

        /// <summary>
        /// Función para guardar una lista de facturas (<paramref name="facturas"/>) en un archivo (<paramref name="rutaArchivo"/>) 
        /// </summary>
        /// <param name="facturas">Lista de facturas a guardar</param>
        /// <param name="rutaArchivo">Ruta en donde guardar las facturas como archivo binario</param>
        public void EscribirFacturas(List<Factura> facturas, string rutaArchivo)
        {
            // Loggeamos que la función fue llamada
            Program.Log($"FUNCION GUARDAR FACTURAS LLAMADA CON RUTA: {rutaArchivo}");
            // Un try por si algo sale mal
            try
            {
                // Creamos el filestream para el archivo
                using FileStream fStream = new(rutaArchivo, FileMode.Create);
                // Creamos el escritor binario
                using BinaryWriter bWriter = new(fStream);
                // Usamos el escritor binario para 'sellar' el archivo
                bWriter.Write(_sello);
                // Loggeamos la cantidad de facturas que se guardarán
                Program.Log($"Facturas Totales: {facturas.Count}");
                // Usamos el escritor binario para guardar la cantidad de facturas
                // Esto será útil para cargarlas después
                bWriter.Write(facturas.Count);
                // Iteramos por todas las facturas para guardar cada una
                for (int i = 0; i < facturas.Count; i++)
                {
                    // Loggeamos el progreso del programa
                    Program.Log($"------------- Escribiendo factura #{i + 1} / {facturas.Count} -------------");
                    // Escribimos la factura
                    EscribirFactura(facturas[i], bWriter);
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
        /// Función privada para guardar una sola factura (<paramref name="factura"/>) dado un escritor binario (<paramref name="bWriter"/>) 
        /// </summary>
        /// <param name="factura">Factura a guardar</param>
        /// <param name="bWriter">Escritor binario</param>
        private void EscribirFactura(Factura factura, BinaryWriter bWriter)
        {
            ServicioArchivoProductosFacturados sapf = new();
            ServicioArchivoDescuentos sad = new();

            // Escribir el atributo NUMFACTURA
            bWriter.Write((Int32)IDAtributos.NUMFACTURA);
            bWriter.Write(factura.NumFactura);
            Program.Log($"-- Atributo Escrito [NUMFACTURA]: {factura.NumFactura}");

            // Escribir el atributo NUMFACTURISTA
            bWriter.Write((Int32)IDAtributos.NUMFACTURISTA);
            bWriter.Write(factura.NumFacturista);
            Program.Log($"-- Atributo Escrito [NUMFACTURISTA]: {factura.NumFacturista}");

            // Escribir el atributo FACTURISTA
            bWriter.Write((Int32)IDAtributos.FACTURISTA);
            bWriter.Write(factura.Facturista);
            Program.Log($"-- Atributo Escrito [FACTURISTA]: {factura.Facturista}");

            // Escribir el atributo NOMBREFACURA
            bWriter.Write((Int32)IDAtributos.NOMBREFACURA);
            bWriter.Write(factura.NombreFactura);
            Program.Log($"-- Atributo Escrito [NOMBREFACURA]: {factura.NombreFactura}");

            // Escribir el atributo SUCURSAL
            bWriter.Write((Int32)IDAtributos.SUCURSAL);
            bWriter.Write(factura.Sucursal);
            Program.Log($"-- Atributo Escrito [SUCURSAL]: {factura.Sucursal}");

            // Escribir el atributo PRODUCTOSFACTURADOS
            bWriter.Write((Int32)IDAtributos.PRODUCTOSFACTURADOS);
            bWriter.Write(factura.ProductosFacturados.Count);
            foreach (var producto in factura.ProductosFacturados)
            {
                sapf.EscribirProductoFacturado(producto, bWriter);
            }
            Program.Log($"-- Productos Facturados Escritos: {factura.ProductosFacturados.Count}");

            // Escribir el atributo DESCUENTOSAPLICADOS
            bWriter.Write((Int32)IDAtributos.DESCUENTOSAPLICADOS);
            bWriter.Write(factura.DescuentosAplicados.Count);
            foreach (var descuento in factura.DescuentosAplicados)
            {
                sad.EscribirDescuento(descuento, bWriter);
            }
            Program.Log($"-- Descuentos Aplicados Escritos: {factura.DescuentosAplicados.Count}");

            // Escribir el atributo DESCUENTOGLOBAL
            if (factura.DescuentoGlobal != null)
            {
                bWriter.Write((Int32)IDAtributos.DESCUENTOGLOBAL);
                
                sad.EscribirDescuento(factura.DescuentoGlobal, bWriter);
                Program.Log($"-- Descuento Global Escrito: {factura.DescuentoGlobal.Nombre}");
            }

            // Escribir el atributo FECHAFACTURA
            bWriter.Write((Int32)IDAtributos.FECHAFACTURA);
            bWriter.Write(factura.FechaFactura.ToBinary());
            Program.Log($"-- Atributo Escrito [FECHAFACTURA]: {factura.FechaFactura}");

            // Escribir el atributo TOTAL
            bWriter.Write((Int32)IDAtributos.TOTAL);
            bWriter.Write(factura.Total);
            Program.Log($"-- Atributo Escrito [TOTAL]: {factura.Total}");

            // Escribir el atributo SUBTOTAL
            bWriter.Write((Int32)IDAtributos.SUBTOTAL);
            bWriter.Write(factura.Subtotal);
            Program.Log($"-- Atributo Escrito [SUBTOTAL]: {factura.Subtotal}");

            // Escribir el atributo DESCUENTO
            bWriter.Write((Int32)IDAtributos.DESCUENTO);
            bWriter.Write(factura.Descuento);
            Program.Log($"-- Atributo Escrito [DESCUENTO]: {factura.Descuento}");

            // Escribimos el atributo FIN para marcar el fin de la escritura
            bWriter.Write((Int32)IDAtributos.ATRIBUTOFIN);
            Program.Log($"-- Atributo Escrito [ATRIBUTOFIN]");
        }

        /// <summary> 
        /// Función para cargar una lista de facturas en un archivo (<paramref name="rutaArchivo"/>) 
        /// <para>
        /// Esta función usa <seealso cref="BinaryReader"/> para cargar una lista de facturas (clase <seealso cref="Factura"/>) de un archivo binario<br/>
        /// Se carga un sello (<seealso cref="_sello"/>) para comprobar que es un archivo perteneciente a esta función<br/><br/>
        /// Para guardar las facturas a un archivo, usa <seealso cref="EscribirFacturas(List{Factura}, string)"/>
        /// </para>
        /// </summary>
        /// <param name="rutaArchivo">Ruta en donde se encuentra el archivo binario para cargar las facturas</param>
        /// <returns>Lista de facturas cargadas</returns>
        public List<Factura> LeerFacturas(string rutaArchivo)
        {
            // Loggeamos que la función fue llamada
            Program.Log($"FUNCION CARGARFACTURAS LLAMADA CON RUTA: {rutaArchivo}");
            // Creamos la lista de retorno
            List<Factura> listaRetorno = new();
            // Un try por si algo sale mal
            try
            {
                // Creamos el filestream para el archivo
                using FileStream fStream = new(rutaArchivo, FileMode.Open, FileAccess.Read);
                // Creamos el lector binario
                using BinaryReader bReader = new(fStream);
                // Si el sello no está presente tiramos una excepción (esto lo atrapa el catch de todas maneras)
                if (bReader.ReadString() != _sello)
                    throw new FileFormatException("El archivo cargado no pertenece a un archivo de facturas FactuCrossing");
                // Leemos la cantidad de facturas a cargar
                int cantidadFacturas = bReader.ReadInt32();
                // Loggeamos la cantidad de facturas
                Program.Log($"- Cantidad de Facturas a leer: {cantidadFacturas}");
                // Loggeamos los bytes en el archivo
                Program.Log($"- Bytes en el archivo: {fStream.Length}");
                // Iteramos por la cantidad de facturas que vamos a cargar
                for (int i = 0; i < cantidadFacturas; i++)
                {
                    // Loggeamos nuestro progreso
                    Program.Log($"------------- Leyendo factura #{i + 1} / {cantidadFacturas} -------------");
                    // Leemos la factura y la agregamos a nuestra lista de retorno
                    listaRetorno.Add(LeerFactura(bReader));
                    // Loggeamos el éxito
                    Program.Log($"-- Factura creada y añadida con éxito --");
                }
            }
            // Un catch para avisar del error al usuario
            catch (Exception ex)
            {
                // Mostramos el mensaje al usuario para que sepa que salió mal
                MessageBox.Show($"Error cargando el archivo: {ex}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Retornamos las facturas que logramos cargar
            return listaRetorno;
        }

        /// <summary> 
        /// Función privada para cargar una sola factura dada un lector binario (<paramref name="bReader"/>) 
        /// <para>
        /// Usada en la función publica <seealso cref="LeerFacturas(string)"/><br/>
        /// Lee una factura (clase <seealso cref="Factura"/>) dado un lector binario
        /// </para>
        /// </summary>
        /// <param name="bReader">Lector binario</param>
        /// <returns>Factura cargada del archivo</returns>
        private Factura LeerFactura(BinaryReader bReader)
        {
            // Declaramos la variable para almacenar el atributo leído
            IDAtributos atributoLeido;
            // Inicializamos las variables para almacenar los valores de los atributos de la factura
            int? numFactura = null;
            int? numFacturista = null;
            string? facturista = null;
            string? nombreFactura = null;
            string? sucursal = null;
            List<ProductoFacturado>? productosFacturados = null;
            List<Descuento>? descuentosAplicados = null;
            Descuento? descuentoGlobal = null;
            DateTime? fechaFactura = null;
            double? total = null;
            double? subtotal = null;
            double? descuento = null;

            // Iniciamos un bucle para leer los atributos de la factura hasta encontrar el atributo de fin
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
                    // Caso para el atributo NUMFACTURA
                    case IDAtributos.NUMFACTURA:
                        {
                            // Leemos el valor del número de factura y lo almacenamos
                            int buffer = bReader.ReadInt32();
                            Program.Log($"-- Número de Factura leído: {buffer}");
                            numFactura = buffer;
                            break;
                        }
                    // Caso para el atributo NUMFACTURISTA
                    case IDAtributos.NUMFACTURISTA:
                        {
                            // Leemos el valor del número del facturista y lo almacenamos
                            int buffer = bReader.ReadInt32();
                            Program.Log($"-- Número del Facturista leído: {buffer}");
                            numFacturista = buffer;
                            break;
                        }
                    // Caso para el atributo FACTURISTA
                    case IDAtributos.FACTURISTA:
                        {
                            // Leemos el valor del facturista y lo almacenamos
                            string buffer = bReader.ReadString();
                            Program.Log($"-- Facturista leído: {buffer}");
                            facturista = buffer;
                            break;
                        }
                    // Caso para el atributo NOMBREFACURA
                    case IDAtributos.NOMBREFACURA:
                        {
                            // Leemos el valor del nombre de la factura y lo almacenamos
                            string buffer = bReader.ReadString();
                            Program.Log($"-- Nombre de la Factura leído: {buffer}");
                            nombreFactura = buffer;
                            break;
                        }
                    // Caso para el atributo SUCURSAL
                    case IDAtributos.SUCURSAL:
                        {
                            // Leemos el valor de la sucursal y lo almacenamos
                            string buffer = bReader.ReadString();
                            Program.Log($"-- Sucursal leída: {buffer}");
                            sucursal = buffer;
                            break;
                        }
                    // Caso para el atributo PRODUCTOSFACTURADOS
                    case IDAtributos.PRODUCTOSFACTURADOS:
                        {
                            // Leemos la lista de productos facturados
                            int count = bReader.ReadInt32();
                            productosFacturados = new List<ProductoFacturado>();
                            ServicioArchivoProductosFacturados sapf = new();
                            for (int i = 0; i < count; i++)
                            {
                                productosFacturados.Add(sapf.LeerProductoFacturado(bReader));
                            }
                            Program.Log($"-- Productos Facturados leídos: {productosFacturados.Count}");
                            break;
                        }
                    // Caso para el atributo DESCUENTOSAPLICADOS
                    case IDAtributos.DESCUENTOSAPLICADOS:
                        {
                            // Leemos la lista de descuentos aplicados
                            int count = bReader.ReadInt32();
                            descuentosAplicados = new List<Descuento>();
                            ServicioArchivoDescuentos sad = new();
                            for (int i = 0; i < count; i++)
                            {
                                descuentosAplicados.Add(sad.LeerDescuento(bReader));
                            }
                            Program.Log($"-- Descuentos Aplicados leídos: {descuentosAplicados.Count}");
                            break;
                        }
                    // Caso para el atributo DESCUENTOGLOBAL
                    case IDAtributos.DESCUENTOGLOBAL:
                        {
                            // Leemos el descuento global
                            ServicioArchivoDescuentos sad = new();
                            descuentoGlobal = sad.LeerDescuento(bReader);
                            Program.Log($"-- Descuento Global leído: {descuentoGlobal.Nombre}");
                            break;
                        }
                    // Caso para el atributo FECHAFACTURA
                    case IDAtributos.FECHAFACTURA:
                        {
                            // Leemos el valor de la fecha de la factura y lo almacenamos
                            DateTime buffer = DateTime.FromBinary(bReader.ReadInt64());
                            Program.Log($"-- Fecha de Factura leída: {buffer}");
                            fechaFactura = buffer;
                            break;
                        }
                    // Caso para el atributo TOTAL
                    case IDAtributos.TOTAL:
                        {
                            // Leemos el valor del total y lo almacenamos
                            double buffer = bReader.ReadDouble();
                            Program.Log($"-- Total leído: {buffer}");
                            total = buffer;
                            break;
                        }
                    // Caso para el atributo SUBTOTAL
                    case IDAtributos.SUBTOTAL:
                        {
                            // Leemos el valor del subtotal y lo almacenamos
                            double buffer = bReader.ReadDouble();
                            Program.Log($"-- Subtotal leído: {buffer}");
                            subtotal = buffer;
                            break;
                        }
                    // Caso para el atributo DESCUENTO
                    case IDAtributos.DESCUENTO:
                        {
                            // Leemos el valor del descuento y lo almacenamos
                            double buffer = bReader.ReadDouble();
                            Program.Log($"-- Descuento leído: {buffer}");
                            descuento = buffer;
                            break;
                        }
                }
                // Continuamos el bucle hasta encontrar el atributo de fin
            } while (atributoLeido != IDAtributos.ATRIBUTOFIN);

            // Creamos nuestra variable para almacenar la factura
            Factura nuevaFactura;
            // Creamos la nueva factura con los datos
            nuevaFactura = new(
                // Si el numFactura es nulo, tirar una excepción ya que es un parametro obligatorio
                numFactura: numFactura ?? throw new ArgumentNullException("[requerido] El parametro numFactura no se encontró"),
                // Si el numFacturista es nulo, tirar una excepción ya que es un parametro obligatorio
                numFacturista: numFacturista ?? throw new ArgumentNullException("[requerido] El parametro numFacturista no se encontró"),
                // Si el facturista es nulo, asignarlo a 'Desconocido' (parametro opcional)
                nombreFacturista: facturista ?? "Desconocido",
                // Si el nombreFactura es nulo, asignarlo a 'Desconocido' (parametro opcional)
                nombreFactura: nombreFactura ?? "Desconocido",
                // Si la sucursal es nulo, asignarlo a 'Desconocida' (parametro opcional)
                sucursal: sucursal ?? "Desconocida",
                // Si los productosFacturados es nulo, tirar una excepción ya que es un parametro obligatorio
                productosFacturados: productosFacturados ?? throw new ArgumentNullException("[requerido] El parametro productosFacturados no se encontró"),
                // Si los descuentosAplicados es nulo, asignarlo a una lista vacía (parametro opcional)
                descuentosAplicados: descuentosAplicados ?? new List<Descuento>(),
                // Si el descuentoGlobal es nulo, asignarlo a null (parametro opcional)
                descuentoGlobal: descuentoGlobal,
                // Si la fechaFactura es nulo, asignarlo a la fecha actual (parametro opcional)
                fechaFactura: fechaFactura ?? DateTime.Now
                );

            // Asignamos los valores calculados
            nuevaFactura.CalcularTotales();

            // Retornamos la factura creada
            return nuevaFactura;
        }
    }
}