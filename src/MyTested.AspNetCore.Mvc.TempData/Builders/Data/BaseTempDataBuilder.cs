namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Utilities.Validators;

    /// <summary>
    /// Used for building mocked <see cref="ITempDataDictionary"/>.
    /// </summary>
    public abstract class BaseTempDataBuilder
    {
        /// <summary>
        /// Abstract <see cref="BaseTempDataBuilder"/> class.
        /// </summary>
        /// <param name="tempData"><see cref="ITempDataDictionary"/> to built.</param>
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
