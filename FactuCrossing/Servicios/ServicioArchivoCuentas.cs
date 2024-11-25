using FactuCrossing.Estructuras;
using System.Diagnostics;

namespace FactuCrossing.Servicios
{
    public class ServicioArchivoCuentas
    {
        private const string _sello = "FACTUCROSSING - ARCHIVO DE CUENTAS";

        public void GuardarCuentas(List<Cuenta> cuentas, string rutaArchivo)
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
            bWriter.Write(cuentas.Count);

            foreach(Cuenta cuenta in cuentas)
            {
                bWriter.Write((Int32)cuenta.Id);
                bWriter.Write(cuenta.NombreDisplay);
                bWriter.Write(cuenta.NombreUsuario);
                bWriter.Write(cuenta.Salt);
                bWriter.Write((UInt16)cuenta.Rol);
                bWriter.Write(cuenta.Habilitada);

                bWriter.Write((Int32)cuenta.Hash.Length);
                foreach(Byte b in cuenta.Hash) {
                    bWriter.Write(b);
                }
            }

            bWriter.Close();
            fStream.Close();
        }

        public List<Cuenta> CargarCuentas(string rutaArchivo)
        {
            Debug.WriteLine($"FUNCION CARGARCUENTAS LLAMADA CON RUTA: {rutaArchivo}");
            List<Cuenta> listaRetorno = new();

            FileStream fStream;
            BinaryReader bReader;
            try
            {
                fStream = new(rutaArchivo, FileMode.Open, FileAccess.Read);
                bReader = new(fStream);

                Debug.WriteLine($"- fStream y bReader creados exitosamente");
            } catch (Exception ex)
            {
                MessageBox.Show($"Error cargando el archivo: {ex}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Cuenta>() { };
            }

            try
            {
                if (bReader.ReadString() != _sello)
                {
                    MessageBox.Show("El archivo cargado no pertenece a un archivo de cuentas FactuCrossing", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new List<Cuenta>() { };
                }

                int cantidadCuentas = bReader.ReadInt32();
                Debug.WriteLine($"- Cantidad de Cuentas a leer: {cantidadCuentas}");
                Debug.WriteLine($"- Bytes en el archivo: {fStream.Length}");

                List<Cuenta> cuentasLeidas = new();
                for (int i = 0; i < cantidadCuentas; i++)
                {
                    Cuenta nuevaCuenta;

                    Debug.WriteLine($"------------- Leyendo cuenta #{i} -------------");

                    int idLeido = bReader.ReadInt32();
                    Debug.WriteLine($"-- id leido: {idLeido}");

                    string nombreDisplay = bReader.ReadString();
                    Debug.WriteLine($"-- Nombre leido: {nombreDisplay}");

                    string nombreUsuario = bReader.ReadString();
                    Debug.WriteLine($"-- Nombre de Usuario leido: {nombreUsuario}");

                    string saltLeido = bReader.ReadString();
                    Debug.WriteLine($"-- Salt Leido: {saltLeido}");

                    Roles rolLeido = (Roles)bReader.ReadUInt16();
                    Debug.WriteLine($"-- Rol leido: {rolLeido}");

                    bool habilitada = bReader.ReadBoolean();
                    Debug.WriteLine($"-- Cuenta Habilitada? {habilitada}");

                    Debug.WriteLine($"== LEYENDO HASH ==");
                    int cantBytesHash = bReader.ReadInt32();

                    Debug.WriteLine($"-- Cantidad de bytes del hash: {cantBytesHash}");

                    byte[] hashLeido = new byte[cantBytesHash];

                    for(int j = 0; j < cantBytesHash; j++)
                    {
                        byte b = bReader.ReadByte();
                        hashLeido[j] = b;

                        Debug.WriteLine($"Byte #{j + 1} leido, ");
                    }

                    nuevaCuenta = new(
                        _hash: hashLeido,
                        _salt: saltLeido,
                        _id: idLeido,
                        _nombredisplay: nombreDisplay,
                        _nombre: nombreUsuario,
                        _rol: rolLeido
                        );

                    nuevaCuenta.EstablecerHabilitada(habilitada);
                    cuentasLeidas.Add(nuevaCuenta);

                    Debug.WriteLine($"-- Cuenta creada y añadida con éxito --");
                }

                listaRetorno = cuentasLeidas;

            } catch(Exception ex)
            {
                MessageBox.Show($"Error cargando el archivo: {ex}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally
            {
                bReader.Close();
                fStream.Close();
            }

            return listaRetorno;
        }
    }
}
