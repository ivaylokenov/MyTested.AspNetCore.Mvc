namespace MyTested.AspNetCore.Mvc.Utilities.Validators
{
    using Exceptions;
    using Internal.TestContexts;

    public static class DataProviderValidator
    {
        public static void ValidateDataProviderNumberOfEntries(ComponentTestContext testContext, string name, int? expectedCount, int actualCount)
        {
            if (actualCount == 0
                || (expectedCount != null && actualCount != expectedCount))
            {
                ThrowNewDataProviderAssertionException(
                    testContext,
                    name,
                    expectedCount == null ? " entries" : $" with {expectedCount} {(expectedCount != 1 ? "entries" : "entry")}",
                    expectedCount == null ? "none were found" : $"in fact contained {actualCount}");
            }
        }

        public static void ThrowNewDataProviderAssertionExceptionWithNoEntries(ComponentTestContext testContext, string name)
        {
            ThrowNewDataProviderAssertionException(
                testContext,
                name,
                " with no entries",
                "in fact it had some");
        }

        public static void ThrowNewDataProviderAssertionException(
            ComponentTestContext testContext,
            string name,
            string expectedValue,
            string actualValue)
        {
            throw new DataProviderAssertionException(string.Format(
                "{0} to have {1}{2}, but {3}.",
                testContext.ExceptionMessagePrefix,
                name,
                expectedValue,
                actualValue));
        }
    }
}
