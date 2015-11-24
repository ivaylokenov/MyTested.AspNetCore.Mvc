namespace MyTested.Mvc.Tests.BuildersTests.ModelsTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;
    using Setups;

    public class ResponseModelDetailsTestBuilderTests
    {
        [Fact]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .Passing(m =>
                {
                    Assert.Equal(2, m.Count);
                    Assert.Equal(1, m.First().IntegerValue);
                });
        }

        [Fact] // TODO: ? exception?
        public void WithResponseModelShouldThrowExceptionWithIncorrectAssertions()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .Passing(m =>
                {
                    Assert.Equal(1, m.First().IntegerValue);
                    Assert.Equal(3, m.Count);
                });
        }

        [Fact]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .Passing(m => m.First().IntegerValue == 1);
        }

        [Fact]
        public void WithResponseModelShouldThrowExceptionWithWrongPredicate()
        {
            Test.AssertException<ResponseModelAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.OkResultWithResponse())
                    .ShouldReturn()
                    .Ok()
                    .WithResponseModelOfType<IList<ResponseModel>>()
                    .Passing(m => m.First().IntegerValue == 2);
            }, "When calling OkResultWithResponse action in MvcController expected response model IList<ResponseModel> to pass the given condition, but it failed.");
        }
    }
}
