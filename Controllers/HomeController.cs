using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using crud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace crud.Controllers
{
    public class HomeController : Controller
    {
        private CRUDContext dbContext;
        public HomeController(CRUDContext context)
        {
            dbContext = context;
        }
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View("LoginReg");
        }
        [HttpPost("createAccount")]
        public IActionResult createAccount(User newUser)
        {
            if(ModelState.IsValid){
                if(dbContext.users.Any(u => u.Email == newUser.Email))
                    {
                    return View("LoginReg");
            // You may consider returning to the View at this point
                    }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                dbContext.Add(newUser);
                dbContext.SaveChanges();
                HttpContext.Session.SetString("UserName", newUser.Fname);

                return RedirectToAction("Success");
            }
            else{
                return View("LoginReg");
            }
            
        }
        [HttpGet("success")]
        public IActionResult Success()
        {
            if(HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("register");
            }            
            return View("success");
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost("login")]
        public IActionResult LoginAction(LoginUser userSubmission)
        {
          if(ModelState.IsValid)
        {
            var userInDb = dbContext.users.FirstOrDefault(u => u.Email == userSubmission.Email);
            if(userInDb == null)
            {             
                return View("Login");
            }
            var hasher = new PasswordHasher<LoginUser>();
            var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);
            if(result == 0)
            {
               return View("Login");
            }
            HttpContext.Session.SetString("UserName", userInDb.Fname);
            return RedirectToAction("Success");
        } 
           return View("Login"); 
        }
         [HttpGet("logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Register");
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("Register");
            // List<Dish> AllDishes = dbContext.dishes.ToList();
            
            // return View("index",AllDishes);
        }
        
    //     [HttpGet("new")]
    //     public IActionResult New()
    //     {
    //         return View("New");
    //     }
        
    //     [HttpPost("create")]

    //     public IActionResult Create(Dish NewDish)
    //     {
        
    //         dbContext.Add(NewDish);

    //         dbContext.SaveChanges();
    //         return RedirectToAction("Index");
    //     }
    //     [HttpGet("{id}")]
    //     public IActionResult New(int id)
    //     {
    //         var singleDish = dbContext.dishes.Where(dishes => dishes.DishId ==id).ToList();
    //         return View("SingleDish",singleDish);
    //     }
    //     [HttpGet("delete/{id}")]
    //     public IActionResult delete(int id)
    //     {
    //         Dish singleDish = dbContext.dishes.SingleOrDefault(dishes => dishes.DishId ==id);
    //         dbContext.dishes.Remove(singleDish);
    //         dbContext.SaveChanges();
    //         return RedirectToAction("Index");
    //     }
    //     [HttpGet("edit/{id}")]
    //     public IActionResult edit(int id)
    //     {
            
    //         Dish singleDish = dbContext.dishes.SingleOrDefault(dishes => dishes.DishId ==id);

            
    //         return View("editDish",singleDish);
    //     }
    //     [HttpPost("home/update/{id}")]
    //     public IActionResult update(int id, Dish updatedDish)
    //     {
    //         Dish singleDish = dbContext.dishes.SingleOrDefault(dishes => dishes.DishId ==id);
    //          if(ModelState.IsValid){
    //             singleDish.Name = updatedDish.Name;
    //             singleDish.Chef = updatedDish.Chef;
    //             singleDish.Calories = updatedDish.Calories;
    //             singleDish.Tastiness = updatedDish.Tastiness;
    //             singleDish.Description = updatedDish.Description;
    //             dbContext.SaveChanges();
    //             return RedirectToAction("Index");
    //          }
    //          else{
    //              return RedirectToAction("edit");
    //          }           
            
    //     }
    }
}
