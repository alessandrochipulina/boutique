
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
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly IConfiguration config;
        private readonly ILogger<UsuarioRepository> logger;
        private readonly DatabaseManager databaseManager;

        public UsuarioRepository(
            IConfiguration configuration, 
            ILogger<UsuarioRepository> logger, 
            DatabaseManager databaseManager)
        {
            this.config = configuration;
            this.logger = logger;
            this.databaseManager = databaseManager;
        }

        public List<UsuarioResult> Listar()
        {
            try
            {                
                return databaseManager.LookupDatabaseConnectorById(ApiConstants.DatabaseId)
                .Query<UsuarioResult>("dbo.USP_USUARIO_LISTAR");
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
                throw new Exception("Failed to fetch.", e);
            }
        }

        public List<UsuarioResult> Buscar( 
            string texto )
        {
            var parameters = new DynamicParameters();
            parameters.Add("@texto", texto);

            try
            {
                return databaseManager.LookupDatabaseConnectorById(ApiConstants.DatabaseId).
                    Query<UsuarioResult>("dbo.USP_USUARIO_BUSCAR", parameters);
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
                throw new Exception("Failed to fetch.", e);
            }
        }

        public List<UsuarioResult> Login(string codigo, string pwd)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@codigo", codigo);
            parameters.Add("@pwd", pwd);

            try
            {
                return databaseManager.LookupDatabaseConnectorById(ApiConstants.DatabaseId).
                    Query<UsuarioResult>("dbo.USP_USUARIO_LOGIN", parameters);
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
                throw new Exception("Failed to fetch.", e);
            }
        }
    }

}
