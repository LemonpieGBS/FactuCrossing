using FactuCrossing.Estructuras;
using FactuCrossing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FactuCrossing.Servicios
{
    /// <summary>
    /// Clase responsable de todos los métodos de la clase <seealso cref="Descuento"/> 
    /// para manejo de archivos
    /// </summary>
    public class ServicioArchivoDescuentos
    {
        /// <summary>
        /// Sello para comprobar si un archivo es perteneciente a uno de descuentos
        /// </summary>
        private const string _sello = "FACTUCROSSING - ARCHIVO DE DESCUENTOS";

        /// <summary>
        /// Enumerador responsable de identificar los atributos de la clase <seealso cref="Descuento"/> 
        /// a la hora de guardar y cargar
        /// </summary>
        private enum IDAtributos
        {
            ATRIBUTOFIN,
            ID,
            NOMBRE,
            PORCENTAJE,
            FECHAINICIO,
            FECHAFIN,
            PRODUCTOAPLICABLE
        }

        /// <summary> 
        /// Función para guardar una lista de descuentos (<paramref name="descuentos"/>) en un archivo (<paramref name="rutaArchivo"/>) 
        /// <para>
        /// Esta función usa <seealso cref="BinaryWriter"/> para escribir una lista de descuentos (clase <seealso cref="Descuento"/>) a un archivo binario<br/>
        /// Se guarda un sello (<seealso cref="_sello"/>) para a la hora de cargar un archivo comprobar que es un archivo perteneciente a esta función<br/><br/>
        /// Para cargar los descuentos de un archivo, usa <seealso cref="LeerDescuentos(string)"/>
        /// </para>
        /// </summary>
        /// <param name="descuentos">Lista de descuentos a guardar</param>
        /// <param name="rutaArchivo">Ruta en donde guardar los descuentos como archivo binario</param>
        public void EscribirDescuentos(List<Descuento> descuentos, string rutaArchivo)
        {
            // Loggeamos que la función fue llamada
            Program.Log($"FUNCION GUARDAR DESCUENTOS LLAMADA CON RUTA: {rutaArchivo}");
            // Un try por si algo sale mal
            try
            {
                // Creamos el filestream para el archivo
                using FileStream fStream = new(rutaArchivo, FileMode.Create);
                // Creamos el escritor binario
                using BinaryWriter bWriter = new(fStream);
                // Usamos el escritor binario para 'sellar' el archivo
                bWriter.Write(_sello);
                // Loggeamos la cantidad de descuentos que se guardarán
                Program.Log($"Descuentos Totales: {descuentos.Count}");
                // Usamos el escritor binario para guardar la cantidad de descuentos
                // Esto será útil para cargarlos después
                bWriter.Write(descuentos.Count);
                // Iteramos por todos los descuentos para guardar cada uno
                for (int i = 0; i < descuentos.Count; i++)
                {
                    // Loggeamos el progreso del programa
                    Program.Log($"------------- Escribiendo descuento #{i + 1} / {descuentos.Count} -------------");
                    // Escribimos el descuento
                    EscribirDescuento(descuentos[i], bWriter);
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
        /// Función privada para guardar un solo descuento (<paramref name="descuento"/>) dado un escritor binario (<paramref name="bWriter"/>) 
        /// <para>
        /// Usada en la función publica <seealso cref="EscribirDescuentos(List{Descuento}, string)"/><br/>
        /// Escribe un descuento (clase <seealso cref="Descuento"/>) a un escritor binario
        /// </para>
        /// </summary>
        /// <param name="descuento">Descuento a guardar</param>
        /// <param name="bWriter">Escritor binario</param>
        private void EscribirDescuento(Descuento descuento, BinaryWriter bWriter)
        {
            // Escribir el atributo ID
            bWriter.Write((Int32)IDAtributos.ID);
            bWriter.Write((Int32)descuento.Id);
            Program.Log($"-- Atributo Escrito [ID]: {descuento.Id}");

            // Escribir el atributo NOMBRE
            bWriter.Write((Int32)IDAtributos.NOMBRE);
            bWriter.Write(descuento.Nombre);
            Program.Log($"-- Atributo Escrito [NOMBRE]: {descuento.Nombre}");

            // Escribir el atributo PORCENTAJE
            bWriter.Write((Int32)IDAtributos.PORCENTAJE);
            bWriter.Write(descuento.Porcentaje);
            Program.Log($"-- Atributo Escrito [PORCENTAJE]: {descuento.Porcentaje}");

            // Escribir el atributo FECHA INICIO
            bWriter.Write((Int32)IDAtributos.FECHAINICIO);
            bWriter.Write(descuento.FechaInicio.ToBinary());
            Program.Log($"-- Atributo Escrito [FECHAINICIO]: {descuento.FechaInicio}");

            // Escribir el atributo FECHA FIN
            bWriter.Write((Int32)IDAtributos.FECHAFIN);
            bWriter.Write(descuento.FechaFin.ToBinary());
            Program.Log($"-- Atributo Escrito [FECHAFIN]: {descuento.FechaFin}");

            // Escribir el atributo PRODUCTO APLICABLE
            bWriter.Write((Int32)IDAtributos.PRODUCTOAPLICABLE);
            bWriter.Write((Int32)descuento.ProductoAplicable);
            Program.Log($"-- Atributo Escrito [PRODUCTOAPLICABLE]: {descuento.ProductoAplicable}");

            // Escribimos el atributo FIN para marcar el fin de la escritura
            bWriter.Write((Int32)IDAtributos.ATRIBUTOFIN);
            Program.Log($"-- Atributo Escrito [ATRIBUTOFIN]");
        }

        /// <summary> 
        /// Función para cargar una lista de descuentos en un archivo (<paramref name="rutaArchivo"/>) 
        /// <para>
        /// Esta función usa <seealso cref="BinaryReader"/> para cargar una lista de descuentos (clase <seealso cref="Descuento"/>) de un archivo binario<br/>
        /// Se carga un sello (<seealso cref="_sello"/>) para comprobar que es un archivo perteneciente a esta función<br/><br/>
        /// Para guardar los descuentos a un archivo, usa <seealso cref="EscribirDescuentos(List{Descuento}, string)"/>
        /// </para>
        /// </summary>
        /// <param name="rutaArchivo">Ruta en donde se encuentra el archivo binario para cargar los descuentos</param>
        /// <returns>Lista de descuentos cargados</returns>
        public List<Descuento> LeerDescuentos(string rutaArchivo)
        {
            // Loggeamos que la función fue llamada
            Program.Log($"FUNCION CARGARDESCUENTOS LLAMADA CON RUTA: {rutaArchivo}");
            // Creamos la lista de retorno
            List<Descuento> listaRetorno = new();
            // Un try por si algo sale mal
            try
            {
                // Creamos el filestream para el archivo
                using FileStream fStream = new(rutaArchivo, FileMode.Open, FileAccess.Read);
                // Creamos el lector binario
                using BinaryReader bReader = new(fStream);
                // Si el sello no está presente tiramos una excepción (esto lo atrapa el catch de todas maneras)
                if (bReader.ReadString() != _sello)
                    throw new FileFormatException("El archivo cargado no pertenece a un archivo de descuentos FactuCrossing");
                // Leemos la cantidad de descuentos a cargar
                int cantidadDescuentos = bReader.ReadInt32();
                // Loggeamos la cantidad de descuentos
                Program.Log($"- Cantidad de Descuentos a leer: {cantidadDescuentos}");
                // Loggeamos los bytes en el archivo
                Program.Log($"- Bytes en el archivo: {fStream.Length}");
                // Iteramos por la cantidad de descuentos que vamos a cargar
                for (int i = 0; i < cantidadDescuentos; i++)
                {
                    // Loggeamos nuestro progreso
                    Program.Log($"------------- Leyendo descuento #{i + 1} / {cantidadDescuentos} -------------");
                    // Leemos el descuento y lo agregamos a nuestra lista de retorno
                    listaRetorno.Add(LeerDescuento(bReader));
                    // Loggeamos el éxito
                    Program.Log($"-- Descuento creado y añadido con éxito --");
                }
            }
            // Un catch para avisar del error al usuario
            catch (Exception ex)
            {
                // Mostramos el mensaje al usuario para que sepa que salió mal
                MessageBox.Show($"Error cargando el archivo: {ex}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Retornamos los descuentos que logramos cargar
            return listaRetorno;
        }

        /// <summary> 
        /// Función privada para cargar un solo descuento dado un lector binario (<paramref name="bReader"/>) 
        /// <para>
        /// Usada en la función publica <seealso cref="LeerDescuentos(string)"/><br/>
        /// Lee un descuento (clase <seealso cref="Descuento"/>) dado un lector binario
        /// </para>
        /// </summary>
        /// <param name="bReader">Lector binario</param>
        /// <returns>Descuento cargado del archivo</returns>
        private Descuento LeerDescuento(BinaryReader bReader)
        {
            // Declaramos la variable para almacenar el atributo leído
            IDAtributos atributoLeido;
            // Inicializamos las variables para almacenar los valores de los atributos del descuento
            int? idLeido = null;
            string? nombreLeido = null;
            double? porcentajeLeido = null;
            DateTime? fechaInicioLeido = null;
            DateTime? fechaFinLeido = null;
            int? productoAplicableLeido = null;
            // Iniciamos un bucle para leer los atributos del descuento hasta encontrar el atributo de fin
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
                            Program.Log($"-- ID leido: {buffer}");
                            idLeido = buffer;
                            break;
                        }
                    // Caso para el atributo NOMBRE
                    case IDAtributos.NOMBRE:
                        {
                            // Leemos el valor del nombre y lo almacenamos
                            string buffer = bReader.ReadString();
                            Program.Log($"-- Nombre leido: {buffer}");
                            nombreLeido = buffer;
                            break;
                        }
                    // Caso para el atributo PORCENTAJE
                    case IDAtributos.PORCENTAJE:
                        {
                            // Leemos el valor del porcentaje y lo almacenamos
                            double buffer = bReader.ReadDouble();
                            Program.Log($"-- Porcentaje leido: {buffer}");
                            porcentajeLeido = buffer;
                            break;
                        }
                    // Caso para el atributo FECHA INICIO
                    case IDAtributos.FECHAINICIO:
                        {
                            // Leemos el valor de la fecha de inicio y lo almacenamos
                            DateTime buffer = DateTime.FromBinary(bReader.ReadInt64());
                            Program.Log($"-- Fecha Inicio leida: {buffer}");
                            fechaInicioLeido = buffer;
                            break;
                        }
                    // Caso para el atributo FECHA FIN
                    case IDAtributos.FECHAFIN:
                        {
                            // Leemos el valor de la fecha de fin y lo almacenamos
                            DateTime buffer = DateTime.FromBinary(bReader.ReadInt64());
                            Program.Log($"-- Fecha Fin leida: {buffer}");
                            fechaFinLeido = buffer;
                            break;
                        }
                    // Caso para el atributo PRODUCTO APLICABLE
                    case IDAtributos.PRODUCTOAPLICABLE:
                        {
                            // Leemos el valor del producto aplicable y lo almacenamos
                            Int32 buffer = bReader.ReadInt32();
                            Program.Log($"-- Producto Aplicable leido: {buffer}");
                            productoAplicableLeido = buffer;
                            break;
                        }
                }
                // Continuamos el bucle hasta encontrar el atributo de fin
            } while (atributoLeido != IDAtributos.ATRIBUTOFIN);

            // Creamos nuestra variable para almacenar el descuento
            Descuento nuevoDescuento;
            // Creamos el nuevo descuento con los datos
            nuevoDescuento = new(
                // Si el idLeido es nulo, tirar una excepción ya que es un parametro obligatorio
                id: idLeido ?? throw new ArgumentNullException("[requerido] El parametro id no se encontró"),
                // Si el nombre es nulo, tirar una excepción ya que es un parametro obligatorio
                nombre: nombreLeido ?? throw new ArgumentNullException("[requerido] El parametro nombre no se encontró"),
                // Si el porcentaje es nulo, tirar una excepción ya que es un parametro obligatorio
                porcentaje: porcentajeLeido ?? throw new ArgumentNullException("[requerido] El parametro porcentaje no se encontró"),
                // Si la fecha de inicio es nula, tirar una excepción ya que es un parametro obligatorio
                fechaInicio: fechaInicioLeido ?? throw new ArgumentNullException("[requerido] El parametro fechaInicio no se encontró"),
                // Si la fecha de fin es nula, tirar una excepción ya que es un parametro obligatorio
                fechaFin: fechaFinLeido ?? throw new ArgumentNullException("[requerido] El parametro fechaFin no se encontró"),
                // Si el producto aplicable es nulo, tirar una excepción ya que es un parametro obligatorio
                productoAplicable: productoAplicableLeido ?? throw new ArgumentNullException("[requerido] El parametro productoAplicable no se encontró")
                );
            // Retornamos el descuento creado
            return nuevoDescuento;
        }
    }
}