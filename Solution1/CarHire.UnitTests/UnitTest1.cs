using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
using CarHire.Domain.Abstract;
using CarHire.Domain.Entities;
using CarHire.WebUI.Controllers;
using System.Web.Mvc;
using CarHire.WebUI.Models;
using CarHire.WebUI.HtmlHelpers;
using System.Web;

namespace CarHire.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate() {
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns(new Car[]
            {
                new Car {CarID=1,Model="P1" },
                new Car {CarID=2,Model="P2" },
                new Car {CarID=3,Model="P3" },
                new Car {CarID=4,Model="P4" },
                new Car {CarID=5,Model="P5" }
            });
            CarController controller = new CarController(mock.Object);
            controller.PageSize = 3;
            //działanie
            CarsListViewModel result = (CarsListViewModel)controller.List(null,2).Model;
            //asercje
            Car[] carArray = result.Cars.ToArray();
            Assert.IsTrue(carArray.Length == 2);
            Assert.AreEqual(carArray[0].Model, "P4");
            Assert.AreEqual(carArray[1].Model, "P5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // przygotowanie - definiowanie metody pomocniczej HTML — potrzebujemy tego,
            // aby użyć metody rozszerzającej
            HtmlHelper myHelper = null;

            // przygotowanie - tworzenie danych PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalCars = 28,
                CarsPerPage = 10
            };

            // przygotowanie - konfigurowanie delegatu z użyciem wyrażenia lambda
            Func<int, string> pageUrlDelegate = i => "Strona" + i;

            // działanie
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // asercje
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Strona1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Strona2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Strona3"">3</a>",
                result.ToString());
        }
        [TestMethod]
        public void Can_Edit_Car()
        {
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns(new Car[]
            {
                new Car {CarID=1,Model="M1" },
                new Car {CarID=2,Model="M2" },
                new Car {CarID=3,Model="M3" },
            });

            CarController target = new CarController(mock.Object);

            Car m1 = target.Edit(1).ViewData.Model as Car;
            Car m2 = target.Edit(2).ViewData.Model as Car;
            Car m3 = target.Edit(3).ViewData.Model as Car;


            Assert.AreEqual(1, m1.CarID);
            Assert.AreEqual(2, m2.CarID);
            Assert.AreEqual(3, m3.CarID);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Car()
        {
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns(new Car[]
            {
                new Car {CarID=1,Model="M1" },
                new Car {CarID=2,Model="M2" },
                new Car {CarID=3,Model="M3" },
            });

            CarController target = new CarController(mock.Object);

            Car result = (Car)target.Edit(4).ViewData.Model;


            Assert.IsNull(result);
        }
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
        public void Can_Delete_Valid_Car()
        {
            var car = new Car { CarID = 2, Model = "M1" };

            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns(new Car[] {
                new Car { CarID = 1, Model = "M2" },
                car,
                new Car { CarID = 3, Model = "M3" },
             });

            var target = new CarController(mock.Object);

            target.Delete(car.CarID);

            mock.Verify(m => m.DeleteCar(car.CarID));
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
