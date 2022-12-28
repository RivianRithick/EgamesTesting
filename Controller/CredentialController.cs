using EgamesAPI.Controllers;
using Egames.Tests.InMemory;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Egames.Tests.InMemory;
using FakeItEasy;
using EgamesAPI.Models;

namespace Egames.Tests.Controller
{
    public class CredentialControllerTests
    {
        [Fact]
        public async Task AdminLoginTests()
        {
            //Arrange
            var inmemory = new EgamesInMemory();
            var dbcontext = await inmemory.GetDatabaseContext();
            var credential = new CredentialController(dbcontext);
            Admin admin = new Admin()
            {
                Gmail = "admin@gmail.com",
                Password = "1234"
            };



            //Act
            var result = await credential.AdminLogin(admin);



            //Assert
            result.Value.Should().NotBeNull();
            dbcontext.admins.Should().HaveCount(1);
        }
        [Fact]
        public async Task RegisterTests()
        {
            //Arrange
            var inmemory = new EgamesInMemory();
            var dbcontext = await inmemory.GetDatabaseContext();
            var credential = new CredentialController(dbcontext);
            User user = new User()
            {
                Id = 104,
                Name = "Rithick",
                Gmail = "rithick@gmail.com",
                Password = "1234"
            };



            //Act
            var result = await credential.Register(user);



            //Assert
            //result.Value.Should().NotBeNull();
            dbcontext.users.Should().Contain(user);
            dbcontext.users.Should().HaveCount(2);
        }


            [Fact]
            public async Task UserLoginTests()
            {
                //Arrange
                var inmemory = new EgamesInMemory();
                var dbcontext = await inmemory.GetDatabaseContext();
                var credential = new CredentialController(dbcontext);
                User user = new User()
                {
                    Gmail = "surya@gmail.com",
                    Password = "1234"
                };



                //Act
                var result = await credential.UserLogin(user);



                //Assert
                result.Value.Should().NotBeNull();
                dbcontext.users.Should().Contain(result.Value);
            }
    }
    
}