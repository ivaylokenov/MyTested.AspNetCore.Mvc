namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Class containing methods for testing LocalRedirectResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        public void LocalRedirect()
        {
            this.ResultOfType<LocalRedirectResult>();

        }
    }
}
