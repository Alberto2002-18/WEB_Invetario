using System;
using System.Collections.Generic;

namespace MODEL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Contrasena { get; set; }

    public string? Correo { get; set; }

    public string? Image { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Compra> Compras { get; } = new List<Compra>();

    public virtual ICollection<Usuariorole> Usuarioroles { get; } = new List<Usuariorole>();

    public virtual ICollection<Venta> Venta { get; } = new List<Venta>();
}
