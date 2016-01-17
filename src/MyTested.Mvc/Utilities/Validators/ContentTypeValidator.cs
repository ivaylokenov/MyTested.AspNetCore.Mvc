namespace MyTested.Mvc.Utilities.Validators
{
    using System;
    using Microsoft.Net.Http.Headers;

    public static class ContentTypeValidator
    {
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

        public static void ValidateContentType(
            dynamic actionResult,
            MediaTypeHeaderValue expectedHttpStatusCode,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                ValidateContentType(
                    expectedHttpStatusCode,
                    (MediaTypeHeaderValue)actionResult.ContentType,
                    failedValidationAction);
            });
        }
    }
}
