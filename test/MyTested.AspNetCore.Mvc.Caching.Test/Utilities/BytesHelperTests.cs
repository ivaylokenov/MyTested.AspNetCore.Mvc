namespace MyTested.AspNetCore.Mvc.Test.Utilities
{
    using Mvc.Utilities;
    using Xunit;

    public class BytesHelperTests
    {
        [Fact]
        public void BytesHelperShouldReturnEmptyArrayOnEmptyString()
        {
            var input = string.Empty;
            var expectedBytes = new byte[0];

            var actualBytes = BytesHelper.GetBytes(input);
            
            Assert.Equal(expectedBytes, actualBytes);
        }

        [Fact]
        public void BytesHelperShouldReturnCorrectBytesForString()
        {
            var input = "Test";
            var expectedBytes = new byte[4] {84, 101, 115, 116};

            var actualBytes = BytesHelper.GetBytes(input);

            Assert.Equal(expectedBytes, actualBytes);
        }
    }
}
