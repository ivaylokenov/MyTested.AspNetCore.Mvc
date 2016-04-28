namespace MyTested.Mvc.Builders.Actions.ShouldHave
{
    using System;
    using System.Linq;
    using Contracts.And;
    using Contracts.Models;
    using Exceptions;
    using Models;
    using Utilities.Extensions;

    /// <summary>
    /// Class containing methods for testing <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/>.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> ModelStateFor<TRequestModel>(Action<IModelErrorTestBuilder<TRequestModel>> modelErrorTestBuilder)
        {
            modelErrorTestBuilder(new ModelErrorTestBuilder<TRequestModel>(this.TestContext));
            return this.NewAndTestBuilder();
        }

        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> ValidModelState()
        {
            this.CheckValidModelState();
            return this.NewAndTestBuilder();
        }

        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> InvalidModelState(int? withNumberOfErrors = null)
        {
            var actualModelStateErrors = this.TestContext.ModelState.Values.SelectMany(c => c.Errors).Count();
            if (actualModelStateErrors == 0
                || (withNumberOfErrors != null && actualModelStateErrors != withNumberOfErrors))
            {
                throw new ModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have invalid model state{2}, {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    withNumberOfErrors == null ? string.Empty : $" with {withNumberOfErrors} errors",
                    withNumberOfErrors == null ? "but was in fact valid" : $"but in fact contained {actualModelStateErrors}"));
            }

            return this.NewAndTestBuilder();
        }
    }
}
