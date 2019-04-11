using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using Microsoft.Extensions.DependencyInjection;
using URLAnalyzer.Foundation.Constants;
using URLAnalyzer.Foundation.DependencyInjection;
using URLAnalyzer.Foundation.ExceptionHandling;

namespace URLAnalyzer.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            ConfigureCORS(config);

            var services = new ServiceCollection();
            ConfigureMSExtensionsDI(services);
            var provider = services.BuildServiceProvider();
            config.Services.Replace(typeof(IHttpControllerActivator), new ServiceProviderControllerActivator(provider));

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            config.Services.Add(typeof(IExceptionLogger), new ExceptionManagerApi());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void ConfigureMSExtensionsDI(IServiceCollection services)
        {
            // Foundation instances
            services.AddTransient<URLAnalyzer.Foundation.Clients.IClientService, URLAnalyzer.Foundation.Clients.WebContentClientService>();
            services.AddTransient<URLAnalyzer.Foundation.Configuration.IConfigurationSettings, URLAnalyzer.Foundation.Configuration.WebConfigurationSettings>();

            // Feature level - service layer and repositories instances
            services.AddTransient<URLAnalyzer.API.Repositories.IContentRepository, URLAnalyzer.API.Repositories.WebContentRepository>();
            services.AddTransient<URLAnalyzer.API.Services.IContentService, URLAnalyzer.API.Services.WebContentService>();

            // API Controllers
            services.AddScoped<URLAnalyzer.API.Controllers.ContentController>(sp => 
            new URLAnalyzer.API.Controllers.ContentController(
                sp.GetRequiredService<URLAnalyzer.API.Services.IContentService>(),
                sp.GetRequiredService<URLAnalyzer.Foundation.Configuration.IConfigurationSettings>()));
        }

        private static void ConfigureCORS(HttpConfiguration config)
        {
            // Default values for CORS

            string origins = string.Empty;
            string headers = "*";
            string methods = "*";

            try
            {
                origins = ConfigurationManager.AppSettings[URLAnalyzerSettings.CORS_ORIGINS];
                headers = ConfigurationManager.AppSettings[URLAnalyzerSettings.CORS_HEADERS];
                methods = ConfigurationManager.AppSettings[URLAnalyzerSettings.CORS_METHODS];
            }
            catch { }

            var cors = new EnableCorsAttribute(origins, headers, methods);
            config.EnableCors(cors);
        }
    }
}
