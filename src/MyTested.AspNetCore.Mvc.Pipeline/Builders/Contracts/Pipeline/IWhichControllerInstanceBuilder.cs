﻿namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Pipeline
{
    using Builders.Contracts.Actions;
    using Controllers;
    using Internal.Results;

    /// <summary>
    /// Used for building the controller which will be tested after a route assertion.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET Core MVC controller.</typeparam>
    public interface IWhichControllerInstanceBuilder<TController> 
        : IBaseControllerBuilder<TController, IAndWhichControllerInstanceBuilder<TController>>,
        IActionResultTestBuilder<MethodResult>,
        IVoidActionResultTestBuilder
        where TController : class
    {
    }
}
