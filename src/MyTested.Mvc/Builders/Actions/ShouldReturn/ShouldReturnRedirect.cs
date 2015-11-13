namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    /// <summary>
    /// Class containing methods for testing RedirectResult or RedirectToRouteResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        // TODO: redirect is different
        ///// <summary>
        ///// Tests whether action result is RedirectResult or RedirectToRouteResult.
        ///// </summary>
        ///// <returns>Redirect test builder.</returns>
        //public IRedirectTestBuilder Redirect()
        //{
        //    var actionResultAsRedirectResult = this.ActionResult as RedirectToRouteResult;
        //    if (actionResultAsRedirectResult != null)
        //    {
        //        return this.ReturnRedirectTestBuilder<RedirectToRouteResult>();
        //    }

        //    return this.ReturnRedirectTestBuilder<RedirectResult>();
        //}

        //private IRedirectTestBuilder ReturnRedirectTestBuilder<TRedirectResult>()
        //    where TRedirectResult : class
        //{
        //    var redirectResult = this.GetReturnObject<TRedirectResult>();
        //    return new RedirectTestBuilder<TRedirectResult>(
        //        this.Controller,
        //        this.ActionName,
        //        this.CaughtException,
        //        redirectResult);
        //}
    }
}
