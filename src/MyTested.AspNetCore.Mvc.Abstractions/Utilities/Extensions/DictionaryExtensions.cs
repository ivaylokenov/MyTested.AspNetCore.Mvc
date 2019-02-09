namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Internal.TestContexts;

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

        public static IDictionary<string, object> ToRouteValues(
            this IDictionary<string, MethodArgumentTestContext> dictionary,
            Func<KeyValuePair<string, MethodArgumentTestContext>, bool> filter = null)
        {
            var result = dictionary.AsEnumerable();

            if (filter != null)
            {
                result = result.Where(filter);
            }

            return result.ToDictionary(
                a => a.Key,
                a => a.Value.Value);
        }

        public static IDictionary<string, object> ToSortedRouteValues(
            this IDictionary<string, MethodArgumentTestContext> dictionary,
            Func<KeyValuePair<string, MethodArgumentTestContext>, bool> filter = null) 
            => new SortedDictionary<string, object>(dictionary.ToRouteValues(filter));
    }
}
