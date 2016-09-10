namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class FullSessionComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var session = this.HttpContext.Session;

            var hasId = session.GetString("HasId");
            if (!string.IsNullOrWhiteSpace(hasId) && hasId == "HasIdValue")
            {
                return this.Content(session.Id);
            }

            var byteEntry = session.Get("ByteEntry");
            if (byteEntry != null)
            {
                return this.View(byteEntry);
            }
            
            return this.Content("Invalid");
        }
    }
}
