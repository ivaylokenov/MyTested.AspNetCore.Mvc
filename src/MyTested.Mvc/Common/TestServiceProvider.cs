namespace MyTested.Mvc.Common
{
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class TestServiceProvider
    {
        private static IServiceCollection serviceCollection;
        private static IServiceProvider serviceProvider;

        public static IServiceProvider Current => serviceProvider ?? serviceCollection?.BuildServiceProvider();

        public static void Setup(Action<IServiceCollection> servicesAction)
        {
            serviceCollection = new ServiceCollection();
            serviceCollection.AddMvc();

            if (servicesAction != null)
            {
                servicesAction(serviceCollection);
            }
        }

        public static void Clear()
        {
            serviceCollection = null;
            serviceProvider = null;
        }
    }
}
