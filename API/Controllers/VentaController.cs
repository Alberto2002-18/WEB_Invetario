using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BLL.Servicios.Contrato;
using DTO;
using API.Utilidad;
using BLL.Servicios;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }
        // Guardar Venta
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] VentaDTO venta)
        {
            var rsp = new Reponse<VentaDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _ventaService.Registrar(venta);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
        // Historial de venta 
        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string buscarPor, string? numeroVenta, string? fechaInicio, string? fechaFin)
        {
            var rsp = new Reponse<List<VentaDTO>>();
            numeroVenta = numeroVenta is null ? "" : numeroVenta;
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;

            try
            {
                rsp.status = true;
                rsp.value = await _ventaService.Historial(buscarPor, numeroVenta, fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
        // Reporte de venta
        [HttpGet]
        [Route("Reporte")]
        public async Task<IActionResult> Reporte(string? fechaInicio, string? fechaFin)
        {
            var rsp = new Reponse<List<ReporteDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _ventaService.Reporte(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
    }
}
