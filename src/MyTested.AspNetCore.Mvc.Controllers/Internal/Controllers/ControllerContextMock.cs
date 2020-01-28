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
        private ControllerContextMock(ActionContext actionContext)
            : base(actionContext)
        {
            this.PrepareControllerContext(actionContext);
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

            return new ControllerContextMock(actionContext);
        }

        private void PrepareControllerContext(ActionContext actionContext)
        {
            this.HttpContext = actionContext.HttpContext;
            this.RouteData = actionContext.RouteData;
            this.ValueProviderFactories = this.ValueProviderFactories ?? new List<IValueProviderFactory>();

            TestHelper.SetActionContextToAccessor(this);
        }
    }
}
