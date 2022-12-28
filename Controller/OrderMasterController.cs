using EgamesAPI.Controllers;
using EgamesAPI.Models;
using Egames.Tests.InMemory;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Egames.Tests.Controller
{
    public class OrderMasterControllerTests
    {
        [Fact]
        public async Task IndexTests()
        {
            //Arrange
            var inmemory = new EgamesInMemory();
            var dbcontext = await inmemory.GetDatabaseContext();
            var master = new MasterController(dbcontext);

            //Act
            var result = await master.Index();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<MasterController>>();
            dbcontext.OrderMasters.Should().HaveCount(2);
        }
    }
}
