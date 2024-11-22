using FactuCrossing.Estructuras;

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
                if(!cuenta.Temporal)
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
            }

            bWriter.Close();
            fStream.Close();
        }

        public List<Cuenta> CargarCuentas(string rutaArchivo)
        {
            List<Cuenta> listaRetorno = new();

            FileStream fStream;
            BinaryReader bReader;
            try
            {
                fStream = new(rutaArchivo, FileMode.Open, FileAccess.Read);
                bReader = new(fStream);
            } catch(Exception ex)
            {
                MessageBox.Show($"Error cargando el archivo: {ex}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return [];
            }

            try
            {
                if (bReader.ReadString() != _sello)
                {
                    MessageBox.Show("El archivo cargado no pertenece a un archivo de cuentas FactuCrossing", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return [];
                }

                int cantidadCuentas = bReader.ReadInt32();

                List<Cuenta> cuentasLeidas = new();
                for (int i = 0; i < cantidadCuentas; i++)
                {
                    Cuenta nuevaCuenta;

                    int idLeido = bReader.ReadInt32();
                    string nombreDisplay = bReader.ReadString();
                    string nombreUsuario = bReader.ReadString();
                    string saltLeido = bReader.ReadString();
                    Roles rolLeido = (Roles)bReader.ReadUInt16();
                    bool habilitada = bReader.ReadBoolean();

                    int cantBytesHash = bReader.ReadInt32();
                    byte[] hashLeido = new byte[cantBytesHash];

                    for(int j = 0; j < cantBytesHash; j++)
                    {
                        byte b = bReader.ReadByte();
                        hashLeido[j] = b;
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
