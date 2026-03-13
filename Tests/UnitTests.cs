using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers;
using WebApplication1.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Tests
{
    public class UsersControllerTests
    {
        private DBContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            return new DBContext(options);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsUsers()
        {
            var ctx = CreateContext();
            ctx.User.Add(new User { ID = 1, User_Name = "John", User_LastName = "b", User_MidName = "c" });
            ctx.User.Add(new User { ID = 2, User_Name = "Mike", User_LastName = "b", User_MidName = "c" });
            await ctx.SaveChangesAsync();
            var controller = new UsersController(ctx);
            var result = await controller.GetAllUsers();
            var ok = result.Result as OkObjectResult;
            ok.Should().NotBeNull();
            (ok.Value as IEnumerable<User>).Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAllAbonents_ReturnsAbonents()
        {
            var ctx = CreateContext();
            ctx.Abonent.Add(new Abonent { ID = 1,Abonent_Idcard="fefhu333", Abonent_name = "Alex", Abonent_LastName="smith", Abonent_Login="ad",Abonent_MidName="aa",Abonent_Password="1" });
            await ctx.SaveChangesAsync();
            var controller = new UsersController(ctx);
            var result = await controller.GetAllAbonents();
            var ok = result.Result as OkObjectResult;
            ok.Should().NotBeNull();
            (ok.Value as IEnumerable<Abonent>).Should().HaveCount(1);
        }

        [Fact]
        public async Task GetAllPackages_ReturnsPackages()
        {
            var ctx = CreateContext();
            ctx.Package.Add(new Package { ID = 1, Package_name = "Internet",Package_descriptiion="aaa",Package_num=011,Package_price=1333 });
            await ctx.SaveChangesAsync();
            var controller = new UsersController(ctx);
            var result = await controller.GetAllPackages();
            var ok = result.Result as OkObjectResult;
            ok.Should().NotBeNull();
            (ok.Value as IEnumerable<Package>).Should().HaveCount(1);
        }

        [Fact]
        public async Task GetAllAgreements_ReturnsAgreements()
        {
            var ctx = CreateContext();
            ctx.Agreement.Add(new Agreement { ID = 1 });
            await ctx.SaveChangesAsync();
            var controller = new UsersController(ctx);
            var result = await controller.GetAllAgreements();
            var ok = result.Result as OkObjectResult;
            ok.Should().NotBeNull();
            (ok.Value as IEnumerable<Agreement>).Should().HaveCount(1);
        }

        [Fact]
        public async Task GetAllHistory_ReturnsHistory()
        {
            var ctx = CreateContext();
            ctx.History.Add(new History { ID = 1, ID_Action = 5 });
            await ctx.SaveChangesAsync();
            var controller = new UsersController(ctx);
            var result = await controller.GetAllhistorys();
            var ok = result.Result as OkObjectResult;
            ok.Should().NotBeNull();
            (ok.Value as IEnumerable<History>).Should().HaveCount(1);
        }

        [Fact]
        public async Task InsertUser_ReturnsCreated()
        {
            var ctx = CreateContext();
            var controller = new UsersController(ctx);
            var user = new User { ID = 13, User_Name = "Test",User_LastName="TEstovich",User_MidName="TEst" };
            var result = await controller.InsertUser(user);
            result.Should().BeOfType<CreatedResult>();
            ctx.User.Should().HaveCount(1);
        }

        [Fact]
        public async Task InsertAbonent_ReturnsCreated()
        {
            var ctx = CreateContext();
            var controller = new UsersController(ctx);
            var abonent = new Abonent { ID = 1, Abonent_name = "Alex", Abonent_Idcard="vfe1132",Abonent_Login="loginTest",Abonent_LastName="LAstTest",Abonent_MidName="MidTEst",Abonent_Password="111" };
            var result = await controller.InsertAbonent(abonent);
            result.Should().BeOfType<CreatedResult>();
            ctx.Abonent.Should().HaveCount(1);
        }

        [Fact]
        public async Task InsertPackage_ReturnsCreated()
        {
            var ctx = CreateContext();
            var controller = new UsersController(ctx);
            var package = new Package { ID = 1, Package_name = "Test",Package_descriptiion="testdescription",Package_num=001,Package_price=1111 };
            var result = await controller.InsertPackage(package);
            result.Should().BeOfType<OkObjectResult>();
            ctx.Package.Should().HaveCount(1);
        }

        [Fact]
        public async Task UpdateUser_UpdatesSuccessfully()
        {
            var ctx = CreateContext();
            ctx.User.Add(new User { ID = 1, User_Name = "Old",User_LastName="TEstOld",User_MidName="Testold" });
            await ctx.SaveChangesAsync();
            var controller = new UsersController(ctx);
            var updated = new User { ID = 1, User_Name = "New", User_LastName = "TEstNew", User_MidName = "TestNew" };
            var result = await controller.UpdateUser(1, updated);
            result.Should().BeOfType<NoContentResult>();
            ctx.User.Find(1).User_Name.Should().Be("New");
        }

        [Fact]
        public async Task UpdateUser_ReturnsBadRequest_WhenIdMismatch()
        {
            var ctx = CreateContext();
            var controller = new UsersController(ctx);
            var updated = new User { ID = 2 };
            var result = await controller.UpdateUser(1, updated);
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateUser_ReturnsNotFound_WhenMissing()
        {
            var ctx = CreateContext();
            var controller = new UsersController(ctx);
            var updated = new User { ID = 1 };
            var result = await controller.UpdateUser(1, updated);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteUser_DeletesSuccessfully()
        {
            var ctx = CreateContext();
            ctx.User.Add(new User { ID = 1, User_Name = "a", User_LastName = "b", User_MidName = "c" });
            await ctx.SaveChangesAsync();
            var controller = new UsersController(ctx);
            var result = await controller.DeleteUser(1, new User { ID = 1,User_Name="a",User_LastName="b",User_MidName="c" });
            result.Should().BeOfType<NoContentResult>();
            ctx.User.Should().BeEmpty();
        }

        [Fact]
        public async Task DeleteUser_ReturnsNotFound_WhenMissing()
        {
            var ctx = CreateContext();
            var controller = new UsersController(ctx);
            var result = await controller.DeleteUser(1, new User { ID = 1 });
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteUser_ReturnsBadRequest_WhenIdMismatch()
        {
            var ctx = CreateContext();
            var controller = new UsersController(ctx);
            var result = await controller.DeleteUser(1, new User { ID = 2 });
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}