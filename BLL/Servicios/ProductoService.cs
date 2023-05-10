﻿using System;
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
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }
        // Metodo de lista producto
        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var queryProducto = await _productoRepository.Consultar();
                var listaProducto = queryProducto.Include(cat => cat.IdCategoriaNavigation).ToList();

                return _mapper.Map<List<ProductoDTO>>(listaProducto.ToList());
            }
            catch
            {
                throw;
            }
        }
        // Metodo crear producto
        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var productoCreado = await _productoRepository.Crear(_mapper.Map<Producto>(modelo));

                if(productoCreado.IdProducto == 0)
                    throw new TaskCanceledException("No se puede crear!");
                return _mapper.Map<ProductoDTO>(productoCreado);
            }
            catch
            {
                throw;
            }

        }
        // Metodo Editar producto
        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var productoModelo = _mapper.Map<Producto>(modelo);
                var productoEncontrado = await _productoRepository.Obtener(u =>
                    u.IdProducto == productoModelo.IdProducto);

                if (productoEncontrado == null)
                    throw new TaskCanceledException("El producto no existe");

                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.EsActivo = productoModelo.EsActivo;

                bool repuesta = await _productoRepository.Editar(productoEncontrado);

                if (!repuesta)
                    throw new TaskCanceledException("No se puede editar!");
                return repuesta;
            }
            catch
            {
                throw;
            }

        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var productoEncontrado = await _productoRepository.Obtener(p => p.IdProducto == id);

                if(productoEncontrado == null )
                    throw new TaskCanceledException("El producto no existe!");

                bool repuesta = await _productoRepository.Eliminar(productoEncontrado);


                if (!repuesta)
                    throw new TaskCanceledException("No se puede eliminar!");
                return repuesta;


            }
            catch
            {
                throw;
            }

        }


    }
}
