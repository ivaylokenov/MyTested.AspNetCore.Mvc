namespace MyTested.AspNetCore.Mvc.Internal.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Routing;
    using TestContexts;
    using Utilities.Validators;

    public class ControllerContextMock : ControllerContext
    {
        private HttpTestContext testContext;

        private ControllerContextMock(HttpTestContext testContext, ActionContext actionContext)
            : base(actionContext)
        {
            this.PrepareControllerContext(testContext);
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

        public static ControllerContext Default(HttpTestContext testContext)
            => FromActionContext(testContext, new ActionContext());

        public static ControllerContext FromActionContext(HttpTestContext testContext, ActionContext actionContext)
        {
            CommonValidator.CheckForNullReference(testContext, nameof(HttpTestContext));
            CommonValidator.CheckForNullReference(actionContext, nameof(ActionContext));

            actionContext.HttpContext = actionContext.HttpContext ?? testContext.HttpContext;
            actionContext.RouteData = actionContext.RouteData ?? testContext.RouteData ?? new RouteData();
            actionContext.ActionDescriptor = actionContext.ActionDescriptor ?? ActionDescriptorMock.Default;

            return new ControllerContextMock(testContext, actionContext);
        }

        private void PrepareControllerContext(HttpTestContext testContext)
        {
            this.TestContext = testContext;
            this.HttpContext = testContext.HttpContext;
            this.RouteData = testContext.RouteData ?? new RouteData();
            this.ValueProviderFactories = this.ValueProviderFactories ?? new List<IValueProviderFactory>();

            ControllerTestHelper.SetActionContextToAccessor(this);
        }
    }
}
