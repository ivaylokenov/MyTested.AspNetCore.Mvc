namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    public interface IWithoutModelStateBuilder
    {
        IAndWithoutModelStateBuilder WithoutModelState();

        IAndWithoutModelStateBuilder WithoutModelState(string key);
    }
}