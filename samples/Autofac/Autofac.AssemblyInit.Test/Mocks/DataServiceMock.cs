namespace Autofac.AssemblyInit.Test.Mocks
{
    using Web.Services;

    public class DataServiceMock : IDataService
    {
        public string GetData() => "Test Data";
    }
}
