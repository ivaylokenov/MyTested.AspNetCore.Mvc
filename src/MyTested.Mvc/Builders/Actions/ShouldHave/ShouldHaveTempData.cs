namespace MyTested.Mvc.Builders.Actions.ShouldHave
{
    using Contracts.And;
    using Contracts.Data;
    using Data;
    using System;

    /// <summary>
    /// Class containing methods for testing temp data.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        public IAndTestBuilder<TActionResult> NoTempData()
        {
            if (this.TestContext.TempData.Count > 0)
            {
                this.ThrowNewDataProviderAssertionExceptionWithNoEntries(TempDataTestBuilder.TempDataName);
            }

            return this.NewAndTestBuilder();
        }

        public IAndTestBuilder<TActionResult> TempData(int? withNumberOfEntries = null)
        {
            this.ValidateDataProviderNumberOfEntries(
                TempDataTestBuilder.TempDataName,
                withNumberOfEntries,
                this.TestContext.TempData.Count);

            return this.NewAndTestBuilder();
        }

        public IAndTestBuilder<TActionResult> TempData(Action<ITempDataTestBuilder> tempDataTestBuilder)
        {
            tempDataTestBuilder(new TempDataTestBuilder(this.TestContext));
            return this.NewAndTestBuilder();
        }
    }
}
