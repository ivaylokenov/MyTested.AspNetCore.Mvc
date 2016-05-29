namespace MyTested.Mvc.Internal.Contracts
{
    using System;

    public interface IValidControllersCache
    {
        bool IsValid(Type controllerType);
    }
}
