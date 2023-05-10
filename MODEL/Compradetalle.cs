using System;
using System.Collections.Generic;

namespace MODEL;

public partial class Compradetalle
{
    public int IdCompraDetalle { get; set; }

    public int? IdCompra { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public virtual Compra? IdCompraNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
