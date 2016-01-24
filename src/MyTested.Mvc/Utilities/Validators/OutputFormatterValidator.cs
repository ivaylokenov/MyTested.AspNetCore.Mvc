namespace MyTested.Mvc.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.Formatters;

    public static class OutputFormatterValidator
    {
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
                    $"to contain {expectedOutputFormatter.GetName()} formatter",
                    "such was not found");
            }
        }

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
                    $"to be {expectedOutputFormatters.Count}",
                    $"instead found {actualOutputFormatters.Count}");
            }

            for (int i = 0; i < actualOutputFormatters.Count; i++)
            {
                var actualMediaTypeFormatter = actualOutputFormatters[i];
                var expectedMediaTypeFormatter = expectedOutputFormatters[i];
                if (actualMediaTypeFormatter != expectedMediaTypeFormatter)
                {
                    failedValidationAction(
                        "output formatters",
                        $"to contain formatter of {expectedMediaTypeFormatter} type",
                        "none was found");
                }
            }
        }

        private static IList<string> SortOutputFormatterNames(IEnumerable<IOutputFormatter> outputFormatters)
        {
            return outputFormatters
                .Select(of => of.GetType().Name)
                .OrderBy(oft => oft)
                .ToList();
        }
    }
}
