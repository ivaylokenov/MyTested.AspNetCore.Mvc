namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.Accepted;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IAcceptedTestBuilder"/>.
    /// </summary>
    public static class AcceptedTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether <see cref="AcceptedAtActionResult"/> or <see cref="AcceptedAtRouteResult"/>
        /// returns accepted at specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="builder">Instance of <see cref="IAcceptedTestBuilder"/> type.</param>
        /// <param name="actionCall">Method call expression indicating the expected action.</param>
        /// <returns>The same <see cref="IAndAcceptedTestBuilder"/>.</returns>
        public static IAndAcceptedTestBuilder At<TController>(
            this IAcceptedTestBuilder builder,
            Expression<Action<TController>> actionCall)
            where TController : class
            => ProcessRouteLambdaExpression(builder, actionCall);

        /// <summary>
        /// Tests whether <see cref="AcceptedAtActionResult"/> or <see cref="AcceptedAtRouteResult"/>
        /// returns accepted at specific asynchronous action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="builder">Instance of <see cref="IAcceptedTestBuilder"/> type.</param>
        /// <param name="actionCall">Method call expression indicating the expected asynchronous accepted action.</param>
        /// <returns>The same <see cref="IAndAcceptedTestBuilder"/>.</returns>
        public static IAndAcceptedTestBuilder At<TController>(
            this IAcceptedTestBuilder builder,
            Expression<Func<TController, Task>> actionCall)
            where TController : class
            => ProcessRouteLambdaExpression(builder, actionCall);

        private static IAndAcceptedTestBuilder ProcessRouteLambdaExpression(
            IAcceptedTestBuilder acceptedTestBuilder,
            LambdaExpression actionCall)
        {
            var actualBuilder = GetActualBuilder(acceptedTestBuilder);

            actualBuilder.IncludeCountCheck = false;

            var controllerTestContext = actualBuilder.TestContext as ControllerTestContext;
            var actionResult = actualBuilder.TestContext.MethodResult as IActionResult;

            ExpressionLinkValidator.Validate(
                controllerTestContext,
                LinkGenerationTestContext.FromAcceptedResult(actionResult),
                actionCall,
                actualBuilder.ThrowNewFailedValidationException);

            return (IAndAcceptedTestBuilder)acceptedTestBuilder;
        }

        private static IBaseTestBuilderWithRouteValuesResultInternal<IAndAcceptedTestBuilder>
            GetActualBuilder(IAcceptedTestBuilder acceptedTestBuilder)
            => (IBaseTestBuilderWithRouteValuesResultInternal<IAndAcceptedTestBuilder>)acceptedTestBuilder;
    }
}
