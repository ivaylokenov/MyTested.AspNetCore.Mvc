namespace MyTested.AspNetCore.Mvc.Internal.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Routing;
    using TestContexts;
    using Utilities.Validators;

    public class MockedControllerContext : ControllerContext
    {
        private HttpTestContext testContext;

        public MockedControllerContext(HttpTestContext testContext)
        {
            this.PrepareControllerContext(testContext);
        }

        private MockedControllerContext(HttpTestContext testContext, ActionContext actionContext)
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

        private IServiceProvider Services => this.testContext.HttpContext.RequestServices;
        
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
            ControllerTestHelper.SetActionContextToAccessor(this);
        }
    }
}
