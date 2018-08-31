using JMeDQgddW9.Core.Entities;
using JMeDQgddW9.Data.Repositories.Interfaces;
using JMeDQgddW9.Service.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace JMeDQgddW9.Service.Services
{
    /// <summary>
    /// User service
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// User Repository instance
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository">User repository dependency injection</param>
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Insert a entity user in database
        /// </summary>
        /// <param name="entity">Object with user data</param>
        public void Insert(User entity)
        {
            userRepository.Insert(entity);
        }

        /// <summary>
        /// Get all users from database
        /// </summary>
        /// <returns>List of all users</returns>
        public IEnumerable<User> GetAll()
        {
            return userRepository.GetAll();
        }

        /// <summary>
        /// Get user entity by identifier
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>User entity</returns>
        public User GetById(int id)
        {
            return userRepository.GetById(id);
        }

        /// <summary>
        /// Get user entity by login
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>User entity</returns>
        public User GetByLogin(string login)
        {
            return userRepository.GetByLogin(login);
        }

        /// <summary>
        /// Update a entity user in database
        /// </summary>
        /// <param name="user">Object with user data</param>
        public void Update(User user)
        {
            userRepository.Update(user);
        }

        /// <summary>
        /// Delete a entity user in database
        /// </summary>
        /// <param name="entity">Object with user data</param>
        public void Delete(User entity)
        {
            userRepository.Delete(entity);
        }
    }
}