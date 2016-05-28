namespace MyTested.Mvc.Builders.Actions.ShouldHave
{
    using System;
    using Contracts.And;
    using Contracts.Data;
    using Data;

    /// <content>
    /// Class containing methods for testing <see cref="Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary"/>.
    /// </content>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> NoTempData()
        {
            if (this.TestContext.TempData.Count > 0)
            {
                this.ThrowNewDataProviderAssertionExceptionWithNoEntries(TempDataTestBuilder.TempDataName);
            }

            return this.NewAndTestBuilder();
        }

        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> TempData(int? withNumberOfEntries = null)
        {
            this.ValidateDataProviderNumberOfEntries(
                TempDataTestBuilder.TempDataName,
                withNumberOfEntries,
                this.TestContext.TempData.Count);

            return this.NewAndTestBuilder();
        }

        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> TempData(Action<ITempDataTestBuilder> tempDataTestBuilder)
        {
            tempDataTestBuilder(new TempDataTestBuilder(this.TestContext));
            return this.NewAndTestBuilder();
        }
    }
}
