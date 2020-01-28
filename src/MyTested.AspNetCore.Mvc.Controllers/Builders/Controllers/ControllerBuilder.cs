namespace MyTested.AspNetCore.Mvc.Builders.Controllers
{
    using Contracts.Controllers;
    using Internal.TestContexts;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public partial class ControllerBuilder<TController> 
        : BaseControllerBuilder<TController, IAndControllerBuilder<TController>>, 
        IAndControllerBuilder<TController>
        where TController : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBuilder{TController}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ControllerBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IControllerTestBuilder ShouldHave()
        {
            this.TestContext.ComponentBuildDelegate?.Invoke();
            return new ControllerTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> AndAlso() => this;

        protected override IAndControllerBuilder<TController> SetBuilder() => this;
    }
}
