namespace MyTested.Mvc.Tests.BuildersTests.HttpTests
{
    using Microsoft.AspNetCore.Http;
    using Setups.Controllers;
    using Xunit;

    public class HttpResponseTestBuilderTests
    {
        [Fact]
        public void VoidActionShouldNotThrowExceptionWithCorrectResponse()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomResponseAction())
                .ShouldReturn()
                .Ok();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomResponseAction())
                .ShouldHave()
                .HttpResponse()
                .WithContentType(ContentType.ApplicationJson)
                .AndAlso()
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .AndAlso()
                .ContainingHeader("TestHeader", "TestHeaderValue")
                .ContainingCookie("TestCookie", "TestCookieValue", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true
                });
        }

        [Fact]
        public void ActionShouldNotThrowExceptionWithCorrectResponse()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomVoidResponseAction())
                .ShouldReturnEmpty();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomVoidResponseAction())
                .ShouldHave()
                .HttpResponse()
                .WithContentType(ContentType.ApplicationJson)
                .AndAlso()
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .AndAlso()
                .ContainingHeader("TestHeader", "TestHeaderValue")
                .ContainingCookie("TestCookie", "TestCookieValue", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true
                });
        }
    }
}
