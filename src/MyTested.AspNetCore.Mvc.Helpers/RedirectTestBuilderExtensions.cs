namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Builders.Contracts.ActionResults.Redirect;
    using Internal.Contracts.ActionResults;
    using Utilities.Validators;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contains extension methods for <see cref="IRedirectTestBuilder"/>.
    /// </summary>
    public static class RedirectTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/>
        /// redirects to specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="builder">Instance of <see cref="IRedirectTestBuilder"/> type.</param>
        /// <param name="actionCall">Method call expression indicating the expected redirect action.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        public static IAndRedirectTestBuilder To<TController>(
            this IRedirectTestBuilder builder,
            Expression<Action<TController>> actionCall)
            where TController : class 
            => ProcessRouteLambdaExpression(builder, actionCall);

        /// <summary>
        /// Tests whether <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/>
        /// redirects to specific asynchronous action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="builder">Instance of <see cref="IRedirectTestBuilder"/> type.</param>
        /// <param name="actionCall">Method call expression indicating the expected asynchronous redirect action.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        public static IAndRedirectTestBuilder To<TController>(
            this IRedirectTestBuilder builder, 
            Expression<Func<TController, Task>> actionCall)
            where TController : class 
            => ProcessRouteLambdaExpression(builder, actionCall);

        private static IAndRedirectTestBuilder ProcessRouteLambdaExpression(
            IRedirectTestBuilder redirectTestBuilder,
            LambdaExpression actionCall)
        {
            var actualBuilder = GetActualBuilder(redirectTestBuilder);

            actualBuilder.IncludeCountCheck = false;

            var controllerTestContext = actualBuilder.TestContext as ControllerTestContext;
            var actionResult = actualBuilder.TestContext.MethodResult as IActionResult;

            ExpressionLinkValidator.Validate(
                controllerTestContext, 
                LinkGenerationTestContext.FromRedirectResult(actionResult),
                actionCall,
                actualBuilder.ThrowNewFailedValidationException);

            return (IAndRedirectTestBuilder)actualBuilder;
        }
        
        private static IBaseTestBuilderWithRouteValuesResultInternal<IAndRedirectTestBuilder>
            GetActualBuilder(IRedirectTestBuilder redirectTestBuilder)
            => (IBaseTestBuilderWithRouteValuesResultInternal<IAndRedirectTestBuilder>)redirectTestBuilder;
    }
}
