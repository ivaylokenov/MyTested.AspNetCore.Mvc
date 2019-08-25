namespace MyTested.AspNetCore.Mvc.Builders.Controllers
{
    using Contracts.Controllers;
    using Internal.TestContexts;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public class ControllerInstanceBuilder<TController>
        : BaseControllerBuilder<TController, IAndControllerInstanceBuilder<TController>>,
        IAndControllerInstanceBuilder<TController>
        where TController : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerInstanceBuilder{TController}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ControllerInstanceBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndControllerInstanceBuilder<TController> AndAlso() => this;

        protected override IAndControllerInstanceBuilder<TController> SetBuilder() => this;
    }
}
