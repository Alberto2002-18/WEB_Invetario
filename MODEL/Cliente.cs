using System;
using System.Collections.Generic;

namespace MODEL;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Cedula { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public int IdTipoDocumento { get; set; }

    public virtual Tipodocumento IdTipoDocumentoNavigation { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; } = new List<Venta>();
}
