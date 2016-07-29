namespace MyTested.AspNetCore.Mvc.Builders.Controllers
{
    using System;
    using System.Linq.Expressions;
    using Contracts.Controllers;
    using Internal.Application;
    using Internal.Routing;
    using Utilities.Extensions;

    /// <content>
    /// Used for building the controller which will be tested.
    /// </content>
    public partial class ControllerBuilder<TController>
    {
        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithRouteData()
        {
            return this.WithRouteData(null);
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithRouteData(object additionalRouteValues)
        {
            this.resolveRouteValues = true;
            this.additionalRouteValues = additionalRouteValues; 
            return this;
        }
        
        private void SetRouteData(LambdaExpression actionCall)
        {
            if (this.resolveRouteValues)
            {
                this.TestContext.RouteData = RouteExpressionParser.ResolveRouteData(TestApplication.Router, actionCall);
                this.TestContext.RouteData.Values.Add(this.additionalRouteValues);
            }
        }
    }
}
