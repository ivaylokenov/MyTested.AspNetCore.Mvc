namespace MyTested.Mvc.Builders.Actions.ShouldHave
{
    using Contracts.And;
    using Contracts.Data;
    using Data;
    using System;
    using Utilities.Validators;

    /// <summary>
    /// Class containing methods for testing view data.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        public IAndTestBuilder<TActionResult> ViewData(int? withNumberOfEntries = null)
        {
            return this.NewAndTestBuilder();
        }

        public IAndTestBuilder<TActionResult> ViewData(Action<IViewDataTestBuilder> viewDataTestBuilder)
        {
            CommonValidator.CheckForException(this.CaughtException);

            viewDataTestBuilder(new ViewDataTestBuilder(this.TestContext));

            return this.NewAndTestBuilder();
        }
    }
}
