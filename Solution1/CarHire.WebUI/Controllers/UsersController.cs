using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarHire.Domain.Abstract;
using CarHire.Domain.Entities;
using System.Web.Security;

namespace CarHire.WebUI.Controllers
{
    public class UsersController : Controller
    {
        private IUserRepository repository;
        public UsersController(IUserRepository repo)
        {
            this.repository = repo;
        }
        // GET: Users
        public ActionResult Holder()
        {
            if (Session["Category"] == null) 
            {
                    return RedirectToAction("Index", "Home");
                    
            }
            else if (Session["Category"].Equals("Kierownik"))
            {
                return View(repository.Users);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ViewResult SignIn()
        {
            return View("New", new User());

        }
        public ActionResult Create()
        {
            if (Session["Category"] == null) 
            {
                return RedirectToAction("Index", "Home");
            }else if (Session["Category"].Equals("Kierownik"))
            {
                return View("Edit", new User());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ViewResult Edit(int userId)
        {
            User user = repository.Users.FirstOrDefault(p => p.UserID == userId);
            return View(user);
        }
        [HttpPost]
        public ActionResult New(User user)
        {
            if (ModelState.IsValid)
            {
                if (Session["Category"] == null)
                {
                    repository.NewUser(user, "Klient");
                    return RedirectToAction("Index", "Home");
                }
                else if (Session["Category"].Equals("Kierownik"))
                {
                    repository.NewUser(user, "Pracownik");
                    return RedirectToAction("Index", "Home");
                }else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(user);
            }
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                repository.SaveUser(user);
                TempData["message"] = string.Format("Zapisano {0}", user.Name);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View(user);
            }
        }
        [HttpPost]
        public ActionResult Delete(int UserId)
        {
            User deletedUser = repository.DeleteUser(UserId);
            if (deletedUser != null)
            {
                TempData["massage"] = string.Format("Usunięto {0}", deletedUser.Name);
            }
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public JsonResult IsUserExistInDatabase(string username)
        {
            var user = Membership.GetUser(username);

            return Json(user == null);
        }

    }


}