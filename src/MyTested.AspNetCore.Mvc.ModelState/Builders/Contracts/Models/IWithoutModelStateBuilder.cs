namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    /// <summary>
    /// Used for building <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>.
    /// </summary>
    public interface IWithoutModelStateBuilder
    {
        /// <summary>
        /// Removes all keys and values from this instance of <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>
        /// </summary>
        /// <returns>The same <see cref="IAndWithoutModelStateBuilder"/>.</returns>
        IAndWithoutModelStateBuilder WithoutModelState();

        /// <summary>
        /// Removes the <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateEntry"/> with the specified key 
        /// from <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>
        /// </summary>
        /// <param name="key">The key of the model state to remove.</param>
        /// <returns>The same <see cref="IAndWithoutModelStateBuilder"/>.</returns> 
        IAndWithoutModelStateBuilder WithoutModelState(string key);
    }
}