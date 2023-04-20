using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class EmployeeController  : Controller
    {
        // GET: Employee 
        IChildRepository childRepository;
        IMainRepository mainRepository;
        public EmployeeController()
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
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult AddEmployeeDetails(int teamId = -1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);
                EmployeeDetailsVM house = childRepository.GetEmployeeById(teamId, PId);
                return View(house);
            }
            else
                return Redirect("/Account/Login");
        }

        [HttpPost]
        public ActionResult AddEmployeeDetails(EmployeeDetailsVM emp, HttpPostedFileBase filesUpload)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                var AppDetails = mainRepository.GetApplicationDetails(SessionHandler.Current.AppId);
                if (filesUpload != null)
                {

                    var guid = Guid.NewGuid().ToString().Split('-');
                    string image_Guid = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid[1] + ".jpg";

                    //Converting  Url to image 

                    string imagePath = Path.Combine(Server.MapPath(AppDetails.basePath + AppDetails.UserProfile), image_Guid);
                    var exists = System.IO.Directory.Exists(Server.MapPath(AppDetails.basePath + AppDetails.UserProfile));
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(AppDetails.basePath + AppDetails.UserProfile));
                    }
                    filesUpload.SaveAs(imagePath);
                    emp.userProfileImage = image_Guid;
                }

                childRepository.SaveEmployee(emp,emp.EmployeeType);
                return Redirect("Index");
            }
            else
                return Redirect("/Account/Login");
        }


        [HttpPost]
        public ActionResult CheckUserDetails(string user1)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
                //string user1 = "";

                //if (obj.userLoginId != null)
                //{
                //    user1 = obj.userLoginId;
                //}
                //ElistItems.Add(new SelectListItem { Text = "Waste", Value = "Waste" });
                //ElistItems.Add(new SelectListItem { Text = "CT/PT/U", Value = "CT" });
                //ElistItems.Add(new SelectListItem { Text = "DYSI/SI", Value = "DSI" });
                //int e=0;
                //if(emptype== "Waste")
                //{
                //    e = 0;
                //}

                //if (emptype == "CT")
                //{
                //    e = 1;
                //}
                //if (emptype == "DSI")
                //{
                //    e = 2;
                //}
                EmployeeDetailsVM obj = new EmployeeDetailsVM();
                HouseScanifyEmployeeDetailsVM user = childRepository.GetUser(0, user1);

                if (obj.userId > 0)
                {
                    if (user.qrEmpId == 0)
                    {
                        user.qrEmpId = obj.userId;
                    }
                    if (user1 == obj.userLoginId & user.qrEmpId != obj.userId)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                    else if (user1 == user.qrEmpLoginId & user.qrEmpId != obj.userId)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                 if (user.qrEmpLoginId != null)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(true, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(true, JsonRequestBehavior.AllowGet);




        }
        [HttpGet]
        public ActionResult DeleteEmployee(int teamId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                childRepository.DeleteEmployee(teamId);
                return Redirect("Index");
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult EmployeeSummaryIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuEmployeeSummaryIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult LoadPrabhagNoList(int ZoneId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                EmployeeDetailsVM obj = new EmployeeDetailsVM();
                try
                {
                    int PId = Convert.ToInt32(Session["PrabhagId"]);
                    obj.PrabhagList = childRepository.LoadListPrabhagNo(ZoneId, PId);
                }
                catch (Exception ex) { throw ex; }

                return Json(obj.PrabhagList, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }
    }
}
