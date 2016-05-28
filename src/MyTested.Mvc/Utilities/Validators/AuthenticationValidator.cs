namespace MyTested.Mvc.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders.Authentication;
    using Builders.Contracts.Authentication;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http.Authentication;
    using Utilities.Extensions;

    /// <summary>
    /// Validator class containing authentication validation logic.
    /// </summary>
    public static class AuthenticationValidator
    {
        /// <summary>
        /// Validates whether AuthenticationSchemes contains specific scheme from action result containing one.
        /// </summary>
        /// <param name="actionResult">Action result with AuthenticationSchemes.</param>
        /// <param name="authenticationScheme">Expected authentication scheme.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateAuthenticationScheme(
            dynamic actionResult,
            string authenticationScheme,
            Action<string, string, string> failedValidationAction)
        {
            var actualAuthenticationSchemes = (IList<string>)TryGetAuthenticationSchemes(actionResult);
            if (!actualAuthenticationSchemes.Contains(authenticationScheme))
            {
                failedValidationAction(
                    "authentication schemes",
                    $"to contain {authenticationScheme}",
                    "none was found");
            }
        }

        /// <summary>
        /// Validates whether AuthenticationSchemes contains the provided schemes from action result containing one.
        /// </summary>
        /// <param name="actionResult">Action result with AuthenticationSchemes.</param>
        /// <param name="authenticationSchemes">Expected authentication schemes.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateAuthenticationSchemes(
            dynamic actionResult,
            IEnumerable<string> authenticationSchemes,
            Action<string, string, string> failedValidationAction)
        {
            var actualAuthenticationSchemes = (IList<string>)SortAuthenticationSchemes(TryGetAuthenticationSchemes(actionResult));
            var expectedAuthenticationSchemes = SortAuthenticationSchemes(authenticationSchemes);

            if (actualAuthenticationSchemes.Count != expectedAuthenticationSchemes.Count)
            {
                failedValidationAction(
                    "authentication schemes",
                    $"to be {expectedAuthenticationSchemes.Count}",
                    $"instead found {actualAuthenticationSchemes.Count}");
            }

            for (int i = 0; i < actualAuthenticationSchemes.Count; i++)
            {
                var actualMediaTypeFormatter = actualAuthenticationSchemes[i];
                var expectedMediaTypeFormatter = expectedAuthenticationSchemes[i];
                if (actualMediaTypeFormatter != expectedMediaTypeFormatter)
                {
                    failedValidationAction(
                        "authentication schemes",
                        $"to contain {expectedAuthenticationSchemes[i]}",
                        "none was found");
                }
            }
        }

        /// <summary>
        /// Validates whether Properties are the same as the provided ones from action result containing such property.
        /// </summary>
        /// <param name="actionResult">Action result with Properties.</param>
        /// <param name="properties">Expected authentication properties.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateAuthenticationProperties(
            dynamic actionResult,
            AuthenticationProperties properties,
            Action<string, string, string> failedValidationAction)
        {
            var actualProperties = (AuthenticationProperties)TryGetAuthenticationProperties(actionResult);
            if (Reflection.AreNotDeeplyEqual(properties, actualProperties))
            {
                failedValidationAction(
                    "authentication properties",
                    "to be the same as the provided one",
                    "instead received different result");
            }
        }

        /// <summary>
        /// Validates whether Properties are the same as the provided ones from action result containing such property by using a builder.
        /// </summary>
        /// <param name="authenticationPropertiesBuilder">Authentication properties builder.</param>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public static void ValidateAuthenticationProperties(
            Action<IAuthenticationPropertiesTestBuilder> authenticationPropertiesBuilder,
            ControllerTestContext testContext)
        {
            var actionResult = testContext.ActionResultAs<dynamic>();
            var actualAuthenticationProperties = 
                (AuthenticationProperties)TryGetAuthenticationProperties(actionResult) ?? new AuthenticationProperties();

            var newAuthenticationPropertiesTestBuilder = new AuthenticationPropertiesTestBuilder(testContext);
            authenticationPropertiesBuilder(newAuthenticationPropertiesTestBuilder);
            var expectedAuthenticationProperties = newAuthenticationPropertiesTestBuilder.GetAuthenticationProperties();

            var validations = newAuthenticationPropertiesTestBuilder.GetAuthenticationPropertiesValidations();
            validations.ForEach(v => v(expectedAuthenticationProperties, actualAuthenticationProperties));
        }

        private static IList<string> SortAuthenticationSchemes(IEnumerable<string> authenticationSchemes)
        {
            return authenticationSchemes
                .OrderBy(aus => aus)
                .ToList();
        }

        private static IList<string> TryGetAuthenticationSchemes(dynamic actionResult)
        {
            IList<string> authenticationSchemes = new List<string>();

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                authenticationSchemes = (IList<string>)actionResult.AuthenticationSchemes;
            });

            return authenticationSchemes;
        }

        private static AuthenticationProperties TryGetAuthenticationProperties(dynamic actionResult)
        {
            AuthenticationProperties authenticationProperties = null;

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                authenticationProperties = (AuthenticationProperties)actionResult.Properties;
            });

            return authenticationProperties;
        }
    }
}
