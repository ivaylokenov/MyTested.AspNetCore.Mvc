namespace MyTested.AspNetCore.Mvc.Test.InternalTests
{
    using Internal;
    using Setups;
    using System;
    using Xunit;

    public class TestHelperTests
    {
        [Fact]
        public void TryGetShouldPassForValueShouldThrowExceptionWithInvalidComponentType()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    TestHelper.TryGetShouldPassForValue<TestHelperTests>(null);
                },
                "TestHelperTests could not be resolved for the 'ShouldPassForThe<TComponent>' method call.");
        }
    }
}
