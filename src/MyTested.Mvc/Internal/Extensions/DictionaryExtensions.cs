namespace MyTested.Mvc.Internal.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class DictionaryExtensions
    {
        public static IDictionary<string, MethodArgumentContext> ToDetailedValues(this IDictionary<string, object> dictionary)
        {
            return dictionary.ToDictionary(
                a => a.Key,
                a => new MethodArgumentContext
                {
                    Name = a.Key,
                    Type = a.Value?.GetType(),
                    Value = a.Value
                });
        }
    }
}
