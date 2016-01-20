namespace MyTested.Mvc.Utilities.Validators
{
    using System;
    using System.Linq;
    using Builders.Uris;

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
                    string.Format("instead received '{0}'", location));
            }

            return new Uri(location, UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Validates the Uri from action result containing one.
        /// </summary>
        /// <param name="actionResult">Action result with Uri.</param>
        /// <param name="location">Expected Uri.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateUri(
            dynamic actionResult,
            string location,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualLocation = GetUrlFromDynamic(actionResult);
                if (location != actualLocation)
                {
                    failedValidationAction(
                        "location",
                        string.Format("to be '{0}'", location),
                        string.Format("instead received '{0}'", actualLocation));
                }
            });
        }

        /// <summary>
        /// Validates URI by using UriTestBuilder.
        /// </summary>
        /// <param name="actionResult">Dynamic representation of action result.</param>
        /// <param name="uriTestBuilder">UriTestBuilder for validation.</param>
        /// <param name="failedValidationAction">Action to execute, if the validation fails.</param>
        public static void ValidateLocation(
            dynamic actionResult,
            Action<MockedUriTestBuilder> uriTestBuilder,
            Action<string, string, string> failedValidationAction)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualUri = GetUrlFromDynamic(actionResult);

                var newUriTestBuilder = new MockedUriTestBuilder();
                uriTestBuilder(newUriTestBuilder);
                var expectedUri = newUriTestBuilder.GetMockedUri();
                
                var validations = newUriTestBuilder.GetMockedUriValidations();
                if (validations.Any(v => !v(expectedUri, new Uri(actualUri, UriKind.RelativeOrAbsolute))))
                {
                    failedValidationAction(
                        "URI",
                        "to equal the provided one",
                        "was in fact different");
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
