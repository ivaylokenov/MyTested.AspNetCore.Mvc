namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System;

    public static class TupleExtensions
    {
        public static (string, string) GetTypeComparisonNames(this (Type Expected, Type Actual) typeTuple)
        {
            var expected = typeTuple.Expected;
            var actual = typeTuple.Actual;

            var expectedName = expected.ToFriendlyTypeName();
            var actualName = actual.ToFriendlyTypeName();

            if (expectedName == actualName)
            {
                return (expected.ToFriendlyTypeName(true), actual.ToFriendlyTypeName(true));
            }
                
            return (expectedName, actualName);
        }
    }
}
