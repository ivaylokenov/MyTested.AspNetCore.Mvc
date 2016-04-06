namespace MyTested.Mvc.Builders.Contracts.ShouldPassFor
{
    using System;
    using System.Collections.Generic;

    public interface IShouldPassForTestBuilderWithController<TController> : IShouldPassForTestBuilder
        where TController : class
    {
        IShouldPassForTestBuilderWithController<TController> TheController(Action<TController> assertions);

        IShouldPassForTestBuilderWithController<TController> TheController(Func<TController, bool> predicate);

        IShouldPassForTestBuilderWithController<TController> TheControllerAttributes(Action<IEnumerable<object>> assertions);

        IShouldPassForTestBuilderWithController<TController> TheControllerAttributes(Func<IEnumerable<object>, bool> predicate);
    }
}
