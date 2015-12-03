namespace MyTested.Mvc
{
    using System;
    using Utilities;
    using Microsoft.AspNet.Mvc;
    using Builders.Controllers;
    using Microsoft.Extensions.DependencyInjection;
    using Common;

    /// <summary>
    /// Starting point of the testing framework, which provides a way to specify the test case.
    /// </summary>
    public static class MyMvc
    {
        public static void UsesDefaultServices()
        {
            UsesDefaultServices(null);
        }

        public static void UsesDefaultServices(Action<MvcOptions> setupAction)
        {
            HasConfiguredServices(null, setupAction);
        }

        public static void HasConfiguredServices(Action<IServiceCollection> services)
        {
            HasConfiguredServices(services, null);
        }

        public static void HasConfiguredServices(Action<IServiceCollection> services, Action<MvcOptions> setupAction)
        {
            TestServiceProvider.Setup(services, setupAction);
        }
        
        /// <summary>
        /// Selects controller on which the test will be executed. Controller is instantiated with default constructor.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET MVC 6 controller.</typeparam>
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
        /// <typeparam name="TController">Class inheriting ASP.NET MVC 6 controller.</typeparam>
        /// <param name="controller">Instance of the ASP.NET MVC 6 controller to use.</param>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>(TController controller)
            where TController : Controller
        {
            return Controller(() => controller);
        }

        /// <summary>
        /// Selects controller on which the test will be executed. Controller is instantiated using construction function.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET MVC 6 controller.</typeparam>
        /// <param name="construction">Construction function returning the instantiated controller.</param>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>(Func<TController> construction)
            where TController : Controller
        {
            var controllerInstance = construction();
            return new ControllerBuilder<TController>(controllerInstance);
        }

        // TODO: add all other options
    }
}
