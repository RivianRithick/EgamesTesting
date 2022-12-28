using EgamesAPI.Controllers;
using EgamesAPI.Models;
using Egames.Tests.InMemory;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;
using System.Security.Policy;


namespace Egames.Tests.Controller
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task GetProductTests()
        {
            //Arrange
            var inmemory = new EgamesInMemory();
            var dbcontext = await inmemory.GetDatabaseContext();
            var product = new ProductController(dbcontext);

            //Act
            var result = await product.GetProduct();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<IEnumerable<Product>>>();
            int actualresult = result.Value.Count();
            int expresult = 2;
            Assert.Equal(expresult, actualresult);

        }
       
        [Fact]
        public async Task DetailsTests()
        {
            //Arrange
            var inmemory = new EgamesInMemory();
            var dbcontext = await inmemory.GetDatabaseContext();
            var product = new ProductController(dbcontext);

            //Act
            var result = await product.Details(301);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<Product>>();
        }
        [Fact]
        public async Task PutProductTests()
        {
            //Arrange
            var inmemory = new EgamesInMemory();
            var dbcontext = await inmemory.GetDatabaseContext();
            var product = new ProductController(dbcontext);
            Product product1 = new Product()
            {
                Id = 302,
                Name = "Prototype",
                Image = "In the Local file",
                Publisher = "Radical Entertainment",
                Description = "The game is set in New York City as a virus infects people and the military attempts to put an end to it. The protagonist of the story is named Alex Mercer, who has enemy-absorbing and shapeshifting abilities. Mercer can retain memories, experiences, biomass and physical forms of enemies consumed.",
                Price = 1599,
            };

            //Act

            var p = await dbcontext.products.FindAsync(302);
            dbcontext.Entry<Product>(p).State = EntityState.Detached;
            var result = await product.PutProduct(p);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            dbcontext.products.Should().HaveCount(2);
        }
        [Fact]
        public async Task PostProductTests()
        {
            //Arrange
            var inmemory = new EgamesInMemory();
            var dbcontext = await inmemory.GetDatabaseContext(); ;
            var Products = new ProductController(dbcontext);
            Product product = new Product()
            {
                Id = 1308,
                Name = "Need For Speed",
                Image = "File Found Local",
                Publisher = "AOL",
                Description = "Need for Speed is a racing game franchise published by Electronic Arts and currently developed by Criterion Games, the developers of Burnout.",
                Price = 200,
            };

            //Act
            var result = await Products.PostProduct(product);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<Product>>();
            dbcontext.products.Should().HaveCount(3);
        }
        [Fact]
        public async Task DeleteProductTests()
        {
            //Arrange
            var inmemory = new EgamesInMemory();
            var dbcontext = await inmemory.GetDatabaseContext();
            var product = new ProductController(dbcontext);

            //Act
            var result = await product.DeleteProduct(301);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
            dbcontext.products.Should().HaveCount(1);

        }
    }
}