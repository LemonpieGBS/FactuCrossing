using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace FactuCrossing.Estructuras
{
    public enum Roles
    {
        FACTURISTA,
        GESTORDEINVENTARIO,
        ANALISTA,
        ADMINISTRADOR,
        GERENTE
    }

    public class Cuenta
    {
        public int Id { get; }
        public string NombreUsuario { get; private set; }
        public string NombreDisplay { get; private set; }
        public Roles Rol { get; private set; }
        public bool Temporal { get; private set; }
        public bool Habilitada { get; private set; } = true;

        private byte[] _propiedad_interna_hash;
        private string _propiedad_interna_salt;

        public byte[] Hash
        {
            get { return _propiedad_interna_hash; }
        }
        public string Salt
        {
            get { return _propiedad_interna_salt; }
        }

        public Cuenta(int _id, string _nombre, string _contrasena, Roles _rol)
        {
            NombreUsuario = _nombre;
            NombreDisplay = _nombre;
            Id = _id;
            Rol = _rol;
            Temporal = false;

            EstablecerContrasena(_contrasena);
        }

        public Cuenta(int _id, string _nombre, string _nombredisplay, string _contrasena, Roles _rol)
        {
            NombreUsuario = _nombre;
            NombreDisplay = _nombredisplay;
            Id = _id;
            Rol = _rol;
            Temporal = false;

            EstablecerContrasena(_contrasena);
        }

        public Cuenta(int _id, string _nombre, byte[] _hash, string _salt, Roles _rol)
        {
            NombreUsuario = _nombre;
            NombreDisplay = _nombre;
            Id = _id;
            Rol = _rol;
            Temporal = false;
            _propiedad_interna_hash = _hash;
            _propiedad_interna_salt = _salt;
        }

        public Cuenta(int _id, string _nombre, string _nombredisplay, byte[] _hash, string _salt, Roles _rol)
        {
            NombreUsuario = _nombre;
            NombreDisplay = _nombredisplay;
            Id = _id;
            Rol = _rol;
            Temporal = false;
            _propiedad_interna_hash = _hash;
            _propiedad_interna_salt = _salt;
        }

        public void EstablecerHabilitada(bool _temp) { Habilitada = _temp; }
        public void EstablecerTemporal(bool _temp) { Temporal = _temp; }

        public bool ValidarContrasena(string _input)
        {
            byte[] arregloBytesInput = Encoding.UTF8.GetBytes(Salt + _input);
            byte[] hashBytes = SHA256.HashData(arregloBytesInput);

            return (hashBytes.SequenceEqual(Hash));
        }

        [MemberNotNull(nameof(_propiedad_interna_hash))] [MemberNotNull(nameof(_propiedad_interna_salt))]
        public void EstablecerContrasena(string _input)
        {
            byte[] saltBytes = new byte[64];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            string saltCreado = Convert.ToBase64String(saltBytes);

            byte[] inputBytes = Encoding.UTF8.GetBytes(saltCreado + _input);
            byte[] nuevoHashBytes = SHA256.HashData(inputBytes);

            _propiedad_interna_salt = saltCreado;
            _propiedad_interna_hash = nuevoHashBytes;
        }
    }
}
