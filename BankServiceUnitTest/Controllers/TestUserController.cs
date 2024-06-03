using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountModule.ApiResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UserModule.Controllers;
using UserModule.Models;
using UserModule.Service;

namespace BankServiceUnitTest.Controllers
{
    public class TestUserController
    {

        private Mock<IUserService> userservice;
        private Mock<ILogger<UserController>> logger;
        private UserController controller;

       

        
        public TestUserController()
        {
            userservice = new Mock<IUserService>();
            logger = new Mock<ILogger<UserController>>();
            controller = new UserController(userservice.Object, logger.Object);
        }
       
        [Fact]
        public async Task GetAllUsers()
        {
            var data = UsersData();
            userservice.Setup(service => service.GetAll()).ReturnsAsync(data);
            var result = await controller.GetUser();
        }

        [Fact]
        public async Task GetAllUsers_Empty()
        {
            var data = new List<Users>();
            userservice.Setup(service => service.GetAll()).ReturnsAsync(data);
            var result = await controller.GetUser();
            
            
        }

        [Fact]
        public async Task GetUserById()
        {
            var userId = 1;
            var data = UserData();
            userservice.Setup(service => service.GetById(userId)).ReturnsAsync(data);
            var result = await controller.GetUserById(userId);
           
        }

        [Fact]
        public async Task GetUserById_Invalid()
        {
            var userId = 0;
            userservice.Setup(service => service.GetById(userId));
            var result = await controller.GetUserById(userId);
           
        }

      

        [Fact]
        public async Task AddUser()
        {
            var data = UserData();
            userservice.Setup(service => service.Add(data)).ReturnsAsync(data);
            var result = await controller.AddUser(data);
            userservice.Verify(repo => repo.Add(It.IsAny<Users>()), Times.Once);

        }

        [Fact]
        public async Task AddUsers_Empty()
        {
            var data = new Users();
            userservice.Setup(service => service.Add(data)).ReturnsAsync(data);
            var result = await controller.AddUser(data);
        }

        [Fact]
        public async Task UpdateUser()
        {
            var userId = 1;
            var data = UserData();
            userservice.Setup(service => service.Update(userId, data)).ReturnsAsync(data);
            var result = await controller.UpdateUser(userId, data);
            userservice.Verify(repo => repo.Update(userId, data), Times.Once);

        }

        [Fact]
        public async Task UpdateUser_NotFoundId()
        {
            var userId = 16;
            var data = UserData();
            userservice.Setup(service => service.Update(userId, data)).ReturnsAsync(data);
            var result = await controller.UpdateUser(userId, data);
           

        }

        [Fact]
        public async Task UpdateUser_InvalidId()
        {
            var userId = -1;
            var data = UserData();
            userservice.Setup(service => service.Update(userId, data)).ReturnsAsync(data);

        }

        [Fact]
        public async Task DeleteUser()
        {
            var userId = 10;
            var data = UserData();
            userservice.Setup(service => service.Delete(userId)).ReturnsAsync(data);
            var result = await controller.DeleteUser(userId);
        }

        [Fact]
        public async Task DeleteUser_NotFoundId()
        {
            var userId = 20;
            userservice.Setup(service => service.Delete(userId));
            var result = await controller.DeleteUser(userId);
           
           
        }

        [Fact]
        public async Task DeleteUser_InvalidId()
        {
            var userId = 0;
            userservice.Setup(service => service.GetById(userId));
            var result = await controller.DeleteUser(userId);
           
            
        }

      

       
        private Users UserData()
        {
            Users users = new Users
            {
                UserId = 1,
                FirstName = "Sachin",
                LastName = "Tendulkar",
                Gender = 'M',
                Address = "Pune",
                Phone = 9898,
                Email = "sachin@gmail.com",
                PAN = "PRQS123",
                UID = "3039944",
                Status = "Active",
                Username = "sachin@gmail.com",
                Password = "12345",
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };
            return users;
        }

        private List<Users> UsersData()
        {
            var users = new List<Users>
            {
                new Users
                {
                 UserId = 1,
                FirstName = "Sachin",
                LastName = "Tendulkar",
                Gender = 'M',
                Address = "Pune",
                Phone = 984238,
                Email = "sachin@gmail.com",
                PAN = "PRQS123",
                UID = "3039944",
                Status = "Active",
                Username = "sachin@gmail.com",
                Password = "12345",
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now
                },
                new Users
                {
                 UserId = 2,
                FirstName = "Rohit",
                LastName = "Sharma",
                Gender = 'M',
                Address = "Pune",
                Phone = 9898,
                Email = "rohit@gmail.com",
                PAN = "ABCS123",
                UID = "3039944",
                Status = "Active",
                Username = "rohit@gmail.com",
                Password = "12345",
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now
                }
            };
            return users;
        }

       
    }
}
