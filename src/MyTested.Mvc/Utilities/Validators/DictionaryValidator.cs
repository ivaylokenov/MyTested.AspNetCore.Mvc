namespace MyTested.Mvc.Utilities.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DictionaryValidator
    {
        public static void WithStringKey(
            IDictionary<string, object> dictionary,
            string key,
            Action<string, string, string> failedValidationAction)
        {
            if (!dictionary.ContainsKey(key))
            {
                failedValidationAction(
                    "route values",
                    $"to have entry with key '{key}'",
                    "such was not found");
            }
        }

        public static void WithStringKeyAndValue(
            IDictionary<string, object> dictionary,
            string key,
            object value,
            Action<string, string, string> failedValidationAction)
        {
            var entryExists = dictionary.ContainsKey(key);
            var actualValue = entryExists ? dictionary[key] : null;

            if (!entryExists || Reflection.AreNotDeeplyEqual(value, actualValue))
            {
                failedValidationAction(
                    "",
                    $"to have entry with '{key}' key and the provided value",
                    $"{(entryExists ? "the value was different" : "such was not found")}");
            }
        }

        public static void WithValueOfType<TValue>(
            IDictionary<string, object> dictionary,
            Action<string, string, string> failedValidationAction)
        {
            var expectedType = typeof(TValue);
            var argumentOfSameType = dictionary.Values.FirstOrDefault(arg => arg.GetType() == expectedType);

            if (argumentOfSameType == null)
            {
                failedValidationAction(
                    "with at least one argument",
                    $"to be of {expectedType.Name} type",
                    "none was found");
            }
        }

        public static void WithStringKeyAndValueOfType<TValue>(
            IDictionary<string, object> dictionary,
            string key,
            Action<string, string, string> failedValidationAction)
        {
        }

        public static void WithValue<TValue>(
            IDictionary<string, object> dictionary,
            TValue value,
            Action<string, string, string> failedValidationAction)
        {
        }

        public static void WithValues(
            IDictionary<string, object> dictionary,
            IDictionary<string, object> expectedDictionary,
            Action<string, string, string> failedValidationAction)
        {
        }

        public static void WithValues(
            IDictionary<string, object> dictionary,
            object values,
            Action<string, string, string> failedValidationAction)
        {
        }
    }
}
