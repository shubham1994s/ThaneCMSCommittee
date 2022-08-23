using SwachBharat.CMS.Bll.ViewModels;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Bll.ViewModels.MainModel;
using SwachBharat.CMS.Bll.ViewModels.SS2020Reports;
using SwachBharat.CMS.Dal.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SwachBharat.CMS.Bll.Repository.ChildRepository
{
    public interface IChildRepository
    {
     
        DashBoardVM GetDashBoardDetails(int PrabhagId);

        DashBoardVM GetLiquidDashBoardDetails(int PrabhagId);

        DashBoardVM GetStreetDashBoardDetails(int PrabhagId);
        string Address(string location);
        AreaVM GetArea(int teamId,string name,int PId);
        void DeletArea(int teamId);
        void SaveArea(AreaVM area);

        void LiquidSaveArea(AreaVM area);

        void StreetSaveArea(AreaVM area);


        VehicleTypeVM GetVehicleType(int teamId);

        VehicleRegVM GetVehicleReg(int teamId,int PId);

        StreetSweepVM GetBeat(int teamId, int ? PId);


        void DeletVehicleType(int teamId);
        void SaveVehicleType(VehicleTypeVM type);
        void SaveVehicleReg(VehicleRegVM type);



        WardNumberVM GetWardNumber(int teamId,string name,int PId);

        CommitteeVM GetCommitteeName(int teamId, string name,int PId);

        CommitteeVM GetComitteeName(int teamId, string name,int PId);
        void DeleteWardNumber(int teamId);
        void SaveWardNumber(WardNumberVM type);

        void SaveCommittee(CommitteeVM type);

        void LiquidSaveWardNumber(WardNumberVM type);

        void StreetSaveWardNumber(WardNumberVM type);


        HouseDetailsVM GetHouseById(int teamId,int PId);

        SWMDetailsVM GetSWMById(int teamId,int PId);

        CommercialDetailsVM GetCommercailById(int teamId,int PId);

        SBALUserLocationMapView GetHouseByIdforMap(int teamId,int daId,int PId);

        SBALUserLocationMapView GetCTPTByIdforMap(int teamId, int daId,int PId);

        SBALUserLocationMapView GetLiquidByIdforMap(int teamId, int daId,string EmpType);
        HouseDetailsVM SaveHouse(HouseDetailsVM data,int PId);

        SWMDetailsVM SaveSWM(SWMDetailsVM data,int PId);

        CommercialDetailsVM SaveHouse(CommercialDetailsVM data,int PId);
        void DeletHouse(int teamId);


        SBALUserLocationMapView GetLocation(int teamId,string Emptype,int PrabhagId);
        List<SBALUserLocationMapView> GetAllUserLocation(string date,string Emptype,int PId);
        // for admin
        List<SBALUserLocationMapView> GetAdminLocation();
        List<SBALUserLocationMapView> GetUserWiseLocation(int id,string date,string Emptype);
        List<SBALUserLocationMapView> GetUserAttenLocation(int id);
        List<SBALUserLocationMapView> GetUserAttenRoute(int id);

        List<SBALUserLocationMapView> GetCTPTUserAttenRoute(int id);

        //Added By Saurabh (11 July 2019)
        List<SBALUserLocationMapView> GetHouseAttenRoute(int id,int areaid,int PId);

        List<SBALUserLocationMapView> GetCTPTAttenRoute(int id, int areaid,int PId);
        List<SBALUserLocationMapView> GetLiquidAttenRoute(int id, int areaid,int PId);
        List<SBALUserLocationMapView> GetStreetAttenRoute(int id, int areaid,int PId);
        HouseAttenRouteVM GetStreetBeatAttenRoute(int daId, int areaid, int polyId, int ZoneId, int PrabhagNo, int WardNo, int PId);

        GarbagePointDetailsVM GetGarbagePointById(int teamId,int PId);
        GarbagePointDetailsVM SaveGarbagePoint(GarbagePointDetailsVM data,int PId);
        void DeletGarbagePoint(int teamId);

        EmployeeDetailsVM GetEmployeeById(int teamId,int PId);

        //EmployeeDetailsVM GetLiquidEmployeeById(int teamId);

        SBAAttendenceSettingsGridRow GetAttendenceEmployeeById(int teamId);
        void DeleteEmployee(int teamId);
        void SaveEmployee(EmployeeDetailsVM employee,string Emptype);

        void SaveAttendenceSettingsDetail(SBAAttendenceSettingsGridRow atten);

        ComplaintVM GetComplaint(int teamId);
        void SaveComplaintStatus(ComplaintVM comp);

        ZoneVM GetZone(int teamId);

        ZoneVM StreetGetZone(int teamId);

        void SaveZone(ZoneVM type);

        void StreetSaveZone(ZoneVM type);
        ZoneVM GetValidZone(string name,int zoneId);

        //Added By Saurabh

        DumpYardDetailsVM GetDumpYardById(int teamId);

        StreetSweepVM GetStreetSweepId(int teamId,int PId);

        LiquidWasteVM GetLiquidWasteId(int teamId,int PId);

        DumpYardDetailsVM SaveDumpYard(DumpYardDetailsVM data,string Emptype);

     

        StreetSweepVM SaveStreetSweep(StreetSweepVM data,int PId);
        StreetSweepVM SaveStreetBeat(StreetSweepVM data);

        LiquidWasteVM SaveLiquidWastes(LiquidWasteVM data,int PId);

        #region HouseScanify
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

        // Added By Saurabh (27 May 2019)
        List<QrEmployeeMaster> GetUserList(int AppId, int teamId);

        //Added By Saurabh (03 June 2019)

        HouseScanifyEmployeeDetailsVM GetHSEmployeeById(int teamId);

        //Added By saurabh (04 June 2019)
        void SaveHSEmployee(HouseScanifyEmployeeDetailsVM employee);

        //Added By saurabh (04 June 2019)
        HSDashBoardVM GetHSDashBoardDetails();

        List<SBALHSUserLocationMapView> GetHSUserAttenRoute(int qrEmpDaId);
        #endregion

        List<SBAHSHouseDetailsGrid> GetHSQRCodeImageByDate(int type, int UserId, DateTime fDate, DateTime tDate, string QrStatus);

        //Added By saurabh (04 June 2019)

        List<SBALHouseLocationMapView> GetAllHouseLocation(string date, int userid, int areaid, int wardNo, string SearchString, int? GarbageType, int FilterType,string Emptype,string ctype,int SegType,int PId,int zoneId,int prabhagId);

        List<SBALCommercialLocationMapView> GetAllCommercialLocation(string date, int userid, int areaid, int wardNo, string SearchString, int? GarbageType, int FilterType, string Emptype, string ctype, int SegType,int PId, int zoneId, int prabhagId);
        List<SBALCTPTLocationMapView> GetAllCTPTLocation(string date, int userid, int areaid, int wardNo, string SearchString,  int FilterType, string Emptype, int PId, int zoneId, int prabhagId);

        List<SBALSWMLocationMapView> GetAllSWMLocation(string date, int userid, int areaid, int wardNo, string SearchString, int FilterType, string Emptype, int PId, int zoneId, int prabhagId);
        //Code Optimization (code)
        //SBALHouseLocationMapView1 GetAllHouseLocation(string date, int userid,int areaid,int wardNo, string SearchString, string start);


        //Added By saurabh ( June 2019)
        HouseScanifyEmployeeDetailsVM GetUser(int teamId, string name);

        //Added By saurabh ( 02 July 2019)
        DashBoardVM GetHouseOnMapDetails(int PId);
        DashBoardVM GetCommercialOnMapDetails(int PId);
        DashBoardVM GetCTPTOnMapDetails(int PrabhagId);

        DashBoardVM GetSWMOnMapDetails(int PId);

        DashBoardVM GetLiquidWasteDetails(int PId);

        DashBoardVM GetStreetSweepingDetails(int PId);
        //Added By Neha ( 12 July 2019)
        List<SBAEmplyeeIdelGrid> GetIdleTimeRoute(int userId, string Date,int PId);
        OnePoint4VM GetOnePointFourTotalCount(int ANS_ID);

        OnePoint4VM GetMaxINSERTID();

        List<SelectListItem> ZoneListPId(int PId);
        List<SelectListItem> WardListPId(int PId);
        List<SelectListItem> PrabhagListPId(int PId);
        List<SelectListItem> AreaLstPId(int PId);
        List<SelectListItem> LoadListWardNoPId(int PId,int PrabhagId);
        List<SelectListItem> LoadPrabhagNoListPId(int PId, int ZoneId);

        List<SelectListItem> LoadAreaListPId(int PId,int WardNo);

        void Save1Point4(List<OnePoint4VM> point);

        List<OnePoint4VM> GetQuetions(string ReportName);

        void Save1Point5(List<OnePoint5VM> point);

        List<OnePoint5VM> GetQuetions1pointfive();

        List<OnePoint4VM> GetOnepointfourEditData(int INSERT_ID);

        void SaveTotalCount(OnePoint4VM onepointfour);

        void Save1Point7(List<OnePointSevenVM> point);

        List<OnePoint7QuestionVM> GetOnePointSevenAnswers(int INSERT_ID);

        List<OnePoint7QuestionVM> GetOnePointSevenQuestions();

        void EditOnePointSeven(List<OnePoint7QuestionVM> OnePoint7);

        List<SelectListItem> LoadListWardNo(int PrabhagId);
        List<SelectListItem> LoadListPrabhagNo(int ZoneId,int PId);

        List<SelectListItem> LoadListArea(int WardNo, int PId);

        //InfotainmentDetailsVW GetInfotainmentDetailsById(int ID);
        //void SaveGameDetails(InfotainmentDetailsVW data);

        SauchalayDetailsVM GetSauchalayById(int teamId,int PId);

        SauchalayDetailsVM SaveSauchalay(SauchalayDetailsVM data,int PId);

        List<SauchalayDetailsVM> GetCTPTLocation();

        #region WasteManagement

        WasteDetailsVM GetWasteById(int teamId);

        //List<WasteDetailsVM> SaveWaste(List<WasteDetailsVM> data);

        string SaveWaste(string data);

        string SaveSalesWaste(string data);
        List<SelectListItem> LoadListSubCategory(int categoryId);
        List<SelectListItem> GetWMUser();
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
        HouseAttenRouteVM GetBeatHouseAttenRoute(int daId, int areaid, int polyId, int ZoneId,int PrabhagNo,int WardNo,int PId);
        List<SelectListItem> LoadListArea(int WardNo);
        List<SelectListItem> ListBeatMapArea(int daId, int? areaid);

    }
}
