using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }

        public string? Codigo { get; set; }

        public string? Nombre { get; set; }

        public int? Stock { get; set; }

        public string? Imagen { get; set; }

        public string? DescripcionCategoria { get; set; }

        public string? Precio { get; set; }

        public int? EsActivo { get; set; }

        public int IdCategoria { get; set; }

        public int IdProveedor { get; set; }
    }
}
