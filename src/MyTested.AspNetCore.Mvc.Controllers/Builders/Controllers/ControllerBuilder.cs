namespace MyTested.AspNetCore.Mvc.Builders.Controllers
{
    using Components;
    using Contracts.Controllers;
    using Internal.Application;
    using Internal.Configuration;
    using Internal.Contracts;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Extensions;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public partial class ControllerBuilder<TController> : BaseComponentBuilder<TController, ControllerTestContext, IAndControllerBuilder<TController>>, IAndControllerBuilder<TController>
        where TController : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBuilder{TController}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ControllerBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
            this.EnabledModelStateValidation = TestApplication
                .Configuration()
                .Controllers()
                .ModelStateValidation();
        }
        
        public bool EnabledModelStateValidation { get; set; }

        protected override string ComponentName => "controller";

        protected override bool IsValidComponent
            => this.Services
                .GetRequiredService<IValidControllersCache>()
                .IsValid(typeof(TController));

        /// <inheritdoc />
        public IAndControllerBuilder<TController> AndAlso() => this;

        /// <inheritdoc />
        public IControllerTestBuilder ShouldHave()
        {
            this.TestContext.ComponentBuildDelegate?.Invoke();
            return new ControllerTestBuilder(this.TestContext);
        }
        
        protected override IAndControllerBuilder<TController> SetBuilder() => this;
        
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
        {
            this.Services
                .GetServices<IControllerPropertyActivator>()
                ?.ForEach(a => a.Activate(this.TestContext.ComponentContext, this.TestContext.Component));
        }
    }
}
