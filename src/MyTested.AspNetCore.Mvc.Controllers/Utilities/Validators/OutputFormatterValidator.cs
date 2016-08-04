namespace MyTested.AspNetCore.Mvc.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;

    /// <summary>
    /// Validator class containing output formatter validation logic.
    /// </summary>
    public static class OutputFormatterValidator
    {
        /// <summary>
        /// Validates whether object result Formatters contains the provided output formatter.
        /// </summary>
        /// <param name="objectResult">Object result to test.</param>
        /// <param name="expectedOutputFormatter">Expected output formatter.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateContainingOfOutputFormatter(
            ObjectResult objectResult,
            IOutputFormatter expectedOutputFormatter,
            Action<string, string, string> failedValidationAction)
        {
            var outputFormatters = objectResult.Formatters;
            if (!outputFormatters.Contains(expectedOutputFormatter))
            {
                failedValidationAction(
                    "output formatters",
                    $"to contain the provided formatter",
                    "such was not found");
            }
        }

        /// <summary>
        /// Validates whether object result Formatters contains the provided type of output formatter.
        /// </summary>
        /// <typeparam name="TOutputFormatter">Type of expected output formatter.</typeparam>
        /// <param name="objectResult">Object result to test.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateContainingOutputFormatterOfType<TOutputFormatter>(
            ObjectResult objectResult,
            Action<string, string, string> failedValidationAction)
        {
            var outputFormatters = objectResult.Formatters;
            var typeOfExpectedOutputFormatter = typeof(TOutputFormatter);

            if (outputFormatters.All(f => Reflection.AreDifferentTypes(f.GetType(), typeOfExpectedOutputFormatter)))
            {
                failedValidationAction(
                    "output formatters",
                    $"to contain formatter of {typeOfExpectedOutputFormatter.Name} type",
                    "such was not found");
            }
        }

        /// <summary>
        /// Validates whether object result Formatters contains the provided output formatters.
        /// </summary>
        /// <param name="objectResult">Object result to test.</param>
        /// <param name="outputFormatters">Expected output formatters.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateOutputFormatters(
            ObjectResult objectResult,
            IEnumerable<IOutputFormatter> outputFormatters,
            Action<string, string, string> failedValidationAction)
        {
            var actualOutputFormatters = SortOutputFormatterNames(objectResult.Formatters);
            var expectedOutputFormatters = SortOutputFormatterNames(outputFormatters);

            if (actualOutputFormatters.Count != expectedOutputFormatters.Count)
            {
                failedValidationAction(
                    "output formatters",
                    $"to have {expectedOutputFormatters.Count} {(expectedOutputFormatters.Count != 1 ? "items" : "item")}",
                    $"instead found {actualOutputFormatters.Count}");
            }

            for (int i = 0; i < actualOutputFormatters.Count; i++)
            {
                var actualOutputFormatter = actualOutputFormatters[i];
                var expectedOutputFormatter = expectedOutputFormatters[i];
                if (actualOutputFormatter != expectedOutputFormatter)
                {
                    failedValidationAction(
                        "output formatters",
                        $"to contain formatter of {expectedOutputFormatter} type",
                        "none was found");
                }
            }
        }

        private static IList<string> SortOutputFormatterNames(IEnumerable<IOutputFormatter> outputFormatters)
        {
            return outputFormatters
                .Select(of => of.GetType().ToFriendlyTypeName())
                .OrderBy(oft => oft)
                .ToList();
        }
    }
}
