namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Application;
    using Builders.Contracts.Controllers;
    using Builders.Contracts.Routing;
    using Builders.Contracts.Server;
    using Builders.Contracts.ViewComponents;
    using Builders.Server;
    using Internal.Application;

    /// <summary>
    /// Provides methods to specify an ASP.NET Core MVC test case.
    /// </summary>
    public static class MyMvc
    {
        static MyMvc() => TestApplication.TryInitialize();

        /// <summary>
        /// Configures the tested application with the provided Startup class. This method should be called only once per test
        /// project. If you need to use different test configurations with more than one Startup class in your tests, you should
        /// separate them in independent assemblies.
        /// </summary>
        /// <typeparam name="TStartup">Type of startup class.</typeparam>
        /// <returns>Builder of <see cref="IApplicationConfigurationBuilder"/> type.</returns>
        public static IApplicationConfigurationBuilder StartsFrom<TStartup>()
            where TStartup : class 
            => new MyApplication(typeof(TStartup));

        /// <summary>
        /// Configures the test server on which the ASP.NET Core MVC test application is running. This method should be called
        /// only once per test project. If you need to use different test server configurations in your tests, you should
        /// separate them in independent assemblies.
        /// </summary>
        /// <param name="testServerBuilder">Action setting the test server.</param>
        public static void IsRunningOn(Action<ITestServerBuilder> testServerBuilder)
            => testServerBuilder(new TestServerBuilder());

        /// <summary>
        /// Starts a route test.
        /// </summary>
        /// <returns>Test builder of <see cref="IRouteTestBuilder"/> type.</returns>
        public static IRouteTestBuilder Routing() 
            => new MyRouting();

        /// <summary>
        /// Starts a controller test.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <returns>Test builder of <see cref="IControllerBuilder{TController}"/> type.</returns>
        public static IControllerBuilder<TController> Controller<TController>()
            where TController : class 
            => new MyController<TController>();

        /// <summary>
        /// Starts a controller test.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <param name="controller">Instance of the ASP.NET Core MVC controller to test.</param>
        /// <returns>Test builder of <see cref="IControllerBuilder{TController}"/> type.</returns>
        public static IControllerBuilder<TController> Controller<TController>(TController controller)
            where TController : class 
            => new MyController<TController>(controller);

        /// <summary>
        /// Starts a controller test.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <param name="construction">Construction function returning the instantiated controller.</param>
        /// <returns>Test builder of <see cref="IControllerBuilder{TController}"/> type.</returns>
        public static IControllerBuilder<TController> Controller<TController>(Func<TController> construction)
            where TController : class 
            => new MyController<TController>(construction);

        /// <summary>
        /// Starts a controller test.
        /// </summary>
        /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
        /// <param name="controllerInstanceBuilder">Builder for creating the controller instance.</param>
        /// <returns>Test builder of <see cref="IControllerActionCallBuilder{TController}"/> type.</returns>
        public static IControllerActionCallBuilder<TController> Controller<TController>(Action<IControllerInstanceBuilder<TController>> controllerInstanceBuilder)
            where TController : class
            => new MyController<TController>(controllerInstanceBuilder);

        /// <summary>
        /// Starts a view component test.
        /// </summary>
        /// <typeparam name="TViewComponent">Class representing ASP.NET Core MVC view component.</typeparam>
        /// <returns>Test builder of <see cref="IViewComponentBuilder{TViewComponent}"/> type.</returns>
        public static IViewComponentBuilder<TViewComponent> ViewComponent<TViewComponent>()
            where TViewComponent : class 
            => new MyViewComponent<TViewComponent>();

        /// <summary>
        /// Starts a view component test.
        /// </summary>
        /// <typeparam name="TViewComponent">Class representing ASP.NET Core MVC view component.</typeparam>
        /// <param name="viewComponent">Instance of the ASP.NET Core MVC view component to use.</param>
        /// <returns>Test builder of <see cref="IViewComponentBuilder{TViewComponent}"/> type.</returns>
        public static IViewComponentBuilder<TViewComponent> ViewComponent<TViewComponent>(TViewComponent viewComponent)
            where TViewComponent : class 
            => new MyViewComponent<TViewComponent>(viewComponent);

        /// <summary>
        /// Starts a view component test.
        /// </summary>
        /// <typeparam name="TViewComponent">Class representing ASP.NET Core MVC view component.</typeparam>
        /// <param name="construction">Construction function returning the instantiated view component.</param>
        /// <returns>Test builder of <see cref="IViewComponentBuilder{TViewComponent}"/> type.</returns>
        public static IViewComponentBuilder<TViewComponent> ViewComponent<TViewComponent>(Func<TViewComponent> construction)
            where TViewComponent : class 
            => new MyViewComponent<TViewComponent>(construction);
    }
}
