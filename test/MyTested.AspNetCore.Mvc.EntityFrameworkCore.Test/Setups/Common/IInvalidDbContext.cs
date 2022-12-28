namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using Microsoft.EntityFrameworkCore;

    public interface IInvalidDbContext
    {
        DbSet<CustomModel> Models { get; set; }
    }
}
