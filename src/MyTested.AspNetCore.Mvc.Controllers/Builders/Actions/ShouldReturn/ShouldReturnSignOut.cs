namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Authentication;
    using Contracts.ActionResults.Authentication;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="SignOutResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder SignOut() => this.SignOut(null);

        /// <inheritdoc />
        public IAndTestBuilder SignOut(Action<ISignOutTestBuilder> signOutTestBuilder)
            => this.ValidateActionResult<SignOutResult, ISignOutTestBuilder>(
                signOutTestBuilder,
                new SignOutTestBuilder(this.TestContext));
    }
}
