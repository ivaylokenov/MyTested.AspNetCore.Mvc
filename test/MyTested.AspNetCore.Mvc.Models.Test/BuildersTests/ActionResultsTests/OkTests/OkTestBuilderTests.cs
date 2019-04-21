namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.OkTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class OkTestBuilderTests
    {
        [Fact]
        public void WithNoResponseModelShouldNotThrowExceptionIfNoResponseModelIsProvided()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok()
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
                        .Calling(c => c.OkResultWithResponse())
                        .ShouldReturn()
                        .Ok()
                        .WithNoModel();
                },
                "When calling OkResultWithResponse action in MvcController expected to not have a response model but in fact such was found.");
        }

        [Fact]
        public void WithResponseModelShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldHave()
                .NoActionAttributes()
                .AndAlso()
                .ShouldReturn()
                .Ok()
                .WithModelOfType<List<ResponseModel>>();
        }

        [Fact]
        public void WithResponseModelOfTypeShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldHave()
                .NoActionAttributes()
                .AndAlso()
                .ShouldReturn()
                .Ok()
                .WithModelOfType(typeof(IList<>))
                .AndAlso()
                .ShouldPassForThe<ICollection<ResponseModel>>(m => m != null);
        }
    }
}
