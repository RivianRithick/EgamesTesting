using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Resource;
using EgamesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Egames.Tests.InMemory
{
    public class EgamesInMemory
    {
        public async Task<DemoContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DemoContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var dbcontext = new DemoContext(options);
            dbcontext.Database.EnsureCreated();
            int uid = 100;
            int aid = 200;
            int pid = 300;
            int omid = 400;
            int odid = 500;
            int cid = 600;
            dbcontext.admins.Add(
                new Admin()
                {
                    Id = 101,
                    Name = "Admin",
                    Gmail = "admin@gmail.com",
                    Password = "1234"
                });
            dbcontext.Add(
              new User()
              {
                  Id = uid++,
                  Name = "Surya",
                  Gmail = "surya@gmail.com",
                  Password = "1234"
              });
            await dbcontext.SaveChangesAsync();
            for (int i = 1; i < 3; i++)
            {
                dbcontext.Add(
                    new Product()
                    {
                        Id = pid + i,
                        Name = "Prototype" + i,
                        Image = "In the Local file",
                        Price = 1599,
                        Publisher = "Ubisoft",
                        Description = "This is Description"
                    });
                dbcontext.Add(
                    new Cart() 
                    {
                        CartId = cid+i,
                        Quantity = 1,
                        TotalAmt = 1599,
                        Productid = pid+ i,
                        Userid = uid+i
                    });
                dbcontext.Add(
                    new OrderMaster()
                    {
                        OrderMasterId = omid + i,
                        UserId = uid + i,
                        Date = DateTime.Now,
                        CaardNumber = 123 + i,
                        TotalAmount = 1599,
                        AmountPaid = 1599
                    });
                dbcontext.Add(
                    new OrderDetails()
                    {
                        OrderDetailsId = odid + i,
                        UserId = uid + i,
                        ProductId = odid + i,
                        OrderMasterId = omid + i,
                        TotalAmount = 1599
                    });
                await dbcontext.SaveChangesAsync();
            }
            return dbcontext;


        }
        
    }
}

