namespace ApplicationParts.Test
{
    using Controllers;
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;

    [TestFixture]
    public class Tests
    {
        [Test]
        public void CustomRouteShouldMatchCorrectController()
            => MyRouting
                .Configuration()
                .ShouldMap("/CustomRoute")
                .To<HomeController>(c => c.Index());

        [Test]
        public void HomeControllerShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();
        
        [Test]
        public void RedirectShouldRedirectToIndex()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.RedirectToIndex())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HomeController>(c => c.Index()));
    }
}
