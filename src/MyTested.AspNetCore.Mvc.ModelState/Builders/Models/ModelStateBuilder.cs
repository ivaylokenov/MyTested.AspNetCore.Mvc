namespace MyTested.AspNetCore.Mvc.Builders.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Routing;
    using Contracts.Models;
    using Internal.TestContexts;
    using Utilities.Extensions;

    /// <summary>
    /// Used for building <see cref="ModelStateDictionary"/>
    /// </summary>
    public class ModelStateBuilder : BaseModelStateBuilder, IAndModelStateBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelStateBuilder"/> class.
        /// </summary>
        /// <param name="actionContext"><see cref="ModelStateDictionary"/> to build.</param>
        public ModelStateBuilder(ActionTestContext actionContext) 
            : base(actionContext)
        {
        }

        /// <inheritdoc />
        public IAndModelStateBuilder WithError(string key, string errorMessage)
        {
            this.AddError(key, errorMessage);
            return this;
        }

        /// <inheritdoc />
        public IAndModelStateBuilder WithErrors(IDictionary<string, string> errors)
        {
            errors.ForEach(err => this.AddError(err.Key, err.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndModelStateBuilder WithErrors(object errors)
        {
            var errorsAsDictionary = new RouteValueDictionary(errors);
            errorsAsDictionary
                .ForEach(err => this.AddError(err.Key, err.Value.ToString()));

            return this;
        }
            
        /// <inheritdoc />
        public IModelStateBuilder AndAlso() => this;

        private void AddError(string key, string errorMessage) 
            => this.ModelState.AddModelError(key, errorMessage);
    }
}
