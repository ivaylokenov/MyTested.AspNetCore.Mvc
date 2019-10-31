namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using MyTested.AspNetCore.Mvc.Utilities.Validators;

    public abstract class BaseTempDataBuilder
    {
        public BaseTempDataBuilder(ITempDataDictionary tempData)
        {
            CommonValidator.CheckForNullReference(tempData, nameof(ITempDataDictionary));
            this.TempData = tempData;
        }

        /// <summary>
        /// Gets the mocked <see cref="ITempDataDictionary"/>.
        /// </summary>
        /// <value>Built <see cref="ITempDataDictionary"/>.</value>
        protected ITempDataDictionary TempData { get; private set; }
    }
}
