using Microsoft.Extensions.Primitives;

namespace MyTested.AspNetCore.Mvc.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Validator class containing content type validation logic.
    /// </summary>
    public static class ContentTypeValidator
    {
        /// <summary>
        /// Validates whether two content types are the same.
        /// </summary>
        /// <param name="expectedContentType">Expected content type.</param>
        /// <param name="actualContentType">Actual content type.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateContentType(
            string expectedContentType,
            string actualContentType,
            Action<string, string, string> failedValidationAction)
        {
            if (expectedContentType != actualContentType)
            {
                failedValidationAction(
                    "ContentType",
                    $"to be {expectedContentType.GetErrorMessageName()}",
                    $"instead received {actualContentType.GetErrorMessageName()}");
            }
        }

        /// <summary>
        /// Validates whether ContentType is the same as the provided one from action result containing such property.
        /// </summary>
        /// <param name="result">Component result with ContentType.</param>
        /// <param name="expectedContentType">Expected content type.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateContentType(
            dynamic result,
            string expectedContentType,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                ValidateContentType(
                    expectedContentType,
                    (string)result.ContentType,
                    failedValidationAction);
            });
        }

        /// <summary>
        /// Validates whether action result ContentTypes contains the provided content type from action result containing such property.
        /// </summary>
        /// <param name="result">Component result with ContentTypes.</param>
        /// <param name="expectedContentType">Expected content type.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateContainingOfContentType(
            dynamic result,
            MediaTypeHeaderValue expectedContentType,
            Action<string, string, string> failedValidationAction)
        {
            var contentTypes = (MediaTypeCollection)TryGetContentTypesCollection(result);
            if (!contentTypes.Contains(expectedContentType.MediaType.Value))
            {
                failedValidationAction(
                    "content types",
                    $"to contain {expectedContentType.MediaType}",
                    "such was not found");
            }
        }

        /// <summary>
        /// Validates whether action result ContentTypes contains the provided content types from action result containing such property.
        /// </summary>
        /// <param name="result">Component result with ContentTypes.</param>
        /// <param name="contentTypes">Expected content types.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateContentTypes(
            dynamic result,
            IEnumerable<MediaTypeHeaderValue> contentTypes,
            Action<string, string, string> failedValidationAction)
        {
            var actualContentTypes = (IList<string>)SortContentTypes(TryGetContentTypesCollection(result));
            var expectedContentTypes = SortContentTypes(contentTypes.Select(m => m.MediaType.Value));

            if (actualContentTypes.Count != expectedContentTypes.Count)
            {
                failedValidationAction(
                    "content types",
                    $"to have {expectedContentTypes.Count} {(expectedContentTypes.Count != 1 ? "items" : "item")}",
                    $"instead found {actualContentTypes.Count}");
            }

            for (int i = 0; i < actualContentTypes.Count; i++)
            {
                var actualContentType = actualContentTypes[i];
                var expectedContentType = expectedContentTypes[i];
                if (actualContentType != expectedContentType)
                {
                    failedValidationAction(
                        "content types",
                        $"to contain {expectedContentType}",
                        "none was found");
                }
            }
        }

        private static IList<string> SortContentTypes(IEnumerable<string> contentTypes)
            => contentTypes
                .OrderBy(m => m)
                .ToList();

        private static IEnumerable<string> TryGetContentTypesCollection(dynamic actionResult)
        {
            MediaTypeCollection contentTypes = null;

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                contentTypes = (MediaTypeCollection)actionResult.ContentTypes;
            });

            return contentTypes;
        }
    }
}
