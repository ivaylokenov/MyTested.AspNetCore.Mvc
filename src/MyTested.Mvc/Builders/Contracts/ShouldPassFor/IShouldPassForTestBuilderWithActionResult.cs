namespace MyTested.Mvc.Builders.Contracts.ShouldPassFor
{
    using System;

    public interface IShouldPassForTestBuilderWithActionResult<TActionResult> : IShouldPassForTestBuilderWithInvokedAction
    {
        IShouldPassForTestBuilderWithActionResult<TActionResult> TheActionResult(Action<TActionResult> assertions);

        IShouldPassForTestBuilderWithActionResult<TActionResult> TheActionResult(Func<TActionResult, bool> predicate);
    }
}
