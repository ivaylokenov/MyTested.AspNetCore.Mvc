namespace MyTested.Mvc.Utilities.Validators
{
    using System;
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
                    string.Format("to be {0}", expectedContentType != null ? expectedContentType.MediaType : "null"),
                    string.Format("instead received {0}", actualContentType != null ? actualContentType.MediaType : "null"));
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
            RuntimeBinderValidator.ValidateBinding((Action)(() =>
            {
                ValidateContentType(
                    expectedContentType,
                    (MediaTypeHeaderValue)actionResult.ContentType,
                    failedValidationAction);
            }));
        }
    }
}
