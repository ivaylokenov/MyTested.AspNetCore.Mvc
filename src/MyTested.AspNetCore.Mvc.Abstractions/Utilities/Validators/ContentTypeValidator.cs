namespace MyTested.AspNetCore.Mvc.Utilities.Validators
{
    using Extensions;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        /// Validates whether ContentType is the same as the provided one from an action result containing such property.
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
        /// Validates whether action result ContentTypes contain the provided content type from an action result containing such property.
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
            var expectedContentTypeValue = expectedContentType.ToString();

            if (!contentTypes.Contains(expectedContentTypeValue))
            {
                failedValidationAction(
                    "content types",
                    $"to contain {expectedContentTypeValue}",
                    "in fact such was not found");
            }
        }

        /// <summary>
        /// Validates whether action result ContentTypes contain the provided content types from an action result containing such property.
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
            var expectedContentTypes = SortContentTypes(contentTypes.Select(m => m.ToString()));

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
                        "in fact such was not found");
                }
            }
        }

        /// <summary>
        /// Validates whether attribute ContentTypes contain the provided content type from an attribute containing such property.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to validate.</typeparam>
        /// <param name="attribute">Attribute with ContentTypes.</param>
        /// <param name="expectedContentType">Expected content type.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateAttributeContainingOfContentType<TAttribute>(
            TAttribute attribute,
            string expectedContentType,
            Action<string, string> failedValidationAction)
            where TAttribute : Attribute
        {
            var actualContentTypes = (MediaTypeCollection)TryGetContentTypesCollection(attribute);

            if (!actualContentTypes.Contains(expectedContentType))
            {
                failedValidationAction(
                    $"{attribute.GetName()} with '{expectedContentType}' content type",
                    "in fact such was not found");
            }
        }

        /// <summary>
        /// Validates whether attribute ContentTypes contain the provided content types from an attribute containing such property.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to validate.</typeparam>
        /// <param name="attribute">Attribute with ContentTypes.</param>
        /// <param name="expectedContentTypes">Expected content types.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateAttributeContentTypes<TAttribute>(
            TAttribute attribute,
            IEnumerable<string> expectedContentTypes,
            Action<string, string> failedValidationAction)
            where TAttribute : Attribute
        {
            var expectedContentTypesArray = expectedContentTypes.ToArray();
            var actualContentTypes = (MediaTypeCollection)TryGetContentTypesCollection(attribute);

            var expectedContentTypesCount = expectedContentTypesArray.Length;
            var actualContentTypesCount = actualContentTypes.Count;

            if (expectedContentTypesCount != actualContentTypesCount)
            {
                failedValidationAction(
                    $"{attribute.GetName()} with {expectedContentTypesCount} {(expectedContentTypesCount != 1 ? "content types" : "content type")}",
                    $"in fact found {actualContentTypesCount}");
            }

            expectedContentTypesArray.ForEach(contentType =>
            {
                ValidateAttributeContainingOfContentType(
                    attribute,
                    contentType,
                    failedValidationAction);
            });
        }

        private static IList<string> SortContentTypes(IEnumerable<string> contentTypes)
            => contentTypes
                .OrderBy(m => m)
                .ToList();

        private static IEnumerable<string> TryGetContentTypesCollection(dynamic fromObject)
        {
            MediaTypeCollection contentTypes = null;

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                contentTypes = (MediaTypeCollection)fromObject.ContentTypes;
            });

            return contentTypes;
        }
    }
}
