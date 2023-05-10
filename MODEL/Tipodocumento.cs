using System;
using System.Collections.Generic;

namespace MODEL;

public partial class Tipodocumento
{
    public int IdTipoDocumento { get; set; }

    public string? Nombre { get; set; }

    public int UltimoNumero { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Cliente> Clientes { get; } = new List<Cliente>();

    public virtual ICollection<Proveedor> Proveedors { get; } = new List<Proveedor>();
}
