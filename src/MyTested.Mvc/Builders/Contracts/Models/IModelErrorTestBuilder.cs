namespace MyTested.Mvc.Builders.Contracts.Models
{
    using Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
    /// </summary>
    public interface IModelErrorTestBuilder : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Tests whether the tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> is valid.
        /// </summary>
        /// <returns>Test builder of <see cref="IBaseTestBuilderWithInvokedAction"/> type.</returns>
        IBaseTestBuilderWithInvokedAction ContainingNoErrors();
    }
}
