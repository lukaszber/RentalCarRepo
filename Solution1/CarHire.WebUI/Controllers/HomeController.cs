using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarHire.Domain.Abstract;
using CarHire.Domain.Entities;
using CarHire.WebUI.Models;
using CarHire.Domain.Concrete;
namespace CarHire.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private IRentalRepository rentalRepository;
        private ICarRepository carRepository;
        private IUserRepository userRepository;

        public HomeController(IRentalRepository rentalRepository, ICarRepository carRepository, IUserRepository userRepository)
        {
            this.rentalRepository = rentalRepository;
            this.carRepository = carRepository;
            this.userRepository = userRepository;
        }
        public ActionResult Index()
        {
            if (Session["Category"] == null)
            {
                return View();
            }
            if (Session["Category"].Equals("Klient"))
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
                    if (user.UserID == x)
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
            else {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users u)
        {
            if (ModelState.IsValid)
            {
                using (EfDbContext dc = new EfDbContext())
                {
                    var v = dc.Users.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.UserID;
                        Session["LogedUserFullname"] = v.Name.ToString();
                        Session["Category"] = v.Category.ToString();
                        Session["Driver"] = v.Driver.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(u);
        }
        public ActionResult Logout()
        {
            Session["LogedUserID"]=null;
            Session["LogedUserFullname"]=null;
            Session["Category"] = null;
            return RedirectToAction("Index");
        }
    }

}