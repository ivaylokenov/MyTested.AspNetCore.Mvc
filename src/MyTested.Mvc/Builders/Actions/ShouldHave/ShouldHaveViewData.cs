namespace MyTested.Mvc.Builders.Actions.ShouldHave
{
    using System;
    using Contracts.And;
    using Contracts.Data;
    using Data;

    /// <content>
    /// Class containing methods for testing <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary"/>.
    /// </content>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> NoViewData()
        {
            if (this.TestContext.ViewData.Count > 0)
            {
                this.ThrowNewDataProviderAssertionExceptionWithNoEntries(ViewDataTestBuilder.ViewDataName);
            }

            return this.NewAndTestBuilder();
        }

        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> ViewData(int? withNumberOfEntries = null)
        {
            this.ValidateDataProviderNumberOfEntries(
                ViewDataTestBuilder.ViewDataName,
                withNumberOfEntries,
                this.TestContext.ViewData.Count);

            return this.NewAndTestBuilder();
        }

        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> ViewData(Action<IViewDataTestBuilder> viewDataTestBuilder)
        {
            viewDataTestBuilder(new ViewDataTestBuilder(this.TestContext));
            return this.NewAndTestBuilder();
        }
    }
}
