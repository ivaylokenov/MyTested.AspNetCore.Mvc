namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Internal.TestContexts;

    public interface IShouldPassForPlugin
    {
        object TryGetValue(Type type, ComponentTestContext testContext);
    }
}
