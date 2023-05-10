using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Context;
using DAL.Repositorios.Contrato;
using MODEL;

namespace DAL.Repositorios
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly BdinvetaryContext _bdinvetaryContext;

        public VentaRepository(BdinvetaryContext bdinvetaryContext) : base(bdinvetaryContext)
        {
            _bdinvetaryContext = bdinvetaryContext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();

            using(var trasaction = _bdinvetaryContext.Database.BeginTransaction())
            {
                try
                {
                    foreach(Ventadetalle dv in modelo.Ventadetalles)
                    {
                        Producto producto_encontrado = _bdinvetaryContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _bdinvetaryContext.Productos.Update(producto_encontrado);
                    }
                    await _bdinvetaryContext.SaveChangesAsync();

                    Tipodocumento correlativo = _bdinvetaryContext.Tipodocumentos.First();
                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _bdinvetaryContext.Tipodocumentos.Update(correlativo);
                    await _bdinvetaryContext.SaveChangesAsync();

                    int CantidadDigito = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigito));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();

                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigito, CantidadDigito);

                    modelo.NumeroDocumento = numeroVenta;

                    await _bdinvetaryContext.AddAsync(modelo);
                    await _bdinvetaryContext.SaveChangesAsync();

                    ventaGenerada = modelo;

                    trasaction.Commit();
                }
                catch
                {
                    trasaction.Rollback();
                    throw;
                }
                return ventaGenerada;
            }


        }
    }
}
