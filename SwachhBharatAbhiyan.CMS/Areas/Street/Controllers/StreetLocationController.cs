﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.Repository.ChildRepository;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;

namespace SwachhBharatAbhiyan.CMS.Areas.Street.Controllers
{
    public class StreetLocationController : Controller
    {
        IMainRepository mainRepository;
        IChildRepository childRepository;
        public StreetLocationController()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                mainRepository = new MainRepository();
                childRepository = new ChildRepository(SessionHandler.Current.AppId);
            }
            else
                Redirect("/Account/Login");
        }


        // GET: Street/StreetLocation
        public ActionResult MenuIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AllHouseLocation()
        
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);

                ViewBag.lat = SessionHandler.Current.Latitude;
                ViewBag.lang = SessionHandler.Current.Logitude;
                ViewBag.AppName = SessionHandler.Current.AppName;

                var details = childRepository.GetStreetSweepingDetails(PId);
                return View(details);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuMapIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                TempData.Keep();
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult ShowGrid()// default or index Method
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult AllUserLocation()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                ViewBag.lat = SessionHandler.Current.Latitude;
                ViewBag.lang = SessionHandler.Current.Logitude;
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult LocatioList(string date, string userid)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);
                if (date == null || date == "")
                {
                    date = DateTime.Now.ToShortDateString();
                }

                List<SBALUserLocationMapView> obj = new List<SBALUserLocationMapView>();
                obj = childRepository.GetAllUserLocation(date, "S", PId);
                // return Json(obj);
                if (userid != null && userid != "null" && userid != "-1")
                {
                    obj = obj.Where(c => c.userId == Convert.ToInt32(userid)).ToList();
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult UserWiseLocatioList(int userId, string date)
        {
            if (SessionHandler.Current.AppId != 0)
            {

                if (date == null || date == "")
                {
                    date = DateTime.Now.ToShortDateString();
                }
                else
                {
                    date = date.ToString();

                }
                List<SBALUserLocationMapView> obj = new List<SBALUserLocationMapView>();
                obj = childRepository.GetUserWiseLocation(userId, date, "S");
                // return Json(obj);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult UserList(string rn)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                SBALUserLocationMapView obj = new SBALUserLocationMapView();

                int PId = Convert.ToInt32(Session["PrabhagId"]);
                obj = childRepository.GetLocation(-1, rn, PId);
                return Json(obj.UserList, JsonRequestBehavior.AllowGet);

            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult UserCurrentLocation(int userId, string date)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);
                if (date == null || date == "")
                {
                    date = DateTime.Now.ToShortDateString();
                }
                List<SBALUserLocationMapView> obj = new List<SBALUserLocationMapView>();
                obj = childRepository.GetAllUserLocation(date, "S", PId).Where(c => c.userId == userId).ToList();
                // return Json(obj);
                return Json(obj, JsonRequestBehavior.AllowGet);

            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult MenuHouseMapIndex()
        {
            if (SessionHandler.Current.AppId != 0)
            {
                return View();
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult ViewLocation(int teamId = -1 )
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);
                SBALUserLocationMapView loc = childRepository.GetLocation(teamId, "S", PId);
                TempData.Keep();
                return View(loc);
            }
            else
                return Redirect("/Account/Login");
        }

        public ActionResult HouseLocationList(string date, string userid, string areaId, string wardNo, string SearchString, string garbageType, string filterType, string ZoneId, string PrabhagNo)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);
                int user;
                int area;
                int ward;
                int zone;
                int prabhag;
                int? GarbageType;
                int FilterType;
                if (userid == "-1" || userid == "0" || userid == "null")
                {
                    user = 0;
                }
                else
                {
                    user = Convert.ToInt32(userid);
                }

                if (areaId == "-1" || areaId == "0" || areaId == "null")
                {
                    area = 0;
                }
                else
                {
                    area = Convert.ToInt32(areaId);
                }
                if (wardNo == "-1" || wardNo == "0" || wardNo == "null")
                {
                    ward = 0;
                }
                else
                {
                    ward = Convert.ToInt32(wardNo);
                }
                if (garbageType == "-1" || garbageType == null)
                {
                    GarbageType = null;
                }
                else
                {
                    GarbageType = Convert.ToInt32(garbageType);
                }
                if (filterType == "-1" || filterType == "0" || filterType == "null")
                {
                    FilterType = 0;
                }
                else
                {
                    FilterType = Convert.ToInt32(filterType);
                }
                if (date == null || date == "")
                {
                    date = DateTime.Now.ToShortDateString();
                }
                if (ZoneId == "-1" || ZoneId == "0" || ZoneId == "null")
                {
                    zone = 0;
                }
                else
                {
                    zone = Convert.ToInt32(ZoneId);
                }
                if (PrabhagNo == "-1" || PrabhagNo == "0" || PrabhagNo == "null")
                {
                    prabhag = 0;
                }
                else
                {
                    prabhag = Convert.ToInt32(PrabhagNo);
                }
                List<SBALHouseLocationMapView> obj = new List<SBALHouseLocationMapView>();
                obj = childRepository.GetAllHouseLocation(date, user, area, ward, SearchString, GarbageType, FilterType, "S",null,0, PId, zone, prabhag);
                // return Json(obj);
                //if (houseid != null && houseid != "null" && houseid != "-1")
                //{
                //    obj = obj.Where(c => c.houseId == Convert.ToInt32(houseid)).ToList();
                //}
                // return Json(obj, JsonRequestBehavior.AllowGet);

                var jsonResult = Json(obj, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
                return Redirect("/Account/Login");
        }
        public ActionResult HouseLocationListForEmpBitMap(string Emptype,int ebmId)
        {
            if (SessionHandler.Current.AppId != 0)
            {
                int PId = Convert.ToInt32(Session["PrabhagId"]);
                SBALocationMapViewVM obj = new SBALocationMapViewVM();
                List<SBALocationMapView> objLoc = new List<SBALocationMapView>();
                EmpBeatMapVM empBeatMap = childRepository.GetEmpBeatMap(ebmId);

                objLoc = childRepository.GetAllHouseLocationForEmpBitMap(Emptype, ebmId, PId);
                obj.lstUserLocation = objLoc;
                obj.poly = empBeatMap.ebmLatLong;
                // return Json(obj);
                //if (houseid != null && houseid != "null" && houseid != "-1")
                //{
                //    obj = obj.Where(c => c.houseId == Convert.ToInt32(houseid)).ToList();
                //}
                // return Json(obj, JsonRequestBehavior.AllowGet);

                var jsonResult = Json(obj, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
                return Redirect("/Account/Login");
        }

    }
}