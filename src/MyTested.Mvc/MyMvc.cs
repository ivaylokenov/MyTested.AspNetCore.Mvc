namespace MyTested.Mvc
{
    using System;
    using Builders;
    using Builders.Contracts.Application;
    using Builders.Controllers;
    using Internal.Application;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities;

    /// <summary>
    /// Starting point of the ASP.NET MVC testing framework, which provides a way to specify the test case.
    /// </summary>
    public static class MyMvc
    {
        static MyMvc()
        {
            IsUsingDefaultServices();
        }

        public static IApplicationConfigurationBuilder IsUsingDefaultConfiguration()
        {
            return new ApplicationConfigurationBuilder(null);
        }

        public static IApplicationConfigurationBuilder StartsFrom<TStartup>()
            where TStartup : class, new()
        {
            return new ApplicationConfigurationBuilder(typeof(TStartup));
        }

        /// <summary>
        /// Registers default  ASP.NET MVC application services.
        /// </summary>
        public static void IsUsingDefaultServices()
        {
            IsUsing(null);
        }

        /// <summary>
        /// Registers default ASP.NET MVC application services and adds custom ones by using the provided action.
        /// </summary>
        /// <param name="services">Action which configures the application services.</param>
        public static void IsUsing(Action<IServiceCollection> services)
        {
            TestServiceProvider.Setup(services);
        }

        /// <summary>
        /// Registers ASP.NET MVC application services by using the provided Startup class.
        /// </summary>
        /// <typeparam name="TStartup">Class containing public 'ConfigureServices' method.</typeparam>
        public static void IsUsing<TStartup>() where TStartup : class, new()
        {
            IsUsing<TStartup>(null);
        }

        /// <summary>
        /// Registers ASP.NET MVC application services by using the provided Startup class and adds custom ones by using the provided action.
        /// </summary>
        /// <typeparam name="TStartup">Class containing public 'ConfigureServices' method.</typeparam>
        /// <param name="services">Action which configures the application services.</param>
        public static void IsUsing<TStartup>(Action<IServiceCollection> services)
            where TStartup : class, new()
        {
            TestServiceProvider.Setup<TStartup>(services);
        }

        /// <summary>
        /// Selects controller on which the test will be executed. Controller is instantiated with default constructor.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET MVC controller.</typeparam>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>()
            where TController : Controller
        {
            var controller = Reflection.TryCreateInstance<TController>();
            return Controller(() => controller);
        }

        /// <summary>
        /// Selects controller on which the test will be executed.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET MVC controller.</typeparam>
        /// <param name="controller">Instance of the ASP.NET MVC controller to use.</param>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>(TController controller)
            where TController : Controller
        {
            return Controller(() => controller);
        }

        /// <summary>
        /// Selects controller on which the test will be executed. Controller is instantiated using construction function.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET MVC controller.</typeparam>
        /// <param name="construction">Construction function returning the instantiated controller.</param>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>(Func<TController> construction)
            where TController : Controller
        {
            var controllerInstance = construction();
            return new ControllerBuilder<TController>(controllerInstance);
        }
    }
}
