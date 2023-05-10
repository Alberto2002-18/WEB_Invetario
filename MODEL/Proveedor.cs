using System;
using System.Collections.Generic;

namespace MODEL;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Cedula { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public int IdTipoDocumento { get; set; }

    public virtual ICollection<Compra> Compras { get; } = new List<Compra>();

    public virtual Tipodocumento IdTipoDocumentoNavigation { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
