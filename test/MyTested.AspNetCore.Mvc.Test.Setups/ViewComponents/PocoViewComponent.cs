namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public class PocoViewComponent
    {
        [ViewComponentContext]
        public ViewComponentContext Context { get; set; }

        public ITempDataDictionary TempData => this.Context.ViewContext.TempData;

        public string Invoke()
        {
            return "POCO";
        }
    }
}
