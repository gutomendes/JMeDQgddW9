using JMeDQgddW9.Core.Entities;
using JMeDQgddW9.Models;
using JMeDQgddW9.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMeDQgddW9.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [Route("api/[controller]/[action]")]    
    [ApiController]
    public class UserController : BaseController
    {
        /// <summary>
        /// User service instance
        /// </summary>
        private IUserService userService;

        /// <summary>
        /// User service instance
        /// </summary>
        private IAuthenticationService authenticationService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService">User service dependency injection</param>
        public UserController(IUserService userService, IAuthenticationService authenticationService)
        {
            this.userService = userService;
            this.authenticationService = authenticationService;
        }

        /// <summary>
        /// POST: api/User
        /// </summary>
        /// <param name="model">User model</param>
        [HttpPost]
        public void Create(UserModel model)
        {
            User entity = new User
            {
                Email = model.Email,
                Name = model.Name,
                Login = model.Login,
                Password = EncryptPassword(model.Password),
                Phone = model.Phone
            };

            userService.Insert(entity);
        }

        /// <summary>
        /// GET: api/User
        /// </summary>
        /// <returns>List of all users</returns>
        [HttpGet]
        public IEnumerable<UserModel> GetAll()
        {
            return userService.GetAll()
                .Select(user => new UserModel
                {
                    Email = user.Email,
                    Id = user.Id,
                    Login = user.Login,
                    Name = user.Name,
                    Phone = user.Phone
                });
        }

        /// <summary>
        /// GET: api/User/5
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>User</returns>
        [HttpGet("{id}")]
        public UserModel Get(int id)
        {
            User user = userService.GetById(id);

            if (user != null)
            {
                return new UserModel
                {
                    Email = user.Email,
                    Id = user.Id,
                    Login = user.Login,
                    Name = user.Name,
                    Phone = user.Phone
                };
            }

            return null;
        }

        /// <summary>
        /// PUT: api/User
        /// </summary>        
        /// <param name="model">User model</param>
        [HttpPut("{id}")]
        public void Update(int id, UserModel model)
        {
            User entity = userService.GetById(id);

            entity.Id = id;
            entity.Email = model.Email;
            entity.Login = model.Login;
            entity.Name = model.Name;
            entity.Phone = model.Phone;

            userService.Update(entity);
        }

        /// <summary>
        /// DELETE: api/ApiWithActions/5
        /// </summary>
        /// <param name="id">User identifier</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (id > 0)
            {
                User entity = userService.GetById(id);
                userService.Delete(entity);
            }
        }

        /// <summary>
        /// Verify login and password to generate token
        /// </summary>
        /// <param name="login">Login object</param>
        /// <returns>Token text</returns>
        [HttpPost]
        [AllowAnonymous]
        public string Login(LoginModel login)
        {
            try
            {
                string userLogin = login.Login;
                string userPassword = EncryptPassword(login.Password);
                //string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                string token = Guid.NewGuid().ToString();
                User user = userService.GetByLogin(userLogin);
                if (user != null && user.Password == userPassword)
                {
                    authenticationService.PersistToken(user.Id, token);
                    return token;
                }

                throw new Exception(Properties.Resources.LoginFail);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="login">Login object</param>
        public void ChangePassword(LoginModel login)
        {
            string userLogin = login.Login;
            string userPassword = EncryptPassword(login.Password);
            User user = userService.GetByLogin(userLogin);
            if (user != null && user.Password == userPassword)
            {
                user.Password = EncryptPassword(login.NewPassword);
                userService.Update(user);
            }
        }

        /// <summary>
        /// Encrypt password
        /// </summary>
        /// <param name="password">Password to encrypt</param>
        /// <returns>Encrypted passowrd text</returns>
        private static string EncryptPassword(string password)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(byteArray);
        }
    }
}