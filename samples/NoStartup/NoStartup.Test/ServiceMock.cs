namespace NoStartup.Test
{
    using Services;

    public class ServiceMock : IService
    {
        public static IService GetInstance() => new ServiceMock();

        public string[] GetData() => new[] { "Mock", "Test" };
    }
}
