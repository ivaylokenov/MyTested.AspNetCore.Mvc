namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Used for building <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>.
    /// </summary>
    public interface IModelStateBuilder
    {
        /// <summary>
        /// Adds an error to the built <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>
        /// </summary>
        /// <param name="key">Key to set as string.</param>
        /// <param name="errorMessage">Error message to set as string.</param>
        /// <returns></returns>
        IAndModelStateBuilder WithError(string key, string errorMessage);

        /// <summary>
        /// Adds model state entries to the built <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>
        /// </summary>
        /// <param name="errors">Model state entries as dictionary.</param>
        /// <returns></returns>
        IAndModelStateBuilder WithErrors(IDictionary<string, string> errors);

        /// <summary>
        /// Adds model state entries to the built <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>
        /// </summary>
        /// <param name="errors">Model state entries as anonymous object.</param>
        /// <returns></returns>
        IAndModelStateBuilder WithErrors(object errors);
    }
}
