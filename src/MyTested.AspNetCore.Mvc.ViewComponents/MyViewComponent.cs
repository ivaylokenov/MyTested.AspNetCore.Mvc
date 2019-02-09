namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.ViewComponents;
    using Builders.ViewComponents;
    using Internal.Application;
    using Internal.TestContexts;

    /// <summary>
    /// Provides methods to specify an ASP.NET Core MVC view component test case.
    /// </summary>
    /// <typeparam name="TViewComponent">Type of ASP.NET Core MVC view component to test.</typeparam>
    public class MyViewComponent<TViewComponent> : ViewComponentBuilder<TViewComponent>
        where TViewComponent : class
    {
        static MyViewComponent() => TestApplication.TryInitialize();

        /// <summary>
        /// Initializes a new instance of the <see cref="MyViewComponent{TViewComponent}"/> class.
        /// </summary>
        public MyViewComponent()
            : this((TViewComponent)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyViewComponent{TViewComponent}"/> class.
        /// </summary>
        /// <param name="viewComponent">Instance of the ASP.NET Core MVC view component to test.</param>
        public MyViewComponent(TViewComponent viewComponent)
            : this(() => viewComponent)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyViewComponent{TViewComponent}"/> class.
        /// </summary>
        /// <param name="construction">Construction function returning the instantiated view component.</param>
        public MyViewComponent(Func<TViewComponent> construction)
            : base(new ViewComponentTestContext { ComponentConstructionDelegate = construction })
        {
        }

        /// <summary>
        /// Starts a view component test.
        /// </summary>
        /// <returns>Test builder of <see cref="IViewComponentBuilder{TViewComponent}"/> type.</returns>
        public static IViewComponentBuilder<TViewComponent> Instance() 
            => Instance((TViewComponent)null);

        /// <summary>
        /// Starts a view component test.
        /// </summary>
        /// <param name="viewComponent">Instance of the ASP.NET Core MVC view component to test.</param>
        /// <returns>Test builder of <see cref="IViewComponentBuilder{TViewComponent}"/> type.</returns>
        public static IViewComponentBuilder<TViewComponent> Instance(TViewComponent viewComponent) 
            => Instance(() => viewComponent);

        /// <summary>
        /// Starts a view component test.
        /// </summary>
        /// <param name="construction">Construction function returning the instantiated view component.</param>
        /// <returns>Test builder of <see cref="IViewComponentBuilder{TViewComponent}"/> type.</returns>
        public static IViewComponentBuilder<TViewComponent> Instance(Func<TViewComponent> construction) 
            => new MyViewComponent<TViewComponent>(construction);
    }
}
