namespace MyTested.AspNetCore.Mvc.Utilities.Validators
{
    using Exceptions;
    using Extensions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Validator class containing <see cref="ModelStateDictionary"/> validation logic.
    /// </summary>
    public static class ModelStateValidator
    {
        /// <summary>
        /// Tests whether the tested action's model state is valid.
        /// </summary>
        public static void CheckValidModelState(ActionTestContext testContext)
        {
            if (!testContext.ModelState.IsValid)
            {
                throw new ModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have valid model state with no errors, but it had some.",
                    testContext.MethodName,
                    testContext.Component.GetName()));
            }
        }
    }
}
