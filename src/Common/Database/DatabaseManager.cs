using Common.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Common.Database.SQLServer;

namespace Common.Database.Conexion
{
    public class DatabaseManager
    {

        private readonly Dictionary<string, IDatabaseConnector> connectors = new Dictionary<string, IDatabaseConnector>();
        private readonly ILogger<DatabaseManager> logger;
        private readonly ApiGlobalConfiguration apiGlobalConfiguration;

        public DatabaseManager(ApiGlobalConfiguration apiGlobalConfiguration, 
            ILogger<DatabaseManager> logger)
        {
            this.logger = logger;
            this.apiGlobalConfiguration = apiGlobalConfiguration;
        }

        public IDatabaseConnector LookupDatabaseConnectorById(string databaseId)
        {
            return connectors.GetValueOrDefault(databaseId);
        }

        public void Configure()
        {
            string engine = apiGlobalConfiguration.GetEngine();

            if (engine == "ORACLE")
            {
                    //
            }
            else if (engine == "SQLSERVER")
            {
                    this.logger.LogInformation($"Database SQLSERVER: Starting configurations");                    
                    SQLServerDatabaseConnector databaseConnector = new();
                    databaseConnector.Configure(apiGlobalConfiguration);
                    this.connectors.Add("SQLSERVER", databaseConnector);                    
                    this.logger.LogInformation($"Database SQLSERVER: Was configured successfully");
            }
            else if (engine == "Mysql")
            {
                    //
            } else
            {
                    this.logger.LogInformation($"Unsuported database engine {engine}");
            }
        }
    }
}
