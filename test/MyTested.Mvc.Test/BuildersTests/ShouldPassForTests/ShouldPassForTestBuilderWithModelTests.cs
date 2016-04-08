namespace MyTested.Mvc.Test.BuildersTests.ShouldPassForTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using System.Collections.Generic;
    using Xunit;

    public class ShouldPassForTestBuilderWithModelTests
    {
        [Fact]
        public void ModelAssertionsShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .ShouldPassFor()
                .TheModel(model =>
                {
                    Assert.NotNull(model);
                });
        }

        [Fact]
        public void ModelPredicateShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .ShouldPassFor()
                .TheModel(model => model != null);
        }

        [Fact]
        public void ModelPredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullOkAction())
                        .ShouldReturn()
                        .Ok()
                        .WithResponseModelOfType<ICollection<ResponseModel>>()
                        .ShouldPassFor()
                        .TheModel(model => model == null);
                },
                "Expected the List<ResponseModel> to pass the given predicate but it failed.");
        }
    }
}
