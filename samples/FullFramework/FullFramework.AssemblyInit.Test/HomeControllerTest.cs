namespace FullFramework.AssemblyInit.Test
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
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(nameof(HomeController.Index))
                .WithModel("Test Data");

        [TestMethod]
        public void RedirectShouldRedirectToIndex()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.RedirectToIndex())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HomeController>(c => c.Index()));
    }
}
