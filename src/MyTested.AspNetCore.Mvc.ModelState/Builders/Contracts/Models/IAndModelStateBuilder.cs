namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> builder.
    /// </summary>
    public interface IAndModelStateBuilder : IModelStateBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>.
        /// </summary>
        /// <returns>The same <see cref="IModelStateBuilder"/>.</returns>
        IModelStateBuilder AndAlso();
    }
}
