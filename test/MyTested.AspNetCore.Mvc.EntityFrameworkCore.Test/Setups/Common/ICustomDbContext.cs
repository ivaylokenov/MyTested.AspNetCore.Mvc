namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using Microsoft.EntityFrameworkCore;

    public interface ICustomDbContext
    {
        DbSet<CustomModel> Models { get; set; }
    }
}
