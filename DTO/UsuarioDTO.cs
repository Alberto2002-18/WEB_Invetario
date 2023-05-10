using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public int IdRol { get; set; }

        public string? NombreUsuario { get; set; }

        public string? Contrasena { get; set; }

        public string? Correo { get; set; }

        public string? RolDescripcion { get; set; }

        public string? Image { get; set; }

        public bool? EsActivo { get; set; }
    }
}
