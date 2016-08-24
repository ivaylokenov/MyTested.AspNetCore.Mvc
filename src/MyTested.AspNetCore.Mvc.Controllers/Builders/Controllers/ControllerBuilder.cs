namespace MyTested.AspNetCore.Mvc.Builders.Controllers
{
    using System;
    using Base;
    using Components;
    using Contracts.Controllers;
    using Internal.Application;
    using Internal.Contracts;
    using Internal.Http;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public partial class ControllerBuilder<TController> : BaseComponentBuilder<IAndControllerBuilder<TController>>, IAndControllerBuilder<TController>
        where TController : class
    {
        private ControllerTestContext testContext;
        private Action<ControllerContext> controllerContextAction;
        private Action<TController> controllerSetupAction;
        private bool isPreparedForTesting;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBuilder{TController}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ControllerBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
            this.TestContext = testContext;

            this.EnabledValidation = TestApplication.TestConfiguration.Controllers.ModelStateValidation;

#if NETSTANDARD1_6
            this.ValidateControllerType();
#endif
        }
        
        private TController Controller
        {
            get
            {
                this.TestContext.ComponentBuildDelegate?.Invoke();
                return this.TestContext.ComponentAs<TController>();
            }
        }

        public new ControllerTestContext TestContext
        {
            get
            {
                return this.testContext;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(this.TestContext));
                this.testContext = value;
            }
        }

        public bool EnabledValidation { get; set; }

        private new MockedHttpContext HttpContext => this.TestContext.MockedHttpContext;

        private HttpRequest HttpRequest => this.HttpContext.Request;

        private IServiceProvider Services => this.HttpContext.RequestServices;
        
        /// <inheritdoc />
        public IAndControllerBuilder<TController> AndAlso()
        {
            return this;
        }

        /// <inheritdoc />
        public IControllerTestBuilder ShouldHave()
        {
            this.TestContext.ComponentBuildDelegate?.Invoke();
            return new ControllerTestBuilder(this.TestContext);
        }
        
        protected override IAndControllerBuilder<TController> SetBuilder() => this;

        private void ValidateControllerType()
        {
            var validControllers = this.Services.GetRequiredService<IValidControllersCache>();
            var controllerType = typeof(TController);
            if (!validControllers.IsValid(typeof(TController)))
            {
                throw new InvalidOperationException($"{controllerType.ToFriendlyTypeName()} is not a valid controller type.");
            }
        }
    }
}
