﻿using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Dal.DataContexts;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using SwachBharat.CMS.Bll.ViewModels.SS2020Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SwachBharat.CMS.Bll.Services
{
    public interface IScreenService
    {
        DashBoardVM GetDashBoardDetails(int PrabhagId);

        DashBoardVM GetLiquidDashBoardDetails(int PrabhagId);

        DashBoardVM GetStreetDashBoardDetails(int PrabhagId);
        string Address(string location);
        AreaVM GetAreaDetails(int teamId,string Name,int PId);
        void DeletAreaDetails(int teamId);
        void SaveAreaDetails(AreaVM area);
        void LiquidSaveAreaDetails(AreaVM area);

        void StreetSaveAreaDetails(AreaVM area);


        VehicleTypeVM GetVehicleTypeDetails(int teamId);
        VehicleRegVM GetVehicleDetails(int teamId,int PId);

        StreetSweepVM GetBeatDetails(int teamId, int ? PId);

        void DeletVehicleTypeDetails(int teamId);
        void SaveVehicleTypeDetails(VehicleTypeVM type);
        void SaveVehicleRegDetails(VehicleRegVM type);

        WardNumberVM GetWardNumberDetails(int teamId,string name,int PId);

        CommitteeVM GetCommitteeNameDetails(int teamId, string name,int PId);
        void SaveWardNumberDetails(WardNumberVM data);

        void SaveCommitteeDetails(CommitteeVM data);

        void LiquidSaveWardNumberDetails(WardNumberVM data);

        void StreetSaveWardNumberDetails(WardNumberVM data);

        void DeletWardNumberDetails(int teamId);

        HouseDetailsVM GetHouseDetails(int teamId,int PId);

        SWMDetailsVM GetSWMDetails(int teamId,int PId);

        CommercialDetailsVM GetCommercialDetails(int teamId,int PId);
        SBALUserLocationMapView GetHouseByIdforMap(int teamId,int daId,int PId);

        SBALUserLocationMapView GetDSIByIdforMap(int teamId, int daId, int PId);

        SBALUserLocationMapView GetCTPTByIdforMap(int teamId, int daId,int PId);
        SBALUserLocationMapView GetLiquidByIdforMap(int teamId, int daId,string EmpType);
        HouseDetailsVM SaveHouseDetails(HouseDetailsVM data,int PId);

        SWMDetailsVM SaveSWMDetails(SWMDetailsVM data,int PId);

        CommercialDetailsVM SaveCommercialDetails(CommercialDetailsVM data,int PId);
        void DeletHouseDetails(int teamId);

        SBALUserLocationMapView GetLocationDetails(int teamId,string Emptype,int PrabhagId);
        List<SBALUserLocationMapView> GetAllUserLocation(string date,string Emptype,int PId);

        List<SBALUserLocationMapView> GetAdminLocation();
        List<SBALUserLocationMapView> GetUserWiseLocation(int userId,string date, string Emptype);
        List<SBALUserLocationMapView> GetUserAttenLocation(int userId);
        List<SBALUserLocationMapView> GetUserAttenRoute(int userId);

        List<SBALUserLocationMapView> GetDSIUserAttenRoute(int userId);
        List<SBALUserLocationMapView> GetCTPTUserAttenRoute(int userId);

        //Added By Saurabh(11 July 2019)
        List<SBALUserLocationMapView> GetHouseAttenRoute(int userId,int areaid,int PId);

        List<SBALUserLocationMapView> GetDSIAttenRoute(int userId, int areaid, int PId);

        List<SBALUserLocationMapView> GetCTPTAttenRoute(int userId, int areaid,int PId);
        List<SBALUserLocationMapView> GetLiquidAttenRoute(int userId, int areaid,int PId);
        List<SBALUserLocationMapView> GetStreetAttenRoute(int userId, int areaid,int PId);
        HouseAttenRouteVM GetStreetBeatAttenRoute(int daId, int areaid, int polyId, int ZoneId, int PrabhagNo, int WardNo, int PId);
        HouseAttenRouteVM GetLiquidBeatAttenRoute(int daId, int areaid, int polyId, int ZoneId, int PrabhagNo, int WardNo, int PId);

        GarbagePointDetailsVM GetGarbagePointDetails(int teamId,int PId);
        GarbagePointDetailsVM SaveGarbagePointDetails(GarbagePointDetailsVM data,int PId);
        void DeletGarbagePointDetails(int teamId);

        EmployeeDetailsVM GetEmployeeDetails(int teamId,int PId);

      

        SBAAttendenceSettingsGridRow GetAttendenceEmployeeById(int teamId);
        void DeleteEmployeeDetails(int teamId);
        void SaveEmployeeDetails(EmployeeDetailsVM employee,string Emptype);

        void SaveAttendenceSettingsDetail(SBAAttendenceSettingsGridRow atten);
        ComplaintVM GetCompalint(int teamId);
        void SaveComplaintStatus(ComplaintVM employee);

        ZoneVM GetZone(int teamId);

        ZoneVM StreetGetZone(int teamId);
        void SaveZone(ZoneVM employee);

        void StreetSaveZone(ZoneVM employee);
        ZoneVM GetValidZone(string name,int zoneId);

        //Added By Saurabh

        DumpYardDetailsVM GetDumpYardtDetails(int teamId);
        //Added By Shubham
        StreetSweepVM GetStreetSweepDetails(int teamId,int PId);

        LiquidWasteVM GetLiquidWasteDetails(int teamId,int PId);
        DumpYardDetailsVM SaveDumpYardtDetails(DumpYardDetailsVM data,string Emptype);

        StreetSweepVM SaveStreetSweepDetails(StreetSweepVM data,int PId);
        StreetSweepVM SaveStreetBeatDetails(StreetSweepVM data);

        LiquidWasteVM SaveLiquidWasteDetails(LiquidWasteVM data,int PId);
        void DeletDumpYardtDetails(int teamId);

        //Added By Saurabh (27 May 2019)

        List<QrEmployeeMaster> GetUserList(int AppId, int teamId);

        void SaveHSQRStatusHouse(int houseId, string QRStatus);
        void SaveHSQRStatusComr(int comrId, string QRStatus);
        void SaveHSQRStatusCTPT(int CTPTId, string QRStatus);
        void SaveHSQRStatusSWM(int SWMId, string QRStatus);
        void SaveHSQRStatusLW(int LWId, string QRStatus);
        void SaveHSQRStatusSW(int SWId, string QRStatus);

        List<int> GetHSHouseDetailsID(DateTime? fromDate, DateTime? toDate, int userId, string searchString, int QRStatus, string sortColumn, string sortOrder);
        List<int> GetHSComrDetailsID(DateTime? fromDate, DateTime? toDate, int userId, string searchString, int QRStatus, string sortColumn, string sortOrder);
        List<int> GetHSCTPTDetailsID(DateTime? fromDate, DateTime? toDate, int userId, string searchString, int QRStatus, string sortColumn, string sortOrder);
        List<int> GetHSSWMDetailsID(DateTime? fromDate, DateTime? toDate, int userId, string searchString, int QRStatus, string sortColumn, string sortOrder);
        List<int> GetHSLWDetailsID(DateTime? fromDate, DateTime? toDate, int userId, string searchString, int QRStatus, string sortColumn, string sortOrder);
        List<int> GetHSSWDetailsID(DateTime? fromDate, DateTime? toDate, int userId, string searchString, int QRStatus, string sortColumn, string sortOrder);

        SBAHSHouseDetailsGrid GetHouseDetailsById(int houseId);
        SBAHSDumpyardDetailsGrid GetComrDetailsById(int comrId);
        SBAHSDumpyardDetailsGrid GetCTPTDetailsById(int CTPTId);
        SBAHSDumpyardDetailsGrid GetSWMDetailsById(int SWMId);
        SBAHSLiquidDetailsGrid GetLWDetailsById(int LWId);

        SBAHSStreetDetailsGrid GetSWDetailsById(int SWId);

        //Added By Saurabh (03 June 2019)
        HouseScanifyEmployeeDetailsVM GetHSEmployeeDetails(int teamId);

        //Added By Saurabh (04 June 2019)
        void SaveHSEmployeeDetails(HouseScanifyEmployeeDetailsVM employee);

        //Added By Saurabh (04 June 2019)
        HSDashBoardVM GetHSDashBoardDetails();

        List<SBALHSUserLocationMapView> GetHSUserAttenRoute(int qrEmpDaId);

        List<SBAHSHouseDetailsGrid> GetHSQRCodeImageByDate(int type, int UserId, DateTime fDate, DateTime tDate,string QrStatus);

        //Added By Saurabh (06 June 2019)
        List<SBALHouseLocationMapView> GetAllHouseLocation(string date, int userid, int areaid, int wardNo, string SearchString, int? GarbageType, int FilterType, string Emptype, string ctype, int SegType, int PId, int zoneId, int prabhagId);


        List<SBALCommercialLocationMapView> GetAllCommercialLocation(string date, int userid, int areaid, int wardNo, string SearchString, int? GarbageType, int FilterType, string Emptype, string ctype, int SegType,int PId, int zoneId, int prabhagId);
        List<SBALCTPTLocationMapView> GetAllCTPTLocation(string date, int userid, int areaid, int wardNo, string SearchString,  int FilterType, string Emptype, int PId, int zoneId, int prabhagId);

        List<SBALSWMLocationMapView> GetAllSWMLocation(string date, int userid, int areaid, int wardNo, string SearchString, int FilterType, string Emptype, int PId, int zoneId, int prabhagId);
        //Code Optimization (code)
        //SBALHouseLocationMapView1 GetAllHouseLocation(string date, int userid,int areaid, int wardNo, string SearchString, string start);

        //Added By Saurabh (1 June 2019)
        HouseScanifyEmployeeDetailsVM GetUserDetails(int teamId, string Name);

        //Added By Saurabh (2 July 2019)
        DashBoardVM GetHouseOnMapDetails(int PId);
        DashBoardVM GetCommercialOnMapDetails(int PId);
        DashBoardVM GetCTPTOnMapDetails(int PrabhagId);

        DashBoardVM GetSWMOnMapDetails(int PId);
        DashBoardVM GetLiquidWasteDetails(int PId);

        DashBoardVM GetStreetSweepingDetails(int PId);

        //Added By Neha (12 July 2019)
        List<SBAEmplyeeIdelGrid> GetIdleTimeRoute(int userId, string Date,int PId);

        OnePoint4VM GetTotalCountDetails(int ANS_ID);



        OnePoint4VM GetMaxINSERTID();

        void Save1Point4(List<OnePoint4VM> point);

        //OnePoint4VM GetQuetionDetails(int teamId);

        List<OnePoint4VM> GetQuetions(string ReportName);

        void Save1Point5(List<OnePoint5VM> point);

        List<OnePoint5VM> GetQuetions1pointfive();

        void SaveTotalCount(OnePoint4VM onepointfour);

        List<OnePoint4VM> GetOnepointfourEditData(int INSERT_ID);

        void Save1Point7(List<OnePointSevenVM> point);

        List<OnePoint7QuestionVM> GetOnePointSevenQuestions();

        List<OnePoint7QuestionVM> GetOnePointSevenAnswers(int INSERT_ID);

        void EditOnePointSeven(List<OnePoint7QuestionVM> OnePoint7QuestionVM);


        List<SelectListItem> ZoneListPId(int PId);
        List<SelectListItem> WardListPId(int PId);
        List<SelectListItem> PrabhagListPId(int PId);
        List<SelectListItem> AreaLstPId(int PId);
        List<SelectListItem> LoadListWardNoPId(int PId,int PrabhagId);
        List<SelectListItem> LoadPrabhagNoListPId(int PId, int ZoneId);

        List<SelectListItem> LoadAreaListPId(int PId, int WardNo);

        List<SelectListItem> LoadListWardNo(int PrabhagId);
        List<SelectListItem> LoadListPrabhagNo(int ZoneId,int PId);
        List<SelectListItem> LoadListArea(int WardNo, int PId);
        //InfotainmentDetailsVW GetInfotainmentDetailsById(int ID);
        //void SaveGameDetails(InfotainmentDetailsVW data);

        SauchalayDetailsVM GetSauchalayDetails(int teamId,int PId);

        SauchalayDetailsVM SaveSauchalayDetails(SauchalayDetailsVM data,int PId);

        List<SauchalayDetailsVM> GetCTPTLocation();

        #region WasteManagement
        WasteDetailsVM GetWasteDetails(int teamId);

        //List<WasteDetailsVM> SaveWasteDetails(List<WasteDetailsVM> data);
        string SaveWasteDetails(string data);
        string SaveSalesWasteDetails(string data);
        
        List<SelectListItem> LoadListSubCategory(int categoryId);
        List<SelectListItem> GetWMUserDetails();
        #endregion

        List<LogVM> GetLogString();

        List<SBAEmplyeeIdelGrid> GetIdelTimeNotification(int PId);
        List<SBAEmplyeeIdelGrid> GetLiquidIdelTimeNotification(int PId);

        List<SBAEmplyeeIdelGrid> GetStreetIdelTimeNotification(int PId);
        List<SBALUserLocationMapView> GetUserTimeWiseRoute(string date = "", DateTime? fTime = null, DateTime? tTime = null, int? userId = null);

        List<SBALUserLocationMapView> GetHouseTimeWiseRoute(string date = "", DateTime? fTime = null, DateTime? tTime = null, int? userId = null,int? PId = null);

        EmpBeatMapVM GetEmpBeatMap(int ebmId);
        void SaveEmpBeatMap(EmpBeatMapVM data);
        List<SelectListItem> ListUserBeatMap(string Emptype,int PId);
        SBALUserLocationMapView GetHouseByIdforMap(int teamId, int daId);
        HouseAttenRouteVM GetBeatHouseAttenRoute(int daId, int areaid, int polyId, int ZoneId, int PrabhagNo, int WardNo, int PId);
        HouseAttenRouteVM GetBeatCTPTAttenRoute(int daId, int areaid, int polyId, int ZoneId, int PrabhagNo, int WardNo, int PId);

        List<SelectListItem> LoadListArea(int WardNo);
        List<SelectListItem> ListBeatMapArea(int daId, int? areaid);
        List<SBALocationMapView> GetAllHouseLocationForEmpBitMap(string Emptype,int ebmId, int PId);

    }
}
