namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Conflict
{
    /// <summary>
    /// Used for adding AndAlso() method to
    /// the <see cref="Microsoft.AspNetCore.Mvc.ConflictResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.ConflictObjectResult"/> tests.
    /// </summary>
    public interface IAndConflictTestBuilder : IConflictTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when
        /// chaining <see cref="Microsoft.AspNetCore.Mvc.ConflictResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.ConflictObjectResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IConflictTestBuilder"/>.</returns>
        IConflictTestBuilder AndAlso();
    }
}
