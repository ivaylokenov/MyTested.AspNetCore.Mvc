namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.HttpTests
{
    using System.IO;
    using System.Text;
    using Microsoft.AspNetCore.Http;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class HttpRequestBuilderTests
    {
        [Fact]
        public void WithJsonBodyShouldUseTheProvidedEncoding()
        {
            var expectedBody = Encoding.Unicode.GetBytes(@"{""integer"":1,""requiredString"":""Текст"",""nonRequiredString"":null,""notValidateInteger"":0}");

            MyController<MvcController>
                .Instance()
                .WithHttpRequest(request => request
                    .WithJsonBody(new RequestModel
                    {
                        Integer = 1,
                        RequiredString = "Текст"
                    }, Encoding.Unicode))
                .ShouldPassForThe<HttpRequest>(builtRequest =>
                {
                    var body = ((MemoryStream)builtRequest.Body).ToArray();
                    Assert.Equal(expectedBody, body);
                    Assert.Equal(ContentType.ApplicationJson, builtRequest.ContentType);
                    Assert.Equal(expectedBody.Length, builtRequest.ContentLength);
                });
        }
    }
}
