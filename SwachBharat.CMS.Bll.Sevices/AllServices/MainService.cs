
using GramPanchayat.CMS.Bll.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwachBharat.CMS.Bll.ViewModels.MainModel;
using SwachBharat.CMS.Dal.DataContexts;
using System.Web.Mvc;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;

namespace SwachBharat.CMS.Bll.Services
{
    public class MainService : AppMainService, IMainService
    {


        #region State Service
        public void DeleteStateRecord(int id)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    var stateDetails = db.country_states.Where(x => x.id == id).FirstOrDefault();
                    if (stateDetails != null)
                    {
                        db.country_states.Remove(stateDetails);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AppStateVM GetStateDetailsById(int id)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    var stateDetails = db.country_states.Where(x => x.id == id).FirstOrDefault();
                    if (stateDetails != null)
                    {
                        AppStateVM details = FillStatesViewModel(stateDetails);
                        return details;
                    }
                    else
                    {
                        return new AppStateVM();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveStateDetail(AppStateVM data)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    if (data.stateId > 0)
                    {
                        var model = db.country_states.Where(x => x.id == data.stateId).FirstOrDefault();
                        if (model != null)
                        {
                            model.country_name = data.CountryName;
                            model.state_name = data.stateName;
                            model.state_name_mar = data.stateNameMar;
                            model.id = data.stateId;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var state = FillStateDataModel(data);
                        db.country_states.Add(state);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region District Service

        public AppDistrictVM GetDictrictById(int teamId)
        {
            try
            {
                AppDistrictVM details = new AppDistrictVM();
                details.StateList = ListState();
                using (var db = new DevSwachhBharatMainEntities())
                {
                    var districtDetails = db.state_districts.Where(x => x.id == teamId).FirstOrDefault();
                    if (districtDetails != null)
                    {
                        details = FillDistrictViewModel(districtDetails);
                        details.StateList = ListState();
                        return details;
                    }
                    else
                    {
                        return details;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SaveDictrictDetails(AppDistrictVM data)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    if (data.districtId > 0)
                    {
                        var model = db.state_districts.Where(x => x.id == data.districtId).FirstOrDefault();
                        if (model != null)
                        {
                            model.district_name = data.districtName;
                            model.district_name_mar = data.districtNameMmar;
                            model.state_id = data.stateId;
                            model.id = data.districtId;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var district = FillDistrictDataModel(data);
                        db.state_districts.Add(district);
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteDictrictRecord(int teamId)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    var districtDetails = db.state_districts.Where(x => x.id == teamId).FirstOrDefault();
                    if (districtDetails != null)
                    {
                        db.state_districts.Remove(districtDetails);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Taluka Service
        public AppTalukaVM GetTalukaById(int teamId,string name)
        {
            try
            {
                AppTalukaVM details = new AppTalukaVM();
               
                using (var db = new DevSwachhBharatMainEntities())
                {
                    var talukaDetails = db.tehsils.Where(x => x.id == teamId ||x.name==name).FirstOrDefault();
                    if (talukaDetails != null)
                    {
                        details = FillTalukaViewModel(talukaDetails);
                        details.DistrictList = ListDistrict();
                        details.StateList = ListState();
                        return details;
                    }
                    else
                    {
                        details.DistrictList = ListDistrict();
                        details.StateList = ListState();
                        return details;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SaveTalukaDetails(AppTalukaVM data)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    if (data.talukaId > 0)
                    {
                        var model = db.tehsils.Where(x => x.id == data.talukaId).FirstOrDefault();
                        if (model != null)
                        {
                            model.id = data.talukaId;
                            model.name = data.talukaName;
                            model.name_mar = data.talukaNameMar;
                            model.district = data.districtId;
                            model.state = data.stateId;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var taluka = FillTalukaDataModel(data);
                        db.tehsils.Add(taluka);
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void DeleteTalukaRecord(int teamId)
        {
            try
            {
                using (var db = new DevSwachhBharatMainEntities())
                {
                    var talukaDetails = db.tehsils.Where(x => x.id == teamId).FirstOrDefault();
                    if (talukaDetails != null)
                    {
                        db.tehsils.Remove(talukaDetails);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


        #region Manage User Custom Chagnes
        public AppDetailsVM GetApplicationDetails(int AppId)
        {
            var appDetails = (dbMain.AppDetails.Where(x => x.AppId == AppId).FirstOrDefault());
            if (appDetails != null)
            {
                return FillAppDetailsVMViewModel(appDetails);
            }
            else
            {
                return null;
            }

       }
        public string GetDatabaseFromAppID(int AppId)
        {
            var DB_Name = dbMain.AppConnections.Where(x => x.AppId == AppId).FirstOrDefault().InitialCatalog;
            return DB_Name.ToString();
        }
        public int GetUserAppId(string UserId)
        {
            int AppId = 0;
            AppId = dbMain.UserInApps.Where(x => x.UserId == UserId).Select(x => x.AppId).FirstOrDefault();

            return AppId;
        }

        public int GetUserAppIdL(string UserId)
        {
            int AppId = 0;
            int AppId1 = 0;
            AppId1 = Convert.ToInt32(UserId);
            AppId = dbMain.AD_USER_MST_LIQUID.Where(x => x.APP_ID == AppId1).Select(x => x.APP_ID).FirstOrDefault();

            return AppId;
        }

        public int GetUserAppIdSS(string UserId)
        {
            int AppId = 0;
            int AppId1 = 0;
            AppId1 = Convert.ToInt32(UserId);
            AppId = dbMain.AD_USER_MST_STREET.Where(x => x.APP_ID == AppId1).Select(x => x.APP_ID).FirstOrDefault();

            return AppId;
        }

        public List<AppDetail> GetAppName()
        {
            List<AppDetail> appNames = new List<AppDetail>();
            appNames = dbMain.AppDetails.Where(x => x.IsActive == true && x.AppId==4071 ).OrderBy(x => x.AppName).ToList();
            //appNames = dbMain.AppDetails.ToList();
              //var appNames= dbMain.AppDetails.Where(row => row.)
            return appNames.OrderBy(x =>x.AppName).ToList();
        }


        #endregion

        #region DataModel
        private AppDetail FillAppDetailsDataModel(AppDetailsVM data)
        {
            AppDetail model = new AppDetail();
            model.AboutAppynity = data.AboutAppynity;
            model.AboutTeamDetail = data.AboutTeamDetail;
            model.AboutThumbnailURL = data.AboutThumbnailURL;
            model.Android_GCM_pushNotification_Key = data.Android_GCM_pushNotification_Key;
            model.AppId = data.AppId;
            model.baseImageUrl = data.baseImageUrl;
            model.baseImageUrlCMS = data.baseImageUrlCMS;
            model.ContactUsTeamMember = data.ContactUsTeamMember;
            model.EmailId = data.EmailId;
            model.website = data.website;
            model.basePath = data.basePath;
            model.Type = data.Type;
            model.Logo = data.Logo;
            model.HousePDF = data.HousePDF;
            model.HouseQRCode = data.HouseQRCode;
            model.PointPDF = data.PointPDF;
            model.PointQRCode = data.PointQRCode;
            return model;
        }
        private country_states FillStateDataModel(AppStateVM data)
        {
            country_states model = new country_states();
            model.country_name = data.CountryName;
            model.state_name = data.stateName;
            model.state_name_mar = data.stateNameMar;
            model.id = data.stateId;

            return model;
        }
        private state_districts FillDistrictDataModel(AppDistrictVM data)
        {
            state_districts model = new state_districts();
            model.district_name = data.districtName;
            model.district_name_mar = data.districtNameMmar;
            model.state_id = data.stateId;
            model.id = data.districtId;
            return model;
        }

        private tehsil FillTalukaDataModel(AppTalukaVM data)
        {
            tehsil model = new tehsil();
            model.id = data.talukaId;
            model.name = data.talukaName;
            model.name_mar = data.talukaNameMar;
            model.district = data.districtId;
            model.state = data.stateId;
            return model;
        }

        #endregion

        #region ViewModel 
        private AppDetailsVM FillAppDetailsVMViewModel(AppDetail data)
        {
          
                AppDetailsVM model = new AppDetailsVM();
                model.AboutAppynity = data.AboutAppynity;
                model.AboutTeamDetail = data.AboutTeamDetail;
                model.AboutThumbnailURL = data.AboutThumbnailURL;
                model.Android_GCM_pushNotification_Key = data.Android_GCM_pushNotification_Key;
                model.AppId = data.AppId;
                model.AppName = data.AppName;
                model.AppName_mar = data.AppName_mar;
                model.baseImageUrl = data.baseImageUrl;
                model.baseImageUrlCMS = data.baseImageUrlCMS;
                model.ContactUsTeamMember = data.ContactUsTeamMember;
                model.EmailId = data.EmailId;
                model.ContactUs = data.ContactUs;
                model.website = data.website;
                model.basePath = data.basePath;
                model.Type = data.Type;
                model.Logo = data.Logo;
                model.Logitude = data.Logitude;
                model.Latitude = data.Latitude;
                model.Collection = data.Collection;
                model.UserProfile = data.UserProfile;
                model.HousePDF = data.HousePDF;
                model.HouseQRCode = data.HouseQRCode;
                model.PointPDF = data.PointPDF;
                model.PointQRCode = data.PointQRCode;
                model.Grampanchayat_Pro = data.Grampanchayat_Pro;
                model.DumpYardQRCode = data.DumpYardQRCode;
                model.DumpYardPDF = data.DumpYardPDF;
                model.ReportEnable = data.ReportEnable;
                model.YoccClientID = data.YoccClientID;
                model.yoccContact = data.yoccContact;
                model.GramPanchyatAppID = data.GramPanchyatAppID;
                model.YoccFeddbackLink = data.YoccFeddbackLink;
                model.YoccDndLink = data.YoccDndLink;
                model.LiquidQRCode = data.LiquidQRCode;
                model.StreetQRCode = data.StreetQRCode;
                //model.Apikey = data.Apikey;
               return model;
        
        }
        private AppStateVM FillStatesViewModel(country_states data)
        {
            AppStateVM model = new AppStateVM();
            model.CountryName = data.country_name;
            model.stateName = data.state_name;
            model.stateNameMar = data.state_name_mar;
            model.stateId = data.id;
            return model;
        }
        private AppDistrictVM FillDistrictViewModel(state_districts data)
        {
            AppDistrictVM model = new AppDistrictVM();
            model.districtName = data.district_name;
            model.districtNameMmar = data.district_name_mar;
            model.districtId = data.id;
            model.stateId = data.state_id; 
            return model;
        }

        private AppTalukaVM FillTalukaViewModel(tehsil data)
        {
            AppTalukaVM model = new AppTalukaVM();
            model.talukaId = data.id;
            model.talukaName = data.name;
            model.talukaNameMar = data.name_mar;
            model.districtId = data.district;
            model.stateId = data.state;
            return model;
        }
        #endregion
        //*****************************

        #region List

        //public List<SelectListItem> ListLanguage()
        //{
        //    var lstLanguage = new List<SelectListItem>();
        //    SelectListItem itemAdd = new SelectListItem() { Text = "Select Language", Value = "-1" };

        //    try
        //    {
        //        lstLanguage = db.LanguageInfoes.AsNoTracking()
        //            .Select(x => new SelectListItem
        //            {
        //                Text = x.languageType,
        //                Value = x.id.ToString()
        //            }).OrderBy(t => t.Text).ToList();

        //        lstLanguage.Insert(0, itemAdd);
        //    }
        //    catch (Exception ex) { throw ex; }

        //    return lstLanguage;
        //}
        public List<SelectListItem> ListState()
        {
            var State = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select State--", Value = "0" };

            try
            {
                State = dbMain.country_states.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.state_name + '(' + x.state_name_mar + ')',
                        Value = x.id.ToString()
                    }).OrderBy(t => t.Text).ToList();

                State.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }

            return State;
        }
        public List<SelectListItem> ListDistrict()
        {
            var District = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select District--", Value = "0" };

            try
            {
                District = dbMain.state_districts.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.district_name + '(' + x.district_name_mar + ')',
                        Value = x.id.ToString()
                    }).OrderBy(t => t.Text).ToList();

                District.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }

            return District;
        }
        public List<SelectListItem> ListTaluka()
        {
            var Taluka = new List<SelectListItem>();
            SelectListItem itemAdd = new SelectListItem() { Text = "--Select Taluka--", Value = "0" };

            try
            {
                Taluka = dbMain.tehsils.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.name + '(' + x.name_mar + ')',
                        Value = x.id.ToString()
                    }).OrderBy(t => t.Text).ToList();

                Taluka.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }

            return Taluka;
        }
        #endregion

        //*****************************
        //public int GetAppIdForApp(string appName)
        //{
        //    int appId = 0;
        //    using (var db = new DevGramPanchayatAppyMainEntities())
        //    {
        //        appId = db.AppDetails.Where(x => x.AppName == appName).Select(z => z.AppId).FirstOrDefault();
        //    }
        //    return appId;
        //}
        //public void SaveApplicationDetails(AppDetailsVM appDetailsVM)
        //{
        //    AppDetail data = FillAppDetailsDataModel(appDetailsVM);
        //    using (var db = new DevGramPanchayatAppyMainEntities())
        //    {
        //        db.AppDetails.Add(data);
        //        db.SaveChanges();
        //        //Save AppConnection From Old Information
        //        AppConnection appConnection = db.AppConnections.Where(x => x.AppId == 1).FirstOrDefault();
        //        appConnection.AppId = db.AppDetails.Max(z => z.AppId);
        //        appConnection.InitialCatalog = "Restro" + appDetailsVM.AppName;
        //        db.AppConnections.Add(appConnection);
        //        db.SaveChanges();
        //    }

        //}
        //public IEnumerable<SubscriptionVM> GetSubscriptionId()
        //{
        //    IEnumerable<SubscriptionVM> SubscriptionIds;
        //    using (var context = new DevGramPanchayatAppyMainEntities())
        //    {
        //        SubscriptionIds = context.Subscriptions.Select(x => new SubscriptionVM { name = x.subscriptionName, id = x.subscriptionId }).ToList();
        //    }
        //    return SubscriptionIds;
        //}

        //public IEnumerable<VMApplication> GetAppId()
        //{
        //    IEnumerable<VMApplication> AppIds;
        //    using (var context = new DevGramPanchayatAppyMainEntities())
        //    {
        //        AppIds = context.AppDetails.Select(x => new VMApplication { name = x.AppName, id = x.AppId }).ToList();
        //    }
        //    return AppIds;
        //}
        //public bool AddApptoUser(string UserId, int AppId, int SubscriptionId)
        //{
        //    try
        //    {
        //        using (var context = new DevGramPanchayatAppyMainEntities())
        //        {
        //            UserInApp userInApp = context.UserInApps.Where(x => x.UserId == UserId).FirstOrDefault();
        //            if (userInApp != null)
        //            {
        //                userInApp.AppId = AppId;
        //                userInApp.subscriptionId = SubscriptionId;
        //                context.UserInApps.Attach(userInApp);
        //                context.Entry(userInApp).State = EntityState.Modified;
        //            }
        //            else
        //            {
        //                userInApp = new UserInApp();
        //                userInApp.AppId = AppId;
        //                userInApp.subscriptionId = SubscriptionId;
        //                userInApp.UserId = UserId;
        //                context.UserInApps.Add(userInApp);
        //            }
        //            context.SaveChanges();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}


        #region Game
        public List<GameMaster> GetGameList()
        {
            List<GameMaster> gameList = new List<GameMaster>();
            gameList = dbMain.GameMasters.ToList();
            //appNames = dbMain.AppDetails.ToList();
            //var appNames= dbMain.AppDetails.Where(row => row.)
            return gameList;
        }

        public InfotainmentDetailsVW GetInfotainmentDetailsById(int ID)
        {
            try
            {
                InfotainmentDetailsVW type = new InfotainmentDetailsVW();
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();

                //using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                //{
                var Details = dbMain.GameDetails.Where(x => x.GameDetailsID == ID).FirstOrDefault();
                if (Details != null)
                {
                    type = FillInfotainmentViewModel(Details);
                    if (type.Image != null && type.Image != "")
                    {
                        type.Image = type.Image.Trim(); //ThumbnaiUrlCMS + type.Image.Trim();
                    }
                    else
                    {
                        type.Image = "/Images/default_not_upload.png";
                    }
                    return type;
                }
                else
                {
                    type.GameMasterList = LoadGameList();
                    type.SloganList = LoadSloganList();
                    type.AnswerTypeList = LoadAnswerTypeList();
                    type.Image = "/Images/add_image_square.png";
                    return type;
                }
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        //private GameDetail FillGameDetailsDataModel(InfotainmentDetailsVW data)
        //{
        //    GameDetail model = new GameDetail();
        //    model.GameDetailsID = data.GameDetailsId;
        //    model.ImageUrl = data.Image;
        //    model.RightAnswerID = data.AnswerTypeId;
        //    model.Point = data.Points;
        //    model.GameMasterID = data.GameMasterId;
        //    model.SloganID = data.SloganId;
        //    model.Description = data.Description;
        //    model.Created = DateTime.Now;
        //    return model;
        //}

        private InfotainmentDetailsVW FillInfotainmentViewModel(GameDetail data)
        {
            InfotainmentDetailsVW model = new InfotainmentDetailsVW();
            model.GameDetailsId = data.GameDetailsID;
            model.Image = data.ImageUrl;
            model.AnswerTypeId = Convert.ToInt32(data.RightAnswerID);
            model.Points = Convert.ToInt32(data.Point);
            model.GameMasterId = Convert.ToInt32(data.GameMasterID);
            model.SloganId = Convert.ToInt32(data.SloganID);
            model.Description = data.Description;
            model.GameMasterList = LoadGameList();
            model.SloganList = LoadSloganList();
            model.AnswerTypeList = LoadAnswerTypeList();
            return model;
        }

        public List<SelectListItem> LoadGameList()
        {
            var GameList = new List<SelectListItem>();
            try
            {
                SelectListItem itemAdd = new SelectListItem() { Text = "--Select Game--", Value = "0" };
                GameList = dbMain.GameMasters.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.GameName,
                        Value = x.GameId.ToString()
                    }).OrderBy(t => t.Text).ToList();
                GameList.Insert(0, itemAdd);

            }
            catch (Exception ex) { throw ex; }
            return GameList;
        }

        public List<SelectListItem> LoadSloganList()
        {
            var SloganList = new List<SelectListItem>();
            try
            {
                SelectListItem itemAdd = new SelectListItem() { Text = "--Select Slogan--", Value = "0" };
                SloganList = dbMain.Game_Slogan.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.Slogan,
                        Value = x.ID.ToString()
                    }).OrderBy(t => t.Text).ToList();
                SloganList.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }
            return SloganList;
        }

        public List<SelectListItem> LoadAnswerTypeList()
        {
            var SloganList = new List<SelectListItem>();
            try
            {
                SelectListItem itemAdd = new SelectListItem() { Text = "--Select Type--", Value = "0" };
                SloganList = dbMain.Game_AnswerType.ToList()
                    .Select(x => new SelectListItem
                    {
                        Text = x.AnswerType,
                        Value = x.AnswerTypeId.ToString()
                    }).OrderBy(t => t.Text).ToList();
                SloganList.Insert(0, itemAdd);
            }
            catch (Exception ex) { throw ex; }
            return SloganList;
        }

        public void SaveGameDetails(InfotainmentDetailsVW data)
        {
            try
            {
                //using (var db = new DevChildSwachhBharatNagpurEntities(AppID))
                //{
                if (data.GameDetailsId > 0)
                {
                    var model = dbMain.GameDetails.Where(x => x.GameDetailsID == data.GameDetailsId).FirstOrDefault();
                    if (model != null)
                    {
                        if (data.Image != null)
                        {
                            model.ImageUrl = data.Image;
                        }
                        model.RightAnswerID = data.AnswerTypeId;
                        model.Point = data.Points;
                        model.GameMasterID = data.GameMasterId;
                        model.SloganID = data.SloganId;
                        model.Description = data.Description;
                        model.Created = DateTime.Now;
                        dbMain.SaveChanges();
                    }
                }
                else
                {
                    var type = FillGameDetailsDataModel(data);
                    dbMain.GameDetails.Add(type);
                    dbMain.SaveChanges();
                }
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }

        private GameDetail FillGameDetailsDataModel(InfotainmentDetailsVW data)
        {
            GameDetail model = new GameDetail();
            model.GameDetailsID = data.GameDetailsId;
            model.ImageUrl = data.Image;
            model.RightAnswerID = data.AnswerTypeId;
            model.Point = data.Points;
            model.GameMasterID = data.GameMasterId;
            model.SloganID = data.SloganId;
            model.Description = data.Description;
            model.Created = DateTime.Now;
            return model;
        }


        #endregion
    }
}
