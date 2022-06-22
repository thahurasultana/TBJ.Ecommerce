using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBJ.Ecommerce.Models;

namespace TBJ.Ecommerce.Controllers
{
    public class RegisterController : Controller
    {
        DbModels dbModel = new DbModels(); //// variable dbModel is created to access the database tables and their records via this variable
        [HttpGet]
        public ActionResult Register(int id=0)
        {
            Users userModel = new Users();
           
            return View(userModel);
        }
        [HttpPost]
        //To register the user is to add the user record in the database and this method does that action
        // This method checks for duplicate registration
        // Registers the users (i.e. adds user record in the DB) only if same username does not
        // exist in the DB already
        public ActionResult Register(Users userModel)
        {
            using (dbModel)
            {
                if(dbModel.Users.Any(x => x.UserName == userModel.UserName))
                {
                    ViewBag.DuplicateMessage="Username already exists";
                    return View("Register", userModel);
                }
                dbModel.Users.Add(userModel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";

            return View("Register", new Users());
        }
    }
}