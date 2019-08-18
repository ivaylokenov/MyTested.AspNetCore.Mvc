namespace FullFramework.AssemblyInit.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using MyTested.AspNetCore.Mvc;
    using Web;
    using Web.Services;

    [TestClass]
    public class TestInit
    {
        [AssemblyInitialize]
        public static void Init(TestContext context)
            => MyApplication
                .StartsFrom<Startup>()
                .WithServices(services => services
                    .ReplaceTransient<IDataService, DataServiceMock>());
    }
}
