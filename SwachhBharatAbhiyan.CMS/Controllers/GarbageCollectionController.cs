using SwachBharat.CMS.Bll.Repository.GridRepository;
using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class GarbageCollectionController : Controller
    {

        IMainRepository mainRepository;
        IChildRepository childRepository;

        public GarbageCollectionController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }
        // GET: GarbageCollection
        public ActionResult HouseGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuHouseGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuCommercialGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult CommercialGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuResidentialBuildingGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuDSIGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult DSIGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult ResidentialBuildingGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult MenuResidentialSlumGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult ResidentialSlumGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuCTPTGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuCTPTDetailGarbageIndex(int teamId,string fdate, string tdate,int param1)
        {

                ViewBag.userid = teamId;
                ViewBag.fdate = fdate;
                ViewBag.tdate = tdate;
               ViewBag.Rown = param1;
            return PartialView(@"~/Views/Shared/_CTPTGarbageIndex.cshtml");
          
                
        }

        public ActionResult CTPTCountGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult CTPTGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuSWMGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult SWMGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult PointGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuPointGarbageIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        // Added By Saurabh - 25 Apr 2019
        public ActionResult DumpYardIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);

                var details = childRepository.GetDashBoardDetails(PId);
                return View(details);
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuDumpYardIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuEntryDetails()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult EntryDetails()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult MenuIdealtime()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult Idealtime()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        //Add by neha 12 june 2019
        public ActionResult IdleTime_Route()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                //ViewBag.userId = userId;
                //ViewBag.Date = Date;
                //return Json(userId, JsonRequestBehavior.AllowGet);
                return View();
            }
            else
                return Redirect("/Account/Login");
        }
        //Add by neha 12 june 2019
        public ActionResult IdleTimeRouteData(int userId, string Date)
        {
            if (SessionHandler.Current.AppId != 0)
            {

                List<SBAEmplyeeIdelGrid> obj = new List<SBAEmplyeeIdelGrid>();
                int PId = Convert.ToInt32(Session["PrabhagId"]);
                obj = childRepository.GetIdleTimeRoute(userId, Date, PId);

                // return Json(obj);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }
        public ActionResult UserCount(DateTime? fdate = null, DateTime? tdate = null,int userId=0)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                if (fdate == null)
                {
                    string dt = DateTime.Now.ToString("MM/dd/yyyy");
                    fdate = Convert.ToDateTime(dt + " " + "00:00:00");
                    tdate = Convert.ToDateTime(dt + " " + "23:59:59");
                }
                
                IEnumerable<SBAGarbageCountDetails> obj;

                DashBoardRepository objRep = new DashBoardRepository();

                obj = objRep.GetGarbageCountData(0, "", fdate, tdate, Convert.ToInt32(userId), SessionHandler.Current.AppId);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult IdelTimeNotification()
        {
            if (SessionHandler.Current.AppId != 0)
            
            {
                List<SBAEmplyeeIdelGrid> obj = new List<SBAEmplyeeIdelGrid>();

                int PId = Convert.ToInt32(Session["PrabhagId"]);
                obj = childRepository.GetIdelTimeNotification(PId);

                //List<SBAEmplyeeIdelGrid> obj = new List<SBAEmplyeeIdelGrid>()
                //{
                //     new SBAEmplyeeIdelGrid() { UserName = " sad", StartTime = "50", EndTime = "1", IdelTime = "00:22" },
                //       new SBAEmplyeeIdelGrid() { UserName = " gfhfh", StartTime = "50", EndTime = "1", IdelTime = "00:22" },
                //   //........................ and so on
                //};
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult ZoneList()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);
                List<SelectListItem> ZoneLst = new List<SelectListItem>();
                ZoneLst = childRepository.ZoneListPId(PId);
                return Json(ZoneLst, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult WardList()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);
                List<SelectListItem> WardLst = new List<SelectListItem>();
                WardLst = childRepository.WardListPId(PId);
                return Json(WardLst, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult PrabhagList()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);
                List<SelectListItem> PrabhagLst = new List<SelectListItem>();
                PrabhagLst = childRepository.PrabhagListPId(PId);
                return Json(PrabhagLst, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult AreaList()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);
                List<SelectListItem> AreaLst = new List<SelectListItem>();
                AreaLst = childRepository.AreaLstPId(PId);
                return Json(AreaLst, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }


        public ActionResult LoadPrabhagNoList(int ZoneId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);
                List<SelectListItem> PrabhagLst = new List<SelectListItem>();

                PrabhagLst = childRepository.LoadPrabhagNoListPId(PId, ZoneId);
                return Json(PrabhagLst, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }


        public ActionResult LoadWardNoList(int PrabhagId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);
                List<SelectListItem> WardLst = new List<SelectListItem>();

                WardLst = childRepository.LoadListWardNoPId(PId, PrabhagId);
                return Json(WardLst, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }

        public ActionResult LoadAreaList(int WardNo)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);

                List<SelectListItem> AreaLst = new List<SelectListItem>();
                AreaLst = childRepository.LoadAreaListPId(PId, WardNo);
                return Json(AreaLst, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");

        }

    }
}