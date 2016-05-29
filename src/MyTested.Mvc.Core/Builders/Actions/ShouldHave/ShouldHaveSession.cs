namespace MyTested.Mvc.Builders.Actions.ShouldHave
{
    using System;
    using System.Linq;
    using Contracts.And;
    using Contracts.Data;
    using Data;

    /// <content>
    /// Class containing methods for testing <see cref="Microsoft.AspNetCore.Http.ISession"/>.
    /// </content>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> NoSession()
        {
            if (this.TestContext.Session.Keys.Any())
            {
                this.ThrowNewDataProviderAssertionExceptionWithNoEntries(SessionTestBuilder.SessionName);
            }

            return this.NewAndTestBuilder();
        }

        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> Session(int? withNumberOfEntries = null)
        {
            this.ValidateDataProviderNumberOfEntries(
                SessionTestBuilder.SessionName,
                withNumberOfEntries,
                this.TestContext.Session.Keys.Count());

            return this.NewAndTestBuilder();
        }

        /// <inheritdoc />
        public IAndTestBuilder<TActionResult> Session(Action<ISessionTestBuilder> sessionTestBuilder)
        {
            sessionTestBuilder(new SessionTestBuilder(this.TestContext));
            return this.NewAndTestBuilder();
        }
    }
}
