namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Application;
    using Builders.Contracts.Controllers;
    using Builders.Contracts.Routing;
    using Builders.Contracts.ViewComponents;
    using Internal.Application;

    /// <summary>
    /// Starting point of the ASP.NET Core MVC testing framework. Provides methods to specify a test case.
    /// </summary>
    public static class MyMvc
    {
        static MyMvc()
        {
            TestApplication.TryInitialize();
        }
        
        /// <summary>
        /// Configures the tested application with the provided startup class.
        /// </summary>
        /// <typeparam name="TStartup">Type of startup class.</typeparam>
        /// <returns>Builder of <see cref="IApplicationConfigurationBuilder"/> type.</returns>
        public static IApplicationConfigurationBuilder StartsFrom<TStartup>()
            where TStartup : class
        {
            return new MyApplication(typeof(TStartup));
        }

        /// <summary>
        /// Starts a route test.
        /// </summary>
        /// <returns>Test builder of <see cref="IRouteTestBuilder"/> type.</returns>
        public static IRouteTestBuilder Routing()
        {
            return new MyRouting();
        }

        /// <summary>
        /// Starts a controller test.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <returns>Test builder of <see cref="IControllerBuilder{TController}"/> type.</returns>
        public static IControllerBuilder<TController> Controller<TController>()
            where TController : class
        {
            return new MyController<TController>();
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
            return new MyController<TController>(controller);
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
            return new MyController<TController>(construction);
        }

        /// <summary>
        /// Starts a view component test.
        /// </summary>
        /// <typeparam name="TViewComponent">Class representing ASP.NET Core MVC view component.</typeparam>
        /// <returns>Test builder of <see cref="IViewComponentBuilder{TViewComponent}"/> type.</returns>
        public static IViewComponentBuilder<TViewComponent> ViewComponent<TViewComponent>()
            where TViewComponent : class
        {
            return new MyViewComponent<TViewComponent>();
        }

        /// <summary>
        /// Starts a view component test.
        /// </summary>
        /// <typeparam name="TViewComponent">Class representing ASP.NET Core MVC view component.</typeparam>
        /// <param name="viewComponent">Instance of the ASP.NET Core MVC view component to use.</param>
        /// <returns>Test builder of <see cref="IViewComponentBuilder{TViewComponent}"/> type.</returns>
        public static IViewComponentBuilder<TViewComponent> ViewComponent<TViewComponent>(TViewComponent viewComponent)
            where TViewComponent : class
        {
            return new MyViewComponent<TViewComponent>(viewComponent);
        }

        /// <summary>
        /// Starts a view component test.
        /// </summary>
        /// <typeparam name="TViewComponent">Class representing ASP.NET Core MVC view component.</typeparam>
        /// <param name="construction">Construction function returning the instantiated view component.</param>
        /// <returns>Test builder of <see cref="IViewComponentBuilder{TViewComponent}"/> type.</returns>
        public static IViewComponentBuilder<TViewComponent> ViewComponent<TViewComponent>(Func<TViewComponent> construction)
            where TViewComponent : class
        {
            return new MyViewComponent<TViewComponent>(construction);
        }
    }
}
