namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Internal.Session;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.DependencyInjection;

    public class SessionTestPlugin : IServiceRegistrationPlugin, IHttpFeatureRegistrationPlugin
    {
        private readonly Type defaultSessionStoreServiceType = typeof(ISessionStore);
        private readonly Type defaultSessionStoreImplementationType = typeof(DistributedSessionStore);

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
            => serviceDescriptor =>
                serviceDescriptor.ServiceType == this.defaultSessionStoreServiceType &&
                serviceDescriptor.ImplementationType == this.defaultSessionStoreImplementationType;

        public Action<IServiceCollection> ServiceRegistrationDelegate => serviceCollection => serviceCollection.ReplaceSession();

        public Action<HttpContext> HttpFeatureRegistrationDelegate 
            => httpContext =>
            {
                var sessionStore = httpContext.RequestServices.GetService<ISessionStore>();
                if (sessionStore != null)
                {
                    if (httpContext.Features.Get<ISessionFeature>() == null)
                    {
                        ISession mockedSession;
                        if (sessionStore is SessionStoreMock)
                        {
                            mockedSession = new SessionMock();
                        }
                        else
                        {
                            mockedSession = sessionStore.Create(
                                Guid.NewGuid().ToString(), 
                                TimeSpan.Zero,
                                TimeSpan.Zero,
                                () => true, 
                                true);
                        }

                        httpContext.Features.Set<ISessionFeature>(new SessionFeatureMock
                        {
                            Session = mockedSession
                        });
                    }
                }
            };
    }
}
