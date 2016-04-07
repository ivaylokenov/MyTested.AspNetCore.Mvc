namespace MyTested.Mvc.Builders.Contracts.ShouldPassFor
{
    using System;
    using System.Collections.Generic;

    public interface IShouldPassForTestBuilderWithAction : IShouldPassForTestBuilderWithController<object>
    {
        IShouldPassForTestBuilderWithAction TheAction(Action<string> assertions);

        IShouldPassForTestBuilderWithAction TheAction(Func<string, bool> predicate);
        
        IShouldPassForTestBuilderWithAction TheActionAttributes(Action<IEnumerable<object>> assertions);

        IShouldPassForTestBuilderWithAction TheActionAttributes(Func<IEnumerable<object>, bool> predicate);
    }
}
