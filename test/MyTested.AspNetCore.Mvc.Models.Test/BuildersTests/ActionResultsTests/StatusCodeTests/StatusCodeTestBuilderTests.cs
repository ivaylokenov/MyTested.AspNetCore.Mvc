namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.StatusCodeTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class StatusCodeTestBuilderTests
    {
        [Fact]
        public void WithNoResponseModelShouldNotThrowExceptionIfNoResponseModelIsProvided()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.StatusCodeAction())
                .ShouldReturn()
                .StatusCode()
                .WithNoModel();
        }

        [Fact]
        public void WithNoResponseModelShouldThrowExceptionIfResponseModelIsProvided()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .StatusCode()
                        .WithNoModel();
                },
                "When calling FullObjectResultAction action in MvcController expected to not have a response model but in fact such was found.");
        }
        
        [Fact]
        public void WithResponseModelShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .StatusCode()
                .WithModelOfType<List<ResponseModel>>();
        }
    }
}
