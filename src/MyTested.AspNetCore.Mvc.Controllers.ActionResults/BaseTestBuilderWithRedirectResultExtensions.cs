namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.ActionResults.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Uri;
    using Internal.Contracts.ActionResults;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IBaseTestBuilderWithRedirectResult{TRedirectResultTestBuilder}"/>.
    /// </summary>
    public static class BaseTestBuilderWithRedirectResultExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// has specific location provided by string.
        /// </summary>
        /// <param name="baseTestBuilderWithRedirectResult">
        /// Instance of <see cref="IBaseTestBuilderWithRedirectResult{TRedirectResultTestBuilder}"/> type.
        /// </param>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TRedirectResultTestBuilder ToUrl<TRedirectResultTestBuilder>(
            this IBaseTestBuilderWithRedirectResult<TRedirectResultTestBuilder> baseTestBuilderWithRedirectResult,
            string location)
            where TRedirectResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithRedirectResult);

            var uri = LocationValidator.ValidateAndGetWellFormedUriString(
                location,
                actualBuilder.ThrowNewFailedValidationException);

            return baseTestBuilderWithRedirectResult.ToUrl(uri);
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// has specific location provided by <see cref="Uri"/>.
        /// </summary>
        /// <param name="baseTestBuilderWithRedirectResult">
        /// Instance of <see cref="IBaseTestBuilderWithRedirectResult{TRedirectResultTestBuilder}"/> type.
        /// </param>
        /// <param name="location">Expected location as <see cref="Uri"/>.</param>
        /// <returns>The same redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TRedirectResultTestBuilder ToUrl<TRedirectResultTestBuilder>(
            this IBaseTestBuilderWithRedirectResult<TRedirectResultTestBuilder> baseTestBuilderWithRedirectResult,
            Uri location)
            where TRedirectResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithRedirectResult);

            LocationValidator.ValidateUri(
                actualBuilder.TestContext.MethodResult,
                location.OriginalString,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// has specific location provided by builder.
        /// </summary>
        /// <param name="baseTestBuilderWithRedirectResult">
        /// Instance of <see cref="IBaseTestBuilderWithRedirectResult{TRedirectResultTestBuilder}"/> type.
        /// </param>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TRedirectResultTestBuilder ToUrl<TRedirectResultTestBuilder>(
            this IBaseTestBuilderWithRedirectResult<TRedirectResultTestBuilder> baseTestBuilderWithRedirectResult,
            Action<IUriTestBuilder> uriTestBuilder)
            where TRedirectResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithRedirectResult);

            LocationValidator.ValidateLocation(
                actualBuilder.TestContext.MethodResult,
                uriTestBuilder,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// location passes the given assertions.
        /// </summary>
        /// <param name="baseTestBuilderWithRedirectResult">
        /// Instance of <see cref="IBaseTestBuilderWithRedirectResult{TRedirectResultTestBuilder}"/> type.
        /// </param>
        /// <param name="assertions">Action containing all assertions for the location.</param>
        /// <returns>The same redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TRedirectResultTestBuilder ToUrlPassing<TRedirectResultTestBuilder>(
            this IBaseTestBuilderWithRedirectResult<TRedirectResultTestBuilder> baseTestBuilderWithRedirectResult,
            Action<string> assertions)
            where TRedirectResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithRedirectResult);

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var url = actualBuilder
                    .TestContext
                    .MethodResult
                    .AsDynamic()
                    .Url;

                assertions(url);
            });
            
            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// location passes the given predicate.
        /// </summary>
        /// <param name="baseTestBuilderWithRedirectResult">
        /// Instance of <see cref="IBaseTestBuilderWithRedirectResult{TRedirectResultTestBuilder}"/> type.
        /// </param>
        /// <param name="predicate">Predicate testing the location.</param>
        /// <returns>The same redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TRedirectResultTestBuilder ToUrlPassing<TRedirectResultTestBuilder>(
            this IBaseTestBuilderWithRedirectResult<TRedirectResultTestBuilder> baseTestBuilderWithRedirectResult,
            Func<string, bool> predicate)
            where TRedirectResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithRedirectResult);

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var url = actualBuilder
                    .TestContext
                    .MethodResult
                    .AsDynamic()
                    .Url;

                if (!predicate(url))
                {
                    actualBuilder.ThrowNewFailedValidationException(
                        $"location ('{url}')",
                        "to pass the given predicate",
                        "it failed");
                }
            });
            
            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> is permanent.
        /// </summary>
        /// <param name="baseTestBuilderWithRedirectResult">
        /// Instance of <see cref="IBaseTestBuilderWithRedirectResult{TRedirectResultTestBuilder}"/> type.
        /// </param>
        /// <param name="permanent">Expected boolean value.</param>
        /// <returns>The same redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TRedirectResultTestBuilder Permanent<TRedirectResultTestBuilder>(
            this IBaseTestBuilderWithRedirectResult<TRedirectResultTestBuilder> baseTestBuilderWithRedirectResult,
            bool permanent = true)
            where TRedirectResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithRedirectResult);

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualPermanent = actualBuilder
                    .TestContext
                    .MethodResult
                    .AsDynamic()
                    .Permanent;

                if (permanent != actualPermanent)
                {
                    actualBuilder.ThrowNewFailedValidationException(
                        "to",
                        $"{(permanent ? string.Empty : "not ")}be permanent",
                        $"in fact it was{(actualPermanent ? string.Empty : " not")}");
                }
            });

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> is preserving the request method.
        /// </summary>
        /// <param name="baseTestBuilderWithRedirectResult">
        /// Instance of <see cref="IBaseTestBuilderWithRedirectResult{TRedirectResultTestBuilder}"/> type.
        /// </param>
        /// <param name="preserveMethod">Expected boolean value.</param>
        /// <returns>The same redirect <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TRedirectResultTestBuilder PreservingMethod<TRedirectResultTestBuilder>(
            this IBaseTestBuilderWithRedirectResult<TRedirectResultTestBuilder> baseTestBuilderWithRedirectResult,
            bool preserveMethod = true)
            where TRedirectResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithRedirectResult);

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualPreserveMethod = actualBuilder
                    .TestContext
                    .MethodResult
                    .AsDynamic()
                    .PreserveMethod;

                if (preserveMethod != actualPreserveMethod)
                {
                    actualBuilder.ThrowNewFailedValidationException(
                        "to",
                        $"{(preserveMethod ? string.Empty : "not ")}preserve the request method",
                        $"in fact it did{(actualPreserveMethod ? string.Empty : " not")}");
                }
            });

            return actualBuilder.ResultTestBuilder;
        }

        private static IBaseTestBuilderWithRedirectResultInternal<TRedirectResultTestBuilder>
            GetActualBuilder<TRedirectResultTestBuilder>(
                IBaseTestBuilderWithRedirectResult<TRedirectResultTestBuilder> baseTestBuilderWithRedirectResult)
            where TRedirectResultTestBuilder : IBaseTestBuilderWithActionResult
            => (IBaseTestBuilderWithRedirectResultInternal<TRedirectResultTestBuilder>)baseTestBuilderWithRedirectResult;
    }
}
