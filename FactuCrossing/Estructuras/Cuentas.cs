using System.Security.Cryptography;
using System.Text;

namespace FactuCrossing.Estructuras
{
    // Enumerador de los roles, creo que debería haber una mejor manera de hacer
    // esto pero bueeee
    public enum Roles
    {
        FACTURISTA,
        GESTORDEINVENTARIO,
        ANALISTA,
        ADMINISTRADOR,
        GERENTE
    }

    // Clase para el HashSalt, la cosa que contiene y verifica contraseñas
    // Si no saben de esto solo busquenle SHA256 password hashing
    public class HashSalt
    {
        // Las propiedades de Hash y Salt, NO EDITABLES!
        public byte[] Hash { get; init; }
        public string Salt { get; init; }

        // Método para generar salt
        private static string GenerarSalt()
        {
            // Generamos el arreglo de bytes (en este caso 64)
            byte[] saltBytes = new byte[64];
            
            // Creamos el objeto rng para crear un arreglo de bytes aleatorio
            RandomNumberGenerator rng = RandomNumberGenerator.Create();

            // Aqui lo hacemos
            rng.GetBytes(saltBytes);

            // Convertimos la serie de bytes a Base64 para guardarlo como string
            // 3 bytes son 4 caracteres en Base64
            return Convert.ToBase64String(saltBytes);
        }

        // Se genera el hash con el passkey y salt recibidos (perfecto para cuando se valida una contraseña)
        public HashSalt(string _passkey, string _salt)
        {
            // Si alguno de los dos argumentos esta vacío o es nulo, entonces mandamos un error
            if (string.IsNullOrEmpty(_passkey))
                throw new ArgumentException("Passkey cannot be null or empty.", nameof(_passkey));
            if (string.IsNullOrEmpty(_salt))
                throw new ArgumentException("Salt cannot be null or empty.", nameof(_salt));

            // Generamos los bytes del string del salt + passkey
            byte[] inputBytes = Encoding.UTF8.GetBytes(_salt + _passkey);

            // Pasamos los bytes por el algoritmo de hashing
            byte[] nuevoHashBytes = SHA256.HashData(inputBytes);

            // Aplicamos las dos propiedades
            Salt = _salt;
            Hash = nuevoHashBytes;
        }

        // Se genera un nuevo hash y salt aleatorios (perfecto para cuando se crea una contraseña)
        // El constructor simplemente llama al constructor inicial con un salt generado aleatoriamente
        public HashSalt(string _passkey)
            : this(_passkey, GenerarSalt())
        {}

        public HashSalt(byte[] _hash, string _salt)
        {
            // Si alguno de los dos argumentos esta vacío o es nulo, entonces mandamos un error
            if (_hash == null || _hash.Length == 0)
                throw new ArgumentException("Hash cannot be null or empty.", nameof(_hash));
            if (string.IsNullOrEmpty(_salt))
                throw new ArgumentException("Salt cannot be null or empty.", nameof(_salt));

            // Aplicamos las dos propiedades
            Hash = _hash;
            Salt = _salt;
        }

        // Metodo ToString por si acaso tengo que hacer debugging
        public override string ToString()
        {
            return $"Salt: {Salt}, Hash: {Convert.ToBase64String(Hash)}";
        }

        // Método Equals para comparar dos hashes
        public bool Equals(HashSalt? other)
        {
            // Si es nulo, entonces retornamos falso
            if (other is null) return false;

            // Si no, y los dos hashes son iguales, retornamos verdadero
            return Hash.SequenceEqual(other.Hash);
        }

        // Método Equals verdadero, que pasa un objecto casteado como HashSalt
        // (si es nulo quiere decir que no es un HashSalt)
        public override bool Equals(object? obj)
        {
            return Equals(obj as HashSalt);
        }

        // Tienes que editar esta cosa si haces override a Equals, a mi no me pregunten
        // el como y el porque
        public override int GetHashCode()
        {
            return Hash.Aggregate(0, (current, b) => current * 31 + b);
        }
    }

    // La clase donde esta toda la información de las cuentas :3
    public class Cuenta
    {
        // Propiedad de ID, la llave primaria, siempre bueno tener una
        public int Id { get; }

        // Nombre de Usuario, la credencial que se usa para iniciar sesión
        public string NombreUsuario { get; private set; }

        // El nombre del Empleado, diferente al de Usuario
        public string NombreDisplay { get; private set; }

        // Rol en la empresa, ver el enumerador de arriba
        public Roles Rol { get; set; }

        // Marca si la contraseña del usuario es temporal o no
        public bool ContraseñaTemporal { get; set; } = false;

        // Marca si la cuenta está activa
        public bool Habilitada { get; set; } = true;

        // Marca si el usuario ha iniciado sesión alguna vez
        public bool SesionIniciada { get; set; } = true;

        // Contraseña :]
        public HashSalt Contraseña { get; private set; }

        // Constructor principal y único (por ahora)
        public Cuenta(int _id, string _nombre, string _nombredisplay, Roles _rol, HashSalt _contraseña)
        {
            NombreUsuario = _nombre;
            NombreDisplay = _nombredisplay;
            Id = _id;
            Rol = _rol;
            Contraseña = _contraseña;
        }

        // Método para cambiar la contraseña
        public void CambiarContraseña(HashSalt _nuevaContraseña)
        {
            Contraseña = _nuevaContraseña;
        }

        // Método para comparar una contraseña con otra
        public bool CompararContraseña(string _comparativa)
        {
            // Ahora solo creamos un HashSalt con el salt de nuestra contraseña y el string comparativo
            HashSalt comparación = new HashSalt(_comparativa, Contraseña.Salt);

            // Si los hashes son iguales, será validado
            return Contraseña.Equals(comparación);
        }
    }
}
