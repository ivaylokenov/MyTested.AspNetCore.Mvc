namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.Created;
    using Internal.Contracts.ActionResults;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="ICreatedTestBuilder"/>.
    /// </summary>
    public static class CreatedTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/>
        /// returns created at specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="builder">Instance of <see cref="ICreatedTestBuilder"/> type.</param>
        /// <param name="actionCall">Method call expression indicating the expected action.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        public static IAndCreatedTestBuilder At<TController>(
            this ICreatedTestBuilder builder,
            Expression<Action<TController>> actionCall)
            where TController : class
            => ProcessRouteLambdaExpression(builder, actionCall);

        /// <summary>
        /// Tests whether <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/>
        /// returns created at specific asynchronous action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="builder">Instance of <see cref="ICreatedTestBuilder"/> type.</param>
        /// <param name="actionCall">Method call expression indicating the expected asynchronous created action.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        public static IAndCreatedTestBuilder At<TController>(
            this ICreatedTestBuilder builder,
            Expression<Func<TController, Task>> actionCall)
            where TController : class
            => ProcessRouteLambdaExpression(builder, actionCall);

        private static IAndCreatedTestBuilder ProcessRouteLambdaExpression(
            ICreatedTestBuilder createdTestBuilder,
            LambdaExpression actionCall)
        {
            var actualBuilder = GetActualBuilder(createdTestBuilder);

            actualBuilder.IncludeCountCheck = false;

            var controllerTestContext = actualBuilder.TestContext as ControllerTestContext;
            var actionResult = actualBuilder.TestContext.MethodResult as IActionResult;

            ExpressionLinkValidator.Validate(
                controllerTestContext,
                LinkGenerationTestContext.FromCreatedResult(actionResult),
                actionCall,
                actualBuilder.ThrowNewFailedValidationException);

            return (IAndCreatedTestBuilder)createdTestBuilder;
        }

        private static IBaseTestBuilderWithRouteValuesResultInternal<IAndCreatedTestBuilder>
            GetActualBuilder(ICreatedTestBuilder createdTestBuilder)
            => (IBaseTestBuilderWithRouteValuesResultInternal<IAndCreatedTestBuilder>)createdTestBuilder;
    }
}
