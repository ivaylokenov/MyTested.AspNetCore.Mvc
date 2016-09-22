namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Builders.Contracts.ActionResults.Redirect;
    using Utilities.Validators;
    using Internal.TestContexts;

    /// <summary>
    /// Contains extension methods for <see cref="IRedirectTestBuilder"/>.
    /// </summary>
    public static class RedirectTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/> or <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/> redirects to specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="builder">Instance of <see cref="IRedirectTestBuilder"/> type.</param>
        /// <param name="actionCall">Method call expression indicating the expected redirect action.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        public static IAndRedirectTestBuilder To<TController>(
            this IRedirectTestBuilder builder,
            Expression<Action<TController>> actionCall)
            where TController : class
        {
            return ProcessRouteLambdaExpression<TController>(builder, actionCall);
        }

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/> or <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/> redirects to specific asynchronous action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="builder">Instance of <see cref="IRedirectTestBuilder"/> type.</param>
        /// <param name="actionCall">Method call expression indicating the expected asynchronous redirect action.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        public static IAndRedirectTestBuilder To<TController>(
            this IRedirectTestBuilder builder, 
            Expression<Func<TController, Task>> actionCall)
            where TController : class
        {
            return ProcessRouteLambdaExpression<TController>(builder, actionCall);
        }

        private static IAndRedirectTestBuilder ProcessRouteLambdaExpression<TController>(
            dynamic redirectTestBuilder,
            LambdaExpression actionCall)
        {
            redirectTestBuilder.IncludeCountCheck = false;
            
            ExpressionLinkValidator.Validate(
                redirectTestBuilder.TestContext,
                LinkGenerationTestContext.FromRedirectResult(redirectTestBuilder.ActionResult),
                actionCall,
                new Action<string, string, string>((pr, exp, act) =>
                    redirectTestBuilder.ThrowNewRedirectResultAssertionException(pr, exp, act)));

            return redirectTestBuilder;
        }
    }
}
