namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    using And;

    /// <summary>
    /// Base interface for all test builders with model error.
    /// </summary>
    public interface IBaseTestBuilderWithModelError
    {
        /// <summary>
        /// Tests whether the tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> is valid.
        /// </summary>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder ContainingNoErrors();
    }
}
