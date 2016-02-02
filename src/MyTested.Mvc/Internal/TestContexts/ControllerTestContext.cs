namespace MyTested.Mvc.Internal.TestContexts
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;

    public class ControllerTestContext
    {
        public Controller Controller { get; set; }

        public IEnumerable<object> ControllerAttributes { get; set; }
    }
}
