using Api.Common.Error;
using Api.Repository;
using Api.Repository.Impl;
using Api.Services;
using Api.Services.Impl;
using Common.Attributes;
using Common.Attributes.HttpMiddleware;
using Common.Configuration;
using Common.Database.Conexion;
using Common.Logging;
using Common.Middleware;
using Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly LoggerUtil loggerUtil = new LoggerUtil();

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ValidateRequiredVariables();

            services.AddTransient<ApiGlobalConfiguration>();
            services.AddTransient<ApiConfiguration>();

            ConfigureLogService(services);

            services.AddSingleton<DatabaseManager>();
            
            services.AddTransient<HttpMiddlewareLogConfigurer>();
            services.AddTransient<HttpMiddlewareAuthConfigurer>();

            services.AddControllers();

            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types) 
            {
                var customServiceAttributes = type.GetCustomAttributes(typeof(Service), true).FirstOrDefault() as Service;

                if(customServiceAttributes!=null){
                    foreach (var iType in type.GetInterfaces())
                    {
                        if(customServiceAttributes.Scope=="Transient"){
                            services.AddTransient(iType, type);
                        }else if(customServiceAttributes.Scope=="Singleton"){
                            services.AddSingleton(iType, type);
                        }                            
                        Log.Information($"Added {iType} as {customServiceAttributes.Scope}");
                    }
                }                  
            }

            ConfigureSwaggerService(services);

            //Codigo
            services.AddAuthentication(o => {
                        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddCookie(cfg => cfg.SlidingExpiration = true)
                    .AddJwtBearer(cfg => 
                        {
                            cfg.Configuration =
                                new Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfiguration();
                            cfg.RequireHttpsMetadata = false;
                            cfg.SaveToken = true;
                            cfg.Audience = this.Configuration["TOKENS_ISSUER"];
                            cfg.Authority = this.Configuration["TOKENS_ISSUER"];
                            cfg.TokenValidationParameters = new TokenValidationParameters()
                            {
                                ValidIssuer = this.Configuration["TOKENS_ISSUER"],
                                ValidAudience = this.Configuration["TOKENS_ISSUER"],
                                IssuerSigningKey = new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(
                                        this.Configuration["TOKENS_KEY"]
                                    )
                                )
                            };
                        }
                    );

            Log.Information("services were configured successfully");
        }

        public void ValidateRequiredVariables() {
            if (this.Configuration["META_APP_NAME"] == null || this.Configuration["META_APP_NAME"] == "")
            {
                throw new Exception("META_APP_NAME is required");
            }

            if (this.Configuration["META_LOG_PATH"] == null || this.Configuration["META_LOG_PATH"] == "")
            {
                throw new Exception("META_LOG_PATH is required");
            }

            if (this.Configuration["META_ENV"] == null || this.Configuration["META_ENV"] == "")
            {
                throw new Exception("META_ENV is required");
            }

            if (this.Configuration["META_API_KEY"] == null || this.Configuration["META_API_KEY"] == "")
            {
                throw new Exception("META_API_KEY is required");
            }
        }
            

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.ihostingenvironment?view=aspnetcore-5.0
        public void Configure(
            IApplicationBuilder app, IWebHostEnvironment env, 
            ApiGlobalConfiguration apiGlobalConfiguration, 
            DatabaseManager databaseManager, 
            ApiConfiguration apiConfiguration)
        {
            databaseManager.Configure();

            ConfigureDeveloperOptions(app);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // will order middlewares based on attributes on the middleware classes
            app.UseMiddlewares<Startup>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Log.Information("application was started successfully");
        }

        private void ConfigureLogService(IServiceCollection services)
        {
            Serilog.Debugging.SelfLog.Enable(Console.Error);

            LoggingLevelSwitch baseSourceLevelSwitch = new LoggingLevelSwitch(loggerUtil.getLevelFromString(this.Configuration["META_LOG_BASE_LEVEL"], LogEventLevel.Information));
            LoggingLevelSwitch microsoftSourceLevelSwitch = new LoggingLevelSwitch(loggerUtil.getLevelFromString(this.Configuration["META_LOG_MICROSOFT_LEVEL"], LogEventLevel.Warning));

            CustomLoggingLevelSwitchers customLoggingLevelSwitchers = new CustomLoggingLevelSwitchers();
            customLoggingLevelSwitchers.baseSourceLevelSwitch = baseSourceLevelSwitch;
            customLoggingLevelSwitchers.microsoftSourceLevelSwitch = microsoftSourceLevelSwitch;

            services.AddSingleton(customLoggingLevelSwitchers);

            var logPath = this.Configuration["META_LOG_PATH"];
            
            Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " Information   Log path is: " + logPath);

            //TODO: Should we put RequestPath in file log ?
            LoggerConfiguration logConfiguration = new LoggerConfiguration()
                        .MinimumLevel.ControlledBy(baseSourceLevelSwitch)
                        .MinimumLevel.Override("Microsoft", microsoftSourceLevelSwitch)
                        .Enrich.FromLogContext()
                        .WriteTo.Console(outputTemplate:
                            "{Timestamp:dd-MM-yyyy HH:mm:ss} {Level} {RequestIdentifier} {AppIdentifier} {ConsumerIdentifier} {SubjectIdentifier} {SourceContext} {Message} {Exception}{NewLine}")
                        .WriteTo.File(path: $@"{logPath}",
                        outputTemplate:
                            "{Timestamp:dd-MM-yyyy HH:mm:ss} {Level} {RequestIdentifier} {AppIdentifier} {ConsumerIdentifier} {SubjectIdentifier} {SourceContext} {Message} {Exception}{NewLine}",
                              rollingInterval: RollingInterval.Day);           

            Log.Logger = logConfiguration.CreateLogger();
            LogContext.PushProperty("AppIdentifier", String.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("META_APP_NAME")) ? "unkown-app" : System.Environment.GetEnvironmentVariable("META_APP_NAME"));
        }

        private void ConfigureSwaggerService(IServiceCollection services)
        {
            if (this.Configuration["ASPNETCORE_ENVIRONMENT"] != null && this.Configuration["ASPNETCORE_ENVIRONMENT"] == "Development")
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = String.IsNullOrEmpty(this.Configuration["META_APP_NAME"]) ? "Boutique" : this.Configuration["META_APP_NAME"],
                    });
                });
            }
        }

        private void ConfigureDeveloperOptions(IApplicationBuilder app)
        {
            if (!String.IsNullOrEmpty(this.Configuration["ASPNETCORE_ENVIRONMENT"]) && this.Configuration["ASPNETCORE_ENVIRONMENT"] == "Development")
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api.WebApi.xml");
                });
                Log.Information("swagger will be ready to use at: ip:port/swagger/index.html or domain.com/swagger/index.html");
            }
        }

        public void RegisterServices(IServiceCollection services) {
            services.AddTransient<IProductoService, ProductoService>();
            services.AddTransient<IProductoRepository, ProductoRepository>();
            services.AddTransient<IPedidoService, PedidoService>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
