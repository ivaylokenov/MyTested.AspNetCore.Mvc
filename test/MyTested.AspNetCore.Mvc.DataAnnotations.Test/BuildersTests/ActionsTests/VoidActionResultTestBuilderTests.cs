namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests
{
    using Exceptions;
    using Setups.Controllers;
    using Setups;
    using Xunit;

    public class VoidActionResultTestBuilderTests
    {
        [Fact]
        public void ShouldHaveModelStateShouldWorkCorrectly()
        {
            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.EmptyAction())
                        .ShouldHave()
                        .InvalidModelState();
                },
                "When calling EmptyAction action in MvcController expected to have invalid model state, but was in fact valid.");
        }
    }
}
