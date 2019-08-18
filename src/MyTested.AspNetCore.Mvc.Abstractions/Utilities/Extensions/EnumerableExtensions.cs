namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides extension methods to <see cref="IEnumerable{T}" />.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Provides easier linear traversing over all items in collection and executing a function on each of them.
        /// </summary>
        /// <typeparam name="T">Type of objects in the collection.</typeparam>
        /// <param name="collection">Collection to traverse.</param>
        /// <param name="action">Function to execute on each item in the collection.</param>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }

            return collection;
        }
    }
}
