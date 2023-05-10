using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Repositorios.Contrato;
using DAL.Repositorios;

using UTILITY;
using BLL.Servicios.Contrato;
using BLL.Servicios;

namespace IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<BdinvetaryContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository, VentaRepository>();

            
            services.AddScoped<IRolService,RolService>();
            services.AddScoped<IUsuarioService,UsuarioService>();
            services.AddScoped<ICategoriaService,CategoriaService>();
            services.AddScoped<IProductoService,ProductoService>();
            services.AddScoped<IVentaService,VentaService>();
            services.AddScoped<IDashBoardService,DashBoardService>();
            services.AddScoped<IMenuService,MenuService>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

        }
    }
}
