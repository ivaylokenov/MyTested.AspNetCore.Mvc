namespace MyTested.AspNetCore.Mvc.Internal.ViewComponents
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public class ViewDataDictionaryMock : ViewDataDictionary
    {
        public static ViewDataDictionary FromViewContext(ViewContext viewContext)
            => new ViewDataDictionaryMock(new EmptyModelMetadataProvider(), viewContext.ModelState);

        private ViewDataDictionaryMock(
            IModelMetadataProvider metadataProvider,
            ModelStateDictionary modelState)
            : base(metadataProvider, modelState)
        {
        }
    }
}
