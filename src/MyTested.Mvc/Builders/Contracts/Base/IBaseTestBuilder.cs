namespace MyTested.Mvc.Builders.Contracts.Base
{
    using ShouldPass;

    public interface IBaseTestBuilder
    {
        IShouldPassForTestBuilder ShouldPassFor();
    }
}
