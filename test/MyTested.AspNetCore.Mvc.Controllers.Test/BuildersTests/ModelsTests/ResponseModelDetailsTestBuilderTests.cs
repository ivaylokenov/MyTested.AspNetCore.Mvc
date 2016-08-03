namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ModelsTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;
    using Xunit.Sdk;

    public class ResponseModelDetailsTestBuilderTests
    {
        [Fact]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithModelOfType<ICollection<ResponseModel>>()
                .Passing(m =>
                {
                    Assert.Equal(2, m.Count);
                    Assert.Equal(1, m.First().IntegerValue);
                });
        }

        [Fact]
        public void WithResponseModelShouldThrowExceptionWithIncorrectAssertions()
        {
            Assert.Throws<EqualException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultWithResponse())
                        .ShouldReturn()
                        .Ok()
                        .WithModelOfType<ICollection<ResponseModel>>()
                        .Passing(m =>
                        {
                            Assert.Equal(1, m.First().IntegerValue);
                            Assert.Equal(3, m.Count);
                        });
                });
        }

        [Fact]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithModelOfType<ICollection<ResponseModel>>()
                .Passing(m => m.First().IntegerValue == 1);
        }

        [Fact]
        public void WithResponseModelShouldThrowExceptionWithWrongPredicate()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultWithResponse())
                        .ShouldReturn()
                        .Ok()
                        .WithModelOfType<IList<ResponseModel>>()
                        .Passing(m => m.First().IntegerValue == 2);
                }, 
                "When calling OkResultWithResponse action in MvcController expected response model IList<ResponseModel> to pass the given condition, but it failed.");
        }
    }
}
