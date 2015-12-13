namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using MyTested.Mvc.Builders.Contracts.ActionResults.File;

    /// <summary>
    /// Class containing methods for testing FileStreamResult, VirtualFileResult or FileContentResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        public IFileTestBuilder File()
        {
            return null;
        }
    }
}
