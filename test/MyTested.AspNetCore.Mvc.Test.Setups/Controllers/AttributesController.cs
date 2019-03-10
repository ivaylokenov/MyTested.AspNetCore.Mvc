namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    [RequireHttps(Permanent = false)]
    [Route("api/test", Name = "TestRouteAttributes", Order = 1)]
    [IgnoreAntiforgeryToken]
    [SkipStatusCodePages]
    [ResponseCache(
        CacheProfileName = "Test Profile Controller",
        Duration = 60,
        Location = ResponseCacheLocation.Any,
        VaryByHeader = "Test Header Controller",
        VaryByQueryKeys = new[] { "FirstQueryController", "SecondQueryController" },
        NoStore = false,
        Order = 3)]
    [DisableRequestSizeLimit]
    [RequestSizeLimit(2048)]
    [RequestFormLimits(
        BufferBody = true,
        BufferBodyLengthLimit = 1,
        KeyLengthLimit = 2,
        MemoryBufferThreshold = 3,
        MultipartBodyLengthLimit = 4,
        MultipartBoundaryLengthLimit = 5,
        MultipartHeadersCountLimit = 6,
        MultipartHeadersLengthLimit = 7,
        Order = 8,
        ValueCountLimit = 9,
        ValueLengthLimit = 10)]
    public class AttributesController : Controller
    {
        [AllowAnonymous]
        [RequireHttps(Permanent = true)]
        public IActionResult WithAttributesAndParameters(int id)
        {
            return this.Ok(id);
        }
    }
}
