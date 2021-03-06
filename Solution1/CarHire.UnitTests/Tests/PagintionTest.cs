﻿using System;
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
    public class PagintionTest
    {
        [TestMethod]
        public void Can_Paginate() {
            Mock<ICarRepository> mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns(new Car[]
            {
                new Car {CarID=1, Model="P1", Brand="B1", Category="" },
                new Car {CarID=2, Model="P2", Brand="B2", Category="" },
                new Car {CarID=3, Model="P3", Brand="B3", Category="" },
                new Car {CarID=4, Model="P4", Brand="B4", Category="" },
                new Car {CarID=5, Model="P5", Brand="B5", Category="" }
            });
            CarController controller = new CarController(mock.Object);
            controller.PageSize = 3;
            //działanie
            CarsListMainModel result = (CarsListMainModel)controller.List(new CarSearch(),2).Model;
            //asercje
            Car[] carArray = result.CarListViewModel.Cars.ToArray();
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
    }
}
