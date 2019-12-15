namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Builders.Contracts.Actions;
    using Builders.Contracts.Controllers;
    using Builders.Controllers;
    using Internal.Application;
    using Internal.TestContexts;

    /// <summary>
    /// Provides methods to specify an ASP.NET Core MVC controller test case. 
    /// This assertion chain validates the controller as an atomic unit - it does not 
    /// execute server requests, middleware, routing, filters, or application responses.
    /// </summary>
    /// <typeparam name="TController">Type of ASP.NET Core MVC controller to test.</typeparam>
    public class MyController<TController> : ControllerBuilder<TController>
        where TController : class
    {
        static MyController() => TestApplication.TryInitialize();

        /// <summary>
        /// Initializes a new instance of the <see cref="MyController{TController}"/> class.
        /// This assertion chain validates the controller as an atomic unit - it does not 
        /// execute server requests, middleware, routing, filters, or application responses.
        /// </summary>
        public MyController()
            : this((TController)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyController{TController}"/> class.
        /// This assertion chain validates the controller as an atomic unit - it does not 
        /// execute server requests, middleware, routing, filters, or application responses.
        /// </summary>
        /// <param name="controller">Instance of the ASP.NET Core MVC controller to test.</param>
        public MyController(TController controller)
            : this(() => controller)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyController{TController}"/> class.
        /// This assertion chain validates the controller as an atomic unit - it does not 
        /// execute server requests, middleware, routing, filters, or application responses.
        /// </summary>
        /// <param name="construction">Construction function returning the instantiated controller.</param>
        public MyController(Func<TController> construction)
            : base(new ControllerTestContext { ComponentConstructionDelegate = construction })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyController{TController}"/> class.
        /// This assertion chain validates the controller as an atomic unit - it does not 
        /// execute server requests, middleware, routing, filters, or application responses.
        /// </summary>
        /// <param name="controllerInstanceBuilder">Builder for creating the controller instance.</param>
        public MyController(Action<IControllerInstanceBuilder<TController>> controllerInstanceBuilder)
            : this()
            => controllerInstanceBuilder(new ControllerInstanceBuilder<TController>(this.TestContext));

        /// <summary>
        /// Starts a controller test.
        /// This assertion chain validates the controller as an atomic unit - it does not 
        /// execute server requests, middleware, routing, filters, or application responses.
        /// </summary>
        /// <returns>Test builder of <see cref="IControllerBuilder{TController}"/> type.</returns>
        public static IControllerBuilder<TController> Instance() 
            => Instance((TController)null);

        /// <summary>
        /// Starts a controller test.
        /// This assertion chain validates the controller as an atomic unit - it does not 
        /// execute server requests, middleware, routing, filters, or application responses.
        /// </summary>
        /// <param name="controller">Instance of the ASP.NET Core MVC controller to test.</param>
        /// <returns>Test builder of <see cref="IControllerBuilder{TController}"/> type.</returns>
        public static IControllerBuilder<TController> Instance(TController controller) 
            => Instance(() => controller);

        /// <summary>
        /// Starts a controller test.
        /// This assertion chain validates the controller as an atomic unit - it does not 
        /// execute server requests, middleware, routing, filters, or application responses.
        /// </summary>
        /// <param name="construction">Construction function returning the instantiated controller.</param>
        /// <returns>Test builder of <see cref="IControllerBuilder{TController}"/> type.</returns>
        public static IControllerBuilder<TController> Instance(Func<TController> construction) 
            => new MyController<TController>(construction);

        /// <summary>
        /// Starts a controller test.
        /// This assertion chain validates the controller as an atomic unit - it does not 
        /// execute server requests, middleware, routing, filters, or application responses.
        /// </summary>
        /// <param name="controllerInstanceBuilder">Builder for creating the controller instance.</param>
        /// <returns>Test builder of <see cref="IControllerActionCallBuilder{TController}"/> type.</returns>
        public static IControllerActionCallBuilder<TController> Instance(Action<IControllerInstanceBuilder<TController>> controllerInstanceBuilder)
            => new MyController<TController>(controllerInstanceBuilder);

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// This assertion chain validates the controller action as an atomic unit - it does not 
        /// execute server requests, middleware, routing, filters, or application responses.
        /// </summary>
        /// <typeparam name="TActionResult">Type of result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked action.</param>
        /// <returns>Test builder of <see cref="IActionResultTestBuilder{TActionResult}"/> type.</returns>
        public new static IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, TActionResult>> actionCall)
            => Instance()
                .Calling(actionCall);

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// This assertion chain validates the controller action as an atomic unit - it does not 
        /// execute server requests, middleware, routing, filters, or application responses.
        /// </summary>
        /// <typeparam name="TActionResult">Type of result from action.</typeparam>
        /// <param name="actionCall">Method call expression indicating invoked asynchronous action.</param>
        /// <returns>Test builder of <see cref="IActionResultTestBuilder{TActionResult}"/> type.</returns>
        public new static IActionResultTestBuilder<TActionResult> Calling<TActionResult>(Expression<Func<TController, Task<TActionResult>>> actionCall)
            => Instance()
                .Calling(actionCall);

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// This assertion chain validates the controller action as an atomic unit - it does not 
        /// execute server requests, middleware, routing, filters, or application responses.
        /// </summary>
        /// <param name="actionCall">Method call expression indicating invoked void action.</param>
        /// <returns>Test builder of <see cref="IActionResultTestBuilder{TActionResult}"/> type.</returns>
        public new static IVoidActionResultTestBuilder Calling(Expression<Action<TController>> actionCall)
            => Instance()
                .Calling(actionCall);

        /// <summary>
        /// Indicates which action should be invoked and tested.
        /// This assertion chain validates the controller action as an atomic unit - it does not 
        /// execute server requests, middleware, routing, filters, or application responses.
        /// </summary>
        /// <param name="actionCall">Method call expression indicating invoked asynchronous void action.</param>
        /// <returns>Test builder of <see cref="IActionResultTestBuilder{TActionResult}"/> type.</returns>
        public new static IVoidActionResultTestBuilder Calling(Expression<Func<TController, Task>> actionCall)
            => Instance()
                .Calling(actionCall);

        /// <summary>
        /// Used for testing controller additional details.
        /// </summary>
        /// <returns>Test builder of <see cref="IControllerTestBuilder"/> type.</returns>
        public new static IControllerTestBuilder ShouldHave()
            => Instance()
                .ShouldHave();
    }
}
