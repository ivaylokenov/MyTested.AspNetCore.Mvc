namespace MyTested.AspNetCore.Mvc.Builders.Controllers
{
    using Components;
    using Contracts.Base;
    using Internal;
    using Internal.Configuration;
    using Internal.Contracts;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Extensions;

    /// <summary>
    /// Base controller builder.
    /// </summary>
    /// /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    /// <typeparam name="TBuilder">Base builder.</typeparam>
    public abstract partial class BaseControllerBuilder<TController, TBuilder>
        : BaseComponentBuilder<TController, ControllerTestContext, TBuilder>
        where TController : class
        where TBuilder : IBaseTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseControllerBuilder{TController, TBuilder}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        protected BaseControllerBuilder(ControllerTestContext testContext) 
            : base(testContext)
        => this.EnabledModelStateValidation = ServerTestConfiguration
                .Global
                .GetControllersConfiguration()
                .ModelStateValidation;

        public bool EnabledModelStateValidation { get; set; }

        protected override string ComponentName => "controller";

        protected override bool IsValidComponent
            => this.Services
                .GetRequiredService<IValidControllersCache>()
                .IsValid(typeof(TController));

        protected override TController TryCreateComponentWithFactory()
        {
            try
            {
                return this.Services
                    .GetService<IControllerFactory>()
                    ?.CreateController(this.TestContext.ComponentContext) as TController;
            }
            catch
            {
                return null;
            }
        }

        protected override void ActivateComponent()
            => this.Services
                .GetServices(WebFramework.Internals.ControllerPropertyActivator)
                ?.ForEach(a => a
                    .Exposed()
                    .Activate(this.TestContext.ComponentContext, this.TestContext.Component));
    }
}
