﻿namespace MyTested.AspNetCore.Mvc.Builders.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Routing;
    using Contracts.Models;
    using Internal.TestContexts;
    using Utilities.Extensions;
    using System;

    /// <summary>
    /// Used for building <see cref="ModelStateDictionary"/>
    /// </summary>
    public class ModelStateBuilder : IAndModelStateBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelStateBuilder"/> class.
        /// </summary>
        /// <param name="actionContext"><see cref="ModelStateDictionary"/> to build.</param>
        public ModelStateBuilder(ActionTestContext actionContext) 
            => this.ModelState = actionContext.ModelState;

        /// <summary>
        /// Gets the <see cref="ModelStateDictionary"/>
        /// </summary>
        /// <value>The built <see cref="ModelStateDictionary"/></value>
        protected ModelStateDictionary ModelState { get; set; }

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

        /// <inheritdoc />
        public IAndModelStateBuilder Passing(Action assertions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IAndModelStateBuilder Passing(Func<bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
