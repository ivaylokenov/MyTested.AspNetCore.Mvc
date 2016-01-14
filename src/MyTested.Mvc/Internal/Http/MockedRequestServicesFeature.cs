namespace MyTested.Mvc.Internal.Http
{
    using Microsoft.AspNet.Http.Features.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    // TODO: document
    public class MockedRequestServicesFeature : IServiceProvidersFeature, IDisposable
    {
        private IServiceProvider globalServices;
        private IServiceProvider requestServices;
        private IServiceScope scope;

        private bool requestServicesSet;

        public MockedRequestServicesFeature(IServiceProvider globalServices)
        {
            if (globalServices == null)
            {
                throw new ArgumentNullException(nameof(globalServices));
            }

            this.globalServices = globalServices;
        }

        public IServiceProvider RequestServices
        {
            get
            {
                if (!this.requestServicesSet)
                {
                    this.scope = globalServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
                    this.requestServices = scope.ServiceProvider;
                    this.requestServicesSet = true;
                }

                return this.requestServices;
            }
            set
            {
                this.requestServices = value;
                this.requestServicesSet = true;
            }
        }

        public void Dispose()
        {
            this.scope?.Dispose();
            this.scope = null;
            this.requestServices = null;
        }
    }
}
