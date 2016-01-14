using System;
using System.Net;

namespace MyTested.Mvc.Utilities.Validators
{
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

                failedValidationAction(
                    "to have",
                    $"{(int)expectedHttpStatusCode} ({expectedHttpStatusCode}) status code",
                    string.Format(
                        "received {0} ({1})", 
                        actualStatusCode != null ? actualStatusCodeAsInt.ToString() : "no status code",
                        actualStatusCode != null ? actualStatusCode.ToString() : "null"));
            }
        }
    }
}
