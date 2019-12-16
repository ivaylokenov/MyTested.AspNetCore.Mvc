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
                .WithDependencies(dependencies => dependencies
                    .With(ServiceMock.GetInstance()))
                .InvokedWith(c => c.InvokeAsync())
                .ShouldReturn()
                .View(view => view
                    .WithModel(new[] { "Mock", "Test" }));
    }
}
