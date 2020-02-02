namespace MyTested.AspNetCore.Mvc.Builders.Controllers
{
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Internal.Contracts;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities;
    using Utilities.Extensions;

    /// <content>
    /// Used for building the controller which will be tested.
    /// </content>
    public abstract partial class BaseControllerBuilder<TController, TBuilder>
    {
        protected override void ProcessAndValidateMethod(LambdaExpression methodCall, MethodInfo methodInfo)
        {
            this.SetActionDescriptor(methodInfo);

            if (this.EnabledModelStateValidation)
            {
                this.ValidateModelState(methodCall);
            }
        }

        private void SetActionDescriptor(MethodInfo methodInfo)
        {
            var controllerContext = this.TestContext.ComponentContext;
            if (controllerContext.ActionDescriptor?.MethodInfo == null)
            {
                var controllerActionDescriptorCache = this.Services.GetService<IControllerActionDescriptorCache>();
                if (controllerActionDescriptorCache != null)
                {
                    controllerContext.ActionDescriptor
                        = controllerActionDescriptorCache.TryGetActionDescriptor(methodInfo);
                }
            }
        }

        private void ValidateModelState(LambdaExpression actionCall)
        {
            var arguments = ExpressionParser.ResolveMethodArguments(actionCall).ToArray();
            if (arguments.Any())
            {
                var validator = this.Services.GetRequiredService<IObjectModelValidator>();
                arguments.ForEach(argument =>
                {
                    if (argument.Value != null)
                    {
                        validator.Validate(this.TestContext.ComponentContext, argument.Value);
                    }
                });
            }
        }
    }
}
