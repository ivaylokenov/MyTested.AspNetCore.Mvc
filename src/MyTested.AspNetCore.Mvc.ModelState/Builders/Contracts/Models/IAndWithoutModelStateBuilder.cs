namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    public interface IAndWithoutModelStateBuilder : IWithoutModelStateBuilder
    {
        IWithoutModelStateBuilder AndAlso();
    }
}
