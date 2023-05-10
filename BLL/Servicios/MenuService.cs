using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using BLL.Servicios.Contrato;
using DAL.Repositorios.Contrato;
using DTO;
using MODEL;

namespace BLL.Servicios
{
    public class MenuService:IMenuService
    {
        private readonly IGenericRepository<Usuario> _userRepository;
        private readonly IGenericRepository<Usuariorole> _lanserRepository;
        private readonly IGenericRepository<Menurole> _menuroleRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Usuario> userRepository, IGenericRepository<Usuariorole> lanserRepository, IGenericRepository<Menurole> menuroleRepository, IGenericRepository<Menu> menuRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _lanserRepository = lanserRepository;
            _menuroleRepository = menuroleRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> List(int IdUsuario, int IdUsuariorole)
        {
            IQueryable<Usuario> tbUsuario = await _userRepository.Consultar(u => u.IdUsuario == IdUsuario);
            IQueryable<Usuariorole> tbUsuarioRole = await _lanserRepository.Consultar(u => u.IdUsuarioRol == IdUsuariorole);
            IQueryable<Menurole> tbMenuRole = await _menuroleRepository.Consultar();
            IQueryable<Menu> tbMenu = await _menuRepository.Consultar();

            try
            {
                IQueryable<Menu> tbResultado = (from u in tbUsuarioRole
                                                join mr in tbMenuRole on u.IdRol equals mr.IdRol
                                                join m in tbMenu on mr.IdMenu equals m.IdMenu
                                                select m).AsQueryable();

                var listaMenus = tbResultado.ToList();
                return _mapper.Map<List<MenuDTO>>(listaMenus);
            }
            catch
            {
                throw;
            }

        }
    }
}
