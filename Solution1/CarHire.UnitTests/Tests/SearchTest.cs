using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
using CarHire.Domain.Abstract;
using CarHire.Domain.Entities;
using CarHire.WebUI.Controllers;
using CarHire.WebUI.Models;

namespace CarHire.UnitTests
{
    [TestClass]
    public class SearchTest
    {
        [TestMethod]
        public void Can_Search_Strings()
        {
            //przygotowanie
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns(new Car[]
            {
                new Car {CarID=1, Model="P1", Brand="B1", Category="" },
                new Car {CarID=2, Model="P1", Brand="B2", Category="" },
                new Car {CarID=3, Model="P2", Brand="B3", Category="" },
                new Car {CarID=4, Model="P1", Brand="B1", Category="" },
                new Car {CarID=5, Model="P2", Brand="B5", Category="" }
            });
            CarController controller = new CarController(mock.Object);

            CarSearch carSearch = new CarSearch { NameSearch = "P1", BrandSearch = "B1" };

            //działanie
            CarsListMainModel result = (CarsListMainModel)controller.List(carSearch, 1).Model;
            Car[] carArray = result.CarListViewModel.Cars.ToArray();

            //asercje
            Assert.IsTrue(carArray.Length == 2);
            Assert.AreEqual(carArray[0].CarID, 1);
            Assert.AreEqual(carArray[1].CarID, 4);
        }

        [TestMethod]
        public void Can_Search_Numeric()
        {
            //przygotowanie
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns(new Car[]
            {
                new Car {CarID=1, Model="P1", Brand="B1", Category="", PricePerDay = 100, Year = 2001, Capacity = 2},
                new Car {CarID=2, Model="P2", Brand="B2", Category="", PricePerDay = 200, Year = 2002, Capacity = 2},
                new Car {CarID=3, Model="P3", Brand="B3", Category="", PricePerDay = 300, Year = 2003, Capacity = 2},
                new Car {CarID=4, Model="P4", Brand="B4", Category="", PricePerDay = 400, Year = 1990, Capacity = 1},
                new Car {CarID=5, Model="P5", Brand="B5", Category="", PricePerDay = 500, Year = 1991, Capacity = 1},
                new Car {CarID=6, Model="P6", Brand="B6", Category="", PricePerDay = 600, Year = 1993, Capacity = 2},
                new Car {CarID=7, Model="P7", Brand="B7", Category="", PricePerDay = 700, Year = 1999, Capacity = 2},
                new Car {CarID=8, Model="P8", Brand="B8", Category="", PricePerDay = 800, Year = 2001, Capacity = 2},

            });
            CarController controller = new CarController(mock.Object);

            CarSearch carSearch = new CarSearch { MinPrice = 200, MaxPrice = 600, MinYear = 1990, MaxYear = 2000, MinCapacity = 1, MaxCapacity = (decimal)1.6 };

            //działanie
            CarsListMainModel result = (CarsListMainModel)controller.List(carSearch, 1).Model;
            Car[] carArray = result.CarListViewModel.Cars.ToArray();

            //asercje
            Assert.IsTrue(carArray.Length == 2);
            Assert.AreEqual(carArray[0].CarID, 4);
            Assert.AreEqual(carArray[1].CarID, 5);
        }

        [TestMethod]
        public void Can_Search_Category()
        {
            //przygotowanie
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns(new Car[]
            {
                new Car {CarID=1, Model="P1", Brand="B1", Category="A" },
                new Car {CarID=2, Model="P1", Brand="B2", Category="A1" },
                new Car {CarID=3, Model="P2", Brand="B3", Category="B" },
                new Car {CarID=4, Model="P1", Brand="B1", Category="A1" },
                new Car {CarID=5, Model="P2", Brand="B5", Category="" }
            });
            CarController controller = new CarController(mock.Object);

            CarSearch carSearch = new CarSearch { Category = "A" };

            //działanie
            CarsListMainModel result = (CarsListMainModel)controller.List(carSearch, 1).Model;
            Car[] carArray = result.CarListViewModel.Cars.ToArray();

            //asercje
            Assert.IsTrue(carArray.Length == 1);
            Assert.AreEqual(carArray[0].CarID, 1);
        }
    }
}
