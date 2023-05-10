using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using BLL.Servicios.Contrato;
using DAL.Repositorios.Contrato;
using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using MODEL;

namespace BLL.Servicios
{
    public class VentaService:IVentaService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IGenericRepository<Ventadetalle> _ventaDetalleRepository;
        private readonly IMapper _mapper;

        public VentaService(IVentaRepository ventaRepository, IGenericRepository<Ventadetalle> ventaDetalleRepository, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _ventaDetalleRepository = ventaDetalleRepository;
            _mapper = mapper;
        }
        // Implementar Interfaces
        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                var ventaGenerada = await _ventaRepository.Registrar(_mapper.Map<Venta>(modelo));

                if(ventaGenerada.IdVenta == 0)
                    throw new TaskCanceledException("No se puede crear!");
                return _mapper.Map<VentaDTO>(ventaGenerada);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> query = await _ventaRepository.Consultar();
            var ListaResultado = new List<Venta>();
            
            try
            {
                if(buscarPor == "fecha")
                {
                    DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-ES"));
                    DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-ES"));

                    ListaResultado = await query.Where(v =>
                        v.FechaRegistro.Value.Date >= fech_Inicio.Date &&
                        v.FechaRegistro.Value.Date <= fech_Fin.Date
                    ).Include(dv => dv.Ventadetalles)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToListAsync();
                }
                else
                {
                    ListaResultado = await query.Where(v => v.NumeroDocumento == numeroVenta
                        ).Include(dv => dv.Ventadetalles)
                        .ThenInclude(p => p.IdProductoNavigation)
                        .ToListAsync();

                }
            }
            catch
            {
                throw;
            }
            return _mapper.Map<List<VentaDTO>>(ListaResultado);
        }

        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
        {
            IQueryable<Ventadetalle> query = await _ventaDetalleRepository.Consultar();
            var ListaResultado = new List<Ventadetalle>();

            try
            {
                DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-ES"));
                DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-ES"));

                ListaResultado = await query
                    .Include(p => p.IdProductoNavigation)
                    .Include(v => v.IdVentaNavigation)
                    .Where(dv =>
                        dv.IdVentaNavigation.FechaRegistro.Value.Date >= fech_Inicio.Date &&
                        dv.IdVentaNavigation.FechaRegistro.Value.Date <= fech_Fin.Date
                    ).ToListAsync();
            }
            catch
            {
                throw;
            }
            return _mapper.Map<List<ReporteDTO>>(ListaResultado);
        }
    }
}
