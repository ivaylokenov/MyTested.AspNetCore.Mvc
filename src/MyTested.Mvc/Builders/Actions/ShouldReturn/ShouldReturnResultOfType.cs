namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using Contracts.Models;
    using Models;

    /// <summary>
    /// Class containing methods for testing return type.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IModelDetailsTestBuilder<TActionResult> ResultOfType(Type returnType)
        {
            this.ValidateActionReturnType(returnType, true, true);
            return new ModelDetailsTestBuilder<TActionResult>(this.TestContext);
        }

        /// <inheritdoc />
        public IModelDetailsTestBuilder<TActionResult> ResultOfType<TResponseModel>()
        {
            this.ValidateActionReturnType<TResponseModel>(true);
            return new ModelDetailsTestBuilder<TActionResult>(this.TestContext);
        }

        private TReturnObject GetReturnObject<TReturnObject>()
            where TReturnObject : class
        {
            this.ValidateActionReturnType<TReturnObject>();
            return this.ActionResult as TReturnObject;
        }
    }
}
