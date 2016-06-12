using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CarHire.Domain.Abstract;
using CarHire.Domain.Entities;
using CarHire.WebUI.Controllers;
using CarHire.WebUI.Models;
using System.Linq;

namespace CarHire.UnitTests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void Can_Sort_String()
        {
            //przygotowanie
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns(new Car[]
            {
                new Car {CarID=1, Model="F", Brand="B1", Category="" },
                new Car {CarID=2, Model="B", Brand="B2", Category="" },
                new Car {CarID=3, Model="D", Brand="B3", Category="" },
                new Car {CarID=4, Model="A", Brand="B1", Category="" },
                new Car {CarID=5, Model="G", Brand="B5", Category="" }
            });
            CarController controller = new CarController(mock.Object);

            CarSearch carSearch = new CarSearch { Sort = "ModelUp" };

            //działanie
            CarsListMainModel result = (CarsListMainModel)controller.List(carSearch, 1).Model;
            Car[] carArray = result.CarListViewModel.Cars.ToArray();

            //asercje
            Assert.IsTrue(carArray.Length == 5);
            Assert.AreEqual(carArray[0].CarID, 4);
            Assert.AreEqual(carArray[1].CarID, 2);
            Assert.AreEqual(carArray[2].CarID, 3);
            Assert.AreEqual(carArray[3].CarID, 1);
            Assert.AreEqual(carArray[4].CarID, 5);
        }

        [TestMethod]
        public void Can_Sort_Numeric()
        {
            //przygotowanie
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns(new Car[]
            {
                new Car {CarID=1, Model="F", Brand="B1", Category="", PricePerDay = 200 },
                new Car {CarID=2, Model="B", Brand="B2", Category="", PricePerDay = 100 },
                new Car {CarID=3, Model="D", Brand="B3", Category="", PricePerDay = 50 },
                new Car {CarID=4, Model="A", Brand="B1", Category="", PricePerDay = 500 },
                new Car {CarID=5, Model="G", Brand="B5", Category="", PricePerDay = 150 }
            });
            CarController controller = new CarController(mock.Object);

            CarSearch carSearch = new CarSearch { Sort = "PriceUp" };

            //działanie
            CarsListMainModel result = (CarsListMainModel)controller.List(carSearch, 1).Model;
            Car[] carArray = result.CarListViewModel.Cars.ToArray();


            //asercje
            Assert.IsTrue(carArray.Length == 5);
            Assert.AreEqual(carArray[0].CarID, 3);
            Assert.AreEqual(carArray[1].CarID, 2);
            Assert.AreEqual(carArray[2].CarID, 5);
            Assert.AreEqual(carArray[3].CarID, 1);
            Assert.AreEqual(carArray[4].CarID, 4);
        }
    }
}
