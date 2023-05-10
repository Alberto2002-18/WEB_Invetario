using System;
using System.Collections.Generic;

namespace MODEL;

public partial class Venta
{
    public int IdVenta { get; set; }

    public int? IdCliente { get; set; }

    public int? IdUsuario { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? TipoPago { get; set; }

    public decimal? Impuesto { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Ventadetalle> Ventadetalles { get; } = new List<Ventadetalle>();
}
