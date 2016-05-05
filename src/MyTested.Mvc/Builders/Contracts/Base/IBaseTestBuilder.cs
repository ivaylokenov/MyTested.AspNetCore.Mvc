namespace MyTested.Mvc.Builders.Contracts.Base
{
    using ShouldPassFor;

    public interface IBaseTestBuilder
    {
        /// <summary>
        /// Allows additional testing on various components.
        /// </summary>
        /// <returns>Test builder of <see cref="IShouldPassForTestBuilder"/> type.</returns>
        IShouldPassForTestBuilder ShouldPassFor();
    }
}
