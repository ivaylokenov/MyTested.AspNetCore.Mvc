namespace MyTested.Mvc.Internal.Http
{
    using System;
    using Microsoft.AspNet.Http.Features.Internal;
    using Microsoft.Extensions.DependencyInjection;
    
    /// <summary>
    /// Mocked request services feature.
    /// </summary>
    public class MockedRequestServicesFeature : IServiceProvidersFeature, IDisposable
    {
        private IServiceProvider globalServices;
        private IServiceProvider requestServices;
        private IServiceScope scope;

        private bool requestServicesSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedRequestServicesFeature" /> class.
        /// </summary>
        /// <param name="globalServices">Global application services provider.</param>
        public MockedRequestServicesFeature(IServiceProvider globalServices)
        {
            if (globalServices == null)
            {
                throw new ArgumentNullException(nameof(globalServices));
            }

            this.globalServices = globalServices;
        }

        /// <summary>
        /// Gets or sets scoped request services based on the global ones.
        /// </summary>
        /// <value>Service provider.</value>
        public IServiceProvider RequestServices
        {
            get
            {
                if (!this.requestServicesSet)
                {
                    this.scope = this.globalServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
                    this.requestServices = this.scope.ServiceProvider;
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

        /// <summary>
        /// Disposes the current scoped request services.
        /// </summary>
        public void Dispose()
        {
            this.scope?.Dispose();
            this.scope = null;
            this.requestServices = null;
        }
    }
}
