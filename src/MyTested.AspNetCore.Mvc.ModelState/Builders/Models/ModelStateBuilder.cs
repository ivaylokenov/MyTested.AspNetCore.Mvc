namespace MyTested.AspNetCore.Mvc.Builders.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Models;
    using MyTested.AspNetCore.Mvc.Internal.TestContexts;
    using MyTested.AspNetCore.Mvc.Utilities.Extensions;

    public class ModelStateBuilder : IAndModelStateBuilder
    {
        public ModelStateBuilder(ActionTestContext actionContext) 
            => this.ModelState = actionContext.ModelState;

        protected ModelStateDictionary ModelState { get; set; }

        public IAndModelStateBuilder WithError(string key, string errorMessage)
        {
            this.AddError(key, errorMessage);
            return this;
        }

        public IAndModelStateBuilder WithErrors(IDictionary<string, string> errors)
        {
            errors.ForEach(err => this.AddError(err.Key, err.Value));
            return this;
        }

        public IAndModelStateBuilder WithErrors(object erros)
        {
            return this;
        }

        public IModelStateBuilder AndAlso() => this;

        private void AddError(string key, string errorMessage) => this.ModelState.AddModelError(key, errorMessage);
    }
}
