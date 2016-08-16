namespace MyTested.AspNetCore.Mvc.Internal
{
    using Builders.Base;
    using Exceptions;
    using Internal;
    using Utilities.Extensions;

    public static class ViewActionResultsThrow
    {
        public static void NewViewResultAssertionException(
               BaseTestBuilderWithInvokedAction baseTestBuilderWithInvokedAction,
               string viewType,
               string expectedViewName,
               string actualViewName)
        {
            throw new ViewResultAssertionException(string.Format(
                "When calling {0} action in {1} expected {2} result to be {3}, but instead received {4}.",
                baseTestBuilderWithInvokedAction.ActionName,
                baseTestBuilderWithInvokedAction.Controller.GetName(),
                viewType,
                ViewTestHelper.GetFriendlyViewName(expectedViewName),
                ViewTestHelper.GetFriendlyViewName(actualViewName)));
        }
    }
}
