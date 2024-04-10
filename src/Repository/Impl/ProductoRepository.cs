
using Api.Entities;
using Common.Database.Conexion;
using Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Common.Attributes;
using Dapper;
using System.Linq;

namespace Api.Repository.Impl
{
    [Service(Scope="Transient")]
    public class ProductoRepository : IProductoRepository
    {

        private readonly IConfiguration config;
        private readonly ILogger<ProductoRepository> logger;
        private readonly DatabaseManager databaseManager;

        public ProductoRepository(
            IConfiguration configuration, 
            ILogger<ProductoRepository> logger, 
            DatabaseManager databaseManager)
        {
            this.config = configuration;
            this.logger = logger;
            this.databaseManager = databaseManager;
        }

        public List<ProductoResult> Listar()
        {
            try
            {                
                return databaseManager.LookupDatabaseConnectorById(ApiConstants.DatabaseId)
                .Query<ProductoResult>("dbo.USP_PRODUCTO_LISTAR");
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
                throw new Exception("Failed to fetch.", e);
            }
        }

        public List<ProductoResult> Buscar( 
            string texto )
        {
            var parameters = new DynamicParameters();
            parameters.Add("@texto", texto);

            try
            {
                return databaseManager.LookupDatabaseConnectorById(ApiConstants.DatabaseId).
                    Query<ProductoResult>("dbo.USP_PRODUCTO_BUSCAR", parameters);
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
                throw new Exception("Failed to fetch facultades.", e);
            }
        }
    }

}
