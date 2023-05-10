using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DashBoardDTO
    {
        public int TotalVenta { get; set; }
        public string? TotalIngreso { get; set; }
        public int TotalProducto { get; set; }
        public List<VentaSemanaDTO> VentasUltimaSemana { get; set; }
    }
}
