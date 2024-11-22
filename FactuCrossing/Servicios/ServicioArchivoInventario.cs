using FactuCrossing.Estructuras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactuCrossing.Servicios
{
    public class ServicioArchivoProductos
    {
        private const string _sello = "FACTUCROSSING - ARCHIVO DE PRODUCTOS";

        public void GuardarProductos(List<Producto> productos, string rutaArchivo)
        {
            FileStream fStream;
            BinaryWriter bWriter;

            try
            {
                fStream = new(rutaArchivo, FileMode.Create);
                bWriter = new(fStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error guardando el archivo: {ex}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bWriter.Write(_sello);
            bWriter.Write(productos.Count);

            foreach (Producto producto in productos)
            {
                bWriter.Write(producto.Id);
                bWriter.Write(producto.Nombre);
                bWriter.Write(producto.Descripcion);
                bWriter.Write(producto.Precio);
                bWriter.Write(producto.CantidadEnStock);
                bWriter.Write(producto.Proveedor);
                bWriter.Write(producto.FechaIngreso.ToBinary());
            }

            bWriter.Close();
            fStream.Close();
        }

        public List<Producto> CargarProductos(string rutaArchivo)
        {
            List<Producto> listaRetorno = new();

            FileStream fStream;
            BinaryReader bReader;
            try
            {
                fStream = new(rutaArchivo, FileMode.Open, FileAccess.Read);
                bReader = new(fStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando el archivo: {ex}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Producto>();
            }

            try
            {
                if (bReader.ReadString() != _sello)
                {
                    MessageBox.Show("El archivo cargado no pertenece a un archivo de productos FactuCrossing", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new List<Producto>();
                }

                int cantidadProductos = bReader.ReadInt32();

                List<Producto> productosLeidos = new();
                for (int i = 0; i < cantidadProductos; i++)
                {
                    int id = bReader.ReadInt32();
                    string nombre = bReader.ReadString();
                    string descripcion = bReader.ReadString();
                    decimal precio = bReader.ReadDecimal();
                    int cantidadEnStock = bReader.ReadInt32();
                    string proveedor = bReader.ReadString();
                    DateTime fechaIngreso = DateTime.FromBinary(bReader.ReadInt64());

                    Producto nuevoProducto = new(
                        _id: id,
                        _nombre: nombre,
                        _descripcion: descripcion,
                        _precio: precio,
                        _stock: cantidadEnStock,
                        _proveedor: proveedor,
                        _fechaIngreso: fechaIngreso
                    );

                    productosLeidos.Add(nuevoProducto);
                }

                listaRetorno = productosLeidos;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando el archivo: {ex}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                bReader.Close();
                fStream.Close();
            }

            return listaRetorno;
        }
    }
}
