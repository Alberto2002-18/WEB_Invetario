using System;
using System.Collections.Generic;

namespace MODEL;

public partial class Role
{
    public int IdRol { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Menurole> Menuroles { get; } = new List<Menurole>();

    public virtual ICollection<Usuariorole> Usuarioroles { get; } = new List<Usuariorole>();
}
