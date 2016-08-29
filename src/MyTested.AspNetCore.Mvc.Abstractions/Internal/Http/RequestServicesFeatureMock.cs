namespace MyTested.AspNetCore.Mvc.Internal.Http
{
    using System;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    /// <summary>
    /// Mock of request services feature.
    /// </summary>
    public class RequestServicesFeatureMock : IServiceProvidersFeature, IDisposable
    {
        private IServiceProvider requestServices;
        private IServiceScope scope;

        private bool requestServicesSet;
        
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
                    this.scope = TestServiceProvider.Global.GetRequiredService<IServiceScopeFactory>().CreateScope();
                    this.requestServices = this.scope.ServiceProvider;
                    this.requestServicesSet = true;
                    TestServiceProvider.Current = this.requestServices;
                }

                return this.requestServices;
            }

            set
            {
                this.requestServices = value;
                this.requestServicesSet = true;
                TestServiceProvider.Current = this.requestServices;
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
