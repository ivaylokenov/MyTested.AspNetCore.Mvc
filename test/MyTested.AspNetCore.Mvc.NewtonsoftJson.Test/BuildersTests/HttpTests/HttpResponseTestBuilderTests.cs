namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.HttpTests
{
    using System.Text;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class HttpResponseTestBuilderTests
    {
        [Fact]
        public void WithJsonBodyShouldWorkCorrectlyWithEncoding()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomResponseBodyWithUtf16JsonBody())
                .ShouldHave()
                .HttpResponse(response => response.WithJsonBody(new RequestModel { Integer = 1, RequiredString = "Текст" }, Encoding.Unicode));
        }
    }
}
