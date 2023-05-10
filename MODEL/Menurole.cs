using System;
using System.Collections.Generic;

namespace MODEL;

public partial class Menurole
{
    public int IdMenuRole { get; set; }

    public int? IdRol { get; set; }

    public int? IdMenu { get; set; }

    public virtual Menu? IdMenuNavigation { get; set; }

    public virtual Role? IdRolNavigation { get; set; }
}
