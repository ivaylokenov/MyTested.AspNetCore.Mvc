namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.JsonTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class JsonTestBuilderTests
    {
        [Fact]
        public void WithResponseModelOfTypeShouldWorkCorrectlyWithJson()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json(json => json
                    .WithModelOfType<ICollection<ResponseModel>>());
        }

        [Fact]
        public void WithHttpStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithStatusCodeAction())
                .ShouldReturn()
                .Json(json => json
                    .WithStatusCode(200));
        }

        [Fact]
        public void WithHttpStatusCodeShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithStatusCodeAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithStatusCode(500));
                },
                "When calling JsonWithStatusCodeAction action in MvcController expected JSON result to have 500 (InternalServerError) status code, but instead received 200 (OK).");
        }

        [Fact]
        public void WithContentTypeShouldNotThrowExceptionWithCorrectContentType()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithStatusCodeAction())
                .ShouldReturn()
                .Json(json => json
                    .WithContentType(ContentType.ApplicationXml));
        }

        [Fact]
        public void WithContentTypeShouldThrowExceptionWithIncorrectContentType()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithStatusCodeAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithContentType(ContentType.ApplicationJson));
                },
                "When calling JsonWithStatusCodeAction action in MvcController expected JSON result ContentType to be 'application/json', but instead received 'application/xml'.");
        }

        [Fact]
        public void WithContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectContentType()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithStatusCodeAction())
                .ShouldReturn()
                .Json(json => json
                    .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml)));
        }

        [Fact]
        public void WithContentTypeAsMediaTypeHeaderValueShouldThrowExceptionWithNullContentType()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithStatusCodeAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithContentType((MediaTypeHeaderValue)null));
                },
                "When calling JsonWithStatusCodeAction action in MvcController expected JSON result ContentType to be null, but instead received 'application/xml'.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithStatusCodeAction())
                .ShouldReturn()
                .Json(json => json
                    .WithStatusCode(200)
                    .AndAlso()
                    .WithContentType(ContentType.ApplicationXml));
        }

        [Fact]
        public void ShouldPassForTheShouldWorkCorrectlyWithActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json()
                .AndAlso()
                .ShouldPassForThe<ActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<JsonResult>(actionResult);
                });
        }

        [Fact]
        public void ShouldPassForTheShouldWorkCorrectlyWithModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json()
                .AndAlso()
                .ShouldPassForThe<ICollection<ResponseModel>>(model => model.Count == 2);
        }

        [Fact]
        public void WithNullJsonShouldReturnNoModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NullJsonAction())
                .ShouldReturn()
                .Json(json => json.WithNoModel());
        }
    }
}