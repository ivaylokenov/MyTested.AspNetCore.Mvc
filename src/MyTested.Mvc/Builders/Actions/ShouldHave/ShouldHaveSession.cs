namespace MyTested.Mvc.Builders.Actions.ShouldHave
{
    using Contracts.And;
    using Contracts.Data;
    using Data;
    using System;
    using Utilities.Validators;

    /// <summary>
    /// Class containing methods for testing session.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC controller.</typeparam>
    public partial class ShouldHaveTestBuilder<TActionResult>
    {
        public IAndTestBuilder<TActionResult> Session(int? withNumberOfEntries = null)
        {
            return this.NewAndTestBuilder();
        }

        public IAndTestBuilder<TActionResult> Session(Action<ISessionTestBuilder> sessionTestBuilder)
        {
            CommonValidator.CheckForException(this.CaughtException);

            sessionTestBuilder(new SessionTestBuilder(this.TestContext));

            return this.NewAndTestBuilder();
        }
    }
}
