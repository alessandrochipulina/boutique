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
    public class ProductoService : IProductoService
    {
        private readonly IConfiguration config;
        private readonly IProductoRepository oRepository;
        private readonly ILogger<ProductoService> logger;

        public ProductoService(
            IConfiguration config, 
            IProductoRepository oRepository,
            ILogger<ProductoService> logger)
        {
            this.config = config;
            this.oRepository = oRepository;
            this.logger = logger;
        }

        public List<ProductoResult> Listar()
        {
            List<ProductoResult> lst;

            try
            {
                lst = this.oRepository.Listar();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error en el metodo.");
                lst = new List<ProductoResult>();
            }

            return lst;
        }

        public List<ProductoResult> Buscar( 
            string texto )
        {
            List<ProductoResult> lst;

            try
            {
                lst = this.oRepository.Buscar( texto );
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el metodo.", ex);
            }

            return lst;
        }
    }
}
