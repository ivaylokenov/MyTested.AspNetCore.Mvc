namespace ApplicationParts.Test
{
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;
    using Web;

    [SetUpFixture]
    public class TestInit
    {
        [OneTimeSetUp]
        public void Init() 
            => MyApplication
                .StartsFrom<Startup>();
    }
}
