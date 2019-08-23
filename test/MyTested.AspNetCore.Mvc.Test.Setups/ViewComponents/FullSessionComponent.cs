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

            var intEntry = session.GetInt32("IntEntry");
            if (intEntry != null)
            {
                return this.View(intEntry);
            }

            var stringEntry = session.GetString("StringEntry");
            if (stringEntry != null)
            {
                return this.View<string>(stringEntry);
            }

            return this.Content("Invalid");
        }
    }
}
