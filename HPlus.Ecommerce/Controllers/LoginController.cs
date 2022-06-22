using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TBJ.Ecommerce.Models;

namespace TBJ.Ecommerce.Controllers
{
    public class LoginController : Controller
    {
        // This Action Method executes Index.cshtml file under ~/Views/Login folder and retruns that View

        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            Users userModel = new Users();

            return View(userModel);
        }

        //This method validates Username and password against the saved username and password in the database
        // and give appropriate result. If username exist in the DB and its corresponding password as well maches 
        // then session of the user is retained so as to display it using the view
        // otherwise, either username itslef doesnt exist error occurs or Invalid/Incorrect login credentials error can occur
        [HttpPost]
        public ActionResult Index(Users userModel)
        {
            using (DbModels dbModel = new DbModels())
            {
                if (!(dbModel.Users.Any(x => x.UserName == userModel.UserName)))
                {
                    ViewBag.ErrorMessage = "There was a problem. We cannot find an account with that username";
                    return View("Index", userModel);
                }
                if ((dbModel.Users.Where(y => y.UserName == userModel.UserName && y.Password == userModel.Password).FirstOrDefault() == null))
                {

                    ViewBag.ErrorMessage = "Incorrect Login Credentials";
                    return View("Index", userModel);
                }
                else
                {
                    Session["UserID"] = userModel.UserID; //Saving the User ID session so that it can be later used to display the Username of the user who has logged in 

                    Session["UserName"] = userModel.UserName; //Saving the session for username
                     return RedirectToAction("Index", "Home");
                }

            }

        }

        //This method removes all saved sessions, and redirects the user to the Index page i.e. ~/Views/Login/Index.cshtml
        //The significance of this method is when a user hits "Logout" button, this method is called and it removes all sessions that
        // were maintained for that user to be called as a logged in user and now removes them all which basically means
        // that it performs logout action on that webpage

        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}