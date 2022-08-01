﻿using Newtonsoft.Json;
using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.GridRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using SwachBharat.CMS.Dal.DataContexts;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class HomeController : Controller
    {
        IChildRepository childRepository;
        IMainRepository mainRepository;
        public HomeController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }


        public ActionResult Index()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.lat = SessionHandler.Current.Latitude;
                ViewBag.lang = SessionHandler.Current.Logitude;
                ViewBag.YoccFeddbackLink = SessionHandler.Current.YoccFeddbackLink;
                var PrabhagId = Session["PrabhagId"];
               int PId = Convert.ToInt32(Session["PrabhagId"]);

                var details = childRepository.GetDashBoardDetails(PId);
                return View(details);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult About()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.Message = "Your application description page.";
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult Contact()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.Message = "Your contact page.";
                return View();
            }
            else
                return Redirect("/Account/Login");

        }

        public ActionResult DashBoard()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);

                var details = childRepository.GetDashBoardDetails(PId);

                if (details!=null)
                {
                    string jsonstr = JsonConvert.SerializeObject(details);
                    return Json(jsonstr, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            else
                return Redirect("/Account/Login");
        }

        
        public ActionResult EmployeeTargetCount(DateTime? fdate = null, DateTime? tdate = null, int userId = 0)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                if (fdate == null)
                {
                    string dt = DateTime.Now.ToString("MM/dd/yyyy");
                    fdate = Convert.ToDateTime(dt + " " + "00:00:00");
                    tdate = Convert.ToDateTime(dt + " " + "23:59:59");
                }

                IEnumerable<DashBoardVM> obj;

                DashBoardRepository objRep = new DashBoardRepository();

                obj = objRep.getEmployeeTargetData(0, "", fdate, tdate, Convert.ToInt32(userId), SessionHandler.Current.AppId);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }

        //public class SessionExpireAttribute : ActionFilterAttribute
        //{
        //    public override void OnActionExecuting(ActionExecutingContext filterContext)
        //    {
        //        HttpContext ctx = System.Web.HttpContext.Current;
        //        // check  sessions here
        //        if (System.Web.HttpContext.Current.Session["AppId"] == null)
        //        {
        //            filterContext.Result = new RedirectResult("~/Home/Index");
        //            return;
        //        }
        //        base.OnActionExecuting(filterContext);
        //    }
        //}

        //by neha 13 june 2019
        public ActionResult EmployeeHouseCollectionType()
        {
            if (SessionHandler.Current.AppId != 0)
            {

                IEnumerable<EmployeeHouseCollectionType> obj;

                DashBoardRepository objRep = new DashBoardRepository();

                int PId = Convert.ToInt32(Session["PrabhagId"]);

                obj = objRep.getEmployeeHouseCollectionType(SessionHandler.Current.AppId, PId);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult GetCurrentCTPTCollectionCount()
        {
            if (SessionHandler.Current.AppId != 0)
            {

                IEnumerable<CurrentCTPTCollectionCount> obj;

                DashBoardRepository objRep = new DashBoardRepository();

                int PId = Convert.ToInt32(Session["PrabhagId"]);

                obj = objRep.GetCurrentCTPTCollectionCount(SessionHandler.Current.AppId, PId);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }
    }
}
