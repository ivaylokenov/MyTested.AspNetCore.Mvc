namespace MyTested.Mvc.Test.Setups.Controllers
{
    using System;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.Extensions.DependencyInjection;

    public class PrivatePocoController
    {
        private TempDataDictionary tempData;

        public PrivatePocoController(IServiceProvider services)
        {
            this.tempData = (TempDataDictionary)services.GetService<ITempDataDictionaryFactory>().GetTempData(new DefaultHttpContext());
        }

        private ITempDataDictionary TempData => tempData;
    }
}
