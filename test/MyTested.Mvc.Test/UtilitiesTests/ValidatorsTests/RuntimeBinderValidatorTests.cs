namespace MyTested.Mvc.Tests.UtilitiesTests.ValidatorsTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Utilities.Validators;
    
    public class RuntimeBinderValidatorTests
    {
        // TODO: ?
        //[Fact]
        //public void ValidateBindingShouldNotThrowExceptionWithValidPropertyCall()
        //{
        //    var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
        //        TestObjectFactory.GetUri(), 5, MyMvc.Controller<MvcController>().AndProvideTheController());

        //    RuntimeBinderValidator.ValidateBinding(() =>
        //    {
        //        var contentNegotiator = (actionResultWithFormatters as dynamic).ContentNegotiator;
        //        Assert.NotNull(contentNegotiator);
        //    });
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(InvalidCallAssertionException),
        //    ExpectedMessage = "Expected action result to contain a 'ModelState' property to test, but in fact such property was not found.")]
        //public void ValidateBindingShouldThrowExceptionWithInvalidPropertyCall()
        //{
        //    var actionResultWithFormatters = new CreatedNegotiatedContentResult<int>(
        //        TestObjectFactory.GetUri(), 5, MyMvc.Controller<MvcController>().AndProvideTheController());

        //    RuntimeBinderValidator.ValidateBinding(() =>
        //    {
        //        var contentNegotiator = (actionResultWithFormatters as dynamic).ModelState;
        //        Assert.NotNull(contentNegotiator);
        //    });
        //}
    }
}
