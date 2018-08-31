using JMeDQgddW9.Core.Entities;
using JMeDQgddW9.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace JMeDQgddW9.Data.Repositories
{
    /// <summary>
    /// Authentication repository
    /// </summary>
    public class AuthenticationRepository : IAuthenticationRepository
    {
        /// <summary>
        /// Context instance
        /// </summary>
        protected Context context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Context dependency injection</param>
        public AuthenticationRepository(Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get authentication token by login
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>Token object</returns>
        public Token GetToken(string login)
        {
            try
            {
                return context.Users
                    .Include(user => user.Token)
                    .FirstOrDefault(user => user.Login == login).Token;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Persist token at database
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="tokenValue">Generated token</param>
        public void PersistToken(int userId, string tokenValue)
        {
            try
            {
                User entity = context.Users.Include(user => user.Token).FirstOrDefault(user => user.Id == userId);
                if (entity != null)
                {
                    if (entity.Token == null)
                    {
                        entity.Token = new Token { UserId = entity.Id };
                    }

                    entity.Token.ExpirationDate = DateTime.Now.AddMinutes(30);
                    entity.Token.TokenValue = tokenValue;
                    context.SaveChanges();
                }
            }
            catch
            {
                throw new Exception(Properties.Resources.PersistTokenFail);
            }
        }
    }
}