using Microsoft.AspNet.Identity;
using OnlineMobiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineMobiles.Controllers
{
    public class AccountController : Controller
    {
        MobilesContext db = new MobilesContext();
        // GET: Account
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("Name","Name should not be empty");
                ModelState.AddModelError("Email", "Email should not be empty");
                ModelState.AddModelError("Password", "Email should not be empty");
                ModelState.AddModelError("Mobile", "Email should not be empty");
                ModelState.AddModelError("Address", "Email should not be empty");
            }
            SignIn login = new SignIn
            {
                Name = customer.Name,
                Email = customer.Email,
                Password = customer.Password
            };
            db.Logins.Add(login);
            db.Customers.Add(customer);
            db.SaveChanges();
            return View(customer);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult Login(SignIn login)
        {
            Session["userName"] = login.Name;
            if (login.Name=="Admin")
            {
                var adminLogin = db.Logins.Where(x =>   x.Email == login.Email && x.Password == login.Password).FirstOrDefault();
                if (adminLogin != null)
                {
                    return RedirectToAction("Product", "Admin");
                }
                else
                {
                    ViewBag.error = "Incorrect user name and Password";
                }
            }
            var userLogin = db.Logins.Where(x => x.Name == login.Name).FirstOrDefault();
            if(userLogin!=null)
            {
                var user = db.Logins.Where(x => x.Email == login.Email && x.Password == login.Password).FirstOrDefault();
                if(user!=null)
                {
                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    ViewBag.error = "Incorrect email and Password";
                }
            }
            else 
            {
                ViewBag.error = "No Account exists.Create an Account";
            }
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Home", "Home");
        }

    }
}