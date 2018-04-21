using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BeltExam.Controllers
{
    public class HomeController : Controller
    {
        private BeltExamContext _context;
        public HomeController(BeltExamContext context){
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Register()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        [HttpPost]
        [Route("/register")]
        public IActionResult RegisterUser(RegisterViewModel model, User NewUser){
            if(ModelState.IsValid){
                List<User> Allusers = _context.Users.Where(User=>User.email == model.email).ToList();
                if(Allusers.Count>0){
                    TempData["Emailused"] = "This email has already been registered. Login or Register with new email.";
                    return View("Register");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                NewUser.password = Hasher.HashPassword(NewUser, NewUser.password);
                _context.Users.Add(NewUser);
                _context.SaveChanges();
                User Reg = _context.Users.SingleOrDefault(User=>User.email == NewUser.email);
                HttpContext.Session.SetInt32("userid", (int)Reg.UserId);
                return RedirectToAction("Dashboard", "ActivityCenter");
            }
            return View("Index");
        }
        [HttpPost]
        [Route("/login")]
        public IActionResult LoginUser(string loginemail, string loginpassword){
            User Login = _context.Users.SingleOrDefault(User=> User.email == loginemail);
            if(Login == null){
                TempData["Invalidemail"] = "Email not Registered. Have you Registered?";
                return View("Index");
            }
            if(Login != null && loginpassword != null){
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(Login, Login.password, loginpassword)){
                    HttpContext.Session.SetInt32("userid", (int)Login.UserId);
                    return RedirectToAction("Dashboard", "ActivityCenter");
                }
            }
            TempData["InvalidPW"] = "Invalid Password";
            return View("Index");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
