using System.ComponentModel.DataAnnotations;
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
                throw new ArgumentException("Passkey no puede ser nulo o vacío.", nameof(_passkey));
            if (string.IsNullOrEmpty(_salt))
                throw new ArgumentException("Salt no puede ser nulo o vacío.", nameof(_salt));

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
                throw new ArgumentException("Hash no puede ser nulo o vacío.", nameof(_hash));
            if (string.IsNullOrEmpty(_salt))
                throw new ArgumentException("Salt no puede ser nulo o vacío.", nameof(_salt));

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

    /// <summary>
    /// La clase donde está toda la información de las cuentas
    /// </summary>
    public class Cuenta
    {
        /// <summary>
        /// Propiedad de ID, la llave primaria, siempre bueno tener una
        /// </summary>
        [Key]
        public int Id { get; init; }

        /// <summary>
        /// Nombre de Usuario, la credencial que se usa para iniciar sesión
        /// </summary>
        public string NombreUsuario { get; private set; }

        /// <summary>
        /// El nombre del Empleado, diferente al de Usuario
        /// </summary>
        public string NombreDisplay { get; private set; }

        /// <summary>
        /// Rol en la empresa, ver el enumerador de arriba
        /// </summary>
        public Roles Rol { get; set; }

        /// <summary>
        /// Marca si la contraseña del usuario es temporal o no
        /// </summary>
        public bool ContraseñaTemporal { get; set; } = false;

        /// <summary>
        /// Marca si la cuenta está activa
        /// </summary>
        public bool Habilitada { get; set; } = true;

        /// <summary>
        /// Marca si el usuario ha iniciado sesión alguna vez
        /// </summary>
        public bool SesionIniciada { get; set; } = true;

        /// <summary>
        /// Contraseña
        /// </summary>
        public HashSalt Contraseña { get; private set; }

        /// <summary>
        /// Propiedad para llevar cuenta del tiempo de sesión
        /// </summary>
        public TiempoSesion TiempoSesion { get; private set; }

        /// <summary>
        /// Cuenta por default
        /// </summary>
        public static Cuenta CuentaDefault =
            new Cuenta(9999, "def", "def", Roles.ADMINISTRADOR, new HashSalt("1234"));

        /// <summary>
        /// Constructor principal y único
        /// </summary>
        /// <param name="_id">ID de la cuenta</param>
        /// <param name="_nombre">Nombre de usuario</param>
        /// <param name="_nombredisplay">Nombre del empleado</param>
        /// <param name="_rol">Rol en la empresa</param>
        /// <param name="_contraseña">Contraseña</param>
        /// <exception cref="ArgumentException">Lanzada si algún argumento es inválido</exception>
        public Cuenta(int _id, string _nombre, string _nombredisplay, Roles _rol, HashSalt _contraseña)
        {
            // Si alguno de los argumentos está vacío o es nulo, entonces mandamos un error
            if (_id < 0)
                throw new ArgumentException("ID no puede ser negativo.", nameof(_id));
            if (string.IsNullOrEmpty(_nombre))
                throw new ArgumentException("Nombre no puede ser nulo o vacío.", nameof(_nombre));
            if (string.IsNullOrEmpty(_nombredisplay))
                throw new ArgumentException("Nombre Display no puede ser nulo o vacío.", nameof(_nombredisplay));
            NombreUsuario = _nombre;
            NombreDisplay = _nombredisplay;
            Id = _id;
            Rol = _rol;
            Contraseña = _contraseña;
            TiempoSesion = new TiempoSesion(new Dictionary<DateTime, double>());
        }

        /// <summary>
        /// Constructor con tiempoSesion
        /// </summary>
        /// <param name="_id">ID de la cuenta</param>
        /// <param name="_nombre">Nombre de usuario</param>
        /// <param name="_nombredisplay">Nombre del empleado</param>
        /// <param name="_rol">Rol en la empresa</param>
        /// <param name="_contraseña">Contraseña</param>
        /// <param name="_tiempoSesion">Tiempo de Sesión</param>
        /// <exception cref="ArgumentException">Lanzada si algún argumento es inválido</exception>
        public Cuenta(int _id, string _nombre, string _nombredisplay, Roles _rol, HashSalt _contraseña, TiempoSesion _tiempoSesion)
            : this(_id, _nombre, _nombredisplay, _rol, _contraseña)
        {
            // Asignamos la propiedad
            TiempoSesion = _tiempoSesion;
        }

        /// <summary>
        /// Método para cambiar la contraseña
        /// </summary>
        /// <param name="_nuevaContraseña">Nueva contraseña</param>
        public void CambiarContraseña(HashSalt _nuevaContraseña)
        {
            Contraseña = _nuevaContraseña;
        }

        /// <summary>
        /// Método para comparar una contraseña con otra
        /// </summary>
        /// <param name="_comparativa">Contraseña a comparar</param>
        /// <returns>True si las contraseñas coinciden, False en caso contrario</returns>
        /// <exception cref="ArgumentException">Lanzada si el string dado está vacío o es nulo</exception>
        public bool CompararContraseña(string _comparativa)
        {
            // Mandamos un error si el string dado está vacío o es nulo.
            if (string.IsNullOrEmpty(_comparativa))
                throw new ArgumentException("La comparación no se puede dar con un string vacío.", nameof(_comparativa));
            // Ahora solo creamos un HashSalt con el salt de nuestra contraseña y el string comparativo
            HashSalt comparación = new HashSalt(_comparativa, Contraseña.Salt);
            // Si los hashes son iguales, será validado
            return Contraseña.Equals(comparación);
        }

        /// <summary>
        /// Método para añadir tiempo al usuario
        /// </summary>
        public double TiempoEnFecha(DateTime fechaBuscar)
        {
            // Obtenemos la fecha sin hora
            DateTime fechaSinHora = fechaBuscar.Date;
            // Obtenemos el valor en segundos
            double segundos = 0;
            // Tratamos de conseguir el valor (si no hay será 0)
            TiempoSesion.tiempoPorDia.TryGetValue(fechaSinHora, out segundos);
            // Retornamos el valor
            return segundos;
        }
    }
}
