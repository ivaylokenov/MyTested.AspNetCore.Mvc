namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> builder.
    /// </summary>
    public interface IAndWithoutModelStateBuilder : IWithoutModelStateBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>.
        /// </summary>
        /// <returns>The same <see cref="IWithoutModelStateBuilder"/>.</returns>
        IWithoutModelStateBuilder AndAlso();
    }
}
