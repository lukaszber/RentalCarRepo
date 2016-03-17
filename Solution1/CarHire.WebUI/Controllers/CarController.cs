using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarHire.Domain.Abstract;
using CarHire.Domain.Entities;
using CarHire.WebUI.Models;
namespace CarHire.WebUI.Controllers
{
    public class CarController : Controller
    {
        private ICarRepository repository;
        public int PageSize = 10;
        // GET: Car
        public CarController(ICarRepository carRepository)
        {
            this.repository = carRepository;
        }
        public ViewResult List(int page = 1)
        {
            CarsListViewModel model = new CarsListViewModel
            {
                Cars = repository.Cars
                .OrderBy(p => p.CarID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    CarsPerPage = PageSize,
                    TotalCars = repository.Cars.Count()
                }
            };
            return View(model);
        }
        public ViewResult Holder()
        {
            return View(repository.Cars);
        }
        public ViewResult Create()
        {
            return View("Edit", new Car());
        }
        public ViewResult Edit(int carId)
        {
                Car car = repository.Cars.FirstOrDefault(p => p.CarID == carId);
                return View(car);
        }
        [HttpPost]
        public ActionResult Edit(Car car)
        {
            if (Session["Category"] == null || Session["Category"].Equals("Pracownik") || Session["Category"].Equals("Klient"))
            {
                return RedirectToAction("Index", "Home");
            }
            else { 
                if (ModelState.IsValid)
                {
                    repository.SaveCar(car);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(car);
                }    
            }
        }
        [HttpPost]
        public ActionResult Delete(int CarId)
        {
            var car = repository.Cars.First(c => c.CarID == CarId);
            if (car.Hired == true)
            {
                return RedirectToAction("Index", "Home");
            }
            else {
                Car deletedCar = repository.DeleteCar(CarId);
                if (deletedCar != null)
                {
                    TempData["massage"] = string.Format("Usunięto {0}", deletedCar.Model);
                }
                return RedirectToAction("Index", "Home");
            }
         }
    }
}