namespace MyTested.AspNetCore.Mvc.Utilities.Validators
{
    using Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DictionaryValidator
    {
        public static void ValidateStringKey(
            string name,
            IDictionary<string, object> dictionary,
            string key,
            Action<string, string, string> failedValidationAction)
        {
            if (!dictionary.ContainsKey(key))
            {
                failedValidationAction(
                    name,
                    $"to have entry with '{key}' key",
                    "such was not found");
            }
        }
        
        public static void ValidateStringKeyAndValue(
            string name,
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
                    name,
                    $"to have entry with '{key}' key and the provided value",
                    $"{(entryExists ? "the value was different" : "such was not found")}");
            }
        }

        public static void ValidateValueOfType<TValue>(
            string name,
            IDictionary<string, object> dictionary,
            Action<string, string, string> failedValidationAction)
        {
            ValidateValueOfType<TValue>(name, dictionary.Values, failedValidationAction);
        }

        public static void ValidateValueOfType<TValue>(
            string name,
            IDictionary<object, object> dictionary,
            Action<string, string, string> failedValidationAction)
        {
            ValidateValueOfType<TValue>(name, dictionary.Values, failedValidationAction);
        }

        public static void ValidateStringKeyAndValueOfType<TValue>(
            string name,
            IDictionary<string, object> dictionary,
            string key,
            Action<string, string, string> failedValidationAction)
        {
            var entryExists = dictionary.ContainsKey(key);
            var actualValue = entryExists ? dictionary[key] : null;

            var expectedType = typeof(TValue);
            var actualType = actualValue?.GetType();
            if (!entryExists || Reflection.AreDifferentTypes(expectedType, actualType))
            {
                failedValidationAction(
                    name,
                    $"to have entry with '{key}' key and value of {expectedType.ToFriendlyTypeName()} type",
                    $"{(entryExists ? $"in fact found {actualType.ToFriendlyTypeName()}" : "such was not found")}");
            }
        }

        public static void ValidateValue<TDictionaryKey, TValue>(
            string name,
            IDictionary<TDictionaryKey, object> dictionary,
            TValue value,
            Action<string, string, string> failedValidationAction)
        {
            var sameEntry = dictionary.Values.FirstOrDefault(entry => Reflection.AreDeeplyEqual(value, entry));
            if (sameEntry == null)
            {
                failedValidationAction(
                    name,
                    "to have entry with the provided value",
                    "none was found");
            }
        }

        public static void ValidateValues(
            string name,
            IDictionary<string, object> dictionary,
            IDictionary<string, object> expectedDictionary,
            Action<string, string, string> failedValidationAction,
            bool includeCountCheck = true)
        {
            if (includeCountCheck)
            {
                var expectedItems = expectedDictionary.Count;
                var actualItems = dictionary.Count;

                if (expectedItems != actualItems)
                {
                    failedValidationAction(
                        name,
                        $"to have {expectedItems} {(expectedItems != 1 ? "entries" : "entry")}",
                        $"in fact found {actualItems}");
                }
            }

            expectedDictionary.ForEach(item => ValidateStringKeyAndValue(
                name,
                dictionary,
                item.Key, 
                item.Value,
                failedValidationAction));
        }

        private static void ValidateValueOfType<TValue>(
            string name,
            ICollection<object> values,
            Action<string, string, string> failedValidationAction)
        {
            var expectedType = typeof(TValue);
            var entryOfSameType = values.FirstOrDefault(arg => arg.GetType() == expectedType);

            if (entryOfSameType == null)
            {
                failedValidationAction(
                    name,
                    $"to have at least one entry of {expectedType.ToFriendlyTypeName()} type",
                    "none was found");
            }
        }
    }
}
