namespace MyTested.Mvc.Builders.Contracts.Base
{
    using ShouldPassFor;

    public interface IBaseTestBuilder
    {
        IShouldPassForTestBuilder ShouldPassFor();
    }
}
