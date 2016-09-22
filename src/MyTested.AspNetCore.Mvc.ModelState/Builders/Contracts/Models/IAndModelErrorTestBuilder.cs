namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> error tests.
    /// </summary>
    public interface IAndModelErrorTestBuilder : IModelErrorTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> error message tests.
        /// </summary>
        /// <returns>The same <see cref="IModelErrorTestBuilder"/>.</returns>
        IModelErrorTestBuilder AndAlso();
    }
}
