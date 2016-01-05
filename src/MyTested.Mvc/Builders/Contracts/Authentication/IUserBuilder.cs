namespace MyTested.Mvc.Builders.Contracts.Authentication
{
    using System.Collections.Generic;

    /// <summary>
    /// Used for building mocked Controller.User object.
    /// </summary>
    public interface IUserBuilder
    {
        /// <summary>
        /// Used for setting username to the mocked user object.
        /// </summary>
        /// <param name="username">The username to set.</param>
        /// <returns>The same user builder.</returns>
        IAndUserBuilder WithUsername(string username);

        /// <summary>
        /// Used for setting authentication type to the mocked user object.
        /// </summary>
        /// <param name="authenticationType">The authentication type to set.</param>
        /// <returns>The same user builder.</returns>
        IAndUserBuilder WithAuthenticationType(string authenticationType);

        /// <summary>
        /// Used for adding user role to the mocked user object.
        /// </summary>
        /// <param name="role">The user role to add.</param>
        /// <returns>The same user builder.</returns>
        IAndUserBuilder InRole(string role);

        /// <summary>
        /// Used for adding multiple user roles to the mocked user object.
        /// </summary>
        /// <param name="roles">Collection of roles to add.</param>
        /// <returns>The same user builder.</returns>
        IAndUserBuilder InRoles(IEnumerable<string> roles);

        /// <summary>
        /// Used for adding multiple user roles to the mocked user object.
        /// </summary>
        /// <param name="roles">Roles to add.</param>
        /// <returns>The same user builder.</returns>
        IAndUserBuilder InRoles(params string[] roles);
    }
}
