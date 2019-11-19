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
        public void WithResponseModelShouldNotThrowExceptionPassingCorrectAssertions()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<ICollection<ResponseModel>>()
                    .Passing(m =>
                    {
                        Assert.Equal(2, m.Count);
                        Assert.Equal(1, m.First().IntegerValue);
                    }));
        }

        [Fact]
        public void WithResponseModelShouldNotThrowExceptionWithActionWithCorrectAssertions()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<ICollection<ResponseModel>>(m =>
                    {
                        Assert.Equal(2, m.Count);
                        Assert.Equal(1, m.First().IntegerValue);
                    }));
        }

        [Fact]
        public void WithResponseModelShouldThrowExceptionPassingIncorrectAssertions()
        {
            Assert.Throws<EqualException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultWithResponse())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModelOfType<ICollection<ResponseModel>>()
                            .Passing(m =>
                            {
                                Assert.Equal(1, m.First().IntegerValue);
                                Assert.Equal(3, m.Count);
                            }));
                });
        }

        [Fact]
        public void WithResponseModelShouldThrowExceptionWithActionWithIncorrectAssertions()
        {
            Assert.Throws<EqualException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultWithResponse())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModelOfType<ICollection<ResponseModel>>(m =>
                            {
                                Assert.Equal(1, m.First().IntegerValue);
                                Assert.Equal(3, m.Count);
                            }));
                });
        }

        [Fact]
        public void WithResponseModelShouldNotThrowExceptionPassingCorrectPredicate()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<ICollection<ResponseModel>>()
                    .Passing(m => m.First().IntegerValue == 1));
        }

        [Fact]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<ICollection<ResponseModel>>(m => m.First().IntegerValue == 1));
        }

        [Fact]
        public void WithResponseModelShouldThrowExceptionPassingWrongPredicate()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultWithResponse())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModelOfType<IList<ResponseModel>>()
                            .Passing(m => m.First().IntegerValue == 2));
                }, 
                "When calling OkResultWithResponse action in MvcController expected response model IList<ResponseModel> to pass the given predicate, but it failed.");
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
                        .Ok(ok => ok
                            .WithModelOfType<IList<ResponseModel>>(m => m.First().IntegerValue == 2));
                },
                "When calling OkResultWithResponse action in MvcController expected response model IList<ResponseModel> to pass the given predicate, but it failed.");
        }
    }
}
