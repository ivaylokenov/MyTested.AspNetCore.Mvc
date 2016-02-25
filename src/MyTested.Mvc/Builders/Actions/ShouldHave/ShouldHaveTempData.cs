namespace MyTested.Mvc.Builders.Actions.ShouldHave
{
    using Contracts.And;
    using Contracts.Data;
    using Data;
    using System;
    using Utilities.Validators;

    /// <summary>
    /// Class containing methods for testing temp data.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        public IAndTestBuilder<TActionResult> TempData(int? withNumberOfEntries = null)
        {
            return this.NewAndTestBuilder();
        }

        public IAndTestBuilder<TActionResult> TempData(Action<ITempDataTestBuilder> tempDataTestBuilder)
        {
            CommonValidator.CheckForException(this.CaughtException);

            tempDataTestBuilder(new TempDataTestBuilder(this.TestContext));

            return this.NewAndTestBuilder();
        }
    }
}
