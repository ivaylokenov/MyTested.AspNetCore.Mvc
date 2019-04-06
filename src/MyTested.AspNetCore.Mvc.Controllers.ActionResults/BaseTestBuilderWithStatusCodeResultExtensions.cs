namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.Base;
    using Builders.Contracts.Base;
    using Internal.Contracts.ActionResults;
    using Utilities.Validators;

    using SystemHttpStatusCode = System.Net.HttpStatusCode;

    /// <summary>
    /// Contains extension methods for <see cref="IBaseTestBuilderWithStatusCodeResult{TStatusCodeResultTestBuilder}"/>.
    /// </summary>
    public static class BaseTestBuilderWithStatusCodeResultExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult"/>
        /// has the same status code as the provided one.
        /// </summary>
        /// <param name="baseTestBuilderWithStatusCodeResult">
        /// Instance of <see cref="IBaseTestBuilderWithStatusCodeResult{TStatusCodeResultTestBuilder}"/> type.
        /// </param>
        /// <param name="statusCode">Status code as integer.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult"/> test builder.</returns>
        public static TStatusCodeResultTestBuilder WithStatusCode<TStatusCodeResultTestBuilder>(
            this IBaseTestBuilderWithStatusCodeResult<TStatusCodeResultTestBuilder> baseTestBuilderWithStatusCodeResult,
            int statusCode)
            where TStatusCodeResultTestBuilder : IBaseTestBuilderWithActionResult
            => baseTestBuilderWithStatusCodeResult
                .WithStatusCode((SystemHttpStatusCode)statusCode);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult"/>
        /// has the same status code as the provided <see cref="System.Net.HttpStatusCode"/>.
        /// </summary>
        /// <param name="baseTestBuilderWithStatusCodeResult">
        /// Instance of <see cref="IBaseTestBuilderWithStatusCodeResult{TStatusCodeResultTestBuilder}"/> type.
        /// </param>
        /// <param name="statusCode"><see cref="System.Net.HttpStatusCode"/> enumeration.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult"/> test builder.</returns>
        public static TStatusCodeResultTestBuilder WithStatusCode<TStatusCodeResultTestBuilder>(
            this IBaseTestBuilderWithStatusCodeResult<TStatusCodeResultTestBuilder> baseTestBuilderWithStatusCodeResult,
            SystemHttpStatusCode statusCode)
            where TStatusCodeResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = 
                (IBaseTestBuilderWithStatusCodeResultInternal<TStatusCodeResultTestBuilder>)baseTestBuilderWithStatusCodeResult;

            HttpStatusCodeValidator.ValidateHttpStatusCode(
                actualBuilder.TestContext.MethodResult,
                statusCode,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }
    }
}
