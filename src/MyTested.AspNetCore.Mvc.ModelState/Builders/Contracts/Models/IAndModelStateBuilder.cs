namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    public interface IAndModelStateBuilder : IModelStateBuilder
    {
        /// <summary>
        /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> builder.
        /// </summary>
        /// <returns>The same <see cref="IModelStateBuilder"/>.</returns>
        IModelStateBuilder AndAlso();
    }
}
