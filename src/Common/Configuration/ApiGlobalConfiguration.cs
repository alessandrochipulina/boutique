using Microsoft.Extensions.Configuration;
using System;

namespace Common.Configuration
{
    public class ApiGlobalConfiguration
    {
        private readonly IConfiguration config;

        public ApiGlobalConfiguration(IConfiguration config)
        {
            this.config = config;
        }

        public string GetObjectByAbsoluteKey(string key)
        {
            return this.config.GetValue<string>(key);
        }
        public bool GetBooleanByAbsoluteKey(string key)
        {
            return this.config.GetValue<bool>(key);
        }        
        public string GetMetaEnv()
        {
            return this.config.GetValue<string>("Meta:Env");
        }

        public string GetMetaApiKey()
        {
            return this.config.GetValue<string>("Meta:ApiKey");
        }

        public string GetMetaAppName()
        {
            return this.config.GetValue<string>("Meta:AppName");
        }

        public string GetMetaLogPath()
        {
            return this.config.GetValue<string>("Meta:LogPath");
        }
        public string GetMetaLogBaseLevel()
        {
            return this.config.GetValue<string>("Meta:LogBaseLevel");
        }

        public string GetMetaLogMicrosoftLevel()
        {
            return this.config.GetValue<string>("Meta:LogMicrosoftLevel");
        }

        public IConfigurationSection GetValuesFromRootSection(string section)
        {
            return this.config.GetSection(section);
        }

        internal string GetEngine()
        {
            return this.config.GetValue<string>("Engine");
        }
    }
}
