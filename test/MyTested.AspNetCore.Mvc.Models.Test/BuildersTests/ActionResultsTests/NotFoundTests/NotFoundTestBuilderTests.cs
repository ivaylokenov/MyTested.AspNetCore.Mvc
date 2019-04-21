namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.NotFoundTests
{
    using System.Collections.Generic;
    using System.Net;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Xunit;

    public class NotFoundTestBuilderTests
    {
        [Fact]
        public void WithNoResponseModelShouldNotThrowExceptionWithNoResponseModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.HttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .WithNoModel();
        }

        [Fact]
        public void WithNoResponseModelShouldThrowExceptionWithAnyResponseModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.HttpNotFoundWithObjectAction())
                        .ShouldReturn()
                        .NotFound()
                        .WithNoModel();
                },
                "When calling HttpNotFoundWithObjectAction action in MvcController expected to not have a response model but in fact such was found.");
        }
    }
}
