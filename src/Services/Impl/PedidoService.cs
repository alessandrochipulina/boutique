using Api.Entities;
using Api.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Common.Attributes;

namespace Api.Services.Impl
{
    [Service(Scope="Transient")]
    public class PedidoService : IPedidoService
    {
        private readonly IConfiguration config;
        private readonly IPedidoRepository oRepository;
        private readonly ILogger<PedidoService> logger;

        public PedidoService(
            IConfiguration config, 
            IPedidoRepository oRepository,
            ILogger<PedidoService> logger)
        {
            this.config = config;
            this.oRepository = oRepository;
            this.logger = logger;
        }

        public List<PedidoResult> Listar()
        {
            List<PedidoResult> lst;

            try
            {
                lst = this.oRepository.Listar();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error en el metodo Listar ");
                lst = new List<PedidoResult>();
            }

            return lst;
        }

        public List<PedidoResult> Buscar( 
            string texto )
        {
            List<PedidoResult> lst;

            try
            {
                lst = this.oRepository.Buscar( texto );
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el metodo Buscar", ex);
            }

            return lst;
        }

        public SimpleResult AgregarProducto(
            int id_pedido,
            string sku,
            int precioventa,
            int cantidad)
        {
            SimpleResult mensaje;
            try
            {
                mensaje = this.oRepository.AgregarProducto(
                    id_pedido, sku, precioventa, cantidad);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el metodo Agregar.", ex);
            }
            return mensaje;
        }

        public SimpleResult Crear(
            string codigo_usuario)
        {
            SimpleResult mensaje;
            try
            {
                mensaje = this.oRepository.Crear(
                    codigo_usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el metodo Crear", ex);
            }
            return mensaje;
        }

        public SimpleResult Cancelar(
            int id_pedido) 
        {
            SimpleResult mensaje;
            try
            {
                mensaje = this.oRepository.Cancelar(id_pedido);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el metodo.", ex);
            }
            return mensaje;
        }

        public SimpleResult Recuperar(
            int id_pedido) 
        {
            SimpleResult mensaje;
            try
            {
                mensaje = this.oRepository.Recuperar(id_pedido);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el metodo.", ex);
            }
            return mensaje;
        }

        public SimpleResult Atender(
            int id_pedido, 
            string codigo_usuario)
        {
            SimpleResult mensaje;
            try
            {
                mensaje = this.oRepository.Atender(id_pedido, codigo_usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el metodo.", ex);
            }
            return mensaje;
        }
    }
}
