namespace MyTested.Mvc.Session
{
    using System;
    using Internal.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.DependencyInjection;

    public class SessionTestPlugin : IServiceRegistrationPlugin, IHttpFeatureRegistrationPlugin
    {
        private readonly Type defaultSessionStoreServiceType = typeof(ISessionStore);
        private readonly Type defaultSessionStoreImplementationType = typeof(DistributedSessionStore);

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
        {
            get
            {
                return
                    serviceDescriptor =>
                        serviceDescriptor.ServiceType == defaultSessionStoreServiceType &&
                        serviceDescriptor.ImplementationType == defaultSessionStoreImplementationType;
            }
        }

        public Action<IServiceCollection> ServiceRegistrationDelegate => serviceCollection => serviceCollection.ReplaceSession();

        public Action<HttpContext> HttpFeatureRegistrationDelegate
        {
            get
            {
                return httpContext =>
                {
                    var sessionStore = httpContext.RequestServices.GetService<ISessionStore>();
                    if (sessionStore != null)
                    {
                        if (httpContext.Features.Get<ISessionFeature>() == null)
                        {
                            ISession mockedSession;
                            if (sessionStore is MockedSessionStore)
                            {
                                mockedSession = new MockedSession();
                            }
                            else
                            {
                                mockedSession = sessionStore.Create(Guid.NewGuid().ToString(), TimeSpan.Zero, () => true, true);
                            }

                            httpContext.Features.Set<ISessionFeature>(new MockedSessionFeature
                            {
                                Session = mockedSession
                            });
                        }
                    }
                };
            }
        }
    }
}
