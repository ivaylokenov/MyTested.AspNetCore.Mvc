namespace MyTested.Mvc.Tests.Setups
{
    using System;
    using Xunit;

    public class Test
    {
        public static void AssertException<TException>(Action testCode, string expectedMessage)
            where TException : Exception
        {
            var exception = Assert.Throws<TException>(testCode);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
