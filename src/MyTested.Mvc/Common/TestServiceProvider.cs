namespace MyTested.Mvc.Common
{
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    internal class TestServiceProvider
    {
        private static IServiceCollection serviceCollection;
        private static IServiceProvider serviceProvider;

        public static IServiceProvider Current => serviceProvider ?? serviceCollection.BuildServiceProvider();

        public static void Setup(Action<IServiceCollection> servicesAction, Action<MvcOptions> setupAction)
        {
            serviceCollection = new ServiceCollection();
            serviceCollection.AddMvc(setupAction);

            if (servicesAction != null)
            {
                servicesAction(serviceCollection);
            }
        }
    }
}
