using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace Common.Database.SQLServer
{
    public class SQLServerDynamicParameters : SqlMapper.IDynamicParameters
    {
        private readonly DynamicParameters dynamicParameters = new DynamicParameters();
        private readonly List<SqlParameter> parameters = new List<SqlParameter>();

        public void Add(string name, SqlDbType dbType, ParameterDirection direction, object value = null, int? size = null)
        {
            SqlParameter parameter;
            if (size.HasValue)
            {
                parameter = new SqlParameter(name, value);
                parameter.SqlDbType = dbType;
                parameter.Direction = direction;
                parameter.Size = size.Value;
            }
            else
            {
                parameter = new SqlParameter(name, value);
                parameter.SqlDbType = dbType;
                parameter.Direction = direction;
            }

            parameters.Add(parameter);
        }

        public void Add(string name, SqlDbType dbType, ParameterDirection direction)
        {
            var parameter = new SqlParameter(name, dbType);
            parameter.Direction = direction;
            parameters.Add(parameter);
        }

        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            ((SqlMapper.IDynamicParameters)dynamicParameters).AddParameters(command, identity);

            var cmd = command as SqlCommand;

            if (cmd != null)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }
        }

        public object Get(string name)
        {
            object obj;

            var par = parameters.FirstOrDefault(p => p.ParameterName == name);

            obj = par.Value;

            return obj;
        }
    }
}
