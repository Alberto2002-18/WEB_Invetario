using System;
using System.Collections.Generic;

namespace MODEL;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public int? Stock { get; set; }

    public string? Imagen { get; set; }

    public decimal Precio { get; set; }

    public bool? EsActivo { get; set; }

    public int IdCategoria { get; set; }

    public int IdProveedor { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Compradetalle> Compradetalles { get; } = new List<Compradetalle>();

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    public virtual ICollection<Ventadetalle> Ventadetalles { get; } = new List<Ventadetalle>();
}
