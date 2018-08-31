using JMeDQgddW9.Core.Entities;
using JMeDQgddW9.Data.Repositories.Interfaces;
using JMeDQgddW9.Service.Services.Interfaces;

namespace JMeDQgddW9.Service.Services
{
    /// <summary>
    /// Authentication service 
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// Authentication repository instance
        /// </summary>
        private readonly IAuthenticationRepository authenticationRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authenticationRepository">Authentication repository dependency injection</param>
        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            this.authenticationRepository = authenticationRepository;
        }

        /// <summary>
        /// Get authentication token by login
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>Token object</returns>
        public Token GetToken(string login)
        {
            return authenticationRepository.GetToken(login);
        }

        /// <summary>
        /// Persist token at database
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="tokenValue">Generated token</param>
        public void PersistToken(int userId, string tokenValue)
        {
            authenticationRepository.PersistToken(userId, tokenValue);
        }
    }
}