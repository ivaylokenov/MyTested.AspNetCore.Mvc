namespace MyTested.Mvc.Test.UtilitiesTests.ValidatorsTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Utilities.Validators;
    
    public class RuntimeBinderValidatorTests
    {
        // TODO: ?
        //[Test]
        //public void ValidateBindingShouldNotThrowExceptionWithValidPropertyCall()
        //{
        //    var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
        //        TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

        //    RuntimeBinderValidator.ValidateBinding(() =>
        //    {
        //        var contentNegotiator = (actionResultWithFormatters as dynamic).ContentNegotiator;
        //        Assert.IsNotNull(contentNegotiator);
        //    });
        //}

        //[Test]
        //[ExpectedException(
        //    typeof(InvalidCallAssertionException),
        //    ExpectedMessage = "Expected action result to contain a 'ModelState' property to test, but in fact such property was not found.")]
        //public void ValidateBindingShouldThrowExceptionWithInvalidPropertyCall()
        //{
        //    var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
        //        TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

        //    RuntimeBinderValidator.ValidateBinding(() =>
        //    {
        //        var contentNegotiator = (actionResultWithFormatters as dynamic).ModelState;
        //        Assert.IsNotNull(contentNegotiator);
        //    });
        //}
    }
}
