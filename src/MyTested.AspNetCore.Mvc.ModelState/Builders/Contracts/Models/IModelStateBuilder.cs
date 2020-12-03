namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    using System;
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

        /// <summary>
        /// Tests whether the model state entry passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions for the model state entry.</param>
        /// <returns>The same <see cref="IAndModelStateBuilder"/>.</returns>
        IAndModelStateBuilder Passing(Action assertions);

        /// <summary>
        /// Tests whether the data provider entry passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the data provider entry.</param>
        /// <returns>The same <see cref="IAndModelStateBuilder"/>.</returns>
        IAndModelStateBuilder Passing(Func<bool> predicate);
    }
}
