namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc;

    public class HtmlContentComponent : ViewComponent
    {
        public IHtmlContent Invoke() => new HtmlContentBuilder().AppendHtml("<input type='button' />");
    }
}
