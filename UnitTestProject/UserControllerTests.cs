using JMeDQgddW9.Controllers;
using JMeDQgddW9.Core.Entities;
using JMeDQgddW9.Data;
using JMeDQgddW9.Data.Repositories;
using JMeDQgddW9.Data.Repositories.Interfaces;
using JMeDQgddW9.Models;
using JMeDQgddW9.Service.Services;
using JMeDQgddW9.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace UnitTestProject
{
    [TestClass]
    public class UserControllerTests
    {
        UserController userController;
        IUserService userService;
        IAuthenticationService authenticationService;
        IUserRepository userRepository;
        IAuthenticationRepository authenticationRepository;
        Context context;        

        [TestInitialize]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            DbContextOptionsBuilder<Context> optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:DBConnectionString"));
            context = new Context(optionsBuilder.Options);
            userRepository = new UserRepository(context);
            userService = new UserService(userRepository);
            authenticationRepository = new AuthenticationRepository(context);
            authenticationService = new AuthenticationService(authenticationRepository);
            userController = new UserController(userService, authenticationService);
        }

        public int CountUsers()
        {
            return context.Users.Count();
        }

        private static string EncryptPassword(string password)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(byteArray);
        }

        [TestMethod]
        public void GetAllUsers()
        {
            UserController controller = new UserController(userService, authenticationService);
            int totalUsers = controller.GetAll().Count();

            Assert.AreEqual(totalUsers, CountUsers());
        }

        [TestMethod]
        public void CreateUser()
        {
            string randomText = new Random().Next().ToString();
            UserController controller = new UserController(userService, authenticationService);
            controller.Create(new UserModel
            {

                Email = randomText + "@email.com",
                Login = randomText,
                Name = randomText,
                Password = randomText                
            });

            Assert.IsNotNull(context.Users.FirstOrDefault(user => user.Login == randomText));
        }

        [TestMethod]
        public void GetUser()
        {
            CreateUser();
            int id = context.Users.OrderByDescending(user => user.Id).First().Id;
            UserController controller = new UserController(userService, authenticationService);

            Assert.IsNotNull(controller.Get(id));
        }

        [TestMethod]
        public void DeleteUser()
        {
            CreateUser();
            int id = context.Users.OrderByDescending(user => user.Id).First().Id;
            UserController controller = new UserController(userService, authenticationService);

            Assert.IsNotNull(controller.Get(id));

            controller.Delete(id);

            Assert.IsNull(controller.Get(id));
        }

        [TestMethod]
        public void UpdateUser()
        {
            CreateUser();
            User entity = context.Users.OrderByDescending(user => user.Id).First();
            string name = entity.Name;
            UserController controller = new UserController(userService, authenticationService);

            Assert.IsNotNull(controller.Get(entity.Id));

            controller.Update(entity.Id, new UserModel
            {
                Email = entity.Email,
                Login = entity.Login,
                Name = entity.Name + "A",
                Phone = "+5541999887766"
            });
            UserModel model = controller.Get(entity.Id);

            Assert.AreNotEqual(model.Name, name);
            Assert.AreEqual(model.Phone, "+5541999887766");
        }

        [TestMethod]
        public void Login()
        {
            CreateUser();
            User entity = context.Users.OrderByDescending(user => user.Id).First();
            UserController controller = new UserController(userService, authenticationService);
            string token = controller.Login(new LoginModel
            {
                Login = entity.Login,
                Password = entity.Login
            });

            Assert.AreNotEqual(token, "Falha ao efetuar login.");
        }

        [TestMethod]
        public void ChangeUserPassword()
        {
            CreateUser();
            User entity = context.Users.OrderByDescending(user => user.Id).First();
            UserController controller = new UserController(userService, authenticationService);
            controller.ChangePassword(new LoginModel
            {
                Login = entity.Login,
                Password = entity.Login,
                NewPassword = "JMeDQgddW9"
            });

            string token = controller.Login(new LoginModel
            {
                Login = entity.Login,
                Password = "JMeDQgddW9"
            });

            Assert.AreNotEqual(token, "Falha ao efetuar login.");
        }
    }
}
