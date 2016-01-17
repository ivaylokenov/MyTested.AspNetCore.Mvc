namespace MyTested.Mvc.Utilities.Validators
{
    using System;
    using System.Net;

    /// <summary>
    /// Validator class containing HTTP status code validation logic.
    /// </summary>
    public static class HttpStatusCodeValidator
    {
        public static void ValidateHttpStatusCode(
            HttpStatusCode expectedHttpStatusCode,
            int? actualHttpStatusCode,
            Action<string, string, string> failedValidationAction)
        {
            var actualStatusCode = (HttpStatusCode?)actualHttpStatusCode;
            if (actualStatusCode != expectedHttpStatusCode)
            {
                var actualStatusCodeAsInt = (int?)actualStatusCode;

                var receivedErrorMessage = string.Format(
                    "instead received {0} ({1})",
                    actualStatusCode != null ? actualStatusCodeAsInt.ToString() : "no status code",
                    actualStatusCode != null ? actualStatusCode.ToString() : "null");

                failedValidationAction(
                    "to have",
                    $"{(int)expectedHttpStatusCode} ({expectedHttpStatusCode}) status code",
                    receivedErrorMessage);
            }
        }

        public static void ValidateHttpStatusCode(
            dynamic actionResult,
            HttpStatusCode expectedHttpStatusCode,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                ValidateHttpStatusCode(
                    expectedHttpStatusCode,
                    (int?)actionResult.StatusCode,
                    failedValidationAction);
            });
        }
    }
}
