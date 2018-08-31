using JMeDQgddW9.Core.Entities;

namespace JMeDQgddW9.Service.Services.Interfaces
{
    /// <summary>
    /// Authentication service interface
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Get authentication token by login
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>Token object</returns>
        Token GetToken(string login);

        /// <summary>
        /// Persist token at database
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="tokenValue">Generated token</param>
        void PersistToken(int userId, string tokenValue);
    }
}