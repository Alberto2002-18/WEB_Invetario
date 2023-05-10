using System;
using System.Collections.Generic;

namespace MODEL;

public partial class Compra
{
    public int IdCompra { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdUsuario { get; set; }

    public decimal? Impuesto { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Compradetalle> Compradetalles { get; } = new List<Compradetalle>();

    public virtual Proveedor? IdProveedorNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
