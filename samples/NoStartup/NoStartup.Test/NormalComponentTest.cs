namespace NoStartup.Test
{
    using Components;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.AspNetCore.Mvc;

    [TestClass]
    public class NormalComponentTest
    {
        [TestMethod]
        public void InvokeAsyncShouldReturnViewWithCorrectModel()
            => MyViewComponent<NormalComponent>
                .Instance()
                .WithServices(services => services
                    .With(ServiceMock.GetInstance()))
                .InvokedWith(c => c.InvokeAsync())
                .ShouldReturn()
                .View()
                .WithModel(new[] { "Mock", "Test" });
    }
}
