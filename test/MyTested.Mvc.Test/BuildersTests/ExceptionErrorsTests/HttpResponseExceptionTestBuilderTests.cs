namespace MyTested.Mvc.Tests.BuildersTests.ExceptionErrorsTests
{
    using System.Net;
    using Exceptions;
    using Setups.Controllers;
    using Xunit;
    
    public class HttpResponseExceptionTestBuilderTests
    {
        // TODO: no http response exception
        //[Fact]
        //public void ShouldThrowHttpResponseExceptionShouldCatchAndValidateHttpResponseExceptionStatusCode()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ActionWithHttpResponseException())
        //        .ShouldThrow()
        //        .HttpResponseException()
        //        .WithStatusCode(HttpStatusCode.NotFound);
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(HttpStatusCodeResultAssertionException),
        //    ExpectedMessage = "When calling ActionWithHttpResponseException action in MvcController expected HttpResponseException to have 202 (Accepted) status code, but received 404 (NotFound).")]
        //public void ShouldThrowHttpResponseExceptionShouldThrowWithInvalidHttpResponseExceptionStatusCode()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ActionWithHttpResponseException())
        //        .ShouldThrow()
        //        .HttpResponseException()
        //        .WithStatusCode(HttpStatusCode.Accepted);
        //}

        //[Fact]
        //public void ShouldThrowHttpResponseExceptionShouldBeAbleToTestHttpResponseMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ActionWithHttpResponseExceptionAndHttpResponseMessageException())
        //        .ShouldThrow()
        //        .HttpResponseException()
        //        .WithHttpResponseMessage()
        //        .WithStatusCode(HttpStatusCode.InternalServerError);
        //}
    }
}
