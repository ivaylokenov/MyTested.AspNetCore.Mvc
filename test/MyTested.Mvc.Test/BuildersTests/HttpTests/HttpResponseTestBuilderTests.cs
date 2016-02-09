namespace MyTested.Mvc.Tests.BuildersTests.HttpTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Http;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
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
        
        [Fact]
        public void WithJsonBodyShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomResponseAction())
                .ShouldHave()
                .HttpResponse()
                .WithJsonBody(new RequestModel { Integer = 1, RequiredString = "Text" });
        }

        [Fact]
        public void WithJsonBodyShouldThrowExceptionWithWrongBody()
        {
            Test.AssertException<HttpResponseAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CustomResponseAction())
                        .ShouldHave()
                        .HttpResponse()
                        .WithJsonBody(new RequestModel { Integer = 2, RequiredString = "Text" });
                },
                "When calling CustomResponseAction action in MvcController expected HTTP response body to be the given object, but in fact it was different.");
        }

        [Fact]
        public void WithJsonBodyAsStringShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomResponseAction())
                .ShouldHave()
                .HttpResponse()
                .WithJsonBody(@"{""Integer"":1,""RequiredString"":""Text""}");
        }
    }
}
