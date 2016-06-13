using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarHire.Domain.Abstract;
using CarHire.Domain.Entities;
using CarHire.WebUI.Models;
using System.Net;

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
        public ViewResult List(CarSearch searchModel, int page = 1)
        {
            IEnumerable<Car> cars = repository.Cars;
            if (ModelState.IsValid)
            {
                if (searchModel.NameSearch == null)
                    searchModel.NameSearch = "";
                if (searchModel.BrandSearch == null)
                    searchModel.BrandSearch = "";
                if (searchModel.MaxPrice == 0)
                    searchModel.MaxPrice = decimal.MaxValue;
                if (searchModel.MaxYear == 0)
                    searchModel.MaxYear = int.MaxValue;
                if (searchModel.MaxMileage == 0)
                    searchModel.MaxMileage = decimal.MaxValue;
                if (searchModel.MaxCapacity == 0)
                    searchModel.MaxCapacity = decimal.MaxValue;
                if (searchModel.Category == null)
                    searchModel.Category = "";
                cars = from i in repository.Cars
                       where
                           i.Model.Contains(searchModel.NameSearch) &&
                           i.Brand.Contains(searchModel.BrandSearch) &&
                           i.PricePerDay >= searchModel.MinPrice &&
                           i.PricePerDay <= searchModel.MaxPrice &&
                           i.Mileage >= searchModel.MinMileage &&
                           i.Mileage <= searchModel.MaxMileage &&
                           i.Year >= searchModel.MinYear &&
                           i.Year <= searchModel.MaxYear &&
                           i.Capacity >= searchModel.MinCapacity &&
                           i.Capacity <= searchModel.MaxCapacity
                       select i;

                if (searchModel.Hired == "false")
                    cars = cars.Where(p => p.Hired == true);
                else if (searchModel.Hired == "true")
                    cars = cars.Where(p => p.Hired == false);

                if (searchModel.Category != "")
                    cars = cars.Where(p => p.Category.Equals(searchModel.Category));

                if (searchModel.Sort == "ModelUp")
                    cars = cars.OrderBy(p => p.Model);
                else if (searchModel.Sort == "ModelDown")
                    cars = cars.OrderByDescending(p => p.Model);
                else if (searchModel.Sort == "BrandUp")
                    cars = cars.OrderBy(p => p.Brand);
                else if (searchModel.Sort == "BrandDown")
                    cars = cars.OrderByDescending(p => p.Brand);
                else if (searchModel.Sort == "PriceUp")
                    cars = cars.OrderBy(p => p.PricePerDay);
                else if (searchModel.Sort == "PriceDown")
                    cars = cars.OrderByDescending(p => p.PricePerDay);
                else if (searchModel.Sort == "YearUp")
                    cars = cars.OrderBy(p => p.Year);
                else if (searchModel.Sort == "YearDown")
                    cars = cars.OrderByDescending(p => p.Year);
                else if (searchModel.Sort == "MileageUp")
                    cars = cars.OrderBy(p => p.Mileage);
                else if (searchModel.Sort == "MileageDown")
                    cars = cars.OrderByDescending(p => p.Mileage);
                else if (searchModel.Sort == "CapacityUp")
                    cars = cars.OrderBy(p => p.Capacity);
                else if (searchModel.Sort == "CapacityDown")
                    cars = cars.OrderByDescending(p => p.Capacity);
                else
                    cars = cars.OrderBy(p => p.CarID);
            }
            int totalCars = cars.Count();
            cars = cars.Skip((page - 1) * PageSize);
            cars = cars.Take(PageSize);

            CarsListMainModel model = new CarsListMainModel
            {
                CarListViewModel = new CarsListViewModel
                {
                    Cars = cars,
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        CarsPerPage = PageSize,
                        TotalCars = totalCars
                    }
                },
                CarSearch = searchModel   
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

        public ViewResult Search(CarSearch searchModel)
        {
            return View(searchModel);
        }

        [ActionName("Search")]
        [HttpPost]
        public ActionResult SearchPost(CarSearch searchModel)
        {
            return RedirectToAction("List", searchModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = repository.Cars.FirstOrDefault(p => p.CarID == id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }
    }
}