using SwachBharat.CMS.Bll.Repository.GridRepository;
using SwachBharat.CMS.Bll.Repository.GridRepository.Grid;
using SwachBharat.CMS.Bll.Repository.RepositoryGrid.Grid;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwachhBharatAbhiyan.CMS.Controllers
{
    public class DatableController : Controller
    {
        // GET: Datable
        IDataTableRepository gridRepository;


        public string GetJqGridJson(string INSERT_ID, string draw, string start, string length, string rn, DateTime? fdate = null, DateTime? tdate = null, int userId = 0, string clientName = null, int? param1 = null, int? param2 = null, int? param3 = null,int? param5 = null, int? param6 = null)
        {
            if (Convert.ToInt32(length) == 5)
            {
                length = "10";
            }
            int number = 0;
            var sortColumn = "";
            var sortColumnDir = "";
            if (draw == "1")
            {
                sortColumn = "";
                sortColumnDir = "";

            }
            else
            {
                sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                //sortColumnDir = "desc";

            }
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            if (Convert.ToInt32(length) >= 5 || Session["rn"].ToString() == rn)
            {
                if (Session["rn"] == null)
                {
                    Session["Search"] = null;
                }
                else if (Session["rn"].ToString() == rn)
                {
                    string[] a = searchValue.Split(',');
                    if ((searchValue == null || searchValue == "") && (Convert.ToInt32(length) >= 5))
                    {
                        if (a.Length >= 5 && a[5].ToString() == "1")
                        {
                            searchValue = Session["Search"].ToString();
                        }
                        else
                        {
                            if ((searchValue == null || searchValue == "") && (Convert.ToInt32(length) >= 5 && draw != "1"))
                            { searchValue = Session["Search"].ToString(); }
                            else
                            {
                                searchValue = "";
                                Session["Search"] = null;
                                Session["rn"] = null;
                            }
                        }
                    }
                }
            }

            Dictionary<string, string> filtersFromSession = CreateFiltersDictionary(Session);
            gridRepository = GetRepository(INSERT_ID, rn, searchValue, fdate, tdate, userId, clientName, param1, param2, param5, param6);
            //string x = gridRepository.GetDataTabelJson(sord, page, rows, _search, Request.QueryString, filtersFromSession, sidx);

            return gridRepository.GetDataTabelJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
        }

        private Dictionary<string, string> CreateFiltersDictionary(System.Web.HttpSessionStateBase Session)
        {

            Dictionary<string, string> FiltersDictionary = new Dictionary<string, string>();

            foreach (string key in Session.Keys)
            {
                FiltersDictionary.Add(key, Session[key] == null ? null : Session[key].ToString());
            }
            return FiltersDictionary;
        }

        private IDataTableRepository GetRepository(string INSERT_ID, string RepositoryName, string searchString = "", DateTime? fdate = null, DateTime? tdate = null, int userId = 0, string clientId = null, int? param1 = null, int? param2 = null, int? param3 = null, int? param4 = null, int? param5 = null, int? param6 = null)
        {
            int product = 0, category = 0;
            //string[] arr = new string[6];

            if (searchString != "" && searchString != null)
            {
                Session["Search"] = searchString;
                Session["rn"] = RepositoryName;

                string[] arr = searchString.Split(',');
                if (arr.Length >= 3)
                {
                    searchString = "";

                    if (arr[0].ToString() != null && arr[0] != " " && arr[0].ToString() != string.Empty)
                    { fdate = Convert.ToDateTime(arr[0] + " " + "00:00:00"); }
                    else fdate = null;
                    if (arr[1].ToString() != null && arr[1] != " " && arr[1].ToString() != string.Empty)
                    { tdate = Convert.ToDateTime(arr[1] + " " + "23:59:59"); }
                    else tdate = fdate;

                    if (arr[2].ToString() != "null" && arr[2].ToString() != null && arr[2] != " " && arr[2].ToString() != string.Empty)
                    { userId = Convert.ToInt32(arr[2]); }
                    else userId = 0;
                    if (arr[3].ToString() != null && arr[3] != " " && arr[3].ToString() != string.Empty)
                    { searchString = Convert.ToString(arr[3]); }
                    else searchString = "";

                    if (arr.Length > 4)
                    {
                        if (arr[4].ToString() != null && arr[4] != "0" && arr[4] != " " && arr[4].ToString() != string.Empty)
                        { param1 = Convert.ToInt32(arr[4]); }
                        else param1 = null;
                    }

                    if (arr.Length > 5)
                    {
                        if (arr[5].ToString() != null && arr[5].ToString() != "null" && arr[5] != "0" && arr[5] != " " && arr[5].ToString() != string.Empty)
                        { param2 = Convert.ToInt32(arr[5]); }
                        else param2 = null;
                    }

                    if (arr.Length > 6)
                    {
                        if (arr[6].ToString() != null && arr[6].ToString() != "null" && arr[6] != "0" && arr[6] != " " && arr[5].ToString() != string.Empty)
                        { param3 = Convert.ToInt32(arr[6]); }
                        else param3 = null;
                    }

                    if (arr.Length > 7)
                    {
                        if (arr[7].ToString() != null && arr[7].ToString() != "null" && arr[7] != " " && arr[7].ToString() != string.Empty)
                        {
                            param4 = Convert.ToInt32(arr[7]);
                        }
                        else param4 = null;
                    }
                    if (arr.Length > 8)
                    {
                        if (arr[8].ToString() != null && arr[8].ToString() != "null" && arr[8] != " " && arr[8].ToString() != string.Empty)
                        {
                            param5 = Convert.ToInt32(arr[8]);
                        }
                        else param5 = null;
                    }

                    //if (arr.Length > 9)
                    //{
                    //    if (arr[9].ToString() != null && arr[9].ToString() != "null" && arr[9] != " " && arr[9].ToString() != string.Empty)
                    //    {
                    //        param6 = Convert.ToInt32(arr[9]);
                    //    }
                    //    else param6 = null;
                    //}

                    //if (arr[3].ToString() != null && arr[3] != " ")
                    //{ clientId = arr[3]; }
                    //else clientId = null;

                    //if (arr[4].ToString() != null && arr[4] != " ")
                    //{ searchString = arr[4]; }
                    //else searchString = "";

                    //if (arr[5].ToString() != null && arr[5] != "")
                    //{ product = Convert.ToInt32(arr[5]); }
                    //else product = 0;

                    //if (arr[6].ToString() != null && arr[6] != "")
                    //{ category = Convert.ToInt32(arr[6]); }
                    //else category = 0;
                }
                else
                {
                    fdate = null;
                    tdate = null;
                    userId = 0;
                    clientId = null;
                    product = 0;
                    category = 0;
                    param1 = null;
                    param2 = null;
                    param3 = null;
                    param4 = null;
                    param5 = null;
                    // searchString = "";
                }
            }
            else
            {
                if(fdate!=tdate)
                {
                    fdate = fdate;
                    tdate = tdate;
                }
                else
                { 
                string dt = DateTime.Now.ToString("MM/dd/yyyy");
                fdate = Convert.ToDateTime(dt + " " + "00:00:00");
                tdate = Convert.ToDateTime(dt + " " + "23:59:59");
                }


            }
            //var appId = ((SessionHandler)Session["clsSession"]).AppId;
            var appId = SessionHandler.Current.AppId;
            switch (RepositoryName)
            {
                case "Location":
                    gridRepository = new LocationGridRepository(0, searchString, fdate, tdate, userId, appId, null);
                    return gridRepository;
                    break;

                case "LiquidLocation":
                    gridRepository = new LocationGridRepository(0, searchString, fdate, tdate, userId, appId, "L");
                    return gridRepository;
                    break;
                case "StreetLocation":
                    gridRepository = new LocationGridRepository(0, searchString, fdate, tdate, userId, appId, "S");
                    return gridRepository;
                    break;

                case "ActiveEmployee":
                    gridRepository = new EmployeeGridRepository(0, searchString, appId, "1", "");
                    return gridRepository;
                    break;

                case "LiquidActiveEmployee":
                    gridRepository = new EmployeeGridRepository(0, searchString, appId, "1", "L");
                    return gridRepository;
                    break;
                case "StreetActiveEmployee":
                    gridRepository = new EmployeeGridRepository(0, searchString, appId, "1", "S");
                    return gridRepository;
                    break;

                case "NotActiveEmployee":
                    gridRepository = new EmployeeGridRepository(0, searchString, appId, "0", "");
                    return gridRepository;
                    break;

                case "NotActiveLiquidEmployee":
                    gridRepository = new EmployeeGridRepository(0, searchString, appId, "0", "L");
                    return gridRepository;
                    break;
                case "NotActiveStreetEmployee":
                    gridRepository = new EmployeeGridRepository(0, searchString, appId, "0", "S");
                    return gridRepository;
                    break;

                case "AttendenceSettingsDetail":
                    gridRepository = new AttendenceSettingsGridRepository(0, searchString, fdate, tdate, userId, appId);
                    return gridRepository;
                    break;
                case "StateDetail":
                    gridRepository = new StateGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;
                case "TalukaDetail":
                    gridRepository = new TalukaGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;
                case "DistrictDetail":
                    gridRepository = new DistrictGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;
                case "AreaDetail":
                    gridRepository = new AreaGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;
                case "WardDetail":
                    gridRepository = new WardNumberGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;
                case "VehicleDetail":
                    gridRepository = new VehicleTypeGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;
                case "VehicleRegDetail":
                    gridRepository = new VehicleRegGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;
                case "PointDetail":
                    gridRepository = new GarbagePointGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;
                case "HouseDetail":
                    gridRepository = new HouseDetailsGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;

                case "SWMDetail":
                    gridRepository = new SWMDetailsGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;

                case "CommercialDetail":
                    gridRepository = new CommercialDetailsGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;

                case "GarbageHouse":
                    gridRepository = new HGarbageCotectionGridRepository(0, searchString, fdate, tdate, userId, appId, param1, param2, param3, param4,param5);
                    return gridRepository;
                    break;

                case "GarbageCommercial":
                    gridRepository = new CommGarbageCollectionGridRepository(0, searchString, fdate, tdate, userId, appId, param1, param2, param3, param4, param5);
                    return gridRepository;
                    break;
                case "GarbageResidentialBuilding":
                    gridRepository = new ResBuildingGarbageCollectionGridRepository(0, searchString, fdate, tdate, userId, appId, param1, param2, param3, param4, param5);
                    return gridRepository;
                    break;
                case "GarbageResidentialSlum":
                    gridRepository = new ResSlumGarbageCollectionGridRepository(0, searchString, fdate, tdate, userId, appId, param1, param2, param3, param4, param5);
                    return gridRepository;
                    break;
                case "GarbageSWM":
                    gridRepository = new SWMGarbageCollectionGridRepository(0, searchString, fdate, tdate, userId, appId, param1, param2, param3, param4, param5);
                    return gridRepository;
                    break;

                case "GarbageCTPT":
                    gridRepository = new CTPTGarbageCollectionGridRepository(0, searchString, fdate, tdate, userId, appId, param1, param2, param3, param4);
                    return gridRepository;
                    break;
                case "GarbageCountCTPT":
                    gridRepository = new CTPTCountGarbageCollectionGridRepository(0, searchString, fdate, tdate, userId, appId);
                    return gridRepository;
                    break;
                case "GarbagePoint":
                    gridRepository = new PGarbageCollectionGridRepository(0, searchString, fdate, tdate, userId, appId, param1, param2, param3);
                    return gridRepository;
                    break;

                case "LiquidGarbage":
                    gridRepository = new LGarbageCollectionGridRepository(0, searchString, fdate, tdate, userId, appId, param1, param2, param3);
                    return gridRepository;
                    break;
                case "StreetGarbage":
                    gridRepository = new SSCollectionGridRepository(0, searchString, fdate, tdate, userId, appId, param1, param2, param3);
                    return gridRepository;
                    break;
                case "Attendence":
                    gridRepository = new AttendeceGridRepository(0, searchString, fdate, tdate, userId, appId, null);
                    return gridRepository;
                    break;

                case "LiquidAttendence":
                    gridRepository = new AttendeceGridRepository(0, searchString, fdate, tdate, userId, appId, "L");
                    return gridRepository;
                    break;
                case "StreetAttendence":
                    gridRepository = new AttendeceGridRepository(0, searchString, fdate, tdate, userId, appId, "S");
                    return gridRepository;
                    break;

                case "Complaint":
                    gridRepository = new ComplaintGridRepository(0, searchString, fdate, tdate, appId);
                    return gridRepository;
                    break;

                case "ZoneDetail":
                    gridRepository = new ZoneGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;

                case "LiquidZoneDetail":
                    gridRepository = new LiquidZoneGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;

                case "UserIdel":
                    gridRepository = new IdelGridRepository(0, searchString, fdate, tdate, userId, appId);
                    return gridRepository;
                    break;

                case "UserIdelLiquid":
                    gridRepository = new LiquidIdelGridRepository(0, searchString, fdate, tdate, userId, appId);
                    return gridRepository;
                    break;
                case "UserIdelStreet":
                    gridRepository = new StreetIdelGridRepository(0, searchString, fdate, tdate, userId, appId);
                    return gridRepository;
                    break;
                case "GarbageCount":
                    gridRepository = new GarbageCountGridRepository(0, searchString, fdate, tdate, userId, appId);
                    return gridRepository;
                    break;

                case "AdminCount":
                    gridRepository = new AdminCountGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;

                case "DumpYard":
                    gridRepository = new DumpYardGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;
                case "StreetSweep":
                    gridRepository = new StreetSweepGrid(0, searchString, appId);
                    return gridRepository;
                    break;

                case "StreetSweepBeat":
                    gridRepository = new StreetSweepBeatGrid(0, searchString, appId);
                    return gridRepository;
                    break;

                case "LiquidWaste":
                    gridRepository = new LiquidWasteGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;

                case "DumpYardDetails":
                    gridRepository = new DumpYardCollectionGridRepository(0, searchString, fdate, tdate, userId, appId, param1, param2, param3);
                    return gridRepository;
                    break;

                case "LiquidDumpYardDetails":
                    gridRepository = new LiquidDumpYardCollectionGridRepository(0, searchString, fdate, tdate, userId, appId, param1, param2, param3);
                    return gridRepository;
                    break;

                case "StreetDumpYardDetails":
                    gridRepository = new StreetDumpYardCollectionGridRepository(0, searchString, fdate, tdate, userId, appId, param1, param2, param3);
                    return gridRepository;
                    break;

                case "EmployeeSummary":
                    gridRepository = new EmpolyeeSummaryGridRepository(0, searchString, fdate, tdate, userId, appId, null);
                    return gridRepository;
                    break;


                case "LiquidEmployeeSummary":
                    gridRepository = new EmpolyeeSummaryGridRepository(0, searchString, fdate, tdate, userId, appId, "L");
                    return gridRepository;
                    break;

                case "StreetEmployeeSummary":
                    gridRepository = new EmpolyeeSummaryGridRepository(0, searchString, fdate, tdate, userId, appId, "S");
                    return gridRepository;
                    break;

                case "HouseScanify":
                    gridRepository = new HouseScanifyGridRepository(0, searchString, fdate, tdate, userId, appId);
                    return gridRepository;
                    break;

                case "LeagueDetail":
                    gridRepository = new LeagueGridRepository(0, searchString, fdate, tdate, userId, appId);
                    return gridRepository;
                    break;

                case "SauchalayDetail":
                    gridRepository = new SauchalayGridRepository(0, searchString, fdate, tdate, userId, appId);
                    return gridRepository;
                    break;

                case "onepointfourDetail":
                    gridRepository = new OnePointFourGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;

                case "onepointfourEdit":
                    gridRepository = new OnePointFourEditGridRepository(0, searchString, appId, INSERT_ID);
                    return gridRepository;
                    break;

                case "onepointfiveDetail":
                    gridRepository = new OnePointFiveGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;

                case "onepointfiveEdit":
                    gridRepository = new OnePointFourEditGridRepository(0, searchString, appId, INSERT_ID);
                    return gridRepository;
                    break;

                case "onepointsixDetail":
                    gridRepository = new OnePointSixGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;

                case "onepointSixEdit":
                    gridRepository = new OnePointFourEditGridRepository(0, searchString, appId, INSERT_ID);
                    return gridRepository;
                    break;

                case "onepointsevenDetail":
                    gridRepository = new OnePointSevenGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;

                case "onepointeightDetail":
                    gridRepository = new OnepointeightGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;

                case "onepointeightEdit":
                    gridRepository = new OnePointFourEditGridRepository(0, searchString, appId, INSERT_ID);
                    return gridRepository;
                    break;

                case "Infotainment":
                    //gridRepository = new InfotainmentGridRepository(0, searchString, appId);
                    gridRepository = new InfotainmentGridRepository(0, searchString, 0);
                    return gridRepository;
                    break;

                case "HSAttendance":
                    gridRepository = new HSAttendanceGridRepository(0, searchString, fdate, tdate, userId, appId);
                    return gridRepository;
                    break;
                case "HSHouseDetails":
                    gridRepository = new HSHouseDetailsGridRepository(0, searchString, fdate, tdate, userId, appId);
                    return gridRepository;
                    break;
                case "HSDumpyardDetails":
                    gridRepository = new HSDumpyardDetailsGridRepository(0, searchString, fdate, tdate, userId, appId);
                    return gridRepository;
                    break;
                case "HSLiquidDetails":
                    gridRepository = new HSLiquidDetailsGridRepository(0, searchString, fdate, tdate, userId, appId);
                    return gridRepository;
                    break;
                case "HSStreetDetails":
                    gridRepository = new HSStreetDetailsGridRepository(0, searchString, fdate, tdate, userId, appId);
                    return gridRepository;
                    break;
                case "InfotainmentPlayerDetails":
                    gridRepository = new InfotainmentPlayerGridRepository(0, searchString, fdate, tdate, userId, appId);
                    return gridRepository;
                    break;

                case "SauchalayRegistration":
                    gridRepository = new SauchalayRegistrationGridRepository(0, searchString, appId);
                    return gridRepository;
                    break;

                case "WM_WasteDetails":
                    gridRepository = new WM_WasteDetailsGridRepository(0, searchString, fdate, tdate, userId, appId, param1, param2);
                    return gridRepository;
                    break;

                //case "WM_WetWaste":
                //    gridRepository = new WM_DryWasteGridRepository(0, searchString, fdate, tdate, userId, appId, RepositoryName = "WM_WetWaste");
                //    return gridRepository;
                //    break;

                case "RfidDetail":
                    //gridRepository = new InfotainmentGridRepository(0, searchString, appId);
                    gridRepository = new RfidGridRepository(0, searchString, 0);
                    return gridRepository;
                    break;


            }

            return null;
        }

    }


}