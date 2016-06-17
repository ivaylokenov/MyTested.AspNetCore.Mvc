namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Actions.ShouldHave;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Builders.Contracts.Data;
    using Builders.Data;

    /// <summary>
    /// Contains <see cref="Microsoft.EntityFrameworkCore.DbContext"/> extension methods for <see cref="IShouldHaveTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldHaveTestBuilderEntityFrameworkCoreExtensions
    {
        /// <summary>
        /// Tests whether the action saves entities in the <see cref="Microsoft.EntityFrameworkCore.DbContext"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <param name="dbContextTestBuilder">Action containing all assertions on the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> entities.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> DbContext<TActionResult>(
            this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder,
            Action<IDbContextTestBuilder> dbContextTestBuilder)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            dbContextTestBuilder(new DbContextTestBuilder(actualShouldHaveTestBuilder.TestContext));
            
            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }
    }
}
