using SwachBharat.CMS.Bll.ViewModels;
using SwachBharat.CMS.Bll.ViewModels.MainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwachBharat.CMS.Dal.DataContexts;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;


namespace SwachBharat.CMS.Bll.Repository.MainRepository
{
    public interface IMainRepository
    {
        #region State
        AppStateVM GetStateById(int teamId);
        void SaveState(AppStateVM state);
        void DeleteState(int teamId);
        #endregion


        #region District
        AppDistrictVM GetDistrictById(int teamId);
        void SaveDistrict(AppDistrictVM state);
        void DeleteDistrict(int teamId);
        #endregion


        #region Taluka
        AppTalukaVM GetTalukaById(int teamId,string name);
        void SaveTaluka(AppTalukaVM state);
        void DeleteTaluka(int teamId);
        #endregion

        //int GetAppIdForApp(string appName);
        //void SaveApplicationDetails(AppDetailsVM appDetailsVM);
        //IEnumerable<SubscriptionVM> GetSubscriptionId();
        //IEnumerable<VMApplication> GetAppId();
        //bool AddApptoUser(string UserId, int AppId, int SubscriptionId);
        AppDetailsVM GetApplicationDetails(int AppId);
        int GetUserAppId(string UserId);

        int GetUserAppIdL(string UserId);

        int GetUserAppIdSS(string UserId);
        string GetDatabaseFromAppID(int AppId);

       // Added By Saurabh ( 27 May 2019)
        List<AppDetail> GetAppName();

        EmployeeVM Login(EmployeeVM _userinfo);

        EmployeeVM LoginStreet(EmployeeVM _userinfo);

        #region Game
        List<GameMaster> GetGameList();
        InfotainmentDetailsVW GetInfotainmentDetailsById(int ID);
        void SaveGameDetails(InfotainmentDetailsVW data);

        #endregion
    }
}
