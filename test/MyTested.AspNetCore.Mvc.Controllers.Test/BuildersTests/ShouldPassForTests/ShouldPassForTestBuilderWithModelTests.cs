namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ShouldPassForTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ShouldPassForTestBuilderWithModelTests
    {
        [Fact]
        public void ModelAssertionsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .WithModelOfType<ICollection<ResponseModel>>()
                .ShouldPassForThe<ICollection<ResponseModel>>(model =>
                {
                    Assert.NotNull(model);
                });
        }

        [Fact]
        public void ModelPredicateShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .WithModelOfType<ICollection<ResponseModel>>()
                .ShouldPassForThe<ICollection<ResponseModel>>(model => model != null);
        }

        [Fact]
        public void ModelPredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldReturn()
                        .Ok()
                        .WithModelOfType<ICollection<ResponseModel>>()
                        .ShouldPassForThe<ICollection<ResponseModel>>(model => model == null);
                },
                "Expected the List<ResponseModel> to pass the given predicate but it failed.");
        }
    }
}
