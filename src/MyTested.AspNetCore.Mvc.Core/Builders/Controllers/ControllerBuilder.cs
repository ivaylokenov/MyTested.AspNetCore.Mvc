namespace MyTested.AspNetCore.Mvc.Builders.Controllers
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Contracts.Controllers;
    using Contracts.ShouldPassFor;
    using Internal.Application;
    using Internal.Contracts;
    using Internal.Http;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using ShouldPassFor;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public partial class ControllerBuilder<TController> : BaseTestBuilderWithComponentBuilder<IAndControllerBuilder<TController>>, IAndControllerBuilder<TController>
        where TController : class
    {
        private readonly IDictionary<Type, object> aggregatedServices;

        private ControllerTestContext testContext;
        private Action<ControllerContext> controllerContextAction;
        private Action<TController> controllerSetupAction;
        private bool isPreparedForTesting;
        private bool enabledValidation;
        private bool resolveRouteValues;
        private object additionalRouteValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBuilder{TController}"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public ControllerBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
            this.TestContext = testContext;

            this.enabledValidation = TestApplication.TestConfiguration.ModelStateValidation;
            this.aggregatedServices = new Dictionary<Type, object>();

            this.ValidateControllerType();
        }
        
        private TController Controller
        {
            get
            {
                this.BuildControllerIfNotExists();
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

        public Action<ControllerTestContext> ControllerPreparationAction { get; set; }

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
            this.BuildControllerIfNotExists();
            return new ControllerTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public new IShouldPassForTestBuilderWithController<TController> ShouldPassFor()
        {
            this.BuildControllerIfNotExists();
            return new ShouldPassForTestBuilderWithController<TController>(this.TestContext);
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
