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
            IEnumerable<Car> result = (IEnumerable<Car>)controller.List(2).Model;
            //asercje
            Car[] carArray = result.ToArray();
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
