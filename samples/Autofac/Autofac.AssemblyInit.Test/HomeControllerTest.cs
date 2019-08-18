namespace Autofac.AssemblyInit.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.AspNetCore.Mvc;
    using Web.Controllers;

    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void CustomRouteShouldMatchCorrectController()
            => MyRouting
                .Configuration()
                .ShouldMap("/Index")
                .To<HomeController>(c => c.Index());

        [TestMethod]
        public void IndexShouldReturnViewWithCorrectModel()
            => MyController<HomeController>
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                    .WithName(nameof(HomeController.Index))
                    .WithModel("2020 Test Data"));

        [TestMethod]
        public void RedirectShouldRedirectToIndex()
            => MyController<HomeController>
                .Calling(c => c.RedirectToIndex())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HomeController>(c => c.Index()));
    }
}
