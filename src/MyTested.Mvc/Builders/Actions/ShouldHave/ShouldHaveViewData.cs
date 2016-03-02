namespace MyTested.Mvc.Builders.Actions.ShouldHave
{
    using Contracts.And;
    using Contracts.Data;
    using Data;
    using System;

    /// <summary>
    /// Class containing methods for testing view data.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        public IAndTestBuilder<TActionResult> NoViewData()
        {
            if (this.TestContext.ViewData.Count > 0)
            {
                this.ThrowNewDataProviderAssertionExceptionWithNoEntries(ViewDataTestBuilder.ViewDataName);
            }

            return this.NewAndTestBuilder();
        }

        public IAndTestBuilder<TActionResult> ViewData(int? withNumberOfEntries = null)
        {
            this.ValidateDataProviderNumberOfEntries(
                ViewDataTestBuilder.ViewDataName,
                withNumberOfEntries,
                this.TestContext.ViewData.Count);

            return this.NewAndTestBuilder();
        }

        public IAndTestBuilder<TActionResult> ViewData(Action<IViewDataTestBuilder> viewDataTestBuilder)
        {
            viewDataTestBuilder(new ViewDataTestBuilder(this.TestContext));
            return this.NewAndTestBuilder();
        }
    }
}
