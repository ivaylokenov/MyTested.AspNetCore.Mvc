namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders;
    using Builders.Contracts.Application;
    using Builders.Controllers;
    using Builders.Routes;
    using Internal.Application;
    using Internal.TestContexts;

    /// <summary>
    /// Starting point of the ASP.NET Core MVC testing framework. Provides methods to specify a test case.
    /// </summary>
    public static class MyMvc
    {
        internal const string ReleaseDate = "2016-06-01";

        static MyMvc()
        {
            TestApplication.TryInitialize();
        }

        /// <summary>
        /// Sets default configuration on the tested application. Calls 'AddMvc' on the services collection and 'UseMvcWithDefaultRoute' on the application builder.
        /// </summary>
        /// <returns>Builder of <see cref="IApplicationConfigurationBuilder"/> type.</returns>
        public static IApplicationConfigurationBuilder IsUsingDefaultConfiguration()
        {
            return new ApplicationConfigurationBuilder(null);
        }

        /// <summary>
        /// Configures the tested application with the provided startup class.
        /// </summary>
        /// <typeparam name="TStartup">Type of startup class.</typeparam>
        /// <returns>Builder of <see cref="IApplicationConfigurationBuilder"/> type.</returns>
        public static IApplicationConfigurationBuilder StartsFrom<TStartup>()
            where TStartup : class, new()
        {
            return new ApplicationConfigurationBuilder(typeof(TStartup));
        }

        /// <summary>
        /// Starts a route test.
        /// </summary>
        /// <returns>Test builder of <see cref="IRouteTestBuilder"/> type.</returns>
        public static IRouteTestBuilder Routes()
        {
            return new RouteTestBuilder(new RouteTestContext
            {
                Router = TestApplication.Router,
                Services = TestApplication.RouteServices
            });
        }

        /// <summary>
        /// Starts a controller test.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <returns>Test builder of <see cref="IControllerBuilder{TController}"/> type.</returns>
        public static IControllerBuilder<TController> Controller<TController>()
            where TController : class
        {
            return Controller((TController)null);
        }

        /// <summary>
        /// Starts a controller test.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <param name="controller">Instance of the ASP.NET Core MVC controller to use.</param>
        /// <returns>Test builder of <see cref="IControllerBuilder{TController}"/> type.</returns>
        public static IControllerBuilder<TController> Controller<TController>(TController controller)
            where TController : class
        {
            return Controller(() => controller);
        }

        /// <summary>
        /// Starts a controller test.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <param name="construction">Construction function returning the instantiated controller.</param>
        /// <returns>Test builder of <see cref="IControllerBuilder{TController}"/> type.</returns>
        public static IControllerBuilder<TController> Controller<TController>(Func<TController> construction)
            where TController : class
        {
            return new ControllerBuilder<TController>(new ControllerTestContext { ControllerConstruction = construction });
        }
    }
}
