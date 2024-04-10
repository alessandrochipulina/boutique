using Microsoft.Extensions.Configuration;

namespace Configuration
{
    public class ApiConfiguration
    {
        private readonly IConfiguration config;

        public ApiConfiguration(IConfiguration config)
        {
            this.config = config;
        }

        public object GetObjecByAbsoluteKey(string key)
        {
            return this.config.GetValue<object>(key);
        }

        //public string GetSapCpiBaseUrl()
        //{
        //    return this.config.GetValue<string>("SapCpi:BaseUrl");
        //}

    }
}
