namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Authentication;
    using Contracts.ActionResults.Authentication;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="SignInResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder SignIn() => this.SignIn(null);

        /// <inheritdoc />
        public IAndTestBuilder SignIn(Action<ISignInTestBuilder> signInTestBuilder)
            => this.ValidateActionResult<SignInResult, ISignInTestBuilder>(
                signInTestBuilder,
                new SignInTestBuilder(this.TestContext));
    }
}
