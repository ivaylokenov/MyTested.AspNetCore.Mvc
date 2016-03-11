namespace MyTested.Mvc
{
    using System;
    using Builders;
    using Builders.Contracts.Application;
    using Builders.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Builders.Routes;
    using Internal.Application;
    using Internal.TestContexts;

    /// <summary>
    /// Starting point of the ASP.NET MVC testing framework, which provides a way to specify the test case.
    /// </summary>
    public static class MyMvc
    {
        static MyMvc()
        {
            TestApplication.TryInitialize();
        }

        /// <summary>
        /// Sets default configuration on the tested application. Calls 'AddMvc' on the services collection and 'UseMvc' on the application builder.
        /// </summary>
        /// <returns>Application configuration builder.</returns>
        public static IApplicationConfigurationBuilder IsUsingDefaultConfiguration()
        {
            return new ApplicationConfigurationBuilder(null);
        }

        /// <summary>
        /// Configures the tested application with the provided startup class.
        /// </summary>
        /// <typeparam name="TStartup">Type of startup class.</typeparam>
        /// <returns>Application configuration builder.</returns>
        public static IApplicationConfigurationBuilder StartsFrom<TStartup>()
            where TStartup : class, new()
        {
            return new ApplicationConfigurationBuilder(typeof(TStartup));
        }

        /// <summary>
        /// Starts a route test.
        /// </summary>
        /// <returns>Route test builder.</returns>
        public static IRouteTestBuilder Routes()
        {
            return new RouteTestBuilder(new RouteTestContext
            {
                Router = TestApplication.Router,
                Services = TestApplication.RouteServices
            });
        }

        /// <summary>
        /// Selects controller on which the test will be executed.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET MVC controller.</typeparam>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>()
            where TController : class
        {
            return Controller((TController)null);
        }

        /// <summary>
        /// Selects controller on which the test will be executed.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET MVC controller.</typeparam>
        /// <param name="controller">Instance of the ASP.NET MVC controller to use.</param>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>(TController controller)
            where TController : class
        {
            return Controller(() => controller);
        }

        /// <summary>
        /// Selects controller on which the test will be executed. Controller is instantiated using construction function.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET MVC controller.</typeparam>
        /// <param name="construction">Construction function returning the instantiated controller.</param>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>(Func<TController> construction)
            where TController : class
        {
            return new ControllerBuilder<TController>(new ControllerTestContext { ControllerConstruction = construction });
        }
    }
}
