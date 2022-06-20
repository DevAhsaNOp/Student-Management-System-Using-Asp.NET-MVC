using Student_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Student_Management_System.Controllers
{
    public class UserController : Controller
    {
        db obj = new db();
        // GET: User
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
                return RedirectToAction("UserDashBoard");
            return View();
        }

        // To display and create the view || or used for UI for Signup Page
        public ActionResult Signup()
        {
            if (Session["UserName"] != null)
                return RedirectToAction("UserDashBoard");
            return View();
        }

        [HttpPost]
        // To Submit the view and works on the behind logics that are to be implemented 
        public ActionResult Signup(Signup model)
        {
            Signup s = new Signup();
            s.name = model.name;
            s.email = model.email;
            s.password = model.password;
            s.Confirmpassword = model.Confirmpassword;
            obj.Signups.Add(s);                         // Adding the data into tbl_account table 
            obj.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            if (Session["UserName"] != null)
                return RedirectToAction("UserDashBoard");
            return View();
        }

        [HttpPost]
        public ActionResult Login(Signup model)
        {
            //Checking the Signup class data with model class or you may say finding the data in the database of the given login credentials 
            Signup s = obj.Signups.Where(a => a.email.Equals(model.email) && a.password.Equals(model.password)).SingleOrDefault();
            if (s != null)
            {
                //Response.Cookies["UserID"].Value = s.userid.ToString();
                //Response.Cookies["UserName"].Value = s.name.ToString();
                Session["UserID"] = s.userid.ToString();
                Session["UserName"] = s.name.ToString();
                return RedirectToAction("UserDashBoard");
            }
            else
            {
                ViewBag.msg = "Invalid Email or Password!";
            }
            return View();
        }

        public ActionResult UserDashBoard()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}