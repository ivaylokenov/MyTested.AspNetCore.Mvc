namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Used for building <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>.
    /// </summary>
    public interface IModelStateBuilder
    {
        IAndModelStateBuilder WithError(string key, string errorMessage);

        IAndModelStateBuilder WithErrors(IDictionary<string, string> errors);

        IAndModelStateBuilder WithErrors(object erros);
    }
}
