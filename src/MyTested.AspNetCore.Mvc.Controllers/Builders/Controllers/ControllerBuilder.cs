namespace MyTested.AspNetCore.Mvc.Builders.Controllers
{
    using Components;
    using Contracts.Controllers;
    using Internal.Application;
    using Internal.Contracts;
    using Internal.TestContexts;
    using Microsoft.Extensions.DependencyInjection;

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
            this.EnabledModelStateValidation = TestApplication.TestConfiguration.Controllers.ModelStateValidation;
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
    }
}
