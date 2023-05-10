using System;
using System.Collections.Generic;

namespace MODEL;

public partial class Usuariorole
{
    public int IdUsuarioRol { get; set; }

    public int? IdRol { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdNegocio { get; set; }

    public virtual Negocio? IdNegocioNavigation { get; set; }

    public virtual Role? IdRolNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
