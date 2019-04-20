namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.Base;
    using Builders.Contracts.Base;
    using Internal.Contracts.ActionResults;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IBaseTestBuilderWithFileResult{TFileResultTestBuilder}"/>.
    /// </summary>
    public static class BaseTestBuilderWithFileResultExtensions
    {
        private const string DownloadName = "download name";

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.FileResult"/>
        /// has the same file download name as the provided one.
        /// </summary>
        /// <param name="baseTestBuilderWithFileResult">
        /// Instance of <see cref="IBaseTestBuilderWithFileResult{TFileResultTestBuilder}"/> type.
        /// </param>
        /// <param name="fileDownloadName">File download name as string.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.FileResult"/> test builder.</returns>
        public static TFileResultTestBuilder WithDownloadName<TFileResultTestBuilder>(
            this IBaseTestBuilderWithFileResult<TFileResultTestBuilder> baseTestBuilderWithFileResult,
            string fileDownloadName) 
            where TFileResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithFileResult);

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualFileDownloadName = actualBuilder
                    .TestContext
                    .MethodResult
                    .AsDynamic()
                    .FileDownloadName;

                if (fileDownloadName != actualFileDownloadName)
                {
                    actualBuilder.ThrowNewFailedValidationException(
                        DownloadName,
                        $"to be {fileDownloadName.GetErrorMessageName()}",
                        $"instead received {(actualFileDownloadName != string.Empty ? $"'{actualFileDownloadName}'" : "empty string")}");
                }
            });

            return actualBuilder.ResultTestBuilder;
        }

        private static IBaseTestBuilderWithFileResultInternal<TFileResultTestBuilder>
            GetActualBuilder<TFileResultTestBuilder>(
                IBaseTestBuilderWithFileResult<TFileResultTestBuilder> baseTestBuilderWithFileResult)
            where TFileResultTestBuilder : IBaseTestBuilderWithActionResult
            => (IBaseTestBuilderWithFileResultInternal<TFileResultTestBuilder>)baseTestBuilderWithFileResult;
    }
}
