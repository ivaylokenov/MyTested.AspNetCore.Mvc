namespace MyTested.AspNetCore.Mvc.Internal
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public class TempDataProviderMock : ITempDataProvider
    {
        public IDictionary<string, object> LoadTempData(HttpContext context) 
            => new Dictionary<string, object>();

        public void SaveTempData(HttpContext context, IDictionary<string, object> values)
        {
            // intentionally does nothing
        }
    }
}
