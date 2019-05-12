namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.ActionResults.Base;
    using Builders.Contracts.Base;
    using Internal.Contracts.ActionResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Contains extension methods for <see cref="IBaseTestBuilderWithFileResult{TFileResultTestBuilder}"/>.
    /// </summary>
    public static class BaseTestBuilderWithFileResultExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="FileResult"/>
        /// has the same file download name as the provided one.
        /// </summary>
        /// <param name="baseTestBuilderWithFileResult">
        /// Instance of <see cref="IBaseTestBuilderWithFileResult{TFileResultTestBuilder}"/> type.
        /// </param>
        /// <param name="fileDownloadName">File download name as string.</param>
        /// <returns>The same <see cref="FileResult"/> test builder.</returns>
        public static TFileResultTestBuilder WithDownloadName<TFileResultTestBuilder>(
            this IBaseTestBuilderWithFileResult<TFileResultTestBuilder> baseTestBuilderWithFileResult,
            string fileDownloadName) 
            where TFileResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithFileResult);
            
            var actualFileDownloadName = GetFileResult(actualBuilder).FileDownloadName;

            if (fileDownloadName != actualFileDownloadName)
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "download name",
                    $"to be {fileDownloadName.GetErrorMessageName()}",
                    $"instead received {(actualFileDownloadName != string.Empty ? $"'{actualFileDownloadName}'" : "empty string")}");
            }

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="FileResult"/>
        /// has the same last modified value as the provided one.
        /// </summary>
        /// <param name="baseTestBuilderWithFileResult">
        /// Instance of <see cref="IBaseTestBuilderWithFileResult{TFileResultTestBuilder}"/> type.
        /// </param>
        /// <param name="lastModified">Last modified date time offset.</param>
        /// <returns>The same <see cref="FileResult"/> test builder.</returns>
        public static TFileResultTestBuilder WithLastModified<TFileResultTestBuilder>(
            this IBaseTestBuilderWithFileResult<TFileResultTestBuilder> baseTestBuilderWithFileResult,
            DateTimeOffset? lastModified)
            where TFileResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithFileResult);

            var actualLastModified = GetFileResult(actualBuilder).LastModified;

            if (lastModified != actualLastModified)
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "last modified",
                    $"to be {lastModified.GetErrorMessageName()}",
                    $"instead received {actualLastModified.GetErrorMessageName()}");
            }

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="FileResult"/>
        /// has the same entity tag as the provided one.
        /// </summary>
        /// <param name="baseTestBuilderWithFileResult">
        /// Instance of <see cref="IBaseTestBuilderWithFileResult{TFileResultTestBuilder}"/> type.
        /// </param>
        /// <param name="entityTag">Entity tag header value.</param>
        /// <returns>The same <see cref="FileResult"/> test builder.</returns>
        public static TFileResultTestBuilder WithEntityTag<TFileResultTestBuilder>(
            this IBaseTestBuilderWithFileResult<TFileResultTestBuilder> baseTestBuilderWithFileResult,
            EntityTagHeaderValue entityTag)
            where TFileResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithFileResult);

            var actualEntityTag = GetFileResult(actualBuilder).EntityTag;

            if (Reflection.AreNotDeeplyEqual(entityTag, actualEntityTag))
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "entity tag",
                    $"to be {entityTag.GetErrorMessageName()}",
                    $"instead received {actualEntityTag.GetErrorMessageName()}");
            }

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="FileResult"/>
        /// has range processing enabled.
        /// </summary>
        /// <param name="baseTestBuilderWithFileResult">
        /// Instance of <see cref="IBaseTestBuilderWithFileResult{TFileResultTestBuilder}"/> type.
        /// </param>
        /// <param name="enableRangeProcessing">Expected boolean value.</param>
        /// <returns>The same <see cref="FileResult"/> test builder.</returns>
        public static TFileResultTestBuilder WithEnabledRangeProcessing<TFileResultTestBuilder>(
            this IBaseTestBuilderWithFileResult<TFileResultTestBuilder> baseTestBuilderWithFileResult,
            bool enableRangeProcessing)
            where TFileResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithFileResult);

            var actualEnableRangeProcessing = GetFileResult(actualBuilder).EnableRangeProcessing;

            if (enableRangeProcessing != actualEnableRangeProcessing)
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "range processing",
                    $"to be {(enableRangeProcessing ? "enabled" : "disabled")}",
                    $"in fact it was {(actualEnableRangeProcessing ? "enabled" : "disabled")}");
            }

            return actualBuilder.ResultTestBuilder;
        }

        private static FileResult GetFileResult<TFileResultTestBuilder>(
            IBaseTestBuilderWithFileResultInternal<TFileResultTestBuilder> baseTestBuilderWithFileResult)
            where TFileResultTestBuilder : IBaseTestBuilderWithActionResult
            => baseTestBuilderWithFileResult
                .TestContext
                .MethodResultAs<FileResult>();

        private static IBaseTestBuilderWithFileResultInternal<TFileResultTestBuilder>
            GetActualBuilder<TFileResultTestBuilder>(
                IBaseTestBuilderWithFileResult<TFileResultTestBuilder> baseTestBuilderWithFileResult)
            where TFileResultTestBuilder : IBaseTestBuilderWithActionResult
            => (IBaseTestBuilderWithFileResultInternal<TFileResultTestBuilder>)baseTestBuilderWithFileResult;
    }
}
