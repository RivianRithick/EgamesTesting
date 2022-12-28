using EgamesAPI.Controllers;
using EgamesAPI.Models;
using Egames.Tests.InMemory;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Egames.Tests.Controller
{
    public class CartControllerTests
    {
        [Fact]
        public async Task IndexTests()
        {
            //Arrange
            var inmemory = new EgamesInMemory();
            var dbcontext = await inmemory.GetDatabaseContext();
            var cart = new CartController(dbcontext);

            //Act
            var result = await cart.Index();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<Cart>>();
            dbcontext.carts.Should().HaveCount(2);
        }
        
        [Fact]
        public async Task DeleteCartTests()
        {
            //Arrange
            var inmemory = new EgamesInMemory();
            var dbcontext = await inmemory.GetDatabaseContext();
            var cart = new CartController(dbcontext);
            int id = 602;

            //Act
            var result = await cart.DeleteCart(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
            dbcontext.carts.Should().HaveCount(1);
        }
        [Fact]
        public async Task AddToCartTests()
        {
            //Arrange
            var inmemory = new EgamesInMemory();
            var dbcontext = await inmemory.GetDatabaseContext();
            var cart = new CartController(dbcontext);
            Cart b = new Cart()
            {
                CartId = 404,
                Productid = 1307,
                Userid = 202,
                TotalAmt = 1889,
                Quantity = 1
            };

            //Act
            var result = await cart.AddToCart(b);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<Cart>>();
            dbcontext.carts.Should().HaveCount(3);
        }
        [Fact]
        public async Task ProceedToBuyTests()
        {
            //Arrange
            var inmemory = new EgamesInMemory();
            var dbcontext = await inmemory.GetDatabaseContext();
            var cart = new CartController(dbcontext);
            int Uid = 202;

            //Act
            var result = await cart.ProceedtoBuy(Uid);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OrderMaster>();
            dbcontext.OrderMasters.Should().HaveCount(3);
        }
        [Fact]
        public async Task GetPaymentByIdTests()
        {
            //Arrange
            var inmemory = new EgamesInMemory();
            var dbcontext = await inmemory.GetDatabaseContext();
            var cart = new CartController(dbcontext);
            int omid = 401;

            //Act
            var result = await cart.GetPaymentById(omid);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OrderMaster>();
            result.OrderMasterId.Should().Be(omid);
        }
        [Fact]
        public async Task GetPaymentTests()
        {
            //Arrange
            var inmemory = new EgamesInMemory();
            var dbcontext = await inmemory.GetDatabaseContext();
            var cart = new CartController(dbcontext);
            OrderMaster om = new OrderMaster()
            {
                OrderMasterId = 402,
                UserId = 102,
                CaardNumber = 12345,
                TotalAmount = 1889
            };

            //Act
            var m = await dbcontext.OrderMasters.FindAsync(om.OrderMasterId); ;
            var result = await cart.GetPayment(m);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
            dbcontext.OrderMasters.Should().HaveCount(2);
            dbcontext.carts.Should().HaveCount(1);
        }
        [Fact]
        public async Task GetCartByIdTests()
        {
            //Arrange
            var inmemory = new EgamesInMemory();
            var dbcontext = await inmemory.GetDatabaseContext();
            var cart = new CartController(dbcontext);
            int CId = 601;

            //Act
            var result = await cart.GetCartById(CId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Cart>();
            result.CartId.Should().Be(CId);
        }

    }
    
}