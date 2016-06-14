using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarHire.Domain.Abstract;
using CarHire.Domain.Entities;
using CarHire.WebUI.Models;
using System.IO;
using System.Web.UI;

namespace CarHire.WebUI.Controllers
{
    public class RentalController : Controller
    {
        private IRentalRepository rentalRepository;
        private ICarRepository carRepository;
        private IUserRepository userRepository;

        public RentalController(IRentalRepository rentalRepository, ICarRepository carRepository, IUserRepository userRepository)
        {
            this.rentalRepository = rentalRepository;
            this.carRepository = carRepository;
            this.userRepository = userRepository;
        }
        public ActionResult Index()
        {
            int x;
            Int32.TryParse(Session["LogedUserID"].ToString(), out x);
            var cars = carRepository.Cars;
            var rentals = rentalRepository.Rent;
            var users = userRepository.Users;
            var rentalViewModelList = new List<RentalViewAggregator>();
            foreach (var rent in rentals)
            {
                var car = cars.First(c => c.CarID == rent.CarId);
                var user = users.First(c => c.UserID == rent.UserId);
                    rentalViewModelList.Add(new RentalViewAggregator()
                    {
                        User = user,
                        Car = car,
                        Rental = rent
                    });
            }
            return View(rentalViewModelList);
        }
        public ViewResult Create(int carId)
        {
            return View("Edit", new Rental());
        }
        public ViewResult Edit(int rentalId)
        {
            var rent = rentalRepository.Rent.FirstOrDefault(p => p.RentalId == rentalId);
            return View(rent);
        }

        [HttpPost]
        public ActionResult Edit(Rental rent)
        {
            int x;
            Int32.TryParse(Session["LogedUserID"].ToString(), out x);
            if (ModelState.IsValid && (rent.RentalDate < rent.ReturnDate || rent.RentalDate == rent.ReturnDate))
            {
                rentalRepository.RentCar(rent,x);
                carRepository.HireCar(rent.CarId);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(rent);
            }
        }
        public ViewResult Extend(int rentalId)
        {
            var rent = rentalRepository.Rent.FirstOrDefault(p => p.RentalId == rentalId);
            return View(rent);
        }
        [HttpPost]
        public ActionResult Extend(Rental rent)
        {
            if (ModelState.IsValid && rent.Extension>rent.ReturnDate)
            {
                rentalRepository.RentalExtension(rent);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(rent);
            }
        }
        public ActionResult ActionList()
        {

            var cars = carRepository.Cars;
            var rentals = rentalRepository.Rent;
            var users = userRepository.Users;
            var rentalViewModelList = new List<RentalViewAggregator>();
            foreach (var rent in rentals)
            {
                var car = cars.First(c => c.CarID == rent.CarId);
                var user = users.First(c => c.UserID == rent.UserId);
                if (rent.Status == "Oczekujacy")
                {
                    rentalViewModelList.Add(new RentalViewAggregator()
                    {
                        User = user,
                        Car = car,
                        Rental = rent
                    });
                }
            }
            return View(rentalViewModelList);
        }
        public ActionResult ReturnList()
        {

            var cars = carRepository.Cars;
            var rentals = rentalRepository.Rent;
            var users = userRepository.Users;
            var rentalViewModelList = new List<RentalViewAggregator>();
            foreach (var rent in rentals)
            {
                var car = cars.First(c => c.CarID == rent.CarId);
                var user = users.First(c => c.UserID == rent.UserId);
                if (DateTime.Now > rent.ReturnDate && rent.Status != "Oddany")
                {
                    rentalViewModelList.Add(new RentalViewAggregator()
                    {
                        User = user,
                        Car = car,
                        Rental = rent
                    });
                }
            }
            return View(rentalViewModelList);
        }
        public ActionResult ReturnCar(Rental rent)
        {
            rentalRepository.Return(rent);
            carRepository.ReturnCar(rent.CarId);
            return RedirectToAction("ReturnList", "Rental");

        }
        public ActionResult AproveExtention(Rental rent)
        {
            rentalRepository.ExtensionApproval(rent);
            return RedirectToAction("ActionList", "Rental");

        }
        public ActionResult AproveRent(Rental rent)
        {
            rentalRepository.RentalApproval(rent);
            return RedirectToAction("ActionList", "Rental");

        }

        public ActionResult ExportToExcel()
        {
            var loggedUser = Session["LogedUserID"].ToString();
            var cars = carRepository.Cars;
            var rentals = rentalRepository.Rent;
            var users = userRepository.Users;
            var rentalViewModelList = new List<RentalViewAggregator>();
            foreach (var rent in rentals)
            {
                var car = cars.First(c => c.CarID == rent.CarId);
                var user = users.First(c => c.UserID == rent.UserId);
                if (Session["Category"].Equals("Kierownik") || Session["Category"].Equals("Pracownik") || Session["LogedUserID"].Equals(user.UserID))
                    rentalViewModelList.Add(new RentalViewAggregator()
                    {
                        User = user,
                        Car = car,
                        Rental = rent
                    });
            }
            Response.Charset = "utf-8";
            Response.AddHeader("content-disposition", "attachment; filename=Raport.xls");
            Response.ContentType = "application/excel";
            return View(rentalViewModelList);
  
        }
    }
}