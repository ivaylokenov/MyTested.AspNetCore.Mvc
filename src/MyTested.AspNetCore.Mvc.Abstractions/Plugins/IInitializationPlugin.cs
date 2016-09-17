namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;

    public interface IInitializationPlugin
    {
        Action<IServiceProvider> InitializationDelegate { get; }
    }
}
