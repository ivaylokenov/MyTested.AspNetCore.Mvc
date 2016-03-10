namespace MyTested.Mvc.Test.Setups.Controllers
{
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class FullPocoController
    {
        private readonly ViewDataDictionary viewData;
        private readonly TempDataDictionary tempData;

        public FullPocoController(IServiceProvider services)
        {
            this.viewData = new ViewDataDictionary(services.GetService<IModelMetadataProvider>(), new ControllerContext().ModelState);
            this.tempData = (TempDataDictionary)services.GetService<ITempDataDictionaryFactory>().GetTempData(new DefaultHttpContext());
        }

        [ActionContext]
        public ActionContext CustomActionContext { get; set; }

        [ControllerContext]
        public ControllerContext CustomControllerContext { get; set; }

        [ViewDataDictionary]
        public ViewDataDictionary CustomViewData => this.viewData;

        public TempDataDictionary CustomTempData => this.tempData;
    }
}
