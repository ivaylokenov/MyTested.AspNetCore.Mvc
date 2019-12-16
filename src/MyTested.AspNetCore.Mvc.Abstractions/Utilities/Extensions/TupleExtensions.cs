namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System;

    public static class TupleExtensions
    {
        public static (string, string) GetTypeComparisonNames(this (Type Expected, Type Actual) typeTuple)
        {
            var expected = typeTuple.Expected;
            var actual = typeTuple.Actual;

            var useFullName = expected?.Name == actual?.Name;

            return (expected.ToFriendlyTypeName(useFullName), actual.ToFriendlyTypeName(useFullName));
        }
    }
}
