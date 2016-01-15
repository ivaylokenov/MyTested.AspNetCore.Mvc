namespace MyTested.Mvc.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders.Attributes;
    using Internal.Extensions;

    /// <summary>
    /// Validator class containing attributes validation logic.
    /// </summary>
    public static class AttributesValidator
    {
        /// <summary>
        /// Validates whether the provided collection of attributes contains zero elements.
        /// </summary>
        /// <param name="attributes">Collection of attributes to validate.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateNoAttributes(
            IEnumerable<object> attributes,
            Action<string, string> failedValidationAction)
        {
            if (attributes.Any())
            {
                failedValidationAction(
                    "not have any attributes",
                    "it had some");
            }
        }

        /// <summary>
        /// Validates if any attributes are contained in the provided collection of attributes.
        /// </summary>
        /// <param name="attributes">Collection of attributes to validate.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        /// <param name="withTotalNumberOf">Optional parameter specifying the exact number of expected attributes.</param>
        public static void ValidateNumberOfAttributes(
            IEnumerable<object> attributes,
            Action<string, string> failedValidationAction,
            int? withTotalNumberOf = null)
        {
            var attributesList = attributes.ToList();
            if (!attributesList.Any())
            {
                failedValidationAction(
                    "have at least 1 attribute",
                    "in fact none was found");
            }

            var actualNumberOfActionAttributes = attributesList.Count;
            if (withTotalNumberOf.HasValue && actualNumberOfActionAttributes != withTotalNumberOf)
            {
                failedValidationAction(
                    string.Format(
                        "have {0} {1}",
                        withTotalNumberOf,
                        withTotalNumberOf != 1 ? "attributes" : "attribute"),
                    string.Format("in fact found {0}", actualNumberOfActionAttributes));
            }
        }

        /// <summary>
        /// Validation collection attribute based on attribute test builder.
        /// </summary>
        /// <param name="attributes">Collection of attributes to validate.</param>
        /// <param name="attributesTestBuilder">Test builder containing attribute specific validation.</param>
        /// <param name="failedValidationAction">Action to call in case of failed validation.</param>
        public static void ValidateAttributes(
            IEnumerable<object> attributes,
            BaseAttributesTestBuilder attributesTestBuilder,
            Action<string, string> failedValidationAction)
        {
            var attributesList = attributes.ToList();
            ValidateNumberOfAttributes(attributesList, failedValidationAction);
            var validations = attributesTestBuilder.GetAttributeValidations();
            validations.ForEach(v => v(attributesList));
        }
    }
}
