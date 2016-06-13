namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Used for building <see cref="DbContext"/>.
    /// </summary>
    public interface IDbContextBuilder
    {
        /// <summary>
        /// Sets initial values to the provided <see cref="DbContext"/> service.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <param name="dbContextSetup">Action setting the <see cref="DbContext"/>.</param>
        void WithSetup<TDbContext>(Action<TDbContext> dbContextSetup)
            where TDbContext : DbContext;
    }
}
