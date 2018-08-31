using JMeDQgddW9.Core.Entities;
using JMeDQgddW9.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMeDQgddW9.Data.Repositories
{
    /// <summary>
    /// User repository 
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Context instance
        /// </summary>
        protected Context context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Context dependency injection</param>
        public UserRepository(Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Insert a entity user in database
        /// </summary>
        /// <param name="entity">Object with user data</param>
        public void Insert(User entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity", Properties.Resources.NullUserEntity);
                }

                context.Users.Add(entity);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all users from database
        /// </summary>
        /// <returns>List of all users</returns>
        public IEnumerable<User> GetAll()
        {
            try
            {
                return context.Users;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get user entity by identifier
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>User entity</returns>        
        public User GetById(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentNullException("id", Properties.Resources.InvalidArguments);
                } 

                return context.Users.FirstOrDefault(user => user.Id == id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get user entity by login
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>User entity</returns>        
        public User GetByLogin(string login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.Trim()))
                {
                    throw new ArgumentNullException("login", Properties.Resources.InvalidArguments);
                }

                return context.Users.FirstOrDefault(user => user.Login == login);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update a entity user in database
        /// </summary>
        /// <param name="user">Object with user data</param>
        public void Update(User entity)
        {
            try
            {
                if(entity == null)
                {
                    throw new ArgumentNullException("entity", Properties.Resources.NullUserEntity);
                }

                if (entity.Id == 0)
                {
                    throw new ArgumentNullException("Id", Properties.Resources.InvalidArguments);
                }

                User userEntity = context.Users.FirstOrDefault(user => user.Id == entity.Id);
                if(userEntity != null)
                {
                    userEntity.Email = entity.Email;
                    userEntity.Name = entity.Name;
                    userEntity.Phone = entity.Phone;

                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete a entity user in database
        /// </summary>
        /// <param name="entity">Object with user data</param>
        public void Delete(User entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity", Properties.Resources.NullUserEntity);
                }

                if (entity.Id == 0)
                {
                    throw new ArgumentNullException("Id", Properties.Resources.InvalidArguments);
                }

                context.Users.Remove(entity);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}