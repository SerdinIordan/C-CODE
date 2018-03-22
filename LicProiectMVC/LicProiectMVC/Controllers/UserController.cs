//using EFProject.Repositoryes;
using LicProiectMVC.Models;
using LicProiectMVC.Models.EntitiesValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LicProiectMVC.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user )
        {

            if (ModelState.IsValid)
            {
                if (ValidateUser.IsValid(user.Username, user.Password) != null)
                {
                    var userDisplay = ValidateUser.IsValid(user.Username, user.Password);
                    Session["log"] = userDisplay.Username;
                    Session["typeEntity"] = userDisplay.Type;
                    Session["userID"] = userDisplay.UserId;
                    switch (userDisplay.Type)
                    {
                        case "student": return RedirectToAction("Index","Student");
                        case "teacher": return RedirectToAction("Index","Teacher");
                        case "admin": return RedirectToAction("Index","Admin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }
        



        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


    }
}