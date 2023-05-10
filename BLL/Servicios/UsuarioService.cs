using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using BLL.Servicios.Contrato;
using DAL.Repositorios.Contrato;
using DTO;
using Microsoft.EntityFrameworkCore;
using MODEL;

namespace BLL.Servicios
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IGenericRepository<Usuariorole> _userRoleRepository;
        private readonly IGenericRepository<Usuario> _usuarioRepository;

        private readonly IMapper _roleMapper;
        private readonly IMapper _userMapper;

        // Constructor Usuario Role
        public UsuarioService(
            IGenericRepository<Usuariorole> usuarioRoleRepository,
            IMapper rolMapper,
            IGenericRepository<Usuario> usuarioRepository,
            IMapper userMapper)
        {
            _userRoleRepository = usuarioRoleRepository;
            _usuarioRepository = usuarioRepository;
            _roleMapper = rolMapper;
            _userMapper = userMapper;
            
        }
       
        // Implementacion de Interfaz
        // Implementacion de lista

        public async Task<List<UsuarioDTO>> Lista()
        {
            try
            {
                var queryUsuariorole = await _userRoleRepository.Consultar();
                var listaUsuariorole = queryUsuariorole.Include(Role => Role.IdRolNavigation).ToString();

                return _roleMapper.Map<List<UsuarioDTO>>(listaUsuariorole);
            }
            catch
            {
                throw;
            }
        }
        // Validar Credenciales
        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                // Validacion queryUsuariorole

                var queryUsuariorole = await _userRoleRepository.Consultar();
                var listaUsuariorole = queryUsuariorole.Include(Role => Role.IdRolNavigation).ToString();

                // Validacion queryUsuario
                var queryUsuario = await _usuarioRepository.Consultar(u =>
                u.Correo == correo &&
                u.Contrasena == clave
                );
                if( queryUsuario.FirstOrDefault() == null )
                    throw new TaskCanceledException("El usuario no existe!");

                Usuariorole devolverUsuarioRole = queryUsuariorole.Include(Role => Role.IdRolNavigation).First();

                return _roleMapper.Map<SesionDTO>( devolverUsuarioRole);

            }
            catch
            {
                throw;
            }
        }
        // Interfaz Crear
        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try
            {
                var UsuarioCreado = await _userRoleRepository.Crear(_roleMapper.Map<Usuariorole>(modelo));
                if (UsuarioCreado.IdUsuario == 0)
                    throw new TaskCanceledException("No se puede crear!");

                var query = await _userRoleRepository.Consultar(u => u.IdUsuario == UsuarioCreado.IdUsuario);

                UsuarioCreado = query.Include(Role => Role.IdRolNavigation).First();

                return _roleMapper.Map<UsuarioDTO>( UsuarioCreado);
            }
            catch
            {
                throw;
            }
        }
        // Interfaz Editar
        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try
            {
                // UsuarioRole 
                var UsuarioModelo = _roleMapper.Map<Usuariorole>(modelo);
                var UsuarioEncontrado = await _userRoleRepository.Obtener(u => u.IdUsuario == UsuarioModelo.IdUsuario);

                //Usuario
                var usuarioModelo = _userMapper.Map<Usuario>(modelo);
                var usuarioEncontrado = await _usuarioRepository.Obtener(u => u.IdUsuario == usuarioModelo.IdUsuario);


                if (UsuarioEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe!");
                usuarioEncontrado.NombreUsuario = usuarioModelo.NombreUsuario;
                usuarioEncontrado.Correo = usuarioModelo.Correo;
                UsuarioEncontrado.IdUsuarioRol = UsuarioEncontrado.IdUsuarioRol;
                usuarioEncontrado.Contrasena = usuarioModelo.Contrasena;
                usuarioEncontrado.EsActivo = usuarioModelo.EsActivo;

                //bool repuesta = await _usuarioRepository.Editar(usuarioEncontrado);
                bool rep = await _userRoleRepository.Editar(UsuarioEncontrado);

                /*if (!repuesta)
                    throw new TaskCanceledException("No se puede editar!");
                return repuesta;*/

                if (!rep)
                    throw new TaskCanceledException("No se puede editar!");
                return rep;
            }
            catch
            {
                throw;
            }
        }
        // Interfaz Eliminar
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var UsuarioEncontrado = await _userRoleRepository.Obtener(u => u.IdUsuarioRol == id);
                var usuarioEncontrado = await _usuarioRepository.Obtener(u => u.IdUsuario == id);

                if(UsuarioEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe!");

                bool repuesta = await _userRoleRepository.Eliminar(UsuarioEncontrado);

                if (!repuesta)
                    throw new TaskCanceledException("No se puede eliminar!");
                return repuesta;

                /*if (usuarioEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe!");

                bool repuesta = await _usuarioRepository.Eliminar(UsuarioEncontrado);

                if (!repuesta)
                    throw new TaskCanceledException("No se puede editar");
                return repuesta;*/

            }
            catch
            {
                throw;
            }
        }


    }
}
