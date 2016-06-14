namespace MyTested.AspNetCore.Mvc.Builders.Contracts
{
    using System;

    public interface IOptionsBuilder
    {
        void For<TOptions>(Action<TOptions> optionsSetup) where TOptions : class, new();
    }
}
