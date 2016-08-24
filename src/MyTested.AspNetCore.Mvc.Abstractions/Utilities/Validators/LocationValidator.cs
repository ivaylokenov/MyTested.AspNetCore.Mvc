namespace MyTested.AspNetCore.Mvc.Utilities.Validators
{
    using System;
    using System.Linq;
    using Builders.Uri;

    /// <summary>
    /// Validator class containing URI validation logic.
    /// </summary>
    public static class LocationValidator
    {
        /// <summary>
        /// Validates an URI provided as string.
        /// </summary>
        /// <param name="location">Expected URI as string.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <returns>Valid Uri created from the provided string.</returns>
        public static Uri ValidateAndGetWellFormedUriString(
            string location,
            Action<string, string, string> failedValidationAction)
        {
            if (!Uri.IsWellFormedUriString(location, UriKind.RelativeOrAbsolute))
            {
                failedValidationAction(
                    "location",
                    "to be URI valid",
                    $"instead received '{location}'");
            }

            return new Uri(location, UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Validates the Uri from action result containing one.
        /// </summary>
        /// <param name="result">Component result with Uri.</param>
        /// <param name="location">Expected Uri.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateUri(
            dynamic result,
            string location,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualLocation = (string)GetUrlFromDynamic(result);
                if (location != actualLocation)
                {
                    failedValidationAction(
                        "location",
                        $"to be '{location}'",
                        $"instead received '{actualLocation}'");
                }
            });
        }

        /// <summary>
        /// Validates URI by using UriTestBuilder.
        /// </summary>
        /// <param name="result">Dynamic representation of component result.</param>
        /// <param name="uriTestBuilder">UriTestBuilder for validation.</param>
        /// <param name="failedValidationAction">Action to execute, if the validation fails.</param>
        public static void ValidateLocation(
            dynamic result,
            Action<MockedUriTestBuilder> uriTestBuilder,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualUri = (string)GetUrlFromDynamic(result);

                var newUriTestBuilder = new MockedUriTestBuilder();
                uriTestBuilder(newUriTestBuilder);
                var expectedUri = newUriTestBuilder.GetMockedUri();
                
                var validations = newUriTestBuilder.GetMockedUriValidations();
                var actualUriWithUnknownKind = new Uri(actualUri, UriKind.RelativeOrAbsolute);
                if (validations.Any(v => !v(expectedUri, actualUriWithUnknownKind)))
                {
                    failedValidationAction(
                        "URI",
                        $"to be '{expectedUri}'",
                        $"was in fact '{actualUriWithUnknownKind.OriginalString}'");
                }
            });
        }

        private static string GetUrlFromDynamic(dynamic actionResult)
        {
            if (Reflection.DynamicPropertyExists(actionResult, "Location"))
            {
                return actionResult.Location;
            }

            return actionResult.Url;
        }
    }
}
