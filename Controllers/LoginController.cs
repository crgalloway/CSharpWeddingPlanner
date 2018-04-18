using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WeddingPlanner
{
    public class LoginController:Controller
    {
        private WeddingPlannerContext _context;
        public LoginController(WeddingPlannerContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index() => View();
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            List<User> possibleLogin = _context.users.Where( u => (string)u.email == (string)email).ToList();
            if(possibleLogin.Count == 1)
            {
                var Hasher = new PasswordHasher<User>();
                if(0!= Hasher.VerifyHashedPassword(possibleLogin[0], possibleLogin[0].password, password))
                {
                    HttpContext.Session.SetInt32("activeUser", possibleLogin[0].userid);
                    return RedirectToAction("Dashboard", "Wedding");
                }
            }
            ViewBag.error = "Incorrect Login Information";
            return View("Index");
        }
        [HttpPost]
        [Route("process")]
        public IActionResult Registration(UserViewModel model)
        {
            if(ModelState.IsValid)
            {
                User newUser = new User{
                    first_name = model.first_name,
                    last_name = model.last_name,
                    email = model.email,
                    password = model.password
                };
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.password = Hasher.HashPassword(newUser, newUser.password);
                _context.Add(newUser);
                _context.SaveChanges();
                int userId = _context.users.Single(u => (string)u.email == (string)model.email).userid;
                HttpContext.Session.SetInt32("activeUser", userId);
                return RedirectToAction("Dashboard", "Wedding");
            }
            return View("Index");
        }
    }
}