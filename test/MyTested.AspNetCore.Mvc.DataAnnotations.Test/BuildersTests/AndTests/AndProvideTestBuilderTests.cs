namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AndTests
{
    using System;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    using Microsoft.AspNetCore.Mvc;

    public class AndProvideTestBuilderTests
    {
        [Fact]
        public void AndProvideShouldThrowExceptionIfActionIsVoid()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.EmptyActionWithException())
                        .ShouldHave()
                        .ValidModelState()
                        .AndAlso()
                        .ShouldPassForThe<IActionResult>(actionResult => actionResult != null);
                },
                "IActionResult could not be resolved for the 'ShouldPassForThe<TComponent>' method call.");
        }
    }
}
