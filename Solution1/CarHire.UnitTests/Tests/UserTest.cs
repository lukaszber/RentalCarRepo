using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CarHire.Domain.Abstract;
using CarHire.Domain.Entities;
using CarHire.WebUI.Controllers;

namespace CarHire.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Edit_User()
        {
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            mock.Setup(m => m.Users).Returns(new User[]
            {
                new User {UserID=1,Name="U1" },
                new User {UserID=2,Name="U2" },
                new User {UserID=3,Name="U3" },
            });

            UsersController target = new UsersController(mock.Object);

            User m1 = target.Edit(1).ViewData.Model as User;
            User m2 = target.Edit(2).ViewData.Model as User;
            User m3 = target.Edit(3).ViewData.Model as User;


            Assert.AreEqual(1, m1.UserID);
            Assert.AreEqual(2, m2.UserID);
            Assert.AreEqual(3, m3.UserID);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_User()
        {
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            mock.Setup(m => m.Users).Returns(new User[]
            {
                new User {UserID=1,Name="U1" },
                new User {UserID=2,Name="U2" },
                new User {UserID=3,Name="U3" },
            });

            UsersController target = new UsersController(mock.Object);

            User result = (User)target.Edit(4).ViewData.Model;

            Assert.IsNull(result);
        }
        [TestMethod]
        public void Can_Delete_Valid_User()
        {
            var user = new User { UserID = 2, Name = "U1" };

            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            mock.Setup(m => m.Users).Returns(new User[] {
                new User { UserID = 1, Name = "U2" },
                user,
                new User { UserID = 3, Name = "U3" },
             });

            var target = new UsersController(mock.Object);

            target.Delete(user.UserID);

            mock.Verify(m => m.DeleteUser(user.UserID));
        }
    }
}
