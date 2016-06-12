using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CarHire.Domain.Abstract;
using CarHire.Domain.Entities;
using CarHire.WebUI.Controllers;

namespace CarHire.UnitTests
{
    [TestClass]
    public class CarTest
    {
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
    }
}
