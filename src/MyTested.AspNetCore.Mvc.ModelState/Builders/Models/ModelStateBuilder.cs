namespace MyTested.AspNetCore.Mvc.Builders.Models
{
    using System.Collections.Generic;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Models;

    public class ModelStateBuilder : IModelStateBuilder
    {
        public IAndModelStateBuilder WithError(string key, string errorMessage)
        {
            throw new System.NotImplementedException();
        }

        public IAndModelStateBuilder WithErrors(IDictionary<string, string> errors)
        {
            throw new System.NotImplementedException();
        }

        public IAndModelStateBuilder WithErrors(object erros)
        {
            throw new System.NotImplementedException();
        }
    }
}
