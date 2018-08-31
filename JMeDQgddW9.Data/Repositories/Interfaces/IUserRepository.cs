using JMeDQgddW9.Core.Entities;
using System.Collections.Generic;

namespace JMeDQgddW9.Data.Repositories.Interfaces
{
    /// <summary>
    /// User repository interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Insert a entity user in database
        /// </summary>
        /// <param name="entity">Object with user data</param>
        void Insert(User entity);

        /// <summary>
        /// Get all users from database
        /// </summary>
        /// <returns>List of all users</returns>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Get user entity by identifier
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>User entity</returns>
        User GetById(int id);

        /// <summary>
        /// Get user entity by login
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>User entity</returns>        
        User GetByLogin(string login);

        /// <summary>
        /// Update a entity user in database
        /// </summary>
        /// <param name="entity">Object with user data</param>
        void Update(User entity);

        /// <summary>
        /// Delete a entity user in database
        /// </summary>
        /// <param name="entity">Object with user data</param>
        void Delete(User entity);
    }
}