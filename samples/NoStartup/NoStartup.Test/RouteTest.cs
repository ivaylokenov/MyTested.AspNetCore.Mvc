namespace NoStartup.Test
{
    using System;
    using Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.AspNetCore.Mvc;

    [TestClass]
    public class RouteTest
    {
        [TestMethod]
        public void RouteTestShouldThrowExceptionWithoutStartupClass()
        {
            var exception = Assert.ThrowsException<InvalidOperationException>(() =>
            {
                MyRouting
                    .Configuration()
                    .ShouldMap("/")
                    .To<HomeController>(c => c.Index());
            });

            Assert.AreEqual("Testing routes without a Startup class is not supported. Set the 'General:NoStartup' option in the test configuration ('testsettings.json' file by default) to 'true' and provide a Startup class.", exception.Message);
        }
    }
}
