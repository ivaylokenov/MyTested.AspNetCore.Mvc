namespace MyTested.Mvc.Builders.Actions.ShouldHave
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Contracts.Actions;
    using Contracts.Http;
    using Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing action attributes and model state.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>, IShouldHaveTestBuilder<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldHaveTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        /// <param name="actionAttributes">Collected action attributes from the method call.</param>
        public ShouldHaveTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult,
            IEnumerable<object> actionAttributes)
            : base(controller, actionName, caughtException, actionResult, actionAttributes)
        {
        }

        /// <summary>
        /// Checks whether the tested action applies additional features to the HTTP response.
        /// </summary>
        /// <returns>HTTP response test builder.</returns>
        public IHttpResponseTestBuilder HttpResponse()
        {
            return new HttpResponseTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.Controller.Response);
        }
    }
}
