using System;
using System.Collections.Generic;

namespace MODEL;

public partial class Negocio
{
    public int IdNegocio { get; set; }

    public string? Ruc { get; set; }

    public string? Logo { get; set; }

    public string? Direccion { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public string? Vision { get; set; }

    public string? Mision { get; set; }

    public virtual ICollection<Usuariorole> Usuarioroles { get; } = new List<Usuariorole>();
}
