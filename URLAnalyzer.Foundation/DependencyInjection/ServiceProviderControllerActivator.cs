using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Microsoft.Extensions.DependencyInjection;

namespace URLAnalyzer.Foundation.DependencyInjection
{
    public class ServiceProviderControllerActivator : IHttpControllerActivator
    {
        private readonly IServiceProvider provider;

        public ServiceProviderControllerActivator(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            var scope = scopeFactory.CreateScope();
            request.RegisterForDispose(scope);

            return scope.ServiceProvider.GetService(controllerType) as IHttpController;
        }
    }
}
