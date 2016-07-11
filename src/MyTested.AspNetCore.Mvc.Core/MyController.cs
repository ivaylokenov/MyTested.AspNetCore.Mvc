namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Controllers;
    using Builders.Controllers;
    using Internal.Application;
    using Internal.TestContexts;

    public class MyController<TController> : ControllerBuilder<TController>
        where TController : class
    {
        static MyController()
        {
            TestApplication.TryInitialize();
        }

        public MyController()
            : this((TController)null)
        {
        }

        public MyController(TController controller)
            : this(() => controller)
        {
        }

        public MyController(Func<TController> construction)
            : base(new ControllerTestContext { ControllerConstruction = construction })
        {
        }

        public static IControllerBuilder<TController> Instance()
        {
            return Instance((TController)null);
        }

        public static IControllerBuilder<TController> Instance(TController controller)
        {
            return Instance(() => controller);
        }

        public static IControllerBuilder<TController> Instance(Func<TController> construction)
        {
            return new ControllerBuilder<TController>(new ControllerTestContext { ControllerConstruction = construction });
        }
    }
}
