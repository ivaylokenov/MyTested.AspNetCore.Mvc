namespace MyTested.Mvc.Builders.Actions.ShouldHave
{
    using System;
    using Contracts.And;
    using Contracts.Data;
    using Data;

    /// <summary>
    /// Class containing methods for testing view bag.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> NoViewBag()
        {
            if (this.TestContext.ViewData.Count > 0)
            {
                this.ThrowNewDataProviderAssertionExceptionWithNoEntries(ViewBagTestBuilder.ViewBagName);
            }

            return this.NewAndTestBuilder();
        }

        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> ViewBag(int? withNumberOfEntries = null)
        {
            this.ValidateDataProviderNumberOfEntries(
                ViewBagTestBuilder.ViewBagName,
                withNumberOfEntries,
                this.TestContext.ViewData.Count);

            return this.NewAndTestBuilder();
        }

        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> ViewBag(Action<IViewBagTestBuilder> viewBagTestBuilder)
        {
            viewBagTestBuilder(new ViewBagTestBuilder(this.TestContext));
            return this.NewAndTestBuilder();
        }
    }
}
