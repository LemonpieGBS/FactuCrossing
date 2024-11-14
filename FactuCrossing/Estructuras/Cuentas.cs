using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public Roles Rol { get; set; }
    }
}
