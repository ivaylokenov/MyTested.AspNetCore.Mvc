namespace MyTested.Mvc.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Net.Http.Headers;
    using Internal.Extensions;
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
            MediaTypeHeaderValue expectedContentType,
            MediaTypeHeaderValue actualContentType,
            Action<string, string, string> failedValidationAction)
        {
            if ((expectedContentType == null && actualContentType != null)
                || (expectedContentType != null && actualContentType == null)
                || (expectedContentType != null && expectedContentType.MediaType != actualContentType.MediaType))
            {
                failedValidationAction(
                    "ContentType",
                    $"to be {(expectedContentType != null ? expectedContentType.MediaType.GetErrorMessageName() : "null")}",
                    $"instead received {(actualContentType != null ? actualContentType.MediaType.GetErrorMessageName() : "null")}");
            }
        }

        /// <summary>
        /// Validates whether ContentType is the same as the provided one from action result containing such property.
        /// </summary>
        /// <param name="actionResult">Action result with ContentType.</param>
        /// <param name="expectedContentType">Expected content type.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateContentType(
            dynamic actionResult,
            MediaTypeHeaderValue expectedContentType,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                ValidateContentType(
                    expectedContentType,
                    (MediaTypeHeaderValue)actionResult.ContentType,
                    failedValidationAction);
            });
        }

        /// <summary>
        /// Validates whether action result ContentTypes contains the provided content type from action result containing such property.
        /// </summary>
        /// <param name="actionResult">Action result with ContentTypes.</param>
        /// <param name="expectedContentType">Expected content type.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateContainingOfContentType(
            dynamic actionResult,
            MediaTypeHeaderValue expectedContentType,
            Action<string, string, string> failedValidationAction)
        {
            var contentTypes = (IList<MediaTypeHeaderValue>)TryGetContentTypesList(actionResult);
            if (!contentTypes.Contains(expectedContentType))
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
        /// <param name="actionResult">Action result with ContentTypes.</param>
        /// <param name="contentTypes">Expected content types.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateContentTypes(
            dynamic actionResult,
            IEnumerable<MediaTypeHeaderValue> contentTypes,
            Action<string, string, string> failedValidationAction)
        {
            var actualContentTypes = (IList<MediaTypeHeaderValue>)SortContentTypes(TryGetContentTypesList(actionResult));
            var expectedContentTypes = SortContentTypes(contentTypes);

            if (actualContentTypes.Count != expectedContentTypes.Count)
            {
                failedValidationAction(
                    "content types",
                    $"to have {expectedContentTypes.Count} {(expectedContentTypes.Count != 1 ? "items" : "item")}",
                    $"instead found {actualContentTypes.Count}");
            }

            for (int i = 0; i < actualContentTypes.Count; i++)
            {
                var actualMediaTypeFormatter = actualContentTypes[i]?.MediaType;
                var expectedMediaTypeFormatter = expectedContentTypes[i]?.MediaType;
                if (actualMediaTypeFormatter != expectedMediaTypeFormatter)
                {
                    failedValidationAction(
                        "content types",
                        $"to contain {expectedMediaTypeFormatter}",
                        "none was found");
                }
            }
        }

        private static IList<MediaTypeHeaderValue> SortContentTypes(IEnumerable<MediaTypeHeaderValue> contentTypes)
        {
            return contentTypes
                .OrderBy(m => m.MediaType)
                .ToList();
        }

        private static IList<MediaTypeHeaderValue> TryGetContentTypesList(dynamic actionResult)
        {
            IList<MediaTypeHeaderValue> contentTypes = null;

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                contentTypes = (IList<MediaTypeHeaderValue>)actionResult.ContentTypes;
            });

            return contentTypes;
        }
    }
}
