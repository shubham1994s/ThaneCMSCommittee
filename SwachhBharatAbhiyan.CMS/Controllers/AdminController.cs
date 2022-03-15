using SwachhBharatAbhiyan.CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult MenuIndex()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menus()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult login(LoginViewModel model, string returnUrl)
        {
            if (model.Password == "Admin#123" & model.Email == "Admin")
            {

                return RedirectToAction("MenuIndex");
            }
            else {
                return View();

            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult login1(LoginViewModel model, string returnUrl)
        {
            if (model.Password == "Admin#123" & model.Email == "Admin")
            {

                return RedirectToAction("MenuIndex");
            }
            else
            {
                return View();

            }
        }
    }
}