using System;
using System.Collections.Generic;
using System.Linq;
using BeltExam.Data;
using BeltExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeltExam.Controllers {
    public class UserAccessController : Controller {
        public DataContext dbContext;
        public UserAccessController (DataContext context) {
            dbContext = context;
        }

        [HttpGet ("access")]
        public IActionResult Access () {
            return View ();
        }

        [HttpPost ("register")]
        public IActionResult Register (User user) {
            System.Console.WriteLine("**************************************************************************");
            System.Console.WriteLine("Register");
            if (ModelState.IsValid) {
                if (dbContext.Users.Any (u => u.Email == user.Email)) {
                    System.Console.WriteLine("Duplicate e-mail");

                    ModelState.AddModelError ("Email", "A user with this e-mail already exists.");
                    return View ("Access");
                } else {
                    System.Console.WriteLine("Making new user");

                    PasswordHasher<User> Hasher = new PasswordHasher<User> ();
                    user.Password = Hasher.HashPassword (user, user.Password);
                    dbContext.Add (user);
                    dbContext.SaveChanges ();
                    HttpContext.Session.SetInt32 ("LoggedInUserID", user.UserId);
                    HttpContext.Session.SetString ("LoggedInUserName", user.FirstName);
                    return RedirectToAction ("Index", "Home");
                }

            } else {
                System.Console.WriteLine("Model invalid");
                return View ("Access");
            }
        }

        [HttpPost ("login")]
        public IActionResult Login (Login user) {
            if (ModelState.IsValid) {
                User dbUser = dbContext.Users.FirstOrDefault (u => u.Email == user.LoginEmail);
                if (dbUser == null) {
                    ModelState.AddModelError ("LoginEmail", "No user with this e-mail exists");
                    return View ("Access");
                } else {
                    var hasher = new PasswordHasher<Login> ();
                    var result = hasher.VerifyHashedPassword (user, dbUser.Password, user.LoginPassword);
                    if (result != 0) {
                        HttpContext.Session.SetInt32 ("LoggedInUserID", dbUser.UserId);
                        HttpContext.Session.SetString ("LoggedInUserName", dbUser.FirstName);
                        return RedirectToAction ("Index", "Home");
                    } else {
                        ModelState.AddModelError ("Password", "The password is incorrect");
                        return View ("Access");
                    }
                }

            } else {
                return View ("Access");
            }
        }
        [HttpGet("logout")]
        public IActionResult LogOff(){
            HttpContext.Session.Clear();
            return RedirectToAction("Access");
        }
    }
}