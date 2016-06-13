using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CarHire.Domain.Abstract;
using CarHire.Domain.Entities;
using CarHire.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class ImageTests
    {

        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {

            // przygotowanie - tworzenie produktu z danymi zdjęcia
            var car = new Car
            {
                CarID = 2,
                Model = "Test",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            // przygotowanie - tworzenie imitacji repozytorium
            var mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns(new Car[] {
                new Car {CarID = 1, Model = "P1"},
                car,
                new Car {CarID = 3, Model = "P3"},
            }.AsQueryable());

            // przygotowanie - utworzenie kontrolera
            var target = new CarController(mock.Object);

            // działanie - wywołanie metody akcji GetImage
            ActionResult result = target.GetImage(2);

            // asercje
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(car.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {

            // przygotowanie - tworzenie imitacji repozytorium
            var mock = new Mock<ICarRepository>();
            mock.Setup(m => m.Cars).Returns(new Car[] {
                new Car {CarID = 1, Model = "P1"},
                new Car {CarID = 2, Model = "P2"},
            }.AsQueryable());
            // przygotowanie - utworzenie kontrolera
            var target = new CarController(mock.Object);

            // działanie - wywołanie metody akcji GetImage
            ActionResult result = target.GetImage(100);

            // asercje
            Assert.IsNull(result);
        }
    }
}
