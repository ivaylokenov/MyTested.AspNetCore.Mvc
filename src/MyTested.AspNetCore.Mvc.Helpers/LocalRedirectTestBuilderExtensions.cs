namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Builders.ActionResults.LocalRedirect;
    using Builders.Contracts.ActionResults.LocalRedirect;
    using Internal.TestContexts;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="ILocalRedirectTestBuilder"/>.
    /// </summary>
    public static class LocalRedirectTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.LocalRedirectResult"/> redirects to specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="builder">Instance of <see cref="ILocalRedirectTestBuilder"/> type.</param>
        /// <param name="actionCall">Method call expression indicating the expected redirect action.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        public static IAndLocalRedirectTestBuilder To<TController>(
            this ILocalRedirectTestBuilder builder,
            Expression<Action<TController>> actionCall)
            where TController : class 
            => ProcessRouteLambdaExpression<TController>(builder, actionCall);

        /// <summary>
        /// Tests whether <see cref="Microsoft.AspNetCore.Mvc.LocalRedirectResult"/> redirects to specific asynchronous action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="builder">Instance of <see cref="ILocalRedirectTestBuilder"/> type.</param>
        /// <param name="actionCall">Method call expression indicating the expected asynchronous action.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        public static IAndLocalRedirectTestBuilder To<TController>(
            this ILocalRedirectTestBuilder builder, 
            Expression<Func<TController, Task>> actionCall)
            where TController : class 
            => ProcessRouteLambdaExpression<TController>(builder, actionCall);

        private static IAndLocalRedirectTestBuilder ProcessRouteLambdaExpression<TController>(
            ILocalRedirectTestBuilder builder,
            LambdaExpression actionCall)
        {
            var actualBuilder = (LocalRedirectTestBuilder)builder;

            ExpressionLinkValidator.Validate(
                actualBuilder.TestContext,
                LinkGenerationTestContext.FromLocalRedirectResult(actualBuilder.ActionResult),
                actionCall,
                actualBuilder.ThrowNewRedirectResultAssertionException);

            return actualBuilder;
        }
    }
}
