namespace MyTested.Mvc.Utilities.Extensions
{
    using Internal.TestContexts;
    using System.Collections.Generic;
    using System.Linq;

    public static class DictionaryExtensions
    {
        public static IDictionary<string, MethodArgumentTestContext> ToDetailedValues(this IDictionary<string, object> dictionary)
        {
            return dictionary.ToDictionary(
                a => a.Key,
                a => new MethodArgumentTestContext
                {
                    Name = a.Key,
                    Type = a.Value?.GetType(),
                    Value = a.Value
                });
        }
    }
}
