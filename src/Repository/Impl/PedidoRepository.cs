
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
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IConfiguration config;
        private readonly ILogger<PedidoRepository> logger;
        private readonly DatabaseManager databaseManager;

        public PedidoRepository(
            IConfiguration configuration, 
            ILogger<PedidoRepository> logger, 
            DatabaseManager databaseManager)
        {
            this.config = configuration;
            this.logger = logger;
            this.databaseManager = databaseManager;
        }

        public List<PedidoResult> Listar()
        {
            try
            {                
                return databaseManager.LookupDatabaseConnectorById(ApiConstants.DatabaseId)
                    .Query<PedidoResult>("dbo.USP_PEDIDO_LISTAR");
            }
            catch (Exception e)
            {
                throw new Exception("Failed to fetch.", e);
            }
        }

        public List<PedidoResult> Buscar( 
            string texto )
        {
            var parameters = new DynamicParameters();
            parameters.Add("@texto", texto);

            try
            {
                return databaseManager.LookupDatabaseConnectorById(ApiConstants.DatabaseId).
                    Query<PedidoResult>("dbo.USP_PEDIDO_BUSCAR", parameters);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to fetch.", e);
            }
        }

        public SimpleResult AgregarProducto(
            int id_pedido, 
            string sku, 
            int precioventa, 
            int cantidad)
        {
            List<SimpleResult> m;
            SimpleResult mensaje = new()
            {
                result = 0
            };

            var parameters = new DynamicParameters();
            parameters.Add("@id_pedido", id_pedido);
            parameters.Add("@sku", sku);
            parameters.Add("@precioventa", precioventa);
            parameters.Add("@cantidad", cantidad);

            try
            {
                m = databaseManager.LookupDatabaseConnectorById(ApiConstants.DatabaseId).
                    Query<SimpleResult>("dbo.USP_PEDIDO_AGREGAR", parameters);
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
                throw new Exception("Failed to update.", e);
            }
            if (m.Count >= 1) return m.FirstOrDefault();
            return mensaje;
        }

        public SimpleResult Crear(
            string codigo_usuario)
        {
            List<SimpleResult> m;
            SimpleResult mensaje = new()
            {
                result = 0
            };

            var parameters = new DynamicParameters();
            parameters.Add("@codigo_usuario", codigo_usuario);

            try
            {
                m = databaseManager.LookupDatabaseConnectorById(ApiConstants.DatabaseId).
                    Query<SimpleResult>("dbo.USP_PEDIDO_CREAR", parameters);
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
                throw new Exception("Failed to create.", e);
            }
            if (m.Count >= 1) return m.FirstOrDefault();
            return mensaje;
        }

        public SimpleResult Cancelar(int id_pedido)
        {
            List<SimpleResult> m;
            SimpleResult mensaje = new()
            {
                result = 0
            };

            var parameters = new DynamicParameters();
            parameters.Add("@id_pedido", id_pedido);

            try
            {
                m = databaseManager.LookupDatabaseConnectorById(ApiConstants.DatabaseId).
                    Query<SimpleResult>("dbo.USP_PEDIDO_CANCELAR", parameters);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to cancel.", e);
            }

            if (m.Count >= 1) return m.FirstOrDefault();
            return mensaje;
        }

        public SimpleResult Recuperar(int id_pedido)
        {
            List<SimpleResult> m;
            SimpleResult mensaje = new()
            {
                result = 0
            };

            var parameters = new DynamicParameters();
            parameters.Add("@id_pedido", id_pedido);

            try
            {
                m = databaseManager.LookupDatabaseConnectorById(ApiConstants.DatabaseId).
                    Query<SimpleResult>("dbo.USP_PEDIDO_RECUPERAR", parameters);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to recover.", e);
            }

            if (m.Count >= 1) return m.FirstOrDefault();
            return mensaje;
        }

        public SimpleResult Atender(
            int id_pedido,
            string codigo_usuario)
        {
            List<SimpleResult> m;
            SimpleResult mensaje = new()
            {
                result = 0
            };

            var parameters = new DynamicParameters();
            parameters.Add("@id_pedido", id_pedido);
            parameters.Add("@codigo_usuario", codigo_usuario);

            try
            {
                m = databaseManager.LookupDatabaseConnectorById(ApiConstants.DatabaseId).
                    Query<SimpleResult>("dbo.USP_PEDIDO_ATENDER", parameters);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to modify.", e);
            }

            if (m.Count >= 1) return m.FirstOrDefault();
            return mensaje;
        }
    }

}
