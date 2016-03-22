namespace MyTested.Mvc.Internal.Controllers
{
    using System;
    using System.Collections.Generic;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Utilities.Validators;

    public class MockedControllerContext : ControllerContext
    {
        private HttpTestContext testContext;
        private MvcOptions options;
        private FormatterCollection<IInputFormatter> inputFormatters;
        private IList<IModelBinder> modelBinders;
        private IList<IModelValidatorProvider> validatorProviders;
        private IList<IValueProvider> valueProviders;

        public MockedControllerContext(HttpTestContext testContext)
        {
            this.PrepareControllerContext(testContext);
        }

        private MockedControllerContext(HttpTestContext testContext, ActionContext actionContext)
            : base(actionContext)
        {
            this.PrepareControllerContext(testContext);
        }

        public override FormatterCollection<IInputFormatter> InputFormatters
        {
            get
            {
                if (this.inputFormatters == null)
                {
                    this.inputFormatters = this.Options.InputFormatters;
                }

                return this.inputFormatters;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(InputFormatters));
                this.inputFormatters = value;
            }
        }

        public override IList<IModelBinder> ModelBinders
        {
            get
            {
                if (this.modelBinders == null)
                {
                    this.modelBinders = this.Options.ModelBinders;
                }

                return this.modelBinders;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(ModelBinders));
                this.modelBinders = value;
            }
        }

        public override IList<IModelValidatorProvider> ValidatorProviders
        {
            get
            {
                if (this.validatorProviders == null)
                {
                    this.validatorProviders = this.Options.ModelValidatorProviders;
                }

                return this.validatorProviders;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(ValidatorProviders));
                this.validatorProviders = value;
            }
        }

        public override IList<IValueProvider> ValueProviders
        {
            get
            {
                if (this.valueProviders == null)
                {
                    var factoryContext = new ValueProviderFactoryContext(this);

                    var valueProviderFactories = this.Options.ValueProviderFactories;
                    for (var i = 0; i < valueProviderFactories.Count; i++)
                    {
                        var factory = valueProviderFactories[i];
                        factory.CreateValueProviderAsync(factoryContext).Wait();
                    }

                    this.valueProviders = factoryContext.ValueProviders;
                }

                return this.valueProviders;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(ValueProviders));
                this.valueProviders = value;
            }
        }

        private HttpTestContext TestContext
        {
            get
            {
                return this.testContext;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(TestContext));
                this.testContext = value;
            }
        }

        private IServiceProvider Services => this.testContext.HttpContext.RequestServices;

        private MvcOptions Options
        {
            get
            {
                if (this.options == null)
                {
                    this.options = this.Services.GetRequiredService<IOptions<MvcOptions>>().Value;
                }

                return this.options;
            }
        }

        public static ControllerContext FromActionContext(HttpTestContext testContext, ActionContext actionContext)
        {
            CommonValidator.CheckForNullReference(testContext, nameof(HttpTestContext));
            CommonValidator.CheckForNullReference(actionContext, nameof(ActionContext));

            actionContext.HttpContext = actionContext.HttpContext ?? testContext.HttpContext;
            actionContext.RouteData = actionContext.RouteData ?? testContext.RouteData ?? new RouteData();
            actionContext.ActionDescriptor = actionContext.ActionDescriptor ?? new ControllerActionDescriptor();

            return new MockedControllerContext(testContext, actionContext);
        }

        private void PrepareControllerContext(HttpTestContext testContext)
        {
            this.TestContext = testContext;
            this.HttpContext = testContext.HttpContext;
            this.RouteData = testContext.RouteData ?? new RouteData();
            TestHelper.SetActionContextToAccessor(this);
        }
    }
}
