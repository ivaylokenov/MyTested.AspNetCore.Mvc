namespace Lite.Test
{
    using Moq;
    using Web.Services;

    public static class Mocks
    {
        public static IData GetData()
        {
            var mock = new Mock<IData>();

            mock.Setup(m => m.Get()).Returns(new[] { "Mocked", "Test", "Data" });

            return mock.Object;
        }
    }
}
