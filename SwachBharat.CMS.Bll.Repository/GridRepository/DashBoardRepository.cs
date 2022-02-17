
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Bll.ViewModels.Grid;
using SwachBharat.CMS.Dal.DataContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data.SqlClient;
using System.Globalization;

namespace SwachBharat.CMS.Bll.Repository.GridRepository
{
    public class DashBoardRepository
    {

        #region CommonMethod
        public string Address(string location)
        {
            try
            {
                if (location != string.Empty && location != null)
                {
                    string lat = null, log = null;
                    string[] arr = new string[2];
                    arr = location.Split(',');
                    lat = arr[0];
                    log = arr[1];
                    XmlDocument doc = new XmlDocument();
                    string Address = "";

                    //doc.Load("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lat + "," + log + "& sensor=false");
                    doc.Load("https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lat + "," + log + "&sensor=false&key=AIzaSyBy6BUqH6o1r7JBS8s1Tk7cmllapL6xuMA");

                    XmlNode element = doc.SelectSingleNode("//GeocodeResponse/status");
                    if (element.InnerText == "ZERO_RESULTS")
                    {
                        Console.WriteLine("No data available for the specified location");
                    }
                    else
                    {
                        XmlNode xnList1 = doc.SelectSingleNode("//GeocodeResponse/result/formatted_address");

                        if (xnList1 != null)
                        {
                            Address = xnList1.InnerText;
                        }
                        else
                        {
                            Address = "";
                        }
                    }
                    return Address;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";

            }


        }
        public int checkIntNull(string str)
        {
            int result = 0;
            if (str == null || str == "")
            {
                result = 0;
                return result;
            }
            else
            {
                result = Convert.ToInt32(str);
                return result;
            }
        }
        public string checkNull(string str)
        {
            string result = "";
            if (str == null || str == "")
            {
                result = "";
                return result;
            }
            else
            {
                result = str;
                return result;
            }
        }
        public string ImagePath(string FolderName, string Image, AppDetail objmain)
        {
            string ImageUrl;
            if (Image == null || Image == "")
            {
                ImageUrl = "";
                return ImageUrl;
            }
            else
            {
                var AppDetailURL = objmain.baseImageUrl + objmain.basePath + FolderName + "/";
                ImageUrl = AppDetailURL + Image;
                return ImageUrl;
            }
        }
        public string ImagePathCMS(string FolderName, string Image, AppDetail objmain)
        {
            string ImageUrl;
            if (Image == null || Image == "")
            {
                ImageUrl = "";
                return ImageUrl;
            }
            else
            {
                var AppDetailURL = objmain.baseImageUrlCMS + objmain.basePath + FolderName + "/";
                ImageUrl = AppDetailURL + Image;
                return ImageUrl;
            }
        }
        #endregion

        public IEnumerable<SBAStateGridRow> GetStateData(long wildcard, string SearchString, int appId)
        {
            using (var dbMain = new DevSwachhBharatMainEntities())
            {
                var data = dbMain.country_states.Select(x => new SBAStateGridRow
                {
                    Id = x.id,
                    Name = x.state_name,
                    NameMar = x.state_name_mar,
                }).ToList();
                //  var result = data.SkipWhile(element => element.cId != element.reNewId); 
                foreach (var item in data)
                {

                    item.NameMar = checkNull(item.NameMar);
                    item.Name = checkNull(item.Name);
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.Name.ToUpper().ToString().Contains(SearchString) || c.NameMar.ToString().ToUpper().ToString().Contains(SearchString) ||
                     c.NameMar.ToString().ToLower().Contains(SearchString) || c.Name.ToString().ToLower().ToString().Contains(SearchString) ||
                     c.Name.ToString().Contains(SearchString) || c.NameMar.ToString().ToString().Contains(SearchString)).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.Id);
            }
        }
        public IEnumerable<OnePointfourGridRow> GetOnepointeightData(long wildcard, string SearchString, int appId)
        {
            using (var dbMain = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = dbMain.RPT_ONE_POINT_Eight_SHOW().
                    Select(x => new OnePointfourGridRow
                    {
                        INSERT_DATE = x.insert_date.Value,
                        INSERT_ID = x.insert_id.Value
                    }).ToList();

                return data;
            }
        }
        public IEnumerable<OnePointfourGridRow> GetOnepointsevenData(long wildcard, string SearchString, int appId)
        {
            using (var dbMain = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = dbMain.RPT_ONE_POINT_SEVEN_SHOW().
                    Select(x => new OnePointfourGridRow
                    {
                        INSERT_DATE = x.insert_date.Value,
                        INSERT_ID = x.insert_id.Value
                    }).ToList();

                return data;
            }
        }
        public IEnumerable<SBADistrictGridRow> GetDistrictData(long wildcard, string SearchString, int appId)
        {
            using (var dbMain = new DevSwachhBharatMainEntities())
            {

                var data = dbMain.state_districts.Select(x => new SBADistrictGridRow
                {
                    Id = x.id,
                    Name = x.district_name,
                    NameMar = x.district_name_mar,
                    StateId = x.state_id
                }).ToList();

                foreach (var item in data)
                {
                    item.NameMar = checkNull(item.NameMar);
                    item.Name = checkNull(item.Name);

                    if (item.StateId > 0)
                    {
                        item.State = dbMain.country_states.Where(x => x.id == item.StateId).Select(x => x.state_name).FirstOrDefault();
                    }
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.Name.ToUpper().ToString().Contains(SearchString) || c.NameMar.ToUpper().ToString().Contains(SearchString) || c.State.ToUpper().ToString().Contains(SearchString) ||

                    c.NameMar.ToString().ToLower().Contains(SearchString) || c.Name.ToString().ToLower().ToString().Contains(SearchString) || c.State.ToUpper().ToString().Contains(SearchString) ||

                    c.Name.ToString().Contains(SearchString) || c.NameMar.ToString().Contains(SearchString) || c.State.ToUpper().ToString().Contains(SearchString)).ToList();
                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.Id);
            }
        }
        public IEnumerable<SBATalukaGridRow> GetTalukaData(long wildcard, string SearchString, int appId)
        {
            using (var dbMain = new DevSwachhBharatMainEntities())
            {
                var data = dbMain.tehsils.Select(x => new SBATalukaGridRow
                {
                    Id = x.id,
                    Name = x.name,
                    NameMar = x.name_mar,
                    StateId = x.state,
                    DistrictId = x.district
                }).ToList();

                foreach (var item in data)
                {
                    if (item.NameMar == "" && item.NameMar == null)
                        item.NameMar = "";

                    if (item.Name == null && item.Name == "")
                        item.Name = "";
                    if (item.StateId > 0)
                    {
                        item.State = dbMain.country_states.Where(x => x.id == item.StateId).Select(x => x.state_name).FirstOrDefault();
                    }
                    if (item.DistrictId > 0)
                    {
                        item.District = dbMain.state_districts.Where(x => x.id == item.DistrictId).Select(x => x.district_name).FirstOrDefault();
                    }
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    //var model = data.Where(c => c.Name.ToUpper().ToString().Contains(SearchString) || c.NameMar.ToString().ToUpper().ToString().Contains(SearchString) || c.NameMar.ToString().ToLower().ToString().Contains(SearchString) ||

                    // c.NameMar.ToString().ToLower().Contains(SearchString) || c.Name.ToString().ToLower().ToString().Contains(SearchString) ||

                    // c.Name.ToString().Contains(SearchString) || c.NameMar.ToString().ToString().Contains(SearchString)).ToList();

                    var model = data.Where(c => ((c.Name == null ? " " : c.Name) + " " + (c.NameMar == null ? " " : c.NameMar) + " " + (c.District == null ? " " : c.District) + " " + (c.State == null ? " " : c.State)).ToUpper().Contains(SearchString.ToUpper())
                      ).ToList();


                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.Id);
            }
        }

        public IEnumerable<SBAAreaGridRow> GetAreaData(long wildcard, string SearchString, int appId)
        {
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = db.TeritoryMasters.Select(x => new SBAAreaGridRow
                {
                    Id = x.Id,
                    Name = x.Area,
                    NameMar = x.AreaMar,
                    ward = db.WardNumbers.Where(v => v.Id == x.wardId).FirstOrDefault().WardNo
                }).ToList();
                //  var result = data.SkipWhile(element => element.cId != element.reNewId); 
                foreach (var item in data)
                {
                    item.Name = checkNull(item.Name);
                    item.NameMar = checkNull(item.NameMar);
                    item.ward = checkNull(item.ward);
                    int wa = Convert.ToInt32(db.WardNumbers.Where(c => c.WardNo == item.ward).FirstOrDefault().zoneId);
                    string zone = db.ZoneMasters.Where(c => c.zoneId == wa).FirstOrDefault().name;
                    item.ward = item.ward + " (" + zone + ")";

                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.Name.ToUpper().ToString().Contains(SearchString) || c.NameMar.ToString().ToUpper().ToString().Contains(SearchString) ||
                     c.NameMar.ToString().ToLower().Contains(SearchString) || c.Name.ToString().ToLower().ToString().Contains(SearchString) ||
                     c.Name.ToString().Contains(SearchString) || c.NameMar.ToString().ToString().Contains(SearchString) || c.ward.ToString().Contains(SearchString)).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.Id);
            }
        }
        public IEnumerable<SBAZoneGridRow> GetZoneData(long wildcard, string SearchString, int appId)
        {
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = db.ZoneMasters.Select(x => new SBAZoneGridRow
                {
                    Id = x.zoneId,
                    Name = x.name,
                }).ToList();
                //  var result = data.SkipWhile(element => element.cId != element.reNewId); 
                foreach (var item in data)
                {

                    if (item.Name == null && item.Name == "")
                        item.Name = "";
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.Name.ToUpper().ToString().Contains(SearchString) || c.Name.ToString().ToLower().ToString().Contains(SearchString) ||
                     c.Name.ToString().Contains(SearchString)).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.Id);
            }
        }

        public IEnumerable<SBAZoneGridRow> GetLiquidZoneData(long wildcard, string SearchString, int appId)
        {
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = db.ZoneMasters.Select(x => new SBAZoneGridRow
                {
                    LWzoneId = x.zoneId,
                    LWname = x.name,
                }).ToList();
                //  var result = data.SkipWhile(element => element.cId != element.reNewId); 
                foreach (var item in data)
                {

                    if (item.LWname == null && item.LWname == "")
                        item.LWname = "";
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.LWname.ToUpper().ToString().Contains(SearchString) || c.LWname.ToString().ToLower().ToString().Contains(SearchString) ||
                     c.LWname.ToString().Contains(SearchString)).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.LWzoneId);
            }
        }

        public IEnumerable<SBAVehicleTypeGridRow> GetVehicleTypeData(long wildcard, string SearchString, int appId)
        {
            List<SBAVehicleTypeGridRow> obj = new List<SBAVehicleTypeGridRow>();
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {

                var data = db.VehicleTypes.Select(x => new SBAVehicleTypeGridRow
                {
                    Id = x.vtId,
                    Name = x.description,
                    NameMar = x.descriptionMar,
                    isActive = x.isActive.ToString()
                }).ToList();
                //  var result = data.SkipWhile(element => element.cId != element.reNewId); 

                foreach (var item in data)
                {
                    if (item.Name == null && item.Name == "")
                        item.Name = "";

                    if (item.isActive == "True")
                        item.isActive = "Active";
                    else item.isActive = "Not Active";
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.Name.ToUpper().ToString().Contains(SearchString) || c.Name.ToString().ToLower().ToString().Contains(SearchString) || c.Name.ToString().Contains(SearchString) || c.isActive.ToString().Contains(SearchString) || c.isActive.ToLower().ToString().Contains(SearchString) || c.isActive.ToUpper().ToString().Contains(SearchString)).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.Id);

            }


        }
        public IEnumerable<SBAWardNumberGridRow> GetWardNoData(long wildcard, string SearchString, int appId)
        {
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = db.WardNumbers.Select(x => new SBAWardNumberGridRow
                {
                    Id = x.Id,
                    WardNo = x.WardNo,
                    zone = db.ZoneMasters.Where(c => c.zoneId == x.zoneId).FirstOrDefault().name,
                }).ToList();
                foreach (var item in data)
                {
                    item.WardNo = checkNull(item.WardNo);
                    item.zone = checkNull(item.zone);


                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.WardNo.ToUpper().ToString().Contains(SearchString) || c.WardNo.ToString().ToLower().ToString().Contains(SearchString) || c.WardNo.ToString().Contains(SearchString) || c.zone.ToString().Contains(SearchString) || c.zone.ToUpper().ToString().Contains(SearchString) || c.zone.ToLower().ToString().Contains(SearchString)).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.Id);
            }
        }


        public IEnumerable<SBAAttendenceSettingsGridRow> GetAttendenceSettingsDate(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        {
            List<SBAAttendenceSettingsGridRow> obj = new List<SBAAttendenceSettingsGridRow>();
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                //var data = db.Daily_Attendance.Select(x => new SBAAttendenceSettingsGridRow
                //{
                //    Name = db.UserMasters.Where(c => c.userId == x.userId).FirstOrDefault().userName,
                //    InTime = x.startTime,
                //    NotificationTime = x.startTime,
                //    NotificationMobileNumber= db.UserMasters.Where(c => c.userId == x.userId).FirstOrDefault().userMobileNumber,

                //}).ToList();

                var data = db.Daily_Attendance.ToList();

                if (Convert.ToDateTime(fdate).ToString("dd/MM/yyyy") == Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy"))
                {
                    data = data.Where(c => (c.daDate == fdate || c.daEndDate == fdate || c.endTime == "")).ToList();
                }
                else
                {

                    data = data.Where(c => (c.daDate >= fdate && c.daDate <= tdate) || (c.daDate >= fdate && c.daDate <= tdate)).ToList();
                }
                foreach (var x in data)
                {
                    ///x.daDate = checkNull(x.daDate.tp);
                    x.endLat = checkNull(x.endLat);
                    x.endLong = checkNull(x.endLong);
                    x.endTime = checkNull(x.endTime);
                    x.startLat = checkNull(x.startLat);
                    x.startLong = checkNull(x.startLong);
                    x.startTime = checkNull(x.startTime);
                    x.vehicleNumber = checkNull(x.vehicleNumber);
                    x.daEndNote = checkNull(x.daEndNote);
                    x.daStartNote = checkNull(x.daStartNote);
                    string endate = "";
                    if (x.daEndDate == null)
                    {
                        endate = "";
                    }
                    else
                    {
                        endate = Convert.ToDateTime(x.daEndDate).ToString("dd/MM/yyyy");
                    }

                    // string displayTime = Convert.ToDateTime(x.daDate).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    string displayTime = Convert.ToDateTime(x.daDate).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    string time = Convert.ToDateTime(x.startTime).ToString("HH:mm:ss");

                    obj.Add(new SBAAttendenceSettingsGridRow()
                    {
                        // daID = x.daID,
                        userId = Convert.ToInt32(x.userId),
                        userName = db.UserMasters.Where(c => c.userId == x.userId).FirstOrDefault().userName,
                        daDate = Convert.ToDateTime(x.daDate).ToString("dd/MM/yyyy"),
                        daEndDate = endate,
                        InTime = x.startTime,
                        endTime = x.endTime,
                        startLat = x.startLat,
                        startLong = x.startLong,
                        endLat = x.startLong,
                        endLong = x.endLong,
                        NotificationTime = x.startTime,
                        NotificationMobileNumber = db.UserMasters.Where(c => c.userId == x.userId).FirstOrDefault().userMobileNumber,
                        //  vtId = vt,
                        vehicleNumber = x.vehicleNumber,
                        CompareDate = x.daDate,
                        daDateTIme = (displayTime + " " + time)
                    });
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = obj.Where(c => c.vehicleNumber.Contains(SearchString) || c.daDate.Contains(SearchString) || c.endTime.Contains(SearchString) || c.startLat.Contains(SearchString) || c.endLat.Contains(SearchString) || c.startTime.Contains(SearchString) || c.userName.Contains(SearchString) || c.vtId.Contains(SearchString)

                    || c.vehicleNumber.ToLower().Contains(SearchString) || c.vtId.ToLower().Contains(SearchString) || c.daDate.ToLower().Contains(SearchString) || c.endTime.ToLower().Contains(SearchString) || c.startLat.ToLower().Contains(SearchString) || c.endLat.ToLower().Contains(SearchString) || c.startTime.ToLower().Contains(SearchString) || c.userName.ToLower().Contains(SearchString)).ToList();

                    obj = model.ToList();
                }

                //if (!string.IsNullOrEmpty(fdate.ToString()))
                //{
                //    DateTime? dt1 = null;
                //    if (!string.IsNullOrEmpty(tdate.ToString()))
                //    { dt1 = tdate; }
                //    else { dt1 = fdate; }
                //    obj = obj.Where(fullEntry => fullEntry.CompareDate >= fdate && fullEntry.CompareDate <= dt1).OrderByDescending(c => c.CompareDate).ToList();
                //}
                if (userId > 0)
                {
                    var model = obj.Where(c => c.userId == userId).ToList();

                    obj = model.ToList();
                }
                var d = obj.OrderByDescending(c => DateTime.Parse(c.daDateTIme)).ToList();
                return d;

            }
        }
        public IEnumerable<SBAHouseDetailsGridRow> GetHouseDetailsData(long wildcard, string SearchString, int appId)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.HouseQRCode + "/";
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = db.HouseDetails().Select(x => new SBAHouseDetailsGridRow
                {
                    houseId = x.houseId,
                    WardNo = x.Ward,
                    Area = x.Area,
                    zone = x.Zone,
                    Address = x.Address,
                    houseNo = x.HouseNumber,
                    Mobile = x.MobileNumber,
                    Name = x.Name,
                    QRCode = ThumbnaiUrlCMS + x.Images.Trim(),
                    ReferanceId = x.ReferanceId,
                    Category = x.Category
                }).ToList();
                if (!string.IsNullOrEmpty(SearchString))
                {
                    //var model = data.Where(c => c.WardNo.ToUpper().ToString().Contains(SearchString)
                    //|| c.Area.ToUpper().ToString().Contains(SearchString) || c.Name.ToUpper().ToString().Contains(SearchString) || c.houseNo.ToUpper().ToString().Contains(SearchString) || c.Mobile.ToUpper().ToString().Contains(SearchString) || c.zone.ToString().ToUpper().ToString().Contains(SearchString)|| c.Address.ToUpper().ToString().Contains(SearchString) || c.ReferanceId.ToUpper().ToString().Contains(SearchString)
                    // || c.WardNo.ToString().ToLower().ToString().Contains(SearchString) || c.zone.ToString().ToLower().ToString().Contains(SearchString)
                    //|| c.Area.ToString().ToLower().ToString().Contains(SearchString) || c.Name.ToString().ToLower().ToString().Contains(SearchString) || c.houseNo.ToString().ToLower().ToString().Contains(SearchString) || c.Mobile.ToString().ToLower().ToString().Contains(SearchString) || c.Address.ToString().ToLower().ToString().Contains(SearchString) || c.ReferanceId.ToLower().ToString().Contains(SearchString)
                    //|| c.WardNo.ToString().Contains(SearchString) || c.zone.ToString().Contains(SearchString) 
                    //|| c.Area.ToString().Contains(SearchString) || c.Name.ToString().Contains(SearchString)
                    //|| c.houseNo.ToString().Contains(SearchString) || c.Mobile.ToString().Contains(SearchString)
                    //|| c.Address.ToString().Contains(SearchString) || c.ReferanceId.ToString().Contains(SearchString) || c.QRCode.ToString().Contains(SearchString)).ToList();

                    var model = data.Where(c => ((string.IsNullOrEmpty(c.WardNo) ? " " : c.WardNo) + " " +
                                        (string.IsNullOrEmpty(c.zone) ? " " : c.zone) + " " +
                                        (string.IsNullOrEmpty(c.Area) ? " " : c.Area) + " " +
                                        (string.IsNullOrEmpty(c.Name) ? " " : c.Name) + " " +
                                        (string.IsNullOrEmpty(c.houseNo) ? " " : c.houseNo) + " " +
                                        (string.IsNullOrEmpty(c.Mobile) ? " " : c.Mobile) + " " +
                                        (string.IsNullOrEmpty(c.Address) ? " " : c.Address) + " " +
                                        (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId) + " " +
                                        (string.IsNullOrEmpty(c.QRCode) ? " " : c.QRCode) + " " +
                                        (string.IsNullOrEmpty(c.Category) ? " " : c.Category)).ToUpper().Contains(SearchString.ToUpper())).ToList();


                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.houseId);
            }
        }

        public IEnumerable<SBAHouseDetailsGridRow> GetCommercialDetailsData(long wildcard, string SearchString, int appId)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.CommercialQRCode + "/";
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = db.CommercialDetails().Select(x => new SBAHouseDetailsGridRow
                {
                    houseId = x.commercialId,
                    WardNo = x.Ward,
                    Area = x.Area,
                    zone = x.Zone,
                    Address = x.Address,
                    houseNo = x.HouseNumber,
                    Mobile = x.MobileNumber,
                    Name = x.Name,
                    QRCode = ThumbnaiUrlCMS + x.Images.Trim(),
                    ReferanceId = x.ReferanceId,
                    Category = x.Category
                }).ToList();
                if (!string.IsNullOrEmpty(SearchString))
                {
                    //var model = data.Where(c => c.WardNo.ToUpper().ToString().Contains(SearchString)
                    //|| c.Area.ToUpper().ToString().Contains(SearchString) || c.Name.ToUpper().ToString().Contains(SearchString) || c.houseNo.ToUpper().ToString().Contains(SearchString) || c.Mobile.ToUpper().ToString().Contains(SearchString) || c.zone.ToString().ToUpper().ToString().Contains(SearchString)|| c.Address.ToUpper().ToString().Contains(SearchString) || c.ReferanceId.ToUpper().ToString().Contains(SearchString)
                    // || c.WardNo.ToString().ToLower().ToString().Contains(SearchString) || c.zone.ToString().ToLower().ToString().Contains(SearchString)
                    //|| c.Area.ToString().ToLower().ToString().Contains(SearchString) || c.Name.ToString().ToLower().ToString().Contains(SearchString) || c.houseNo.ToString().ToLower().ToString().Contains(SearchString) || c.Mobile.ToString().ToLower().ToString().Contains(SearchString) || c.Address.ToString().ToLower().ToString().Contains(SearchString) || c.ReferanceId.ToLower().ToString().Contains(SearchString)
                    //|| c.WardNo.ToString().Contains(SearchString) || c.zone.ToString().Contains(SearchString) 
                    //|| c.Area.ToString().Contains(SearchString) || c.Name.ToString().Contains(SearchString)
                    //|| c.houseNo.ToString().Contains(SearchString) || c.Mobile.ToString().Contains(SearchString)
                    //|| c.Address.ToString().Contains(SearchString) || c.ReferanceId.ToString().Contains(SearchString) || c.QRCode.ToString().Contains(SearchString)).ToList();

                    var model = data.Where(c => ((string.IsNullOrEmpty(c.WardNo) ? " " : c.WardNo) + " " +
                                        (string.IsNullOrEmpty(c.zone) ? " " : c.zone) + " " +
                                        (string.IsNullOrEmpty(c.Area) ? " " : c.Area) + " " +
                                        (string.IsNullOrEmpty(c.Name) ? " " : c.Name) + " " +
                                        (string.IsNullOrEmpty(c.houseNo) ? " " : c.houseNo) + " " +
                                        (string.IsNullOrEmpty(c.Mobile) ? " " : c.Mobile) + " " +
                                        (string.IsNullOrEmpty(c.Address) ? " " : c.Address) + " " +
                                        (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId) + " " +
                                        (string.IsNullOrEmpty(c.QRCode) ? " " : c.QRCode) + " " +
                                        (string.IsNullOrEmpty(c.Category) ? " " : c.Category)).ToUpper().Contains(SearchString.ToUpper())).ToList();


                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.houseId);
            }
        }
        public IEnumerable<SBAEmployeeDetailsGridRow> GetEmployeeDetailsData(long wildcard, string SearchString, int appId, string isActive, string emptype)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.UserProfile + "/";
            if (isActive == "1" && emptype == "")
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(appId))
                {

                    var data = db.UserMasters.Select(x => new SBAEmployeeDetailsGridRow
                    {
                        userId = x.userId,
                        userAddress = x.userAddress,
                        userLoginId = x.userLoginId,
                        userMobileNumber = x.userMobileNumber,
                        userName = x.userName,
                        userNameMar = x.userNameMar,
                        userProfileImage = x.userProfileImage,
                        userEmployeeNo = x.userEmployeeNo,
                        isActive = x.isActive.ToString(),
                        bloodGroup = x.bloodGroup,
                        gcTarget = x.gcTarget,
                        EmployeeType = x.EmployeeType,
                        userDesignation = x.userDesignation,


                    }).Where(x => x.isActive == "True" && x.EmployeeType == null).ToList();
                    foreach (var item in data)
                    {
                        item.isActive = checkNull(item.isActive);
                        if (item.bloodGroup == "0")
                        {
                            item.bloodGroup = "";
                        }
                        if (item.isActive == "True")
                        {

                            item.isActive = "Active";
                        }
                        else { item.isActive = "Not Active"; }
                        item.userNameMar = checkNull(item.userNameMar);
                        item.bloodGroup = checkNull(item.bloodGroup);
                        if (item.userAddress == null && item.userAddress == "")
                            item.userAddress = "";
                        if (item.userMobileNumber == null && item.userMobileNumber == "")
                            item.userMobileNumber = "";
                        if (item.userName == null && item.userName == "")
                            item.userName = "";
                        if (item.userNameMar == null && item.userNameMar == "")
                            item.userNameMar = "";
                        if (item.userEmployeeNo == null && item.userEmployeeNo == "")
                            item.userEmployeeNo = "";

                        if (item.userProfileImage == null || item.userProfileImage == "")
                        { item.userProfileImage = "/Images/default_not_upload.png"; }
                        else
                        {
                            item.userProfileImage = ThumbnaiUrlCMS + item.userProfileImage.Trim();
                        }
                        if (item.userDesignation == null || item.userDesignation == "")
                            item.userDesignation = "";
                    }

                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        //var model = data.Where(c => c.userMobileNumber.ToString().Contains(SearchString) || c.userEmployeeNo.ToString().Contains(SearchString)
                        //|| c.userAddress.ToString().Contains(SearchString) || c.userName.ToString().Contains(SearchString) || c.userNameMar.ToString().Contains(SearchString)||c.bloodGroup.ToString().Contains(SearchString)

                        //|| c.userMobileNumber.Contains(SearchString) || c.userAddress.ToLower().ToString().Contains(SearchString)|| c.userName.ToLower().ToString().Contains(SearchString) || c.userNameMar.ToLower().ToString().Contains(SearchString)
                        //|| c.userEmployeeNo.ToLower().ToString().Contains(SearchString) || c.bloodGroup.ToLower().ToString().Contains(SearchString)

                        //|| c.userMobileNumber.ToUpper().ToString().Contains(SearchString) || c.userNameMar.ToUpper().ToString().Contains(SearchString)|| c.userName.ToUpper().ToString().Contains(SearchString) || c.bloodGroup.ToUpper().ToString().Contains(SearchString) || c.userAddress.ToUpper().ToString().Contains(SearchString) || c.userEmployeeNo.ToUpper().ToString().Contains(SearchString)).ToList();

                        var model = data.Where(c => ((string.IsNullOrEmpty(c.userMobileNumber) ? " " : c.userMobileNumber) + " " +
                                             (string.IsNullOrEmpty(c.userNameMar) ? " " : c.userNameMar) + " " +
                                             (string.IsNullOrEmpty(c.userName) ? " " : c.userName) + " " +
                                             (string.IsNullOrEmpty(c.bloodGroup) ? " " : c.bloodGroup) + " " +
                                             (string.IsNullOrEmpty(c.userAddress) ? " " : c.userAddress) + " " +
                                              (string.IsNullOrEmpty(c.EmployeeType) ? " " : c.EmployeeType) + " " +
                                             (string.IsNullOrEmpty(c.userEmployeeNo) ? " " : c.userEmployeeNo) + " " +
                                             (string.IsNullOrEmpty(c.userDesignation) ? " " : c.userDesignation)).ToUpper().Contains(SearchString.ToUpper())).ToList();


                        data = model.ToList();

                    }
                    return data.OrderByDescending(c => c.userId);

                }
            }
            else if (isActive == "0" && emptype == "")
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(appId))
                {

                    var data = db.UserMasters.Select(x => new SBAEmployeeDetailsGridRow
                    {
                        userId = x.userId,
                        userAddress = x.userAddress,
                        userLoginId = x.userLoginId,
                        userMobileNumber = x.userMobileNumber,
                        userName = x.userName,
                        userNameMar = x.userNameMar,
                        userProfileImage = x.userProfileImage,
                        userEmployeeNo = x.userEmployeeNo,
                        isActive = x.isActive.ToString(),
                        bloodGroup = x.bloodGroup,
                        gcTarget = x.gcTarget,
                        EmployeeType = x.EmployeeType,
                        userDesignation = x.userDesignation,
                        


                    }).Where(x => x.isActive == "False" && x.EmployeeType == null).ToList();
                    foreach (var item in data)
                    {
                        item.isActive = checkNull(item.isActive);
                        if (item.bloodGroup == "0")
                        {
                            item.bloodGroup = "";
                        }
                        if (item.isActive == "True")
                        {

                            item.isActive = "Active";
                        }
                        else { item.isActive = "Not Active"; }
                        item.userNameMar = checkNull(item.userNameMar);
                        item.bloodGroup = checkNull(item.bloodGroup);
                        if (item.userAddress == null && item.userAddress == "")
                            item.userAddress = "";
                        if (item.userMobileNumber == null && item.userMobileNumber == "")
                            item.userMobileNumber = "";
                        if (item.userName == null && item.userName == "")
                            item.userName = "";
                        if (item.userNameMar == null && item.userNameMar == "")
                            item.userNameMar = "";
                        if (item.userEmployeeNo == null && item.userEmployeeNo == "")
                            item.userEmployeeNo = "";

                        if (item.userProfileImage == null || item.userProfileImage == "")
                        { item.userProfileImage = "/Images/default_not_upload.png"; }
                        else
                        {
                            item.userProfileImage = ThumbnaiUrlCMS + item.userProfileImage.Trim();
                        }
                        if (item.userDesignation == null || item.userDesignation == "")
                            item.userDesignation = "";
                    }

                    if (!string.IsNullOrEmpty(SearchString))
                    {

                        var model = data.Where(c => ((string.IsNullOrEmpty(c.userMobileNumber) ? " " : c.userMobileNumber) + " " +
                                             (string.IsNullOrEmpty(c.userNameMar) ? " " : c.userNameMar) + " " +
                                             (string.IsNullOrEmpty(c.userName) ? " " : c.userName) + " " +
                                             (string.IsNullOrEmpty(c.bloodGroup) ? " " : c.bloodGroup) + " " +
                                             (string.IsNullOrEmpty(c.userAddress) ? " " : c.userAddress) + " " +
                                                 (string.IsNullOrEmpty(c.EmployeeType) ? " " : c.EmployeeType) + " " +
                                             (string.IsNullOrEmpty(c.userEmployeeNo) ? " " : c.userEmployeeNo) + " " +
                                             (string.IsNullOrEmpty(c.userDesignation) ? " " : c.userDesignation)).ToUpper().Contains(SearchString.ToUpper())).ToList();
                        data = model.ToList();

                    }
                    return data.OrderByDescending(c => c.userId);

                }
            }

            else if (isActive == "1" && emptype == "L")
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(appId))
                {

                    var data = db.UserMasters.Select(x => new SBAEmployeeDetailsGridRow
                    {
                        userId = x.userId,
                        userAddress = x.userAddress,
                        userLoginId = x.userLoginId,
                        userMobileNumber = x.userMobileNumber,
                        userName = x.userName,
                        userNameMar = x.userNameMar,
                        userProfileImage = x.userProfileImage,
                        userEmployeeNo = x.userEmployeeNo,
                        isActive = x.isActive.ToString(),
                        bloodGroup = x.bloodGroup,
                        gcTarget = x.gcTarget,
                        EmployeeType = x.EmployeeType,
                        userDesignation = x.userDesignation,


                    }).Where(x => x.isActive == "True" && x.EmployeeType == "L").ToList();
                    foreach (var item in data)
                    {
                        item.isActive = checkNull(item.isActive);
                        if (item.bloodGroup == "0")
                        {
                            item.bloodGroup = "";
                        }
                        if (item.isActive == "True")
                        {

                            item.isActive = "Active";
                        }
                        else { item.isActive = "Not Active"; }
                        item.userNameMar = checkNull(item.userNameMar);
                        item.bloodGroup = checkNull(item.bloodGroup);
                        if (item.userAddress == null && item.userAddress == "")
                            item.userAddress = "";
                        if (item.userMobileNumber == null && item.userMobileNumber == "")
                            item.userMobileNumber = "";
                        if (item.userName == null && item.userName == "")
                            item.userName = "";
                        if (item.userNameMar == null && item.userNameMar == "")
                            item.userNameMar = "";
                        if (item.userEmployeeNo == null && item.userEmployeeNo == "")
                            item.userEmployeeNo = "";

                        if (item.userProfileImage == null || item.userProfileImage == "")
                        { item.userProfileImage = "/Images/default_not_upload.png"; }
                        else
                        {
                            item.userProfileImage = ThumbnaiUrlCMS + item.userProfileImage.Trim();
                        }
                        if (item.userDesignation == null || item.userDesignation == "")
                            item.userDesignation = "";
                    }

                    if (!string.IsNullOrEmpty(SearchString))
                    {

                        var model = data.Where(c => ((string.IsNullOrEmpty(c.userMobileNumber) ? " " : c.userMobileNumber) + " " +
                                             (string.IsNullOrEmpty(c.userNameMar) ? " " : c.userNameMar) + " " +
                                             (string.IsNullOrEmpty(c.userName) ? " " : c.userName) + " " +
                                             (string.IsNullOrEmpty(c.bloodGroup) ? " " : c.bloodGroup) + " " +
                                             (string.IsNullOrEmpty(c.userAddress) ? " " : c.userAddress) + " " +
                                                 (string.IsNullOrEmpty(c.EmployeeType) ? " " : c.EmployeeType) + " " +
                                             (string.IsNullOrEmpty(c.userEmployeeNo) ? " " : c.userEmployeeNo) + " " +
                                             (string.IsNullOrEmpty(c.userDesignation) ? " " : c.userDesignation)).ToUpper().Contains(SearchString.ToUpper())).ToList();
                        data = model.ToList();

                    }
                    return data.OrderByDescending(c => c.userId);

                }
            }
            else if (isActive == "1" && emptype == "S")
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(appId))
                {

                    var data = db.UserMasters.Select(x => new SBAEmployeeDetailsGridRow
                    {
                        userId = x.userId,
                        userAddress = x.userAddress,
                        userLoginId = x.userLoginId,
                        userMobileNumber = x.userMobileNumber,
                        userName = x.userName,
                        userNameMar = x.userNameMar,
                        userProfileImage = x.userProfileImage,
                        userEmployeeNo = x.userEmployeeNo,
                        isActive = x.isActive.ToString(),
                        bloodGroup = x.bloodGroup,
                        gcTarget = x.gcTarget,
                        EmployeeType = x.EmployeeType,
                        userDesignation = x.userDesignation,


                    }).Where(x => x.isActive == "True" && x.EmployeeType == "S").ToList();
                    foreach (var item in data)
                    {
                        item.isActive = checkNull(item.isActive);
                        if (item.bloodGroup == "0")
                        {
                            item.bloodGroup = "";
                        }
                        if (item.isActive == "True")
                        {

                            item.isActive = "Active";
                        }
                        else { item.isActive = "Not Active"; }
                        item.userNameMar = checkNull(item.userNameMar);
                        item.bloodGroup = checkNull(item.bloodGroup);
                        if (item.userAddress == null && item.userAddress == "")
                            item.userAddress = "";
                        if (item.userMobileNumber == null && item.userMobileNumber == "")
                            item.userMobileNumber = "";
                        if (item.userName == null && item.userName == "")
                            item.userName = "";
                        if (item.userNameMar == null && item.userNameMar == "")
                            item.userNameMar = "";
                        if (item.userEmployeeNo == null && item.userEmployeeNo == "")
                            item.userEmployeeNo = "";

                        if (item.userProfileImage == null || item.userProfileImage == "")
                        { item.userProfileImage = "/Images/default_not_upload.png"; }
                        else
                        {
                            item.userProfileImage = ThumbnaiUrlCMS + item.userProfileImage.Trim();
                        }
                        if (item.userDesignation == null || item.userDesignation == "")
                            item.userDesignation = "";
                    }

                    if (!string.IsNullOrEmpty(SearchString))
                    {

                        var model = data.Where(c => ((string.IsNullOrEmpty(c.userMobileNumber) ? " " : c.userMobileNumber) + " " +
                                             (string.IsNullOrEmpty(c.userNameMar) ? " " : c.userNameMar) + " " +
                                             (string.IsNullOrEmpty(c.userName) ? " " : c.userName) + " " +
                                             (string.IsNullOrEmpty(c.bloodGroup) ? " " : c.bloodGroup) + " " +
                                             (string.IsNullOrEmpty(c.userAddress) ? " " : c.userAddress) + " " +
                                                 (string.IsNullOrEmpty(c.EmployeeType) ? " " : c.EmployeeType) + " " +
                                             (string.IsNullOrEmpty(c.userEmployeeNo) ? " " : c.userEmployeeNo) + " " +
                                             (string.IsNullOrEmpty(c.userDesignation) ? " " : c.userDesignation)).ToUpper().Contains(SearchString.ToUpper())).ToList();
                        data = model.ToList();

                    }
                    return data.OrderByDescending(c => c.userId);

                }
            }

            else if (isActive == "0" && emptype == "L")
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(appId))
                {

                    var data = db.UserMasters.Select(x => new SBAEmployeeDetailsGridRow
                    {
                        userId = x.userId,
                        userAddress = x.userAddress,
                        userLoginId = x.userLoginId,
                        userMobileNumber = x.userMobileNumber,
                        userName = x.userName,
                        userNameMar = x.userNameMar,
                        userProfileImage = x.userProfileImage,
                        userEmployeeNo = x.userEmployeeNo,
                        isActive = x.isActive.ToString(),
                        bloodGroup = x.bloodGroup,
                        gcTarget = x.gcTarget,
                        EmployeeType = x.EmployeeType,
                        userDesignation = x.userDesignation,


                    }).Where(x => x.isActive == "False" && x.EmployeeType == "L").ToList();
                    foreach (var item in data)
                    {
                        item.isActive = checkNull(item.isActive);
                        if (item.bloodGroup == "0")
                        {
                            item.bloodGroup = "";
                        }
                        if (item.isActive == "True")
                        {

                            item.isActive = "Active";
                        }
                        else { item.isActive = "Not Active"; }
                        item.userNameMar = checkNull(item.userNameMar);
                        item.bloodGroup = checkNull(item.bloodGroup);
                        if (item.userAddress == null && item.userAddress == "")
                            item.userAddress = "";
                        if (item.userMobileNumber == null && item.userMobileNumber == "")
                            item.userMobileNumber = "";
                        if (item.userName == null && item.userName == "")
                            item.userName = "";
                        if (item.userNameMar == null && item.userNameMar == "")
                            item.userNameMar = "";
                        if (item.userEmployeeNo == null && item.userEmployeeNo == "")
                            item.userEmployeeNo = "";

                        if (item.userProfileImage == null || item.userProfileImage == "")
                        { item.userProfileImage = "/Images/default_not_upload.png"; }
                        else
                        {
                            item.userProfileImage = ThumbnaiUrlCMS + item.userProfileImage.Trim();
                        }
                        if (item.userDesignation == null || item.userDesignation == "")
                            item.userDesignation = "";
                    }

                    if (!string.IsNullOrEmpty(SearchString))
                    {

                        var model = data.Where(c => ((string.IsNullOrEmpty(c.userMobileNumber) ? " " : c.userMobileNumber) + " " +
                                             (string.IsNullOrEmpty(c.userNameMar) ? " " : c.userNameMar) + " " +
                                             (string.IsNullOrEmpty(c.userName) ? " " : c.userName) + " " +
                                             (string.IsNullOrEmpty(c.bloodGroup) ? " " : c.bloodGroup) + " " +
                                             (string.IsNullOrEmpty(c.userAddress) ? " " : c.userAddress) + " " +
                                                 (string.IsNullOrEmpty(c.EmployeeType) ? " " : c.EmployeeType) + " " +
                                             (string.IsNullOrEmpty(c.userEmployeeNo) ? " " : c.userEmployeeNo) + " " +
                                             (string.IsNullOrEmpty(c.userDesignation) ? " " : c.userDesignation)).ToUpper().Contains(SearchString.ToUpper())).ToList();
                        data = model.ToList();

                    }
                    return data.OrderByDescending(c => c.userId);

                }
            }
            else if (isActive == "0" && emptype == "S")
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(appId))
                {

                    var data = db.UserMasters.Select(x => new SBAEmployeeDetailsGridRow
                    {
                        userId = x.userId,
                        userAddress = x.userAddress,
                        userLoginId = x.userLoginId,
                        userMobileNumber = x.userMobileNumber,
                        userName = x.userName,
                        userNameMar = x.userNameMar,
                        userProfileImage = x.userProfileImage,
                        userEmployeeNo = x.userEmployeeNo,
                        isActive = x.isActive.ToString(),
                        bloodGroup = x.bloodGroup,
                        gcTarget = x.gcTarget,
                        EmployeeType = x.EmployeeType,
                        userDesignation = x.userDesignation,


                    }).Where(x => x.isActive == "False" && x.EmployeeType == "S").ToList();
                    foreach (var item in data)
                    {
                        item.isActive = checkNull(item.isActive);
                        if (item.bloodGroup == "0")
                        {
                            item.bloodGroup = "";
                        }
                        if (item.isActive == "True")
                        {

                            item.isActive = "Active";
                        }
                        else { item.isActive = "Not Active"; }
                        item.userNameMar = checkNull(item.userNameMar);
                        item.bloodGroup = checkNull(item.bloodGroup);
                        if (item.userAddress == null && item.userAddress == "")
                            item.userAddress = "";
                        if (item.userMobileNumber == null && item.userMobileNumber == "")
                            item.userMobileNumber = "";
                        if (item.userName == null && item.userName == "")
                            item.userName = "";
                        if (item.userNameMar == null && item.userNameMar == "")
                            item.userNameMar = "";
                        if (item.userEmployeeNo == null && item.userEmployeeNo == "")
                            item.userEmployeeNo = "";

                        if (item.userProfileImage == null || item.userProfileImage == "")
                        { item.userProfileImage = "/Images/default_not_upload.png"; }
                        else
                        {
                            item.userProfileImage = ThumbnaiUrlCMS + item.userProfileImage.Trim();
                        }
                        if (item.userDesignation == null || item.userDesignation == "")
                            item.userDesignation = "";
                    }

                    if (!string.IsNullOrEmpty(SearchString))
                    {

                        var model = data.Where(c => ((string.IsNullOrEmpty(c.userMobileNumber) ? " " : c.userMobileNumber) + " " +
                                             (string.IsNullOrEmpty(c.userNameMar) ? " " : c.userNameMar) + " " +
                                             (string.IsNullOrEmpty(c.userName) ? " " : c.userName) + " " +
                                             (string.IsNullOrEmpty(c.bloodGroup) ? " " : c.bloodGroup) + " " +
                                             (string.IsNullOrEmpty(c.userAddress) ? " " : c.userAddress) + " " +
                                                 (string.IsNullOrEmpty(c.EmployeeType) ? " " : c.EmployeeType) + " " +
                                             (string.IsNullOrEmpty(c.userEmployeeNo) ? " " : c.userEmployeeNo) + " " +
                                             (string.IsNullOrEmpty(c.userDesignation) ? " " : c.userDesignation)).ToUpper().Contains(SearchString.ToUpper())).ToList();
                        data = model.ToList();

                    }
                    return data.OrderByDescending(c => c.userId);

                }
            }

            else if (emptype == "")
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(appId))
                {

                    var data = db.UserMasters.Select(x => new SBAEmployeeDetailsGridRow
                    {
                        userId = x.userId,
                        userAddress = x.userAddress,
                        userLoginId = x.userLoginId,
                        userMobileNumber = x.userMobileNumber,
                        userName = x.userName,
                        userNameMar = x.userNameMar,
                        userProfileImage = x.userProfileImage,
                        userEmployeeNo = x.userEmployeeNo,
                        isActive = x.isActive.ToString(),
                        bloodGroup = x.bloodGroup,
                        gcTarget = x.gcTarget,
                        EmployeeType = x.EmployeeType,
                        userDesignation = x.userDesignation,


                    }).Where(x => x.EmployeeType == "").ToList();
                    foreach (var item in data)
                    {
                        item.isActive = checkNull(item.isActive);
                        if (item.bloodGroup == "0")
                        {
                            item.bloodGroup = "";
                        }
                        if (item.isActive == "True")
                        {

                            item.isActive = "Active";
                        }
                        else { item.isActive = "Not Active"; }
                        item.userNameMar = checkNull(item.userNameMar);
                        item.bloodGroup = checkNull(item.bloodGroup);
                        if (item.userAddress == null && item.userAddress == "")
                            item.userAddress = "";
                        if (item.userMobileNumber == null && item.userMobileNumber == "")
                            item.userMobileNumber = "";
                        if (item.userName == null && item.userName == "")
                            item.userName = "";
                        if (item.userNameMar == null && item.userNameMar == "")
                            item.userNameMar = "";
                        if (item.userEmployeeNo == null && item.userEmployeeNo == "")
                            item.userEmployeeNo = "";

                        if (item.userProfileImage == null || item.userProfileImage == "")
                        { item.userProfileImage = "/Images/default_not_upload.png"; }
                        else
                        {
                            item.userProfileImage = ThumbnaiUrlCMS + item.userProfileImage.Trim();
                        }
                        if (item.userDesignation == null || item.userDesignation == "")
                            item.userDesignation = "";
                    }

                    if (!string.IsNullOrEmpty(SearchString))
                    {

                        var model = data.Where(c => ((string.IsNullOrEmpty(c.userMobileNumber) ? " " : c.userMobileNumber) + " " +
                                             (string.IsNullOrEmpty(c.userNameMar) ? " " : c.userNameMar) + " " +
                                             (string.IsNullOrEmpty(c.userName) ? " " : c.userName) + " " +
                                             (string.IsNullOrEmpty(c.bloodGroup) ? " " : c.bloodGroup) + " " +
                                             (string.IsNullOrEmpty(c.userAddress) ? " " : c.userAddress) + " " +
                                                 (string.IsNullOrEmpty(c.EmployeeType) ? " " : c.EmployeeType) + " " +
                                             (string.IsNullOrEmpty(c.userEmployeeNo) ? " " : c.userEmployeeNo) + " " +
                                             (string.IsNullOrEmpty(c.userDesignation) ? " " : c.userDesignation)).ToUpper().Contains(SearchString.ToUpper())).ToList();
                        data = model.ToList();

                    }
                    return data.OrderByDescending(c => c.userId);

                }
            }

            else if (emptype == "L")
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(appId))
                {

                    var data = db.UserMasters.Select(x => new SBAEmployeeDetailsGridRow
                    {
                        userId = x.userId,
                        userAddress = x.userAddress,
                        userLoginId = x.userLoginId,
                        userMobileNumber = x.userMobileNumber,
                        userName = x.userName,
                        userNameMar = x.userNameMar,
                        userProfileImage = x.userProfileImage,
                        userEmployeeNo = x.userEmployeeNo,
                        isActive = x.isActive.ToString(),
                        bloodGroup = x.bloodGroup,
                        gcTarget = x.gcTarget,
                        EmployeeType = x.EmployeeType,
                        userDesignation = x.userDesignation,


                    }).Where(x => x.EmployeeType == "L").ToList();
                    foreach (var item in data)
                    {
                        item.isActive = checkNull(item.isActive);
                        if (item.bloodGroup == "0")
                        {
                            item.bloodGroup = "";
                        }
                        if (item.isActive == "True")
                        {

                            item.isActive = "Active";
                        }
                        else { item.isActive = "Not Active"; }
                        item.userNameMar = checkNull(item.userNameMar);
                        item.bloodGroup = checkNull(item.bloodGroup);
                        if (item.userAddress == null && item.userAddress == "")
                            item.userAddress = "";
                        if (item.userMobileNumber == null && item.userMobileNumber == "")
                            item.userMobileNumber = "";
                        if (item.userName == null && item.userName == "")
                            item.userName = "";
                        if (item.userNameMar == null && item.userNameMar == "")
                            item.userNameMar = "";
                        if (item.userEmployeeNo == null && item.userEmployeeNo == "")
                            item.userEmployeeNo = "";

                        if (item.userProfileImage == null || item.userProfileImage == "")
                        { item.userProfileImage = "/Images/default_not_upload.png"; }
                        else
                        {
                            item.userProfileImage = ThumbnaiUrlCMS + item.userProfileImage.Trim();
                        }
                        if (item.userDesignation == null || item.userDesignation == "")
                            item.userDesignation = "";
                    }

                    if (!string.IsNullOrEmpty(SearchString))
                    {

                        var model = data.Where(c => ((string.IsNullOrEmpty(c.userMobileNumber) ? " " : c.userMobileNumber) + " " +
                                             (string.IsNullOrEmpty(c.userNameMar) ? " " : c.userNameMar) + " " +
                                             (string.IsNullOrEmpty(c.userName) ? " " : c.userName) + " " +
                                             (string.IsNullOrEmpty(c.bloodGroup) ? " " : c.bloodGroup) + " " +
                                             (string.IsNullOrEmpty(c.userAddress) ? " " : c.userAddress) + " " +
                                                 (string.IsNullOrEmpty(c.EmployeeType) ? " " : c.EmployeeType) + " " +
                                             (string.IsNullOrEmpty(c.userEmployeeNo) ? " " : c.userEmployeeNo) + " " +
                                             (string.IsNullOrEmpty(c.userDesignation) ? " " : c.userDesignation)).ToUpper().Contains(SearchString.ToUpper())).ToList();
                        data = model.ToList();

                    }
                    return data.OrderByDescending(c => c.userId);

                }
            }

            else if (emptype == "S")
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(appId))
                {

                    var data = db.UserMasters.Select(x => new SBAEmployeeDetailsGridRow
                    {
                        userId = x.userId,
                        userAddress = x.userAddress,
                        userLoginId = x.userLoginId,
                        userMobileNumber = x.userMobileNumber,
                        userName = x.userName,
                        userNameMar = x.userNameMar,
                        userProfileImage = x.userProfileImage,
                        userEmployeeNo = x.userEmployeeNo,
                        isActive = x.isActive.ToString(),
                        bloodGroup = x.bloodGroup,
                        gcTarget = x.gcTarget,
                        EmployeeType = x.EmployeeType,
                        userDesignation = x.userDesignation,


                    }).Where(x => x.EmployeeType == "S").ToList();
                    foreach (var item in data)
                    {
                        item.isActive = checkNull(item.isActive);
                        if (item.bloodGroup == "0")
                        {
                            item.bloodGroup = "";
                        }
                        if (item.isActive == "True")
                        {

                            item.isActive = "Active";
                        }
                        else { item.isActive = "Not Active"; }
                        item.userNameMar = checkNull(item.userNameMar);
                        item.bloodGroup = checkNull(item.bloodGroup);
                        if (item.userAddress == null && item.userAddress == "")
                            item.userAddress = "";
                        if (item.userMobileNumber == null && item.userMobileNumber == "")
                            item.userMobileNumber = "";
                        if (item.userName == null && item.userName == "")
                            item.userName = "";
                        if (item.userNameMar == null && item.userNameMar == "")
                            item.userNameMar = "";
                        if (item.userEmployeeNo == null && item.userEmployeeNo == "")
                            item.userEmployeeNo = "";

                        if (item.userProfileImage == null || item.userProfileImage == "")
                        { item.userProfileImage = "/Images/default_not_upload.png"; }
                        else
                        {
                            item.userProfileImage = ThumbnaiUrlCMS + item.userProfileImage.Trim();
                        }
                        if (item.userDesignation == null || item.userDesignation == "")
                            item.userDesignation = "";
                    }

                    if (!string.IsNullOrEmpty(SearchString))
                    {

                        var model = data.Where(c => ((string.IsNullOrEmpty(c.userMobileNumber) ? " " : c.userMobileNumber) + " " +
                                             (string.IsNullOrEmpty(c.userNameMar) ? " " : c.userNameMar) + " " +
                                             (string.IsNullOrEmpty(c.userName) ? " " : c.userName) + " " +
                                             (string.IsNullOrEmpty(c.bloodGroup) ? " " : c.bloodGroup) + " " +
                                             (string.IsNullOrEmpty(c.userAddress) ? " " : c.userAddress) + " " +
                                                 (string.IsNullOrEmpty(c.EmployeeType) ? " " : c.EmployeeType) + " " +
                                             (string.IsNullOrEmpty(c.userEmployeeNo) ? " " : c.userEmployeeNo) + " " +
                                             (string.IsNullOrEmpty(c.userDesignation) ? " " : c.userDesignation)).ToUpper().Contains(SearchString.ToUpper())).ToList();
                        data = model.ToList();

                    }
                    return data.OrderByDescending(c => c.userId);

                }
            }
            else
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(appId))
                {

                    var data = db.UserMasters.Select(x => new SBAEmployeeDetailsGridRow
                    {
                        userId = x.userId,
                        userAddress = x.userAddress,
                        userLoginId = x.userLoginId,
                        userMobileNumber = x.userMobileNumber,
                        userName = x.userName,
                        userNameMar = x.userNameMar,
                        userProfileImage = x.userProfileImage,
                        userEmployeeNo = x.userEmployeeNo,
                        isActive = x.isActive.ToString(),
                        bloodGroup = x.bloodGroup,
                        gcTarget = x.gcTarget,
                        EmployeeType = x.EmployeeType,
                        userDesignation = x.userDesignation,


                    }).ToList();
                    foreach (var item in data)
                    {
                        item.isActive = checkNull(item.isActive);
                        if (item.bloodGroup == "0")
                        {
                            item.bloodGroup = "";
                        }
                        if (item.isActive == "True")
                        {

                            item.isActive = "Active";
                        }
                        else { item.isActive = "Not Active"; }
                        item.userNameMar = checkNull(item.userNameMar);
                        item.bloodGroup = checkNull(item.bloodGroup);
                        if (item.userAddress == null && item.userAddress == "")
                            item.userAddress = "";
                        if (item.userMobileNumber == null && item.userMobileNumber == "")
                            item.userMobileNumber = "";
                        if (item.userName == null && item.userName == "")
                            item.userName = "";
                        if (item.userNameMar == null && item.userNameMar == "")
                            item.userNameMar = "";
                        if (item.userEmployeeNo == null && item.userEmployeeNo == "")
                            item.userEmployeeNo = "";

                        if (item.userProfileImage == null || item.userProfileImage == "")
                        { item.userProfileImage = "/Images/default_not_upload.png"; }
                        else
                        {
                            item.userProfileImage = ThumbnaiUrlCMS + item.userProfileImage.Trim();
                        }
                        if (item.userDesignation == null || item.userDesignation == "")
                            item.userDesignation = "";
                    }


                    return data.OrderByDescending(c => c.userId);
                }

            }

        }
        public IEnumerable<SBAGarbagePointDetailsGridRow> GetGarbagePointData(long wildcard, string SearchString, int appId)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.PointQRCode + "/";
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = db.PointDetails().Select(x => new SBAGarbagePointDetailsGridRow
                {

                    Zone = x.Zone,
                    Ward = x.Ward,
                    Area = x.Area,
                    Name = x.Name,
                    NameMar = x.NameMar,
                    Id = x.gpId,
                    QrCode = ThumbnaiUrlCMS + x.Images.Trim(),
                    Address = checkNull(x.Address).Replace("Unnamed Road, ", ""),
                    ReferanceId = x.ReferanceId

                }).ToList();
                if (!string.IsNullOrEmpty(SearchString))
                {
                    //var model = data.Where(c => c.Area.ToString().Contains(SearchString)
                    //|| c.Name.ToString().Contains(SearchString) || c.NameMar.ToString().Contains(SearchString) || c.ReferanceId.ToString().Contains(SearchString)

                    //|| c.Area.Contains(SearchString) || c.NameMar.ToLower().ToString().Contains(SearchString) || c.Name.ToLower().ToString().Contains(SearchString) || c.ReferanceId.ToLower().ToString().Contains(SearchString)

                    //|| c.Area.ToUpper().ToString().Contains(SearchString) || c.Name.ToUpper().ToString().Contains(SearchString)
                    //|| c.NameMar.ToUpper().ToString().Contains(SearchString) || c.ReferanceId.ToUpper().ToString().Contains(SearchString)).ToList();

                    var model = data.Where(c => ((string.IsNullOrEmpty(c.Area) ? " " : c.Area) + " " +
                                       (string.IsNullOrEmpty(c.Name) ? " " : c.Name) + " " +
                                       (string.IsNullOrEmpty(c.NameMar) ? " " : c.NameMar) + " " +
                                       (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId)).ToUpper().Contains(SearchString.ToUpper())).ToList();


                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.Id).ToList();
            }
        }


        public IEnumerable<SBALocationGridRow> GetLocatioData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId, string Emptype)
        {
            if (Emptype ==null)
            {
                using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(appId))
                {
                    List<SBALocationGridRow> data = new List<SBALocationGridRow>();
                    //  var data = db.GarbageCollectionDetails.Where(x => x.gcType == 3 & x.gcDate >= fdate & x.gcDate <= tdate).Select(x => new SBAGrabageCollectionGridRow


                    //var data1 = db.Locations.Where(l => l.datetime >= fdate && l.datetime <= tdate)
                    //            .Join(db.UserMasters, u => u.userId, i => i.userId


                    var data1 = (from t1 in db.Locations.Where(l => l.datetime >= fdate && l.datetime <= tdate && l.EmployeeType == null)
                                 join t2 in db.UserMasters on t1.userId equals t2.userId
                                 select new { t1.locId, t1.userId, t1.datetime, t1.address, t2.userName }).ToList();


                    if (userId > 0)
                    {
                        var model = data1.Where(c => c.userId == userId).ToList();

                        data1 = model.ToList();
                    }

                    foreach (var x in data1)
                    {
                        string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");

                        string t = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");

                        data.Add(new SBALocationGridRow
                        {
                            locId = x.locId,

                            //userName = db.UserMasters.FirstOrDefault(c => c.userId == x.userId).userName,
                            userId = Convert.ToInt32(x.userId),
                            userName = x.userName,
                            date = dat,
                            time = t,// Convert.ToDateTime(x.datetime).ToString("hh:mm:ss"),
                                     //string filtered = new string(original.SkipWhile(c => c == ';').ToArray());

                            latlong = checkNull(x.address).Replace("Unnamed Road, ", ""),
                            CompareDate = x.datetime,
                        });


                    }
                    foreach (var item in data)
                    {
                        if (item.userName != null && item.userName == "")
                            item.userName = "";
                        item.latlong = checkNull(item.latlong);
                        item.date = checkNull(item.date);
                        item.time = checkNull(item.time);


                    }
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        //var model = data.Where(c => c.userName.Contains(SearchString) || c.date.Contains(SearchString) || c.time.Contains(SearchString) || c.latlong.Contains(SearchString) 

                        //|| c.userName.ToLower().Contains(SearchString) || c.date.ToLower().Contains(SearchString) || c.time.ToLower().Contains(SearchString) || c.latlong.ToLower().Contains(SearchString) 

                        //|| c.userName.ToUpper().Contains(SearchString) || c.date.ToUpper().Contains(SearchString) || c.time.ToUpper().Contains(SearchString) || c.latlong.ToUpper().Contains(SearchString) 
                        //).ToList();

                        var model = data.Where(c => ((string.IsNullOrEmpty(c.userName) ? " " : c.userName) + " " +
                                         (string.IsNullOrEmpty(c.date) ? " " : c.date) + " " +
                                         (string.IsNullOrEmpty(c.time) ? " " : c.time) + " " +
                                         (string.IsNullOrEmpty(c.latlong) ? " " : c.latlong)).ToUpper().Contains(SearchString.ToUpper())).ToList();


                        data = model.OrderByDescending(c => c.date).ToList().ToList();
                    }
                    //if (!string.IsNullOrEmpty(fdate.ToString()))
                    //{

                    //    DateTime? dt1 = null;
                    //    if (!string.IsNullOrEmpty(tdate.ToString()))
                    //    { dt1 = tdate; }
                    //    else { dt1 = fdate; }
                    //    data = data.Where(fullEntry => fullEntry.CompareDate >= fdate && fullEntry.CompareDate <= dt1).OrderByDescending(c => c.CompareDate).ToList();

                    //}MyList.OrderBy(x => x.StartDate).ThenByDescending(x => x.EndDate);
                    //if (userId > 0)
                    //{
                    //    var model = data.Where(c => c.userId == userId).ToList();

                    //    data = model.ToList();
                    //}
                    return data.OrderByDescending(c => c.CompareDate).ToList().ToList(); ;
                }
            }
            else if (Emptype == "L")
            {
                using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(appId))
                {
                    List<SBALocationGridRow> data = new List<SBALocationGridRow>();
                    //  var data = db.GarbageCollectionDetails.Where(x => x.gcType == 3 & x.gcDate >= fdate & x.gcDate <= tdate).Select(x => new SBAGrabageCollectionGridRow


                    //var data1 = db.Locations.Where(l => l.datetime >= fdate && l.datetime <= tdate)
                    //            .Join(db.UserMasters, u => u.userId, i => i.userId



                    var data1 = (from t1 in db.Locations.Where(l => l.datetime >= fdate && l.datetime <= tdate && l.EmployeeType == "L")
                                 join t2 in db.UserMasters on t1.userId equals t2.userId
                                 select new { t1.locId, t1.userId, t1.datetime, t1.address, t2.userName }).ToList();


                    if (userId > 0)
                    {
                        var model = data1.Where(c => c.userId == userId).ToList();

                        data1 = model.ToList();
                    }

                    foreach (var x in data1)
                    {
                        string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");

                        string t = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");

                        data.Add(new SBALocationGridRow
                        {
                            locId = x.locId,

                            //userName = db.UserMasters.FirstOrDefault(c => c.userId == x.userId).userName,
                            userId = Convert.ToInt32(x.userId),
                            userName = x.userName,
                            date = dat,
                            time = t,// Convert.ToDateTime(x.datetime).ToString("hh:mm:ss"),
                                     //string filtered = new string(original.SkipWhile(c => c == ';').ToArray());

                            latlong = checkNull(x.address).Replace("Unnamed Road, ", ""),
                            CompareDate = x.datetime,
                        });


                    }
                    foreach (var item in data)
                    {
                        if (item.userName != null && item.userName == "")
                            item.userName = "";
                        item.latlong = checkNull(item.latlong);
                        item.date = checkNull(item.date);
                        item.time = checkNull(item.time);


                    }
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        //var model = data.Where(c => c.userName.Contains(SearchString) || c.date.Contains(SearchString) || c.time.Contains(SearchString) || c.latlong.Contains(SearchString) 

                        //|| c.userName.ToLower().Contains(SearchString) || c.date.ToLower().Contains(SearchString) || c.time.ToLower().Contains(SearchString) || c.latlong.ToLower().Contains(SearchString) 

                        //|| c.userName.ToUpper().Contains(SearchString) || c.date.ToUpper().Contains(SearchString) || c.time.ToUpper().Contains(SearchString) || c.latlong.ToUpper().Contains(SearchString) 
                        //).ToList();

                        var model = data.Where(c => ((string.IsNullOrEmpty(c.userName) ? " " : c.userName) + " " +
                                         (string.IsNullOrEmpty(c.date) ? " " : c.date) + " " +
                                         (string.IsNullOrEmpty(c.time) ? " " : c.time) + " " +
                                         (string.IsNullOrEmpty(c.latlong) ? " " : c.latlong)).ToUpper().Contains(SearchString.ToUpper())).ToList();


                        data = model.OrderByDescending(c => c.date).ToList().ToList();
                    }
                    //if (!string.IsNullOrEmpty(fdate.ToString()))
                    //{

                    //    DateTime? dt1 = null;
                    //    if (!string.IsNullOrEmpty(tdate.ToString()))
                    //    { dt1 = tdate; }
                    //    else { dt1 = fdate; }
                    //    data = data.Where(fullEntry => fullEntry.CompareDate >= fdate && fullEntry.CompareDate <= dt1).OrderByDescending(c => c.CompareDate).ToList();

                    //}MyList.OrderBy(x => x.StartDate).ThenByDescending(x => x.EndDate);
                    //if (userId > 0)
                    //{
                    //    var model = data.Where(c => c.userId == userId).ToList();

                    //    data = model.ToList();
                    //}
                    return data.OrderByDescending(c => c.CompareDate).ToList().ToList(); ;
                }
            }
            else if (Emptype == "S")
            {
                using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(appId))
                {
                    List<SBALocationGridRow> data = new List<SBALocationGridRow>();
                    //  var data = db.GarbageCollectionDetails.Where(x => x.gcType == 3 & x.gcDate >= fdate & x.gcDate <= tdate).Select(x => new SBAGrabageCollectionGridRow


                    //var data1 = db.Locations.Where(l => l.datetime >= fdate && l.datetime <= tdate)
                    //            .Join(db.UserMasters, u => u.userId, i => i.userId



                    var data1 = (from t1 in db.Locations.Where(l => l.datetime >= fdate && l.datetime <= tdate && l.EmployeeType == "S")
                                 join t2 in db.UserMasters on t1.userId equals t2.userId
                                 select new { t1.locId, t1.userId, t1.datetime, t1.address, t2.userName }).ToList();


                    if (userId > 0)
                    {
                        var model = data1.Where(c => c.userId == userId).ToList();

                        data1 = model.ToList();
                    }

                    foreach (var x in data1)
                    {
                        string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");

                        string t = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");

                        data.Add(new SBALocationGridRow
                        {
                            locId = x.locId,

                            //userName = db.UserMasters.FirstOrDefault(c => c.userId == x.userId).userName,
                            userId = Convert.ToInt32(x.userId),
                            userName = x.userName,
                            date = dat,
                            time = t,// Convert.ToDateTime(x.datetime).ToString("hh:mm:ss"),
                                     //string filtered = new string(original.SkipWhile(c => c == ';').ToArray());

                            latlong = checkNull(x.address).Replace("Unnamed Road, ", ""),
                            CompareDate = x.datetime,
                        });


                    }
                    foreach (var item in data)
                    {
                        if (item.userName != null && item.userName == "")
                            item.userName = "";
                        item.latlong = checkNull(item.latlong);
                        item.date = checkNull(item.date);
                        item.time = checkNull(item.time);


                    }
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        //var model = data.Where(c => c.userName.Contains(SearchString) || c.date.Contains(SearchString) || c.time.Contains(SearchString) || c.latlong.Contains(SearchString) 

                        //|| c.userName.ToLower().Contains(SearchString) || c.date.ToLower().Contains(SearchString) || c.time.ToLower().Contains(SearchString) || c.latlong.ToLower().Contains(SearchString) 

                        //|| c.userName.ToUpper().Contains(SearchString) || c.date.ToUpper().Contains(SearchString) || c.time.ToUpper().Contains(SearchString) || c.latlong.ToUpper().Contains(SearchString) 
                        //).ToList();

                        var model = data.Where(c => ((string.IsNullOrEmpty(c.userName) ? " " : c.userName) + " " +
                                         (string.IsNullOrEmpty(c.date) ? " " : c.date) + " " +
                                         (string.IsNullOrEmpty(c.time) ? " " : c.time) + " " +
                                         (string.IsNullOrEmpty(c.latlong) ? " " : c.latlong)).ToUpper().Contains(SearchString.ToUpper())).ToList();


                        data = model.OrderByDescending(c => c.date).ToList().ToList();
                    }
                    //if (!string.IsNullOrEmpty(fdate.ToString()))
                    //{

                    //    DateTime? dt1 = null;
                    //    if (!string.IsNullOrEmpty(tdate.ToString()))
                    //    { dt1 = tdate; }
                    //    else { dt1 = fdate; }
                    //    data = data.Where(fullEntry => fullEntry.CompareDate >= fdate && fullEntry.CompareDate <= dt1).OrderByDescending(c => c.CompareDate).ToList();

                    //}MyList.OrderBy(x => x.StartDate).ThenByDescending(x => x.EndDate);
                    //if (userId > 0)
                    //{
                    //    var model = data.Where(c => c.userId == userId).ToList();

                    //    data = model.ToList();
                    //}
                    return data.OrderByDescending(c => c.CompareDate).ToList().ToList(); ;
                }
            }
            else
            {
                using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(appId))
                {
                    List<SBALocationGridRow> data = new List<SBALocationGridRow>();
                    //  var data = db.GarbageCollectionDetails.Where(x => x.gcType == 3 & x.gcDate >= fdate & x.gcDate <= tdate).Select(x => new SBAGrabageCollectionGridRow


                    //var data1 = db.Locations.Where(l => l.datetime >= fdate && l.datetime <= tdate)
                    //            .Join(db.UserMasters, u => u.userId, i => i.userId



                    var data1 = (from t1 in db.Locations.Where(l => l.datetime >= fdate && l.datetime <= tdate)
                                 join t2 in db.UserMasters on t1.userId equals t2.userId
                                 select new { t1.locId, t1.userId, t1.datetime, t1.address, t2.userName }).ToList();


                    if (userId > 0)
                    {
                        var model = data1.Where(c => c.userId == userId).ToList();

                        data1 = model.ToList();
                    }

                    foreach (var x in data1)
                    {
                        string dat = Convert.ToDateTime(x.datetime).ToString("dd/MM/yyyy");

                        string t = Convert.ToDateTime(x.datetime).ToString("hh:mm tt");

                        data.Add(new SBALocationGridRow
                        {
                            locId = x.locId,

                            //userName = db.UserMasters.FirstOrDefault(c => c.userId == x.userId).userName,
                            userId = Convert.ToInt32(x.userId),
                            userName = x.userName,
                            date = dat,
                            time = t,// Convert.ToDateTime(x.datetime).ToString("hh:mm:ss"),
                                     //string filtered = new string(original.SkipWhile(c => c == ';').ToArray());

                            latlong = checkNull(x.address).Replace("Unnamed Road, ", ""),
                            CompareDate = x.datetime,
                        });


                    }
                    foreach (var item in data)
                    {
                        if (item.userName != null && item.userName == "")
                            item.userName = "";
                        item.latlong = checkNull(item.latlong);
                        item.date = checkNull(item.date);
                        item.time = checkNull(item.time);


                    }
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        //var model = data.Where(c => c.userName.Contains(SearchString) || c.date.Contains(SearchString) || c.time.Contains(SearchString) || c.latlong.Contains(SearchString) 

                        //|| c.userName.ToLower().Contains(SearchString) || c.date.ToLower().Contains(SearchString) || c.time.ToLower().Contains(SearchString) || c.latlong.ToLower().Contains(SearchString) 

                        //|| c.userName.ToUpper().Contains(SearchString) || c.date.ToUpper().Contains(SearchString) || c.time.ToUpper().Contains(SearchString) || c.latlong.ToUpper().Contains(SearchString) 
                        //).ToList();

                        var model = data.Where(c => ((string.IsNullOrEmpty(c.userName) ? " " : c.userName) + " " +
                                         (string.IsNullOrEmpty(c.date) ? " " : c.date) + " " +
                                         (string.IsNullOrEmpty(c.time) ? " " : c.time) + " " +
                                         (string.IsNullOrEmpty(c.latlong) ? " " : c.latlong)).ToUpper().Contains(SearchString.ToUpper())).ToList();


                        data = model.OrderByDescending(c => c.date).ToList().ToList();
                    }
                    //if (!string.IsNullOrEmpty(fdate.ToString()))
                    //{

                    //    DateTime? dt1 = null;
                    //    if (!string.IsNullOrEmpty(tdate.ToString()))
                    //    { dt1 = tdate; }
                    //    else { dt1 = fdate; }
                    //    data = data.Where(fullEntry => fullEntry.CompareDate >= fdate && fullEntry.CompareDate <= dt1).OrderByDescending(c => c.CompareDate).ToList();

                    //}MyList.OrderBy(x => x.StartDate).ThenByDescending(x => x.EndDate);
                    //if (userId > 0)
                    //{
                    //    var model = data.Where(c => c.userId == userId).ToList();

                    //    data = model.ToList();
                    //}
                    return data.OrderByDescending(c => c.CompareDate).ToList().ToList(); ;
                }
            }
        }
        public IEnumerable<SBAGrabageCollectionGridRow> GetPointGarbageCollectionData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId, int? param1, int? param2, int? param3)
        {
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();
                string ThumbnaiUrlAPI = appDetails.baseImageUrl + appDetails.basePath + appDetails.Collection + "/";
                List<SBAGrabageCollectionGridRow> data = new List<SBAGrabageCollectionGridRow>();
                using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(appId))
                {



                    var data1 = (from t1 in db.GarbageCollectionDetails.Where(g => g.gcType == 2 & g.gcDate >= fdate & g.gcDate <= tdate)
                                 join t2 in db.UserMasters on t1.userId equals t2.userId
                                 join gp in db.GarbagePointDetails on t1.gpId equals gp.gpId into gpp
                                 from t3 in gpp.DefaultIfEmpty()
                                 join zm in db.ZoneMasters on t3.zoneId equals zm.zoneId into zm
                                 from t4 in zm.DefaultIfEmpty()
                                 join wm in db.WardNumbers on t3.wardId equals wm.Id into wm
                                 from t5 in wm.DefaultIfEmpty()
                                 join tm in db.TeritoryMasters on t3.areaId equals tm.Id into tm
                                 from t6 in tm.DefaultIfEmpty()
                                 where (t4.zoneId == param1 || param1 == 0 || param1 == null) && (t3.wardId == param2 || param2 == 0 || param2 == null) && (t3.areaId == param3 || param3 == 0 || param3 == null)

                                 select new
                                 {
                                     t1.gcId,
                                     t1.note,
                                     t1.gpAfterImage,
                                     t1.gpBeforImage,
                                     t1.gcType,
                                     t1.gpId,
                                     t1.userId,
                                     t1.gcDate,
                                     t1.vehicleNumber,
                                     t1.locAddresss,
                                     t1.batteryStatus,
                                     t1.Lat,
                                     t1.Long,
                                     t2.userName,
                                     t3.ReferanceId,
                                     t3.gpName,
                                     t3.zoneId,
                                     t3.wardId,
                                     t3.areaId,
                                     WardName = t5.WardNo,
                                     AreaName = t6.Area,
                                 }).ToList();



                    //var data1 = (from GCD in db.GarbageCollectionDetails
                    //             join _p1 in db.UserMasters on GCD.userId equals _p1.userId into _p1
                    //             from p1 in _p1.DefaultIfEmpty()
                    //             join _p2 in db.GarbagePointDetails on GCD.gpId equals _p2.gpId into _p2
                    //             from p2 in _p2.DefaultIfEmpty()
                    //             join _p3 in db.ZoneMasters on p2.zoneId equals _p3.zoneId into _p3
                    //             from p3 in _p3.DefaultIfEmpty()
                    //             join _p4 in db.WardNumbers on p2.wardId equals _p4.Id into _p4
                    //             from p4 in _p4.DefaultIfEmpty()
                    //             join _p5 in db.TeritoryMasters on p2.areaId equals _p5.Id into _p5
                    //             from p5 in _p5.DefaultIfEmpty()
                    //             where (GCD.gcType == 2 & GCD.gcDate >= fdate & GCD.gcDate <= tdate) && (p2.zoneId == param1 || param1 == 0 || param1 == null) && (p2.wardId == param2 || param2 == 0 || param2 == null) && (p2.areaId == param3 || param3 == 0 || param3 == null)
                    //            && (p1 != null || p2 != null)
                    //             select new
                    //             {
                    //                 GCD.gcId,GCD.note, GCD.gpAfterImage,GCD.gpBeforImage,GCD.gcType,GCD.gpId,GCD.userId,GCD.gcDate,GCD.vehicleNumber, GCD.locAddresss,
                    //                 GCD.batteryStatus,GCD.Lat, GCD.Long,p1.userName,p2.ReferanceId, p2.gpName, p2.zoneId,p2.wardId,p2.areaId,WardName = p4.WardNo,
                    //                 AreaName = p5.Area,
                    //             }).ToList();
                    if (userId > 0)
                    {
                        var model = data1.Where(c => c.userId == userId).ToList();

                        data1 = model.ToList();
                    }
                    foreach (var x in data1)
                    {
                        //var gcdata = db.GarbageCollectionDetails.Where(c => c.gcId == item.Id).FirstOrDefault();
                        //item.ReferanceId = db.GarbagePointDetails.Where(c => c.gpId == item.gpIdfk).FirstOrDefault().ReferanceId;
                        //item.Employee = db.UserMasters.Where(c => c.userId == item.userId).FirstOrDefault().userName;
                        //item.attandDate = Convert.ToDateTime(item.gcDate).ToString("dd/MM/yyyy hh:mm tt");
                        //item.UserName = db.GarbagePointDetails.Where(c => c.gpId == item.gpIdfk).FirstOrDefault().gpName;
                        //item.HouseNumber = checkNull(item.HouseNumber);
                        //item.VehicleNumber = checkNull(item.VehicleNumber);
                        //item.Note = checkNull(item.Note);
                        //if (gcdata.Lat != null && gcdata.Long != "" && gcdata.Lat != "" && gcdata.Long != null)
                        //{ item.Address = gcdata.locAddresss; }
                        //else { item.Address = ""; }
                        //item.Employee = checkNull(item.Employee);  
                        //item.UserName = checkNull(item.UserName);
                        //if ( item.gpAfterImage == "")
                        //{ item.gpAfterImage = "/Images/default_not_upload.png"; }
                        //else
                        //{
                        //    item.gpAfterImage = ThumbnaiUrlAPI + item.gpAfterImage.Trim();
                        //}
                        //if ( item.gpBeforImage == "")
                        //{ item.gpBeforImage = "/Images/default_not_upload.png"; }
                        //else
                        //{ item.gpBeforImage = ThumbnaiUrlAPI + item.gpBeforImage.Trim(); }
                        //item.UserName = checkNull(item.UserName);
                        //item.HouseNumber = checkNull(item.HouseNumber);
                        //item.VehicleNumber = checkNull(item.VehicleNumber);
                        //item.Employee = checkNull(item.Employee);
                        //item.Address = checkNull(item.Address);
                        //item.Note = checkNull(item.Note);
                        //item.ReferanceId = checkNull(item.ReferanceId);

                        data.Add(new SBAGrabageCollectionGridRow
                        {
                            Id = x.gcId,
                            Note = checkNull(x.note),
                            gcType = x.gcType,
                            //houseId = x.houseId,
                            gpIdfk = x.gpId,
                            userId = x.userId,
                            gcDate = x.gcDate,
                            VehicleNumber = checkNull(x.vehicleNumber),
                            batteryStatus = x.batteryStatus,
                            ReferanceId = checkNull(x.ReferanceId),
                            Employee = checkNull(x.userName),
                            attandDate = Convert.ToDateTime(x.gcDate).ToString("dd/MM/yyyy hh:mm tt"),
                            UserName = checkNull(x.gpName),
                            Lat = x.Lat,
                            Long = x.Long,
                            Address = checkNull(x.locAddresss).Replace("Unnamed Road,", ""),
                            gpAfterImage = (x.gpAfterImage == "" ? "/Images/default_not_upload.png" : ThumbnaiUrlAPI + x.gpAfterImage.Trim()),
                            gpBeforImage = (x.gpBeforImage == "" ? "/Images/default_not_upload.png" : ThumbnaiUrlAPI + x.gpBeforImage.Trim())

                        });

                        foreach (var item in data)
                        {
                            if (item.Lat != null && item.Long != "" && item.Lat != "" && item.Long != null)
                            { item.Address = item.Address; }
                            else { item.Address = ""; }
                        }

                    }
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        var model = data.Where(c => ((string.IsNullOrEmpty(c.UserName) ? " " : c.UserName) + " " +
                                      (string.IsNullOrEmpty(c.HouseNumber) ? " " : c.HouseNumber) + " " +
                                      (string.IsNullOrEmpty(c.VehicleNumber) ? " " : c.VehicleNumber) + " " +
                                      (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId) + " " +
                                      (string.IsNullOrEmpty(c.Address) ? " " : c.Address) + " " +
                                      (string.IsNullOrEmpty(c.Employee) ? " " : c.Employee) + " " +
                                      (string.IsNullOrEmpty(c.attandDate) ? " " : c.attandDate) + " " +
                                      (string.IsNullOrEmpty(c.Note) ? " " : c.Note)).ToUpper().Contains(SearchString.ToUpper())).ToList();
                        data = model.ToList();
                    }

                    return data.OrderByDescending(c => c.Id);
                }
            }
        }


        public IEnumerable<SBAGrabageCollectionGridRow> GetLiquidGarbageCollectionData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId, int? param1, int? param2, int? param3)
        {
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();
                string ThumbnaiUrlAPI = appDetails.baseImageUrl + appDetails.basePath + appDetails.Collection + "/";
                List<SBAGrabageCollectionGridRow> data = new List<SBAGrabageCollectionGridRow>();
                using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(appId))
                {



                    var data1 = (from t1 in db.GarbageCollectionDetails.Where(g => g.gcType == 4 & g.gcDate >= fdate & g.gcDate <= tdate & g.EmployeeType == "L")
                                 join t2 in db.UserMasters on t1.userId equals t2.userId
                                 join gp in db.LiquidWasteDetails on t1.LWId equals gp.LWId into gpp
                                 from t3 in gpp.DefaultIfEmpty()
                                 join zm in db.ZoneMasters on t3.zoneId equals zm.zoneId into zm
                                 from t4 in zm.DefaultIfEmpty()
                                 join wm in db.WardNumbers on t3.wardId equals wm.Id into wm
                                 from t5 in wm.DefaultIfEmpty()
                                 join tm in db.TeritoryMasters on t3.areaId equals tm.Id into tm
                                 from t6 in tm.DefaultIfEmpty()
                                 where (t4.zoneId == param1 || param1 == 0 || param1 == null) && (t3.wardId == param2 || param2 == 0 || param2 == null) && (t3.areaId == param3 || param3 == 0 || param3 == null)

                                 select new
                                 {
                                     t1.gcId,
                                     t1.note,
                                     t1.gpAfterImage,
                                     t1.gpBeforImage,
                                     t1.gcType,
                                     t1.gpId,
                                     t1.userId,
                                     t1.gcDate,
                                     t1.vehicleNumber,
                                     t1.locAddresss,
                                     t1.batteryStatus,
                                     t1.Lat,
                                     t1.Long,
                                     t2.userName,
                                     t3.ReferanceId,
                                     t3.LWName,
                                     t3.zoneId,
                                     t3.wardId,
                                     t3.areaId,
                                     WardName = t5.WardNo,
                                     AreaName = t6.Area,
                                 }).ToList();



                    //var data1 = (from GCD in db.GarbageCollectionDetails
                    //             join _p1 in db.UserMasters on GCD.userId equals _p1.userId into _p1
                    //             from p1 in _p1.DefaultIfEmpty()
                    //             join _p2 in db.GarbagePointDetails on GCD.gpId equals _p2.gpId into _p2
                    //             from p2 in _p2.DefaultIfEmpty()
                    //             join _p3 in db.ZoneMasters on p2.zoneId equals _p3.zoneId into _p3
                    //             from p3 in _p3.DefaultIfEmpty()
                    //             join _p4 in db.WardNumbers on p2.wardId equals _p4.Id into _p4
                    //             from p4 in _p4.DefaultIfEmpty()
                    //             join _p5 in db.TeritoryMasters on p2.areaId equals _p5.Id into _p5
                    //             from p5 in _p5.DefaultIfEmpty()
                    //             where (GCD.gcType == 2 & GCD.gcDate >= fdate & GCD.gcDate <= tdate) && (p2.zoneId == param1 || param1 == 0 || param1 == null) && (p2.wardId == param2 || param2 == 0 || param2 == null) && (p2.areaId == param3 || param3 == 0 || param3 == null)
                    //            && (p1 != null || p2 != null)
                    //             select new
                    //             {
                    //                 GCD.gcId,GCD.note, GCD.gpAfterImage,GCD.gpBeforImage,GCD.gcType,GCD.gpId,GCD.userId,GCD.gcDate,GCD.vehicleNumber, GCD.locAddresss,
                    //                 GCD.batteryStatus,GCD.Lat, GCD.Long,p1.userName,p2.ReferanceId, p2.gpName, p2.zoneId,p2.wardId,p2.areaId,WardName = p4.WardNo,
                    //                 AreaName = p5.Area,
                    //             }).ToList();
                    if (userId > 0)
                    {
                        var model = data1.Where(c => c.userId == userId).ToList();

                        data1 = model.ToList();
                    }
                    foreach (var x in data1)
                    {
                        //var gcdata = db.GarbageCollectionDetails.Where(c => c.gcId == item.Id).FirstOrDefault();
                        //item.ReferanceId = db.GarbagePointDetails.Where(c => c.gpId == item.gpIdfk).FirstOrDefault().ReferanceId;
                        //item.Employee = db.UserMasters.Where(c => c.userId == item.userId).FirstOrDefault().userName;
                        //item.attandDate = Convert.ToDateTime(item.gcDate).ToString("dd/MM/yyyy hh:mm tt");
                        //item.UserName = db.GarbagePointDetails.Where(c => c.gpId == item.gpIdfk).FirstOrDefault().gpName;
                        //item.HouseNumber = checkNull(item.HouseNumber);
                        //item.VehicleNumber = checkNull(item.VehicleNumber);
                        //item.Note = checkNull(item.Note);
                        //if (gcdata.Lat != null && gcdata.Long != "" && gcdata.Lat != "" && gcdata.Long != null)
                        //{ item.Address = gcdata.locAddresss; }
                        //else { item.Address = ""; }
                        //item.Employee = checkNull(item.Employee);  
                        //item.UserName = checkNull(item.UserName);
                        //if ( item.gpAfterImage == "")
                        //{ item.gpAfterImage = "/Images/default_not_upload.png"; }
                        //else
                        //{
                        //    item.gpAfterImage = ThumbnaiUrlAPI + item.gpAfterImage.Trim();
                        //}
                        //if ( item.gpBeforImage == "")
                        //{ item.gpBeforImage = "/Images/default_not_upload.png"; }
                        //else
                        //{ item.gpBeforImage = ThumbnaiUrlAPI + item.gpBeforImage.Trim(); }
                        //item.UserName = checkNull(item.UserName);
                        //item.HouseNumber = checkNull(item.HouseNumber);
                        //item.VehicleNumber = checkNull(item.VehicleNumber);
                        //item.Employee = checkNull(item.Employee);
                        //item.Address = checkNull(item.Address);
                        //item.Note = checkNull(item.Note);
                        //item.ReferanceId = checkNull(item.ReferanceId);

                        data.Add(new SBAGrabageCollectionGridRow
                        {
                            Id = x.gcId,
                            Note = checkNull(x.note),
                            gcType = x.gcType,
                            //houseId = x.houseId,
                            gpIdfk = x.gpId,
                            userId = x.userId,
                            gcDate = x.gcDate,
                            VehicleNumber = checkNull(x.vehicleNumber),
                            batteryStatus = x.batteryStatus,
                            ReferanceId = checkNull(x.ReferanceId),
                            Employee = checkNull(x.userName),
                            attandDate = Convert.ToDateTime(x.gcDate).ToString("dd/MM/yyyy hh:mm tt"),
                            UserName = checkNull(x.LWName),
                            Lat = x.Lat,
                            Long = x.Long,
                            Address = checkNull(x.locAddresss).Replace("Unnamed Road,", ""),
                            //gpBeforImage = x.gpBeforImage,
                            //gpAfterImage = x.gpAfterImage,
                            gpAfterImage = (x.gpAfterImage == ""  || x.gpAfterImage is null ? "/Images/default_not_upload.png" : x.gpAfterImage.Trim()),
                            gpBeforImage = (x.gpBeforImage == "" || x.gpBeforImage is null ? "/Images/default_not_upload.png" : x.gpBeforImage.Trim())

                        });

                        foreach (var item in data)
                        {
                            if (item.Lat != null && item.Long != "" && item.Lat != "" && item.Long != null)
                            { item.Address = item.Address; }
                            else { item.Address = ""; }
                        }

                    }
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        var model = data.Where(c => ((string.IsNullOrEmpty(c.UserName) ? " " : c.UserName) + " " +
                                      (string.IsNullOrEmpty(c.HouseNumber) ? " " : c.HouseNumber) + " " +
                                      (string.IsNullOrEmpty(c.VehicleNumber) ? " " : c.VehicleNumber) + " " +
                                      (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId) + " " +
                                      (string.IsNullOrEmpty(c.Address) ? " " : c.Address) + " " +
                                      (string.IsNullOrEmpty(c.Employee) ? " " : c.Employee) + " " +
                                      (string.IsNullOrEmpty(c.attandDate) ? " " : c.attandDate) + " " +
                                      (string.IsNullOrEmpty(c.Note) ? " " : c.Note)).ToUpper().Contains(SearchString.ToUpper())).ToList();
                        data = model.ToList();
                    }

                    return data.OrderByDescending(c => c.Id);
                }
            }
        }
        public IEnumerable<SBAGrabageCollectionGridRow> GetSSCollectionData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId, int? param1, int? param2, int? param3)
        {
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();
                string ThumbnaiUrlAPI = appDetails.baseImageUrl + appDetails.basePath + appDetails.Collection + "/";
                List<SBAGrabageCollectionGridRow> data = new List<SBAGrabageCollectionGridRow>();
                using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(appId))
                {



                    var data1 = (from t1 in db.GarbageCollectionDetails.Where(g => g.gcType == 5 & g.gcDate >= fdate & g.gcDate <= tdate & g.EmployeeType == "S")
                                 join t2 in db.UserMasters on t1.userId equals t2.userId
                                 join gp in db.StreetSweepingDetails on t1.SSId equals gp.SSId into gpp
                                 from t3 in gpp.DefaultIfEmpty()
                                 join zm in db.ZoneMasters on t3.zoneId equals zm.zoneId into zm
                                 from t4 in zm.DefaultIfEmpty()
                                 join wm in db.WardNumbers on t3.wardId equals wm.Id into wm
                                 from t5 in wm.DefaultIfEmpty()
                                 join tm in db.TeritoryMasters on t3.areaId equals tm.Id into tm
                                 from t6 in tm.DefaultIfEmpty()
                                 where (t4.zoneId == param1 || param1 == 0 || param1 == null) && (t3.wardId == param2 || param2 == 0 || param2 == null) && (t3.areaId == param3 || param3 == 0 || param3 == null)

                                 select new
                                 {
                                     t1.gcId,
                                     t1.note,
                                     t1.gpAfterImage,
                                     t1.gpBeforImage,
                                     t1.gcType,
                                     t1.gpId,
                                     t1.userId,
                                     t1.gcDate,
                                     t1.vehicleNumber,
                                     t1.locAddresss,
                                     t1.batteryStatus,
                                     t1.Lat,
                                     t1.Long,
                                     t2.userName,
                                     t3.ReferanceId,
                                     t3.SSName,
                                     t3.zoneId,
                                     t3.wardId,
                                     t3.areaId,
                                     WardName = t5.WardNo,
                                     AreaName = t6.Area,
                                 }).ToList();



                    if (userId > 0)
                    {
                        var model = data1.Where(c => c.userId == userId).ToList();

                        data1 = model.ToList();
                    }
                    foreach (var x in data1)
                    {
                        

                        data.Add(new SBAGrabageCollectionGridRow
                        {
                            Id = x.gcId,
                            Note = checkNull(x.note),
                            gcType = x.gcType,
                            //houseId = x.houseId,
                            gpIdfk = x.gpId,
                            userId = x.userId,
                            gcDate = x.gcDate,
                            VehicleNumber = checkNull(x.vehicleNumber),
                            batteryStatus = x.batteryStatus,
                            ReferanceId = checkNull(x.ReferanceId),
                            Employee = checkNull(x.userName),
                            attandDate = Convert.ToDateTime(x.gcDate).ToString("dd/MM/yyyy hh:mm tt"),
                            UserName = checkNull(x.SSName),
                            Lat = x.Lat,
                            Long = x.Long,
                            Address = checkNull(x.locAddresss).Replace("Unnamed Road,", ""),
                            gpAfterImage = (x.gpAfterImage == "" || x.gpAfterImage is null ? "/Images/default_not_upload.png" :  x.gpAfterImage.Trim()),
                            gpBeforImage = (x.gpBeforImage == "" || x.gpAfterImage is null ? "/Images/default_not_upload.png" :  x.gpBeforImage.Trim())

                        });

                        foreach (var item in data)
                        {
                            if (item.Lat != null && item.Long != "" && item.Lat != "" && item.Long != null)
                            { item.Address = item.Address; }
                            else { item.Address = ""; }
                        }

                    }
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        var model = data.Where(c => ((string.IsNullOrEmpty(c.UserName) ? " " : c.UserName) + " " +
                                      (string.IsNullOrEmpty(c.HouseNumber) ? " " : c.HouseNumber) + " " +
                                      (string.IsNullOrEmpty(c.VehicleNumber) ? " " : c.VehicleNumber) + " " +
                                      (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId) + " " +
                                      (string.IsNullOrEmpty(c.Address) ? " " : c.Address) + " " +
                                      (string.IsNullOrEmpty(c.Employee) ? " " : c.Employee) + " " +
                                      (string.IsNullOrEmpty(c.attandDate) ? " " : c.attandDate) + " " +
                                      (string.IsNullOrEmpty(c.Note) ? " " : c.Note)).ToUpper().Contains(SearchString.ToUpper())).ToList();
                        data = model.ToList();
                    }

                    return data.OrderByDescending(c => c.Id);
                }
            }
        }
        public IEnumerable<SBAGrabageCollectionGridRow> GetDumpYardCollectionData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId, int? param1, int? param2, int? param3)
        {
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                List<SBAGrabageCollectionGridRow> data = new List<SBAGrabageCollectionGridRow>();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();
                //string ThumbnaiUrlAPI = appDetails.baseImageUrl + appDetails.basePath + appDetails.Collection + "/";

                using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(appId))
                {

                    //var data1 = (from t1 in db.GarbageCollectionDetails.Where(g => g.gcType == 3 & g.gcDate >= fdate & g.gcDate <= tdate)
                    //             join t2 in db.UserMasters on t1.userId equals t2.userId
                    //             join dy in db.DumpYardDetails on t1.dyId equals dy.dyId into dy
                    //             from t3 in dy.DefaultIfEmpty()
                    //             from zm in db.ZoneMasters
                    //             join dyy in db.DumpYardDetails on zm.zoneId equals dyy.zoneId into dyy
                    //             from t4 in dyy.DefaultIfEmpty()
                    //             join wm in db.WardNumbers on t3.wardId equals wm.Id into wm
                    //             from t5 in wm.DefaultIfEmpty()
                    //             join tm in db.TeritoryMasters on t3.areaId equals tm.Id into tm
                    //             from t6 in tm.DefaultIfEmpty()
                    //             where (t4.zoneId == param1 || param1 == 0 || param1 == null) && (t3.wardId == param2 || param2 == 0 || param2 == null) && (t3.areaId == param3 || param3 == 0 || param3 == null)

                    var data1 = (from t1 in db.GarbageCollectionDetails.Where(g => g.gcType == 3 & g.gcDate >= fdate & g.gcDate <= tdate & g.EmployeeType==null)
                                 join t2 in db.UserMasters on t1.userId equals t2.userId
                                 join dy in db.DumpYardDetails on t1.dyId equals dy.dyId into dy
                                 from t3 in dy.DefaultIfEmpty()
                                 join zm in db.ZoneMasters on t3.zoneId equals zm.zoneId into zm
                                 from t4 in zm.DefaultIfEmpty()
                                 join wm in db.WardNumbers on t3.wardId equals wm.Id into wm
                                 from t5 in wm.DefaultIfEmpty()
                                 join tm in db.TeritoryMasters on t3.areaId equals tm.Id into tm
                                 from t6 in tm.DefaultIfEmpty()
                                 where (t3.zoneId == param1 || param1 == 0 || param1 == null) && (t5.Id == param2 || param2 == 0 || param2 == null) && (t6.Id == param3 || param3 == 0 || param3 == null)
                                 select new
                                 {
                                     t1.gcId,
                                     t1.note,
                                     t1.gpAfterImage,
                                     t1.gpBeforImage,
                                     t1.gcType,
                                     t1.dyId,
                                     t1.userId,
                                     t1.gcDate,
                                     t1.vehicleNumber,
                                     t1.locAddresss,
                                     t1.totalGcWeight,
                                     t1.totalDryWeight,
                                     t1.totalWetWeight,
                                     t1.batteryStatus,
                                     t1.Lat,
                                     t1.Long,
                                     t2.userName,
                                     t3.ReferanceId,
                                     t3.dyName,
                                     t3.zoneId,
                                     t3.wardId,
                                     t3.areaId,
                                     WardName = t5.WardNo,
                                     AreaName = t6.Area
                                 }).ToList();

                    if (userId > 0)

                    {
                        var model = data1.Where(c => c.userId == userId).ToList();

                        data1 = model.ToList();
                    }
                    foreach (var x in data1)
                    {
                        data.Add(new SBAGrabageCollectionGridRow
                        {
                            Id = x.gcId,
                            Note = checkNull(x.note),
                            gcType = x.gcType,
                            //houseId = x.houseId,
                            dyIdfk = x.dyId,
                            userId = x.userId,
                            gcDate = x.gcDate,
                            VehicleNumber = checkNull(x.vehicleNumber),
                            totalGcWeight = (x.totalDryWeight + x.totalWetWeight),
                            totalDryWeight = x.totalDryWeight,
                            totalWetWeight = x.totalWetWeight,
                            batteryStatus = x.batteryStatus,
                            ReferanceId = checkNull(x.ReferanceId),
                            Employee = checkNull(x.userName),
                            attandDate = Convert.ToDateTime(x.gcDate).ToString("dd/MM/yyyy hh:mm tt"),
                            UserName = checkNull(x.dyName),
                            Lat = x.Lat,
                            Long = x.Long,
                            Address = checkNull(x.locAddresss).Replace("Unnamed Road,", ""),
                            gpAfterImage = (x.gpAfterImage == "" || x.gpAfterImage == null ? "/Images/default_not_upload.png" : x.gpAfterImage.Trim()),
                            gpBeforImage = (x.gpBeforImage == "" || x.gpAfterImage == null ? "/Images/default_not_upload.png" : x.gpBeforImage.Trim())
                        });

                        foreach (var item in data)
                        {
                            if (item.Lat != null && item.Long != "" && item.Lat != "" && item.Long != null)
                            { item.Address = item.Address; }
                            else { item.Address = ""; }
                        }
                    }
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        var model = data.Where(c => ((string.IsNullOrEmpty(c.UserName) ? " " : c.UserName) + " " +
                                      (string.IsNullOrEmpty(c.HouseNumber) ? " " : c.HouseNumber) + " " +
                                      (string.IsNullOrEmpty(c.VehicleNumber) ? " " : c.VehicleNumber) + " " +
                                      (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId) + " " +
                                      (string.IsNullOrEmpty(c.Address) ? " " : c.Address) + " " +
                                      (string.IsNullOrEmpty(c.Employee) ? " " : c.Employee) + " " +
                                      (string.IsNullOrEmpty(c.attandDate) ? " " : c.attandDate) + " " +
                                      (string.IsNullOrEmpty(c.Note) ? " " : c.Note)).ToUpper().Contains(SearchString.ToUpper())).ToList();
                        data = model.ToList();
                    }
                    return data.OrderByDescending(c => c.gcDate).ToList();
                }
            }
        }

        public IEnumerable<SBAGrabageCollectionGridRow> GetLiquidDumpYardCollectionData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId, int? param1, int? param2, int? param3)
        {
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                List<SBAGrabageCollectionGridRow> data = new List<SBAGrabageCollectionGridRow>();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();
                string ThumbnaiUrlAPI = appDetails.baseImageUrl + appDetails.basePath + appDetails.Collection + "/";

                using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(appId))
                {

                    //var data1 = (from t1 in db.GarbageCollectionDetails.Where(g => g.gcType == 3 & g.gcDate >= fdate & g.gcDate <= tdate)
                    //             join t2 in db.UserMasters on t1.userId equals t2.userId
                    //             join dy in db.DumpYardDetails on t1.dyId equals dy.dyId into dy
                    //             from t3 in dy.DefaultIfEmpty()
                    //             from zm in db.ZoneMasters
                    //             join dyy in db.DumpYardDetails on zm.zoneId equals dyy.zoneId into dyy
                    //             from t4 in dyy.DefaultIfEmpty()
                    //             join wm in db.WardNumbers on t3.wardId equals wm.Id into wm
                    //             from t5 in wm.DefaultIfEmpty()
                    //             join tm in db.TeritoryMasters on t3.areaId equals tm.Id into tm
                    //             from t6 in tm.DefaultIfEmpty()
                    //             where (t4.zoneId == param1 || param1 == 0 || param1 == null) && (t3.wardId == param2 || param2 == 0 || param2 == null) && (t3.areaId == param3 || param3 == 0 || param3 == null)

                    var data1 = (from t1 in db.GarbageCollectionDetails.Where(g => g.gcType == 3 & g.gcDate >= fdate & g.gcDate <= tdate & g.EmployeeType == "L")
                                 join t2 in db.UserMasters on t1.userId equals t2.userId
                                 join dy in db.DumpYardDetails on t1.dyId equals dy.dyId into dy
                                 from t3 in dy.DefaultIfEmpty()
                                 join zm in db.ZoneMasters on t3.zoneId equals zm.zoneId into zm
                                 from t4 in zm.DefaultIfEmpty()
                                 join wm in db.WardNumbers on t3.wardId equals wm.Id into wm
                                 from t5 in wm.DefaultIfEmpty()
                                 join tm in db.TeritoryMasters on t3.areaId equals tm.Id into tm
                                 from t6 in tm.DefaultIfEmpty()
                                 where (t3.zoneId == param1 || param1 == 0 || param1 == null) && (t5.Id == param2 || param2 == 0 || param2 == null) && (t6.Id == param3 || param3 == 0 || param3 == null)
                                 select new
                                 {
                                     t1.gcId,
                                     t1.note,
                                     t1.gpAfterImage,
                                     t1.gpBeforImage,
                                     t1.gcType,
                                     t1.dyId,
                                     t1.userId,
                                     t1.gcDate,
                                     t1.vehicleNumber,
                                     t1.locAddresss,
                                     t1.totalGcWeight,
                                     t1.totalDryWeight,
                                     t1.totalWetWeight,
                                     t1.batteryStatus,
                                     t1.Lat,
                                     t1.Long,
                                     t2.userName,
                                     t3.ReferanceId,
                                     t3.dyName,
                                     t3.zoneId,
                                     t3.wardId,
                                     t3.areaId,
                                     WardName = t5.WardNo,
                                     AreaName = t6.Area
                                 }).ToList();

                    if (userId > 0)
                    {
                        var model = data1.Where(c => c.userId == userId).ToList();

                        data1 = model.ToList();
                    }
                    foreach (var x in data1)
                    {
                        data.Add(new SBAGrabageCollectionGridRow
                        {
                            Id = x.gcId,
                            Note = checkNull(x.note),
                            gcType = x.gcType,
                            //houseId = x.houseId,
                            dyIdfk = x.dyId,
                            userId = x.userId,
                            gcDate = x.gcDate,
                            VehicleNumber = checkNull(x.vehicleNumber),
                            totalGcWeight = (x.totalDryWeight + x.totalWetWeight),
                            totalDryWeight = x.totalDryWeight,
                            totalWetWeight = x.totalWetWeight,
                            batteryStatus = x.batteryStatus,
                            ReferanceId = checkNull(x.ReferanceId),
                            Employee = checkNull(x.userName),
                            attandDate = Convert.ToDateTime(x.gcDate).ToString("dd/MM/yyyy hh:mm tt"),
                            UserName = checkNull(x.dyName),
                            Lat = x.Lat,
                            Long = x.Long,
                            Address = checkNull(x.locAddresss).Replace("Unnamed Road,", ""),
                            gpAfterImage = (x.gpAfterImage == "" || x.gpAfterImage == null ? "/Images/default_not_upload.png" : x.gpAfterImage.Trim()),
                            gpBeforImage = (x.gpBeforImage == "" || x.gpAfterImage == null ? "/Images/default_not_upload.png" :  x.gpBeforImage.Trim())
                        });

                        foreach (var item in data)
                        {
                            if (item.Lat != null && item.Long != "" && item.Lat != "" && item.Long != null)
                            { item.Address = item.Address; }
                            else { item.Address = ""; }
                        }
                    }
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        var model = data.Where(c => ((string.IsNullOrEmpty(c.UserName) ? " " : c.UserName) + " " +
                                      (string.IsNullOrEmpty(c.HouseNumber) ? " " : c.HouseNumber) + " " +
                                      (string.IsNullOrEmpty(c.VehicleNumber) ? " " : c.VehicleNumber) + " " +
                                      (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId) + " " +
                                      (string.IsNullOrEmpty(c.Address) ? " " : c.Address) + " " +
                                      (string.IsNullOrEmpty(c.Employee) ? " " : c.Employee) + " " +
                                      (string.IsNullOrEmpty(c.attandDate) ? " " : c.attandDate) + " " +
                                      (string.IsNullOrEmpty(c.Note) ? " " : c.Note)).ToUpper().Contains(SearchString.ToUpper())).ToList();
                        data = model.ToList();
                    }
                    return data.OrderByDescending(c => c.gcDate).ToList();
                }
            }
        }

        public IEnumerable<SBAGrabageCollectionGridRow> GetStreetDumpYardCollectionData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId, int? param1, int? param2, int? param3)
        {
            {
                DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
                List<SBAGrabageCollectionGridRow> data = new List<SBAGrabageCollectionGridRow>();
                var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();
                string ThumbnaiUrlAPI = appDetails.baseImageUrl + appDetails.basePath + appDetails.Collection + "/";

                using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(appId))
                {

                    //var data1 = (from t1 in db.GarbageCollectionDetails.Where(g => g.gcType == 3 & g.gcDate >= fdate & g.gcDate <= tdate)
                    //             join t2 in db.UserMasters on t1.userId equals t2.userId
                    //             join dy in db.DumpYardDetails on t1.dyId equals dy.dyId into dy
                    //             from t3 in dy.DefaultIfEmpty()
                    //             from zm in db.ZoneMasters
                    //             join dyy in db.DumpYardDetails on zm.zoneId equals dyy.zoneId into dyy
                    //             from t4 in dyy.DefaultIfEmpty()
                    //             join wm in db.WardNumbers on t3.wardId equals wm.Id into wm
                    //             from t5 in wm.DefaultIfEmpty()
                    //             join tm in db.TeritoryMasters on t3.areaId equals tm.Id into tm
                    //             from t6 in tm.DefaultIfEmpty()
                    //             where (t4.zoneId == param1 || param1 == 0 || param1 == null) && (t3.wardId == param2 || param2 == 0 || param2 == null) && (t3.areaId == param3 || param3 == 0 || param3 == null)

                    var data1 = (from t1 in db.GarbageCollectionDetails.Where(g => g.gcType == 3 & g.gcDate >= fdate & g.gcDate <= tdate & g.EmployeeType == "S")
                                 join t2 in db.UserMasters on t1.userId equals t2.userId
                                 join dy in db.DumpYardDetails on t1.dyId equals dy.dyId into dy
                                 from t3 in dy.DefaultIfEmpty()
                                 join zm in db.ZoneMasters on t3.zoneId equals zm.zoneId into zm
                                 from t4 in zm.DefaultIfEmpty()
                                 join wm in db.WardNumbers on t3.wardId equals wm.Id into wm
                                 from t5 in wm.DefaultIfEmpty()
                                 join tm in db.TeritoryMasters on t3.areaId equals tm.Id into tm
                                 from t6 in tm.DefaultIfEmpty()
                                 where (t3.zoneId == param1 || param1 == 0 || param1 == null) && (t5.Id == param2 || param2 == 0 || param2 == null) && (t6.Id == param3 || param3 == 0 || param3 == null)
                                 select new
                                 {
                                     t1.gcId,
                                     t1.note,
                                     t1.gpAfterImage,
                                     t1.gpBeforImage,
                                     t1.gcType,
                                     t1.dyId,
                                     t1.userId,
                                     t1.gcDate,
                                     t1.vehicleNumber,
                                     t1.locAddresss,
                                     t1.totalGcWeight,
                                     t1.totalDryWeight,
                                     t1.totalWetWeight,
                                     t1.batteryStatus,
                                     t1.Lat,
                                     t1.Long,
                                     t2.userName,
                                     t3.ReferanceId,
                                     t3.dyName,
                                     t3.zoneId,
                                     t3.wardId,
                                     t3.areaId,
                                     WardName = t5.WardNo,
                                     AreaName = t6.Area
                                 }).ToList();

                    if (userId > 0)
                    {
                        var model = data1.Where(c => c.userId == userId).ToList();

                        data1 = model.ToList();
                    }
                    foreach (var x in data1)
                    {
                        data.Add(new SBAGrabageCollectionGridRow
                        {
                            Id = x.gcId,
                            Note = checkNull(x.note),
                            gcType = x.gcType,
                            //houseId = x.houseId,
                            dyIdfk = x.dyId,
                            userId = x.userId,
                            gcDate = x.gcDate,
                            VehicleNumber = checkNull(x.vehicleNumber),
                            totalGcWeight = (x.totalDryWeight + x.totalWetWeight),
                            totalDryWeight = x.totalDryWeight,
                            totalWetWeight = x.totalWetWeight,
                            batteryStatus = x.batteryStatus,
                            ReferanceId = checkNull(x.ReferanceId),
                            Employee = checkNull(x.userName),
                            attandDate = Convert.ToDateTime(x.gcDate).ToString("dd/MM/yyyy hh:mm tt"),
                            UserName = checkNull(x.dyName),
                            Lat = x.Lat,
                            Long = x.Long,
                            Address = checkNull(x.locAddresss).Replace("Unnamed Road,", ""),
                            gpAfterImage = (x.gpAfterImage == "" || x.gpAfterImage == null ? "/Images/default_not_upload.png" : x.gpAfterImage.Trim()),
                            gpBeforImage = (x.gpBeforImage == "" || x.gpAfterImage == null ? "/Images/default_not_upload.png" : x.gpBeforImage.Trim())
                        });

                        foreach (var item in data)
                        {
                            if (item.Lat != null && item.Long != "" && item.Lat != "" && item.Long != null)
                            { item.Address = item.Address; }
                            else { item.Address = ""; }
                        }
                    }
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        var model = data.Where(c => ((string.IsNullOrEmpty(c.UserName) ? " " : c.UserName) + " " +
                                      (string.IsNullOrEmpty(c.HouseNumber) ? " " : c.HouseNumber) + " " +
                                      (string.IsNullOrEmpty(c.VehicleNumber) ? " " : c.VehicleNumber) + " " +
                                      (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId) + " " +
                                      (string.IsNullOrEmpty(c.Address) ? " " : c.Address) + " " +
                                      (string.IsNullOrEmpty(c.Employee) ? " " : c.Employee) + " " +
                                      (string.IsNullOrEmpty(c.attandDate) ? " " : c.attandDate) + " " +
                                      (string.IsNullOrEmpty(c.Note) ? " " : c.Note)).ToUpper().Contains(SearchString.ToUpper())).ToList();
                        data = model.ToList();
                    }
                    return data.OrderByDescending(c => c.gcDate).ToList();
                }
            }
        }

        public IEnumerable<SBAGrabageCollectionGridRow> GetHouseGarbageCollectionData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId, int? param1, int? param2, int? param3, int? param4)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();
            string ThumbnaiUrlAPI = appDetails.baseImageUrl + appDetails.basePath + appDetails.Collection + "/";

            using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                //                var data = db.GarbageCollectionDetails.Where(x => x.gcType == 1 & x.gcDate>=fdate & x.gcDate<=tdate).Select(x => new SBAGrabageCollectionGridRow
                //                {
                //                    Id = x.gcId,
                //                    houseId = x.houseId,
                //                    userId = x.userId,
                //                    gcDate = x.gcDate,
                //                    gpIdfk = x.gpId,
                //                    VehicleNumber=x.vehicleNumber,
                //// by naz
                //                    Note = x.note,
                //                    gpAfterImage = x.gpAfterImage,
                //                    gpBeforImage = x.gpBeforImage,
                //                    gcType = x.gcType,   
                //                    type1=x.garbageType.ToString(),             
                //                }).OrderByDescending(c => c.gcDate).ToList().ToList();

                //                foreach (var item in data)
                //                {
                //                    var gcdata = db.GarbageCollectionDetails.Where(c => c.gcId == item.Id).FirstOrDefault();
                //                    item.ReferanceId = db.HouseMasters.Where(c => c.houseId == item.houseId).FirstOrDefault().ReferanceId;
                //                    item.attandDate = Convert.ToDateTime(item.gcDate).ToString("dd/MM/yyyy hh:mm tt");
                //                    item.UserName = db.HouseMasters.Where(c => c.houseId == item.houseId).FirstOrDefault().houseOwner;
                //                   // item.Address = db.HouseMasters.Where(c => c.houseId == item.houseId).FirstOrDefault().houseAddress;
                //                    item.HouseNumber = db.HouseMasters.Where(c => c.houseId == item.houseId).FirstOrDefault().houseNumber;
                //                    item.Employee = db.UserMasters.Where(c => c.userId == item.userId).FirstOrDefault().userName;
                //                    var dd = db.Daily_Attendance.Where(c => c.userId == item.userId).FirstOrDefault().daDate;                   
                //                    item.HouseNumber = checkNull(item.HouseNumber);
                //                    item.VehicleNumber = checkNull(item.VehicleNumber);
                //                    item.Note = checkNull(item.Note);
                //                    if (gcdata.Lat != null && gcdata.Long != "" && gcdata.Lat != "" && gcdata.Long != null)
                //                    { item.Address = gcdata.locAddresss; }
                //                    else { item.Address = ""; }

                //                    item.Employee = checkNull(item.Employee);
                //                    item.UserName = checkNull(item.UserName);
                //                    if (appDetails.NewFeatures == false || appDetails.NewFeatures==null)
                //                    {
                //                        item.type1 = "3";
                //                    }
                //                    else {
                //                        if (item.type1 != "null")
                //                        {
                //                            item.type1 = item.type1;
                //                        }

                //                        else {
                //                            item.type1 = "";
                //                        }
                //                    }
                //                       if ( item.gpAfterImage == "")
                //                        { item.gpAfterImage = "/Images/default_not_upload.png"; }
                //                        else
                //                        {
                //                            item.gpAfterImage = ThumbnaiUrlAPI + item.gpAfterImage.Trim();
                //                        }
                //                        if ( item.gpBeforImage == "")
                //                        { item.gpBeforImage = "/Images/default_not_upload.png"; }
                //                        else
                //                        { item.gpBeforImage = ThumbnaiUrlAPI + item.gpBeforImage.Trim(); }

                //                }


                var data = db.SP_GarbageCollection(appId, userId, fdate, tdate, param1, param2, param3,param4).Select(x => new SBAGrabageCollectionGridRow
                {
                    Id = x.gcId,
                    userId = x.userId,
                    houseId = x.houseId,
                    UserName = x.houseOwner,
                    HouseNumber = x.houseOwner,
                    gcDate = x.gcDate,
                   // gcType = 1,
                  
                    type1 = x.garbageType.ToString(),
                    Ctype =x.gcType.ToString(),
                    Address = (x.locAddresss).Replace("Unnamed Road,", ""),
                    gpBeforImage = x.gpBeforImage,
                    gpAfterImage = x.gpAfterImage,
                    VehicleNumber = x.vehicleNumber,
                    Note = x.note,
                    ReferanceId = x.ReferanceId,
                    Employee = x.userName,
                    attandDate = Convert.ToDateTime(x.gcDate).ToString("dd/MM/yyyy hh:mm tt"),
                    gpIdfk = x.gcId,
                    gpIdpk = x.gcId,
                    batteryStatus = x.batteryStatus,
                    los=x.los,
                    ctype=x.CType,


                }).OrderByDescending(c => c.gcDate).ToList().ToList();

                if (!string.IsNullOrEmpty(SearchString))
                {
                    //var model = data.Where(c => c.UserName.Contains(SearchString) || c.HouseNumber.Contains(SearchString) || c.VehicleNumber.Contains(SearchString) || c.ReferanceId.Contains(SearchString) || c.Address.Contains(SearchString) || c.Employee.Contains(SearchString) || c.attandDate.Contains(SearchString) || c.Note.Contains(SearchString)

                    //   || c.UserName.ToLower().Contains(SearchString) || c.HouseNumber.ToLower().Contains(SearchString) || c.VehicleNumber.ToLower().Contains(SearchString) || c.ReferanceId.ToLower().Contains(SearchString) || c.Address.ToLower().Contains(SearchString) || c.Employee.ToLower().Contains(SearchString) || c.attandDate.ToLower().Contains(SearchString) || c.Note.ToLower().Contains(SearchString)

                    //   || c.UserName.ToUpper().Contains(SearchString) || c.HouseNumber.ToUpper().Contains(SearchString) || c.VehicleNumber.ToUpper().Contains(SearchString) || c.ReferanceId.ToUpper().Contains(SearchString) || c.Address.ToUpper().Contains(SearchString) || c.Employee.ToUpper().Contains(SearchString) || c.attandDate.ToUpper().Contains(SearchString) || c.Note.ToUpper().Contains(SearchString)
                    //   ).ToList();
                    var model = data.Where(c => ((c.UserName == null ? " " : c.UserName) + " " + (c.HouseNumber == null ? " " : c.HouseNumber) + " " + (c.VehicleNumber == null ? " " : c.VehicleNumber) + " " + (c.ReferanceId == null ? "" : c.ReferanceId) + " " + (c.Address == null ? " " : c.Address) + " " + (c.Employee == null ? " " : c.Employee) + " " + (c.attandDate == null ? " " : c.attandDate) + " " + (c.Note == null ? " " : c.Note)).ToUpper().Contains(SearchString.ToUpper())
                       ).ToList();


                    data = model.OrderByDescending(c => c.gcDate).ToList().ToList();
                }

                if (userId > 0)
                {
                    var model = data.Where(c => c.userId == userId).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.gcDate).ToList().ToList(); ;

            }
        }

        public IEnumerable<SBAGrabageCollectionGridRow> GetCommercialGarbageCollectionData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId, int? param1, int? param2, int? param3, int? param4)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();
            string ThumbnaiUrlAPI = appDetails.baseImageUrl + appDetails.basePath + appDetails.Collection + "/";

            using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(appId))
            {
              


                var data = db.SP_CommercialGarbageCollection(appId, userId, fdate, tdate, param1, param2, param3, param4).Select(x => new SBAGrabageCollectionGridRow
                {
                    Id = x.gcId,
                    userId = x.userId,
                    houseId = x.houseId,
                    UserName = x.commercialOwner,
                    HouseNumber = x.commercialOwner,
                    gcDate = x.gcDate,
                    // gcType = 1,

                    type1 = x.garbageType.ToString(),
                    Ctype = x.gcType.ToString(),
                    Address = (x.locAddresss).Replace("Unnamed Road,", ""),
                    gpBeforImage = x.gpBeforImage,
                    gpAfterImage = x.gpAfterImage,
                    VehicleNumber = x.vehicleNumber,
                    Note = x.note,
                    ReferanceId = x.ReferanceId,
                    Employee = x.userName,
                    attandDate = Convert.ToDateTime(x.gcDate).ToString("dd/MM/yyyy hh:mm tt"),
                    gpIdfk = x.gcId,
                    gpIdpk = x.gcId,
                    batteryStatus = x.batteryStatus,
                    los = x.los,
                    ctype = x.CType,


                }).OrderByDescending(c => c.gcDate).ToList().ToList();

                if (!string.IsNullOrEmpty(SearchString))
                {
                    //var model = data.Where(c => c.UserName.Contains(SearchString) || c.HouseNumber.Contains(SearchString) || c.VehicleNumber.Contains(SearchString) || c.ReferanceId.Contains(SearchString) || c.Address.Contains(SearchString) || c.Employee.Contains(SearchString) || c.attandDate.Contains(SearchString) || c.Note.Contains(SearchString)

                    //   || c.UserName.ToLower().Contains(SearchString) || c.HouseNumber.ToLower().Contains(SearchString) || c.VehicleNumber.ToLower().Contains(SearchString) || c.ReferanceId.ToLower().Contains(SearchString) || c.Address.ToLower().Contains(SearchString) || c.Employee.ToLower().Contains(SearchString) || c.attandDate.ToLower().Contains(SearchString) || c.Note.ToLower().Contains(SearchString)

                    //   || c.UserName.ToUpper().Contains(SearchString) || c.HouseNumber.ToUpper().Contains(SearchString) || c.VehicleNumber.ToUpper().Contains(SearchString) || c.ReferanceId.ToUpper().Contains(SearchString) || c.Address.ToUpper().Contains(SearchString) || c.Employee.ToUpper().Contains(SearchString) || c.attandDate.ToUpper().Contains(SearchString) || c.Note.ToUpper().Contains(SearchString)
                    //   ).ToList();
                    var model = data.Where(c => ((c.UserName == null ? " " : c.UserName) + " " + (c.HouseNumber == null ? " " : c.HouseNumber) + " " + (c.VehicleNumber == null ? " " : c.VehicleNumber) + " " + (c.ReferanceId == null ? "" : c.ReferanceId) + " " + (c.Address == null ? " " : c.Address) + " " + (c.Employee == null ? " " : c.Employee) + " " + (c.attandDate == null ? " " : c.attandDate) + " " + (c.Note == null ? " " : c.Note)).ToUpper().Contains(SearchString.ToUpper())
                       ).ToList();


                    data = model.OrderByDescending(c => c.gcDate).ToList().ToList();
                }

                if (userId > 0)
                {
                    var model = data.Where(c => c.userId == userId).ToList();

                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.gcDate).ToList().ToList(); ;

            }
        }


        public IEnumerable<SBAAttendenceGrid> GetAttendeceData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId, string Emptype)
        {
            List<SBAAttendenceGrid> obj = new List<SBAAttendenceGrid>();
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = db.Daily_Attendance.Where(c => c.EmployeeType == Emptype).ToList();
                if (Convert.ToDateTime(fdate).ToString("dd/MM/yyyy") == Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy"))
                {
                    data = data.Where(c => (c.daDate == fdate || c.daEndDate == fdate || c.endTime == "")).ToList();
                }
                else
                {

                    data = data.Where(c => (c.daDate >= fdate && c.daDate <= tdate) || (c.daDate >= fdate && c.daDate <= tdate)).ToList();
                }

                foreach (var x in data)
                {
                    int a = Convert.ToInt32(x.vtId.Trim());
                    string vt = "";
                    try { vt = db.VehicleTypes.Where(c => c.vtId == a).FirstOrDefault().description; }
                    catch { vt = ""; }
                    ///x.daDate = checkNull(x.daDate.tp);
                    x.endLat = checkNull(x.endLat);
                    x.endLong = checkNull(x.endLong);
                    x.endTime = checkNull(x.endTime);
                    x.startLat = checkNull(x.startLat);
                    x.startLong = checkNull(x.startLong);
                    x.startTime = checkNull(x.startTime);
                    x.vehicleNumber = checkNull(x.vehicleNumber);
                    x.daEndNote = checkNull(x.daEndNote);
                    x.daStartNote = checkNull(x.daStartNote);
                    x.CType= checkNull(x.CType);
                    string endate = "";
                    if (x.daEndDate == null)
                    {
                        endate = "";
                    }
                    else
                    {
                        endate = Convert.ToDateTime(x.daEndDate).ToString("dd/MM/yyyy");
                    }

                    // string displayTime = Convert.ToDateTime(x.daDate).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    string displayTime = Convert.ToDateTime(x.daDate).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    string time = Convert.ToDateTime(x.startTime).ToString("HH:mm:ss");

                    obj.Add(new SBAAttendenceGrid()
                    {
                        daID = x.daID,
                        userId = Convert.ToInt32(x.userId),
                        userName = db.UserMasters.Where(c => c.userId == x.userId).FirstOrDefault().userName,
                        daDate = Convert.ToDateTime(x.daDate).ToString("dd/MM/yyyy"),
                        daEndDate = endate,
                        startTime = x.startTime,
                        endTime = x.endTime,
                        startLat = x.startLat,
                        startLong = x.startLong,
                        endLat = x.startLong,
                        endLong = x.endLong,
                        vtId = vt,
                        vehicleNumber = x.vehicleNumber,
                        CompareDate = x.daDate,
                        CType=x.CType,
                        daDateTIme = (displayTime + " " + time)
                    });
                }

                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = obj.Where(c => c.vehicleNumber.Contains(SearchString) || c.daDate.Contains(SearchString) || c.endTime.Contains(SearchString) || c.startLat.Contains(SearchString) || c.endLat.Contains(SearchString) || c.startTime.Contains(SearchString) || c.userName.Contains(SearchString) || c.vtId.Contains(SearchString)

                    || c.vehicleNumber.ToLower().Contains(SearchString) || c.vtId.ToLower().Contains(SearchString) || c.daDate.ToLower().Contains(SearchString) || c.endTime.ToLower().Contains(SearchString) || c.startLat.ToLower().Contains(SearchString) || c.endLat.ToLower().Contains(SearchString) || c.startTime.ToLower().Contains(SearchString) || c.userName.ToLower().Contains(SearchString)).ToList();

                    obj = model.ToList();
                }

                //if (!string.IsNullOrEmpty(fdate.ToString()))
                //{
                //    DateTime? dt1 = null;
                //    if (!string.IsNullOrEmpty(tdate.ToString()))
                //    { dt1 = tdate; }
                //    else { dt1 = fdate; }
                //    obj = obj.Where(fullEntry => fullEntry.CompareDate >= fdate && fullEntry.CompareDate <= dt1).OrderByDescending(c => c.CompareDate).ToList();
                //}
                if (userId > 0)
                {
                    var model = obj.Where(c => c.userId == userId).ToList();

                    obj = model.ToList();
                }
                var d = obj.OrderByDescending(c => DateTime.Parse(c.daDateTIme)).ToList();
                return d;
            }
        }

        public IEnumerable<ComplaintGrid> GetComplaintData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int appId)
        {
            List<ComplaintGrid> obj = new List<ComplaintGrid>();
            DevSwachhBharatMainEntities dbm = new DevSwachhBharatMainEntities();
            var appdetails = dbm.AppDetails.Where(c => c.AppId == appId).FirstOrDefault();

            // string json = new WebClient().DownloadString("http://192.168.200.3:8077////api/Get/Complaint?appId=1");
            // string json = new WebClient().DownloadString(appdetails.Grampanchayat_Pro+ "/api/Get/Complaint?appId=1");

            WebClient WebClient = new WebClient();
            WebClient.Encoding = System.Text.Encoding.UTF8;

            string json = WebClient.DownloadString(appdetails.Grampanchayat_Pro + "/api/Get/Complaint?appId=" + appdetails.GramPanchyatAppID);
            List<ComplaintVM> obj2 = JsonConvert.DeserializeObject<List<ComplaintVM>>(json).ToList();
            foreach (var x in obj2)
            {
                string image = "";
                x.houseNumber = checkNull(x.houseNumber);
                x.address = checkNull(x.address).Replace("Unnamed Road, ", "");
                x.comment = checkNull(x.comment);
                x.complaintTypeMar = checkNull(x.typeMar);
                x.complaintType = checkNull(x.type);
                x.details = checkNull(x.details);
                x.place = checkNull(x.place);
                x.refId = checkNull(x.refId);
                x.status = checkNull(x.status);
                x.tips = checkNull(x.tips);
                x.wardNo = checkNull(x.wardNo);
                //Convert.ToDateTime(x.gcDate).ToString("dd/MM/yyyy hh:mm tt")
                obj.Add(new ComplaintGrid()
                {

                    complaintId = x.complaintId,
                    refId = x.refId,
                    //date = x.createdDate,
                    date = Convert.ToDateTime(x.createdDate2).ToString("dd/MM/yyyy hh:mm tt"),
                    startImage = x.startImage,
                    endImage = image,
                    comment = x.comment,
                    tips = x.tips,
                    houseNumber = x.houseNumber,
                    address = x.address,
                    details = x.details,
                    place = x.place,
                    status = x.status,
                    typeMar = x.type,
                    wardNo = x.wardNo,
                    CompareDate = x.createdDate2

                });
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                var model = obj.Where(c => c.wardNo.Contains(SearchString) || c.tips.Contains(SearchString) || c.refId.Contains(SearchString) || c.place.Contains(SearchString) || c.address.Contains(SearchString) || c.details.Contains(SearchString) || c.date.Contains(SearchString) || c.comment.Contains(SearchString) || c.houseNumber.Contains(SearchString) || c.typeMar.Contains(SearchString)

                   || c.wardNo.ToLower().Contains(SearchString) || c.tips.ToLower().Contains(SearchString) || c.refId.ToLower().Contains(SearchString) || c.place.ToLower().Contains(SearchString) || c.address.ToLower().Contains(SearchString) || c.details.ToLower().Contains(SearchString) || c.date.ToLower().Contains(SearchString) || c.comment.ToLower().Contains(SearchString) || c.houseNumber.ToLower().Contains(SearchString) || c.typeMar.ToLower().Contains(SearchString)

                   || c.wardNo.ToUpper().Contains(SearchString) || c.tips.ToUpper().Contains(SearchString) || c.refId.ToUpper().Contains(SearchString) || c.place.ToUpper().Contains(SearchString) || c.address.ToUpper().Contains(SearchString) || c.details.ToUpper().Contains(SearchString) || c.date.ToUpper().Contains(SearchString) || c.comment.ToUpper().Contains(SearchString) || c.houseNumber.ToUpper().Contains(SearchString) || c.typeMar.ToUpper().Contains(SearchString)
                   ).ToList();



                obj = model.ToList();
            }
            if (!string.IsNullOrEmpty(fdate.ToString()))
            {
                DateTime? dt1 = null;
                if (!string.IsNullOrEmpty(tdate.ToString()))
                { dt1 = tdate; }
                else { dt1 = fdate; }
                obj = obj.Where(fullEntry => fullEntry.CompareDate >= fdate && fullEntry.CompareDate <= dt1).OrderByDescending(c => c.CompareDate).ToList();
            }
            return obj;
        }


        public IEnumerable<SBAEmplyeeIdelGrid> GetIdelData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        {
            List<SBAEmplyeeIdelGrid> obj = new List<SBAEmplyeeIdelGrid>();
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {


                var data = db.SP_IdelTime(userId, fdate, tdate).Where(c => c.IdelTime != null & c.IdelTime > 15).ToList().OrderByDescending(c => c.StartTime);

                foreach (var x in data)
                {
                    TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToDouble(x.IdelTime));
                    string workHours = spWorkMin.ToString(@"hh\:mm");

                    string displayTime = Convert.ToDateTime(x.date).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    string time = Convert.ToDateTime(x.StartTime).ToString("HH:mm:ss");

                    obj.Add(new SBAEmplyeeIdelGrid()
                    {
                        UserName = x.userName,
                        Date = Convert.ToDateTime(x.date).ToString("dd/MM/yyyy"),
                        StartTime = x.StartTime,
                        EndTime = x.LastTime,
                        StartAddress = checkNull(x.StartAddress).Replace("Unnamed Road, ", ""),
                        EndAddress = checkNull(x.address).Replace("Unnamed Road, ", ""),
                        IdelTime = workHours,
                        userId = x.userId,
                        startLat = x.StarLat,
                        startLong = x.StartLog,
                        EndLat = x.lat,
                        EndLong = x.@long,
                        daDateTIme = (displayTime + " " + time)

                    });
                }

                if (!string.IsNullOrEmpty(SearchString))
                {
                    //var model = data.Where(c => c.userName.Contains(SearchString) || c.StartAddress.Contains(SearchString) || c.LastTime.Contains(SearchString) || c.address.Contains(SearchString) || c.StartTime.Contains(SearchString) || Convert.ToString(c.IdelTime).Contains(SearchString)

                    //|| c.userName.ToLower().Contains(SearchString) || c.StartAddress.ToLower().Contains(SearchString) || c.address.ToLower().Contains(SearchString) || c.LastTime.ToLower().Contains(SearchString) || c.StartTime.ToLower().Contains(SearchString) || Convert.ToString(c.IdelTime).ToLower().Contains(SearchString)

                    //   || c.userName.ToUpper().Contains(SearchString) || c.StartAddress.ToUpper().Contains(SearchString) || c.address.ToUpper().Contains(SearchString) || c.LastTime.ToUpper().Contains(SearchString) || c.StartTime.ToUpper().Contains(SearchString) || Convert.ToString(c.IdelTime).ToUpper().Contains(SearchString)).ToList();


                    var model = obj.Where(c => ((c.UserName == null ? " " : c.UserName) + " " +
                                                 (c.StartAddress == null ? " " : c.StartAddress) + " " +
                                                 (c.EndTime == null ? " " : c.EndTime) + " " +
                                                 (c.EndAddress == null ? "" : c.EndAddress) + " " +
                                                 (c.StartTime == null ? " " : c.StartTime) + " " +
                                                 (Convert.ToString(c.IdelTime) == null ? " " : Convert.ToString(c.IdelTime))).ToUpper().Contains(SearchString.ToUpper())).ToList();

                    obj = model.ToList();
                }
                // data = model.OrderByDescending(c => c.DisplayTime).ToList();

                //Address= checkNull(x.Address).Replace("Unnamed Road, ", ""),
                //MyList.OrderBy(x => x.StartDate).ThenByDescending(x => x.EndDate);
                //return obj;
                return obj.OrderByDescending(c => c.daDateTIme).ToList();
            }
        }


        public IEnumerable<SBAEmplyeeIdelGrid> GetIdelDataLiquid(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        {
            List<SBAEmplyeeIdelGrid> obj = new List<SBAEmplyeeIdelGrid>();
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {


                var data = db.SP_IdelTimeLiquid(userId, fdate, tdate).Where(c => c.IdelTime != null & c.IdelTime > 15).ToList().OrderByDescending(c => c.StartTime);

                foreach (var x in data)
                {
                    TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToDouble(x.IdelTime));
                    string workHours = spWorkMin.ToString(@"hh\:mm");

                    string displayTime = Convert.ToDateTime(x.date).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    string time = Convert.ToDateTime(x.StartTime).ToString("HH:mm:ss");

                    obj.Add(new SBAEmplyeeIdelGrid()
                    {
                        UserName = x.userName,
                        Date = Convert.ToDateTime(x.date).ToString("dd/MM/yyyy"),
                        StartTime = x.StartTime,
                        EndTime = x.LastTime,
                        StartAddress = checkNull(x.StartAddress).Replace("Unnamed Road, ", ""),
                        EndAddress = checkNull(x.address).Replace("Unnamed Road, ", ""),
                        IdelTime = workHours,
                        userId = x.userId,
                        startLat = x.StarLat,
                        startLong = x.StartLog,
                        EndLat = x.lat,
                        EndLong = x.@long,
                        daDateTIme = (displayTime + " " + time)

                    });
                }

                if (!string.IsNullOrEmpty(SearchString))
                {
                    //var model = data.Where(c => c.userName.Contains(SearchString) || c.StartAddress.Contains(SearchString) || c.LastTime.Contains(SearchString) || c.address.Contains(SearchString) || c.StartTime.Contains(SearchString) || Convert.ToString(c.IdelTime).Contains(SearchString)

                    //|| c.userName.ToLower().Contains(SearchString) || c.StartAddress.ToLower().Contains(SearchString) || c.address.ToLower().Contains(SearchString) || c.LastTime.ToLower().Contains(SearchString) || c.StartTime.ToLower().Contains(SearchString) || Convert.ToString(c.IdelTime).ToLower().Contains(SearchString)

                    //   || c.userName.ToUpper().Contains(SearchString) || c.StartAddress.ToUpper().Contains(SearchString) || c.address.ToUpper().Contains(SearchString) || c.LastTime.ToUpper().Contains(SearchString) || c.StartTime.ToUpper().Contains(SearchString) || Convert.ToString(c.IdelTime).ToUpper().Contains(SearchString)).ToList();


                    var model = obj.Where(c => ((c.UserName == null ? " " : c.UserName) + " " +
                                                 (c.StartAddress == null ? " " : c.StartAddress) + " " +
                                                 (c.EndTime == null ? " " : c.EndTime) + " " +
                                                 (c.EndAddress == null ? "" : c.EndAddress) + " " +
                                                 (c.StartTime == null ? " " : c.StartTime) + " " +
                                                 (Convert.ToString(c.IdelTime) == null ? " " : Convert.ToString(c.IdelTime))).ToUpper().Contains(SearchString.ToUpper())).ToList();

                    obj = model.ToList();
                }
                // data = model.OrderByDescending(c => c.DisplayTime).ToList();

                //Address= checkNull(x.Address).Replace("Unnamed Road, ", ""),
                //MyList.OrderBy(x => x.StartDate).ThenByDescending(x => x.EndDate);
                //return obj;
                return obj.OrderByDescending(c => c.daDateTIme).ToList();
            }
        }

        public IEnumerable<SBAEmplyeeIdelGrid> GetIdelDataStreet(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        {
            List<SBAEmplyeeIdelGrid> obj = new List<SBAEmplyeeIdelGrid>();
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {


                var data = db.SP_IdelTimestreet(userId, fdate, tdate).Where(c => c.IdelTime != null & c.IdelTime > 15).ToList().OrderByDescending(c => c.StartTime);

                foreach (var x in data)
                {
                    TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToDouble(x.IdelTime));
                    string workHours = spWorkMin.ToString(@"hh\:mm");

                    string displayTime = Convert.ToDateTime(x.date).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    string time = Convert.ToDateTime(x.StartTime).ToString("HH:mm:ss");

                    obj.Add(new SBAEmplyeeIdelGrid()
                    {
                        UserName = x.userName,
                        Date = Convert.ToDateTime(x.date).ToString("dd/MM/yyyy"),
                        StartTime = x.StartTime,
                        EndTime = x.LastTime,
                        StartAddress = checkNull(x.StartAddress).Replace("Unnamed Road, ", ""),
                        EndAddress = checkNull(x.address).Replace("Unnamed Road, ", ""),
                        IdelTime = workHours,
                        userId = x.userId,
                        startLat = x.StarLat,
                        startLong = x.StartLog,
                        EndLat = x.lat,
                        EndLong = x.@long,
                        daDateTIme = (displayTime + " " + time)

                    });
                }

                if (!string.IsNullOrEmpty(SearchString))
                {
                    //var model = data.Where(c => c.userName.Contains(SearchString) || c.StartAddress.Contains(SearchString) || c.LastTime.Contains(SearchString) || c.address.Contains(SearchString) || c.StartTime.Contains(SearchString) || Convert.ToString(c.IdelTime).Contains(SearchString)

                    //|| c.userName.ToLower().Contains(SearchString) || c.StartAddress.ToLower().Contains(SearchString) || c.address.ToLower().Contains(SearchString) || c.LastTime.ToLower().Contains(SearchString) || c.StartTime.ToLower().Contains(SearchString) || Convert.ToString(c.IdelTime).ToLower().Contains(SearchString)

                    //   || c.userName.ToUpper().Contains(SearchString) || c.StartAddress.ToUpper().Contains(SearchString) || c.address.ToUpper().Contains(SearchString) || c.LastTime.ToUpper().Contains(SearchString) || c.StartTime.ToUpper().Contains(SearchString) || Convert.ToString(c.IdelTime).ToUpper().Contains(SearchString)).ToList();


                    var model = obj.Where(c => ((c.UserName == null ? " " : c.UserName) + " " +
                                                 (c.StartAddress == null ? " " : c.StartAddress) + " " +
                                                 (c.EndTime == null ? " " : c.EndTime) + " " +
                                                 (c.EndAddress == null ? "" : c.EndAddress) + " " +
                                                 (c.StartTime == null ? " " : c.StartTime) + " " +
                                                 (Convert.ToString(c.IdelTime) == null ? " " : Convert.ToString(c.IdelTime))).ToUpper().Contains(SearchString.ToUpper())).ToList();

                    obj = model.ToList();
                }
                // data = model.OrderByDescending(c => c.DisplayTime).ToList();

                //Address= checkNull(x.Address).Replace("Unnamed Road, ", ""),
                //MyList.OrderBy(x => x.StartDate).ThenByDescending(x => x.EndDate);
                //return obj;
                return obj.OrderByDescending(c => c.daDateTIme).ToList();
            }
        }
        public IEnumerable<SBAGarbageCountDetails> GetGarbageCountData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        {
            List<SBAGarbageCountDetails> obj = new List<SBAGarbageCountDetails>();
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {

                var data = db.SP_Collection_Count(fdate, tdate, userId).ToList();

                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => c.userName.Contains(SearchString) || Convert.ToString(c.Count).Contains(SearchString) || Convert.ToString(c.fromDate).Contains(SearchString) || Convert.ToString(c.ToDate).Contains(SearchString)

                    || c.userName.ToUpper().Contains(SearchString) || Convert.ToString(c.Count).ToUpper().Contains(SearchString) || Convert.ToString(c.fromDate).ToUpper().Contains(SearchString) || Convert.ToString(c.ToDate).ToUpper().Contains(SearchString)

                       || c.userName.ToUpper().Contains(SearchString) || Convert.ToString(c.Count).ToUpper().Contains(SearchString) || Convert.ToString(c.fromDate).ToUpper().Contains(SearchString) || Convert.ToString(c.ToDate).ToUpper().Contains(SearchString)).ToList();

                    data = model.ToList();
                }

                foreach (var x in data)
                {

                    obj.Add(new SBAGarbageCountDetails()
                    {
                        UserName = x.userName,
                        StartTime = x.inTime,
                        FromDate = Convert.ToDateTime(x.fromDate).ToString("dd/MM/yyyy"),
                        ToDate = Convert.ToDateTime(x.ToDate).ToString("dd/MM/yyyy"),
                        _Count = Convert.ToInt32(x.Count),

                    });
                }
                return obj;
            }
        }


        public IEnumerable<SBAAdminCountGrid> GetAdminCountData(long wildcard, string SearchString, int appId)
        {
            using (var dbMain = new DevSwachhBharatMainEntities())
            {
                var data = dbMain.SP_Admin2().Select(x => new SBAAdminCountGrid
                {
                    Name = x.appName,
                    employee = Convert.ToInt32(x.userId),
                    attenemplyee = Convert.ToInt32(x.attenUser) + "/" + Convert.ToInt32(x.userId),
                    bybufer = Convert.ToInt32(x.byfurgetcol),
                    notrecivedcol = Convert.ToInt32(x.notrecviedCollection),
                    notspecified = Convert.ToInt32(x.notspecified),
                    total = Convert.ToInt32(x.collectionCount),
                    mixed = Convert.ToInt32(x.mixedcolle),
                }).ToList();
                ////  var result = data.SkipWhile(element => element.cId != element.reNewId); 
                //foreach (var item in data)
                //{
                //    item.Name = checkNull(item.Name);
                //    item.NameMar = checkNull(item.NameMar);
                //    item.ward = checkNull(item.ward);
                //    int wa = Convert.ToInt32(db.WardNumbers.Where(c => c.WardNo == item.ward).FirstOrDefault().zoneId);
                //    string zone = db.ZoneMasters.Where(c => c.zoneId == wa).FirstOrDefault().name;
                //    item.ward = item.ward + " (" + zone + ")";

                //}
                //if (!string.IsNullOrEmpty(SearchString))
                //{
                //    var model = data.Where(c => c.Name.ToUpper().ToString().Contains(SearchString) || c.NameMar.ToString().ToUpper().ToString().Contains(SearchString) ||
                //     c.NameMar.ToString().ToLower().Contains(SearchString) || c.Name.ToString().ToLower().ToString().Contains(SearchString) ||
                //     c.Name.ToString().Contains(SearchString) || c.NameMar.ToString().ToString().Contains(SearchString) || c.ward.ToString().Contains(SearchString)).ToList();

                //    data = model.ToList();
                //}
                //return data.OrderByDescending(c => c.Id);
                return data.OrderBy(c => c.Name);
            }


        }


        // Added By Saurabh

        public IEnumerable<SBADumpYardDetailsGridRow> GetDumpYardData(long wildcard, string SearchString, int appId)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.DumpYardQRCode + "/";
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = db.SP_DumpYardDetails().Select(x => new SBADumpYardDetailsGridRow
                {

                    Zone = x.Zone,
                    Ward = x.Ward,
                    Area = x.Area,
                    Name = x.Name,
                    NameMar = x.NameMar,
                    Id = x.dyId,
                    QrCode = ThumbnaiUrlCMS + x.Images.Trim(),
                    Address = x.Address,
                    ReferanceId = x.ReferanceId

                }).ToList();
                if (!string.IsNullOrEmpty(SearchString))
                {
                    //var model = data.Where(c => c.Area.ToString().Contains(SearchString)
                    //|| c.Name.ToString().Contains(SearchString) || c.NameMar.ToString().Contains(SearchString) || c.ReferanceId.ToString().Contains(SearchString)

                    //|| c.Area.Contains(SearchString) || c.NameMar.ToLower().ToString().Contains(SearchString) || c.Name.ToLower().ToString().Contains(SearchString) || c.ReferanceId.ToLower().ToString().Contains(SearchString)

                    //|| c.Area.ToUpper().ToString().Contains(SearchString) || c.Name.ToUpper().ToString().Contains(SearchString)
                    //|| c.NameMar.ToUpper().ToString().Contains(SearchString) || c.ReferanceId.ToUpper().ToString().Contains(SearchString)).ToList();

                    var model = data.Where(c => ((string.IsNullOrEmpty(c.Area) ? " " : c.Area) + " " +
                                      (string.IsNullOrEmpty(c.Name) ? " " : c.Name) + " " +
                                      (string.IsNullOrEmpty(c.NameMar) ? " " : c.NameMar) + " " +
                                      (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId)).ToUpper().Contains(SearchString.ToUpper())).ToList();


                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.Id).ToList();
            }
        }

        public IEnumerable<SBAStreeSweepDetailsGridRow> GetStreetSweepData(long wildcard, string SearchString, int appId)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.StreetQRCode + "/";
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = db.SP_StreetSweepDetails().Select(x => new SBAStreeSweepDetailsGridRow
                {

                    Zone = x.Zone,
                    Ward = x.Ward,
                    Area = x.Area,
                    Name = x.Name,
                    NameMar = x.NameMar,
                    Id = x.SSId,
                    QrCode = ThumbnaiUrlCMS + x.Images.Trim(),
                    Address = x.Address,
                    ReferanceId = x.ReferanceId

                }).ToList();
                if (!string.IsNullOrEmpty(SearchString))
                {
                    //var model = data.Where(c => c.Area.ToString().Contains(SearchString)
                    //|| c.Name.ToString().Contains(SearchString) || c.NameMar.ToString().Contains(SearchString) || c.ReferanceId.ToString().Contains(SearchString)

                    //|| c.Area.Contains(SearchString) || c.NameMar.ToLower().ToString().Contains(SearchString) || c.Name.ToLower().ToString().Contains(SearchString) || c.ReferanceId.ToLower().ToString().Contains(SearchString)

                    //|| c.Area.ToUpper().ToString().Contains(SearchString) || c.Name.ToUpper().ToString().Contains(SearchString)
                    //|| c.NameMar.ToUpper().ToString().Contains(SearchString) || c.ReferanceId.ToUpper().ToString().Contains(SearchString)).ToList();

                    var model = data.Where(c => ((string.IsNullOrEmpty(c.Area) ? " " : c.Area) + " " +
                                      (string.IsNullOrEmpty(c.Name) ? " " : c.Name) + " " +
                                      (string.IsNullOrEmpty(c.NameMar) ? " " : c.NameMar) + " " +
                                      (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId)).ToUpper().Contains(SearchString.ToUpper())).ToList();


                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.Id).ToList();
            }
        }

        public IEnumerable<SBALiquidWasteGridRow> GetLiquidWasteData(long wildcard, string SearchString, int appId)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.LiquidQRCode + "/";
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = db.SP_LiquidWasteDetails().Select(x => new SBALiquidWasteGridRow
                {

                    Zone = x.Zone,
                    Ward = x.Ward,
                    Area = x.Area,
                    Name = x.Name,
                    NameMar = x.NameMar,
                    Id = x.LWId,
                    QrCode = ThumbnaiUrlCMS + x.Images.Trim(),
                    Address = x.Address,
                    ReferanceId = x.ReferanceId

                }).ToList();
                if (!string.IsNullOrEmpty(SearchString))
                {
                    //var model = data.Where(c => c.Area.ToString().Contains(SearchString)
                    //|| c.Name.ToString().Contains(SearchString) || c.NameMar.ToString().Contains(SearchString) || c.ReferanceId.ToString().Contains(SearchString)

                    //|| c.Area.Contains(SearchString) || c.NameMar.ToLower().ToString().Contains(SearchString) || c.Name.ToLower().ToString().Contains(SearchString) || c.ReferanceId.ToLower().ToString().Contains(SearchString)

                    //|| c.Area.ToUpper().ToString().Contains(SearchString) || c.Name.ToUpper().ToString().Contains(SearchString)
                    //|| c.NameMar.ToUpper().ToString().Contains(SearchString) || c.ReferanceId.ToUpper().ToString().Contains(SearchString)).ToList();

                    var model = data.Where(c => ((string.IsNullOrEmpty(c.Area) ? " " : c.Area) + " " +
                                      (string.IsNullOrEmpty(c.Name) ? " " : c.Name) + " " +
                                      (string.IsNullOrEmpty(c.NameMar) ? " " : c.NameMar) + " " +
                                      (string.IsNullOrEmpty(c.ReferanceId) ? " " : c.ReferanceId)).ToUpper().Contains(SearchString.ToUpper())).ToList();


                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.Id).ToList();
            }
        }


        //Added By saurabh 05 MAy 2019

        public IEnumerable<SBAEmpolyeeSummaryGrid> GetEmployeeSummaryData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int? userId, int appId, string Emptype)
        {
            List<SBAEmpolyeeSummaryGrid> obj = new List<SBAEmpolyeeSummaryGrid>();
            if (Emptype == null)
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(appId))
                {
                    //if (userId == 0)
                    //{
                    //     userId = null; 
                    //}
                    //db.Database.CommandTimeout = 5000;
                    db.Database.CommandTimeout = 500;
                    var data = db.SP_EmployeeSummary(fdate, tdate, userId <= 0 ? null : userId).ToList();
                    // var data2 = data.OrderByDescending(c => c.Startdate).ThenByDescending(c => c.StartTime).ToList();
                    //var data2 = data1.GroupBy(o => o.userId).Select(o => o.First()).AsEnumerable().ToList();

                    foreach (var x in data)
                    {
                        //TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToDouble(x.IdelTime));
                        //string workHours = spWorkMin.ToString(@"hh\:mm");

                        string EndDate = "";
                        if (x.Enddate == null)
                        {
                            EndDate = "";
                        }
                        else
                        {
                            EndDate = Convert.ToDateTime(x.Enddate).ToString("dd/MM/yyyy");
                        }

                        //DateTime time = Convert.ToDateTime(x.StartTime);
                        string displayTime = Convert.ToDateTime(x.Startdate).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        string time = Convert.ToDateTime(x.StartTime).ToString("HH:mm:ss");
                        obj.Add(new SBAEmpolyeeSummaryGrid()
                        {
                            UserName = x.userName,
                            userId = x.userId,
                            // Date = Convert.ToDateTime(x.date).ToString("dd/MM/yyyy"),
                            daDate = (x.Startdate == null ? "" : Convert.ToDateTime(x.Startdate).ToString("dd/MM/yyyy")),
                            StartTime = x.StartTime,
                            DaEndDate = EndDate,
                            EndTime = x.EndTime,
                            Totalhousecollection = (x.Totalhousecollection).ToString(),
                            Totaldumpyard = (x.Totaldumpyard).ToString(),
                            Totaldistance = string.Format("{0:0.0}", (x.Totaldistance)).ToString(),
                            InBatteryStatus = x.InBatteryStatus,
                            OutBatteryStatus = x.OutBatteryStatus,
                            daDateTIme = (displayTime + " " + time)

                            //daDateTIme = Convert.ToDateTime(x.Startdate + x.StartTime).ToString("dd/MM/yyyy hh:mm tt"),
                            //DateTime startDate = Convert.ToDateTime(a + " " + Time1);

                            //String.Format("{0:0.00}", 123.4567);
                            //IdelTime = workHours
                            //AnsDate = Convert.ToDateTime(x.AnsDate).ToString("dd/MM/yyyy hh:mm tt"),
                        });
                    }


                    //if (Convert.ToDateTime(fdate).ToString("dd/MM/yyyy") == Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy"))
                    //{
                    //    data = data.Where(c => (c.daDate == fdate || c.daEndDate == fdate || c.endTime == "")).ToList();

                    //    //data = data.GroupBy(o => o.userId).Select(o => o.First()).AsEnumerable().ToList();
                    //}
                    //else {

                    //    var abc = data.Where(c => (c.daDate >= fdate && c.daDate <= tdate) || (c.daDate >= fdate && c.daDate <= tdate)).ToList();
                    //}

                    //foreach (var x in data)
                    //{

                    //    ///x.daDate = checkNull(x.daDate.tp);
                    //    x.endLat = checkNull(x.endLat);
                    //    x.endLong = checkNull(x.endLong);
                    //    x.endTime = checkNull(x.endTime);
                    //    x.startLat = checkNull(x.startLat);
                    //    x.startLong = checkNull(x.startLong);
                    //    x.startTime = checkNull(x.startTime);
                    //    //x.vehicleNumber = checkNull(x.vehicleNumber);
                    //    //x.daEndNote = checkNull(x.daEndNote);
                    //    //x.daStartNote = checkNull(x.daStartNote);
                    //    string endate = "";
                    //    if (x.daEndDate == null)
                    //    {
                    //        endate = "";
                    //    }
                    //    else {
                    //        endate = Convert.ToDateTime(x.daEndDate).ToString("dd/MM/yyyy");
                    //    }
                    //    obj.Add(new SBAAttendenceGrid()
                    //    {
                    //        daID = x.daID,
                    //        userId = Convert.ToInt32(x.userId),
                    //        userName = db.UserMasters.Where(c => c.userId == x.userId).FirstOrDefault().userName,
                    //        daDate = Convert.ToDateTime(x.daDate).ToString("dd/MM/yyyy"),
                    //        daEndDate = endate,
                    //        startTime = x.startTime,
                    //        endTime = x.endTime,
                    //        startLat = x.startLat,
                    //        startLong = x.startLong,
                    //        endLat = x.startLong,
                    //        endLong = x.endLong,
                    //        vtId = vt,
                    //        vehicleNumber = x.vehicleNumber,
                    //        CompareDate = x.daDate,
                    //    });
                    //}

                    //if (!string.IsNullOrEmpty(SearchString))
                    //{
                    //    var model = obj.Where(c => c.vehicleNumber.Contains(SearchString) || c.daDate.Contains(SearchString) || c.endTime.Contains(SearchString) || c.startLat.Contains(SearchString) || c.endLat.Contains(SearchString) || c.startTime.Contains(SearchString) || c.userName.Contains(SearchString) || c.vtId.Contains(SearchString)

                    //    || c.vehicleNumber.ToLower().Contains(SearchString) || c.vtId.ToLower().Contains(SearchString) || c.daDate.ToLower().Contains(SearchString) || c.endTime.ToLower().Contains(SearchString) || c.startLat.ToLower().Contains(SearchString) || c.endLat.ToLower().Contains(SearchString) || c.startTime.ToLower().Contains(SearchString) || c.userName.ToLower().Contains(SearchString)).ToList();

                    //    obj = model.ToList();
                    //}

                    //if (!string.IsNullOrEmpty(fdate.ToString()))
                    //{
                    //    DateTime? dt1 = null;
                    //    if (!string.IsNullOrEmpty(tdate.ToString()))
                    //    { dt1 = tdate; }
                    //    else { dt1 = fdate; }
                    //    obj = obj.Where(fullEntry => fullEntry.CompareDate >= fdate && fullEntry.CompareDate <= dt1).OrderByDescending(c => c.CompareDate).ToList();
                    //}

                    //comment by saurabh - 24 July 2019
                    //if (userId > 0)
                    //{
                    //    var model = obj.Where(c => c.userId == userId).ToList();

                    //    obj = model.ToList();
                    //}
                    //return obj.OrderByDescending(c => c.daID );


                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        var model = obj.Where(c => c.daDate.Contains(SearchString) || c.UserName.Contains(SearchString)

                        || c.daDate.ToLower().Contains(SearchString) || c.UserName.ToLower().Contains(SearchString)).ToList();

                        obj = model.ToList();
                    }

                    if (userId > 0)
                    {
                        var model = obj.Where(c => c.userId == userId).ToList();

                        obj = model.ToList();
                    }
                    //return obj.OrderByDescending(c => c.daID);
                    var f = obj.OrderByDescending(c => DateTime.Parse(c.daDateTIme)).ToList();
                    return f;
                }

            }
            else if (Emptype == "L")
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(appId))
                {
                    //if (userId == 0)
                    //{
                    //     userId = null; 
                    //}
                    //db.Database.CommandTimeout = 5000;
                    db.Database.CommandTimeout = 500;
                    var data = db.SP_LSEmployeeSummary(fdate, tdate, userId <= 0 ? null : userId, Emptype).ToList();
                    // var data2 = data.OrderByDescending(c => c.Startdate).ThenByDescending(c => c.StartTime).ToList();
                    //var data2 = data1.GroupBy(o => o.userId).Select(o => o.First()).AsEnumerable().ToList();

                    foreach (var x in data)
                    {
                        //TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToDouble(x.IdelTime));
                        //string workHours = spWorkMin.ToString(@"hh\:mm");

                        string EndDate = "";
                        if (x.Enddate == null)
                        {
                            EndDate = "";
                        }
                        else
                        {
                            EndDate = Convert.ToDateTime(x.Enddate).ToString("dd/MM/yyyy");
                        }

                        //DateTime time = Convert.ToDateTime(x.StartTime);
                        string displayTime = Convert.ToDateTime(x.Startdate).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        string time = Convert.ToDateTime(x.StartTime).ToString("HH:mm:ss");
                        obj.Add(new SBAEmpolyeeSummaryGrid()
                        {
                            UserName = x.userName,
                            userId = x.userId,
                            // Date = Convert.ToDateTime(x.date).ToString("dd/MM/yyyy"),
                            daDate = (x.Startdate == null ? "" : Convert.ToDateTime(x.Startdate).ToString("dd/MM/yyyy")),
                            StartTime = x.StartTime,
                            DaEndDate = EndDate,
                            EndTime = x.EndTime,
                            Totalhousecollection = (x.Totalhousecollection).ToString(),
                            Totaldumpyard = (x.Totaldumpyard).ToString(),
                            Totaldistance = string.Format("{0:0.0}", (x.Totaldistance)).ToString(),
                            InBatteryStatus = x.InBatteryStatus,
                            OutBatteryStatus = x.OutBatteryStatus,
                            daDateTIme = (displayTime + " " + time)

                            //daDateTIme = Convert.ToDateTime(x.Startdate + x.StartTime).ToString("dd/MM/yyyy hh:mm tt"),
                            //DateTime startDate = Convert.ToDateTime(a + " " + Time1);

                            //String.Format("{0:0.00}", 123.4567);
                            //IdelTime = workHours
                            //AnsDate = Convert.ToDateTime(x.AnsDate).ToString("dd/MM/yyyy hh:mm tt"),
                        });
                    }


                    //if (Convert.ToDateTime(fdate).ToString("dd/MM/yyyy") == Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy"))
                    //{
                    //    data = data.Where(c => (c.daDate == fdate || c.daEndDate == fdate || c.endTime == "")).ToList();

                    //    //data = data.GroupBy(o => o.userId).Select(o => o.First()).AsEnumerable().ToList();
                    //}
                    //else {

                    //    var abc = data.Where(c => (c.daDate >= fdate && c.daDate <= tdate) || (c.daDate >= fdate && c.daDate <= tdate)).ToList();
                    //}

                    //foreach (var x in data)
                    //{

                    //    ///x.daDate = checkNull(x.daDate.tp);
                    //    x.endLat = checkNull(x.endLat);
                    //    x.endLong = checkNull(x.endLong);
                    //    x.endTime = checkNull(x.endTime);
                    //    x.startLat = checkNull(x.startLat);
                    //    x.startLong = checkNull(x.startLong);
                    //    x.startTime = checkNull(x.startTime);
                    //    //x.vehicleNumber = checkNull(x.vehicleNumber);
                    //    //x.daEndNote = checkNull(x.daEndNote);
                    //    //x.daStartNote = checkNull(x.daStartNote);
                    //    string endate = "";
                    //    if (x.daEndDate == null)
                    //    {
                    //        endate = "";
                    //    }
                    //    else {
                    //        endate = Convert.ToDateTime(x.daEndDate).ToString("dd/MM/yyyy");
                    //    }
                    //    obj.Add(new SBAAttendenceGrid()
                    //    {
                    //        daID = x.daID,
                    //        userId = Convert.ToInt32(x.userId),
                    //        userName = db.UserMasters.Where(c => c.userId == x.userId).FirstOrDefault().userName,
                    //        daDate = Convert.ToDateTime(x.daDate).ToString("dd/MM/yyyy"),
                    //        daEndDate = endate,
                    //        startTime = x.startTime,
                    //        endTime = x.endTime,
                    //        startLat = x.startLat,
                    //        startLong = x.startLong,
                    //        endLat = x.startLong,
                    //        endLong = x.endLong,
                    //        vtId = vt,
                    //        vehicleNumber = x.vehicleNumber,
                    //        CompareDate = x.daDate,
                    //    });
                    //}

                    //if (!string.IsNullOrEmpty(SearchString))
                    //{
                    //    var model = obj.Where(c => c.vehicleNumber.Contains(SearchString) || c.daDate.Contains(SearchString) || c.endTime.Contains(SearchString) || c.startLat.Contains(SearchString) || c.endLat.Contains(SearchString) || c.startTime.Contains(SearchString) || c.userName.Contains(SearchString) || c.vtId.Contains(SearchString)

                    //    || c.vehicleNumber.ToLower().Contains(SearchString) || c.vtId.ToLower().Contains(SearchString) || c.daDate.ToLower().Contains(SearchString) || c.endTime.ToLower().Contains(SearchString) || c.startLat.ToLower().Contains(SearchString) || c.endLat.ToLower().Contains(SearchString) || c.startTime.ToLower().Contains(SearchString) || c.userName.ToLower().Contains(SearchString)).ToList();

                    //    obj = model.ToList();
                    //}

                    //if (!string.IsNullOrEmpty(fdate.ToString()))
                    //{
                    //    DateTime? dt1 = null;
                    //    if (!string.IsNullOrEmpty(tdate.ToString()))
                    //    { dt1 = tdate; }
                    //    else { dt1 = fdate; }
                    //    obj = obj.Where(fullEntry => fullEntry.CompareDate >= fdate && fullEntry.CompareDate <= dt1).OrderByDescending(c => c.CompareDate).ToList();
                    //}

                    //comment by saurabh - 24 July 2019
                    //if (userId > 0)
                    //{
                    //    var model = obj.Where(c => c.userId == userId).ToList();

                    //    obj = model.ToList();
                    //}
                    //return obj.OrderByDescending(c => c.daID );


                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        var model = obj.Where(c => c.daDate.Contains(SearchString) || c.UserName.Contains(SearchString)

                        || c.daDate.ToLower().Contains(SearchString) || c.UserName.ToLower().Contains(SearchString)).ToList();

                        obj = model.ToList();
                    }

                    if (userId > 0)
                    {
                        var model = obj.Where(c => c.userId == userId).ToList();

                        obj = model.ToList();
                    }
                    //return obj.OrderByDescending(c => c.daID);
                    var f = obj.OrderByDescending(c => DateTime.Parse(c.daDateTIme)).ToList();
                    return f;
                }
            }
            else if (Emptype == "S")
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(appId))
                {
                    //if (userId == 0)
                    //{
                    //     userId = null; 
                    //}
                    //db.Database.CommandTimeout = 5000;
                    db.Database.CommandTimeout = 500;
                    var data = db.SP_SSEmployeeSummary(fdate, tdate, userId <= 0 ? null : userId, Emptype).ToList();
                    // var data2 = data.OrderByDescending(c => c.Startdate).ThenByDescending(c => c.StartTime).ToList();
                    //var data2 = data1.GroupBy(o => o.userId).Select(o => o.First()).AsEnumerable().ToList();

                    foreach (var x in data)
                    {
                        //TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToDouble(x.IdelTime));
                        //string workHours = spWorkMin.ToString(@"hh\:mm");

                        string EndDate = "";
                        if (x.Enddate == null)
                        {
                            EndDate = "";
                        }
                        else
                        {
                            EndDate = Convert.ToDateTime(x.Enddate).ToString("dd/MM/yyyy");
                        }

                        //DateTime time = Convert.ToDateTime(x.StartTime);
                        string displayTime = Convert.ToDateTime(x.Startdate).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        string time = Convert.ToDateTime(x.StartTime).ToString("HH:mm:ss");
                        obj.Add(new SBAEmpolyeeSummaryGrid()
                        {
                            UserName = x.userName,
                            userId = x.userId,
                            // Date = Convert.ToDateTime(x.date).ToString("dd/MM/yyyy"),
                            daDate = (x.Startdate == null ? "" : Convert.ToDateTime(x.Startdate).ToString("dd/MM/yyyy")),
                            StartTime = x.StartTime,
                            DaEndDate = EndDate,
                            EndTime = x.EndTime,
                            Totalhousecollection = (x.Totalhousecollection).ToString(),
                            Totaldumpyard = (x.Totaldumpyard).ToString(),
                            Totaldistance = string.Format("{0:0.0}", (x.Totaldistance)).ToString(),
                            InBatteryStatus = x.InBatteryStatus,
                            OutBatteryStatus = x.OutBatteryStatus,
                            daDateTIme = (displayTime + " " + time)

                            //daDateTIme = Convert.ToDateTime(x.Startdate + x.StartTime).ToString("dd/MM/yyyy hh:mm tt"),
                            //DateTime startDate = Convert.ToDateTime(a + " " + Time1);

                            //String.Format("{0:0.00}", 123.4567);
                            //IdelTime = workHours
                            //AnsDate = Convert.ToDateTime(x.AnsDate).ToString("dd/MM/yyyy hh:mm tt"),
                        });
                    }


                    //if (Convert.ToDateTime(fdate).ToString("dd/MM/yyyy") == Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy"))
                    //{
                    //    data = data.Where(c => (c.daDate == fdate || c.daEndDate == fdate || c.endTime == "")).ToList();

                    //    //data = data.GroupBy(o => o.userId).Select(o => o.First()).AsEnumerable().ToList();
                    //}
                    //else {

                    //    var abc = data.Where(c => (c.daDate >= fdate && c.daDate <= tdate) || (c.daDate >= fdate && c.daDate <= tdate)).ToList();
                    //}

                    //foreach (var x in data)
                    //{

                    //    ///x.daDate = checkNull(x.daDate.tp);
                    //    x.endLat = checkNull(x.endLat);
                    //    x.endLong = checkNull(x.endLong);
                    //    x.endTime = checkNull(x.endTime);
                    //    x.startLat = checkNull(x.startLat);
                    //    x.startLong = checkNull(x.startLong);
                    //    x.startTime = checkNull(x.startTime);
                    //    //x.vehicleNumber = checkNull(x.vehicleNumber);
                    //    //x.daEndNote = checkNull(x.daEndNote);
                    //    //x.daStartNote = checkNull(x.daStartNote);
                    //    string endate = "";
                    //    if (x.daEndDate == null)
                    //    {
                    //        endate = "";
                    //    }
                    //    else {
                    //        endate = Convert.ToDateTime(x.daEndDate).ToString("dd/MM/yyyy");
                    //    }
                    //    obj.Add(new SBAAttendenceGrid()
                    //    {
                    //        daID = x.daID,
                    //        userId = Convert.ToInt32(x.userId),
                    //        userName = db.UserMasters.Where(c => c.userId == x.userId).FirstOrDefault().userName,
                    //        daDate = Convert.ToDateTime(x.daDate).ToString("dd/MM/yyyy"),
                    //        daEndDate = endate,
                    //        startTime = x.startTime,
                    //        endTime = x.endTime,
                    //        startLat = x.startLat,
                    //        startLong = x.startLong,
                    //        endLat = x.startLong,
                    //        endLong = x.endLong,
                    //        vtId = vt,
                    //        vehicleNumber = x.vehicleNumber,
                    //        CompareDate = x.daDate,
                    //    });
                    //}

                    //if (!string.IsNullOrEmpty(SearchString))
                    //{
                    //    var model = obj.Where(c => c.vehicleNumber.Contains(SearchString) || c.daDate.Contains(SearchString) || c.endTime.Contains(SearchString) || c.startLat.Contains(SearchString) || c.endLat.Contains(SearchString) || c.startTime.Contains(SearchString) || c.userName.Contains(SearchString) || c.vtId.Contains(SearchString)

                    //    || c.vehicleNumber.ToLower().Contains(SearchString) || c.vtId.ToLower().Contains(SearchString) || c.daDate.ToLower().Contains(SearchString) || c.endTime.ToLower().Contains(SearchString) || c.startLat.ToLower().Contains(SearchString) || c.endLat.ToLower().Contains(SearchString) || c.startTime.ToLower().Contains(SearchString) || c.userName.ToLower().Contains(SearchString)).ToList();

                    //    obj = model.ToList();
                    //}

                    //if (!string.IsNullOrEmpty(fdate.ToString()))
                    //{
                    //    DateTime? dt1 = null;
                    //    if (!string.IsNullOrEmpty(tdate.ToString()))
                    //    { dt1 = tdate; }
                    //    else { dt1 = fdate; }
                    //    obj = obj.Where(fullEntry => fullEntry.CompareDate >= fdate && fullEntry.CompareDate <= dt1).OrderByDescending(c => c.CompareDate).ToList();
                    //}

                    //comment by saurabh - 24 July 2019
                    //if (userId > 0)
                    //{
                    //    var model = obj.Where(c => c.userId == userId).ToList();

                    //    obj = model.ToList();
                    //}
                    //return obj.OrderByDescending(c => c.daID );


                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        var model = obj.Where(c => c.daDate.Contains(SearchString) || c.UserName.Contains(SearchString)

                        || c.daDate.ToLower().Contains(SearchString) || c.UserName.ToLower().Contains(SearchString)).ToList();

                        obj = model.ToList();
                    }

                    if (userId > 0)
                    {
                        var model = obj.Where(c => c.userId == userId).ToList();

                        obj = model.ToList();
                    }
                    //return obj.OrderByDescending(c => c.daID);
                    var f = obj.OrderByDescending(c => DateTime.Parse(c.daDateTIme)).ToList();
                    return f;
                }
            }
            else
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(appId))
                {
                    //if (userId == 0)
                    //{
                    //     userId = null; 
                    //}
                    //db.Database.CommandTimeout = 5000;
                    db.Database.CommandTimeout = 500;
                    var data = db.SP_EmployeeSummary(fdate, tdate, userId <= 0 ? null : userId).ToList();
                    // var data2 = data.OrderByDescending(c => c.Startdate).ThenByDescending(c => c.StartTime).ToList();
                    //var data2 = data1.GroupBy(o => o.userId).Select(o => o.First()).AsEnumerable().ToList();

                    foreach (var x in data)
                    {
                        //TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToDouble(x.IdelTime));
                        //string workHours = spWorkMin.ToString(@"hh\:mm");

                        string EndDate = "";
                        if (x.Enddate == null)
                        {
                            EndDate = "";
                        }
                        else
                        {
                            EndDate = Convert.ToDateTime(x.Enddate).ToString("dd/MM/yyyy");
                        }

                        //DateTime time = Convert.ToDateTime(x.StartTime);
                        string displayTime = Convert.ToDateTime(x.Startdate).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        string time = Convert.ToDateTime(x.StartTime).ToString("HH:mm:ss");
                        obj.Add(new SBAEmpolyeeSummaryGrid()
                        {
                            UserName = x.userName,
                            userId = x.userId,
                            // Date = Convert.ToDateTime(x.date).ToString("dd/MM/yyyy"),
                            daDate = (x.Startdate == null ? "" : Convert.ToDateTime(x.Startdate).ToString("dd/MM/yyyy")),
                            StartTime = x.StartTime,
                            DaEndDate = EndDate,
                            EndTime = x.EndTime,
                            Totalhousecollection = (x.Totalhousecollection).ToString(),
                            Totaldumpyard = (x.Totaldumpyard).ToString(),
                            Totaldistance = string.Format("{0:0.0}", (x.Totaldistance)).ToString(),
                            InBatteryStatus = x.InBatteryStatus,
                            OutBatteryStatus = x.OutBatteryStatus,
                            daDateTIme = (displayTime + " " + time)

                            //daDateTIme = Convert.ToDateTime(x.Startdate + x.StartTime).ToString("dd/MM/yyyy hh:mm tt"),
                            //DateTime startDate = Convert.ToDateTime(a + " " + Time1);

                            //String.Format("{0:0.00}", 123.4567);
                            //IdelTime = workHours
                            //AnsDate = Convert.ToDateTime(x.AnsDate).ToString("dd/MM/yyyy hh:mm tt"),
                        });
                    }


                    //if (Convert.ToDateTime(fdate).ToString("dd/MM/yyyy") == Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy"))
                    //{
                    //    data = data.Where(c => (c.daDate == fdate || c.daEndDate == fdate || c.endTime == "")).ToList();

                    //    //data = data.GroupBy(o => o.userId).Select(o => o.First()).AsEnumerable().ToList();
                    //}
                    //else {

                    //    var abc = data.Where(c => (c.daDate >= fdate && c.daDate <= tdate) || (c.daDate >= fdate && c.daDate <= tdate)).ToList();
                    //}

                    //foreach (var x in data)
                    //{

                    //    ///x.daDate = checkNull(x.daDate.tp);
                    //    x.endLat = checkNull(x.endLat);
                    //    x.endLong = checkNull(x.endLong);
                    //    x.endTime = checkNull(x.endTime);
                    //    x.startLat = checkNull(x.startLat);
                    //    x.startLong = checkNull(x.startLong);
                    //    x.startTime = checkNull(x.startTime);
                    //    //x.vehicleNumber = checkNull(x.vehicleNumber);
                    //    //x.daEndNote = checkNull(x.daEndNote);
                    //    //x.daStartNote = checkNull(x.daStartNote);
                    //    string endate = "";
                    //    if (x.daEndDate == null)
                    //    {
                    //        endate = "";
                    //    }
                    //    else {
                    //        endate = Convert.ToDateTime(x.daEndDate).ToString("dd/MM/yyyy");
                    //    }
                    //    obj.Add(new SBAAttendenceGrid()
                    //    {
                    //        daID = x.daID,
                    //        userId = Convert.ToInt32(x.userId),
                    //        userName = db.UserMasters.Where(c => c.userId == x.userId).FirstOrDefault().userName,
                    //        daDate = Convert.ToDateTime(x.daDate).ToString("dd/MM/yyyy"),
                    //        daEndDate = endate,
                    //        startTime = x.startTime,
                    //        endTime = x.endTime,
                    //        startLat = x.startLat,
                    //        startLong = x.startLong,
                    //        endLat = x.startLong,
                    //        endLong = x.endLong,
                    //        vtId = vt,
                    //        vehicleNumber = x.vehicleNumber,
                    //        CompareDate = x.daDate,
                    //    });
                    //}

                    //if (!string.IsNullOrEmpty(SearchString))
                    //{
                    //    var model = obj.Where(c => c.vehicleNumber.Contains(SearchString) || c.daDate.Contains(SearchString) || c.endTime.Contains(SearchString) || c.startLat.Contains(SearchString) || c.endLat.Contains(SearchString) || c.startTime.Contains(SearchString) || c.userName.Contains(SearchString) || c.vtId.Contains(SearchString)

                    //    || c.vehicleNumber.ToLower().Contains(SearchString) || c.vtId.ToLower().Contains(SearchString) || c.daDate.ToLower().Contains(SearchString) || c.endTime.ToLower().Contains(SearchString) || c.startLat.ToLower().Contains(SearchString) || c.endLat.ToLower().Contains(SearchString) || c.startTime.ToLower().Contains(SearchString) || c.userName.ToLower().Contains(SearchString)).ToList();

                    //    obj = model.ToList();
                    //}

                    //if (!string.IsNullOrEmpty(fdate.ToString()))
                    //{
                    //    DateTime? dt1 = null;
                    //    if (!string.IsNullOrEmpty(tdate.ToString()))
                    //    { dt1 = tdate; }
                    //    else { dt1 = fdate; }
                    //    obj = obj.Where(fullEntry => fullEntry.CompareDate >= fdate && fullEntry.CompareDate <= dt1).OrderByDescending(c => c.CompareDate).ToList();
                    //}

                    //comment by saurabh - 24 July 2019
                    //if (userId > 0)
                    //{
                    //    var model = obj.Where(c => c.userId == userId).ToList();

                    //    obj = model.ToList();
                    //}
                    //return obj.OrderByDescending(c => c.daID );


                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        var model = obj.Where(c => c.daDate.Contains(SearchString) || c.UserName.Contains(SearchString)

                        || c.daDate.ToLower().Contains(SearchString) || c.UserName.ToLower().Contains(SearchString)).ToList();

                        obj = model.ToList();
                    }

                    if (userId > 0)
                    {
                        var model = obj.Where(c => c.userId == userId).ToList();

                        obj = model.ToList();
                    }
                    //return obj.OrderByDescending(c => c.daID);
                    var f = obj.OrderByDescending(c => DateTime.Parse(c.daDateTIme)).ToList();
                    return f;
                }
            }

        }


        //Added By Saurabh(07 May 2019)
        public IEnumerable<DashBoardVM> getEmployeeTargetData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        {
            List<DashBoardVM> obj = new List<DashBoardVM>();
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {


                var data = db.SP_EmployeeTarget(fdate, tdate, userId).ToList();


                foreach (var x in data)
                {

                    obj.Add(new DashBoardVM()
                    {
                        UserName = x.userName,
                        Target = x.gcTarget,
                        FromDate = Convert.ToDateTime(x.fromDate).ToString("dd/MM/yyyy"),
                        ToDate = Convert.ToDateTime(x.ToDate).ToString("dd/MM/yyyy"),
                        _Count = Convert.ToInt32(x.Count),

                    });
                }
                return obj;
            }
        }

        //Added By Saurabh (3June2019)
        # region HouseScanify 
        public IEnumerable<SBAHouseScanifyDetailsGridRow> GetHouseScanifyDetailsData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.UserProfile + "/";
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {

                var data = db.SP_HouseScanify(fdate, tdate, userId).Select(x => new SBAHouseScanifyDetailsGridRow
                {
                    qrEmpId = x.qrEMpId,
                    qrEmpName = x.qrEmpName,
                    qrEmpNameMar = x.qrEmpNameMar,
                    qrEmpMobileNumber = x.qrEmpMobileNumber,
                    qrEmpAddress = x.qrEmpAddress,
                    qrEmpLoginId = x.qrEmpLoginId,
                    qrEmpPassword = x.qrEmpPassword,
                    isActive = x.isActive,
                    bloodGroup = x.bloodGroup,
                    lastModifyDate = x.lastModifyDate,
                    HouseCount = x.HouseCount,
                    PointCount = x.PointCount,
                    DumpCount = x.DumpCount,
                    LiquidCount=x.LiquidCount,
                    StreetCount=x.StreetCount,
                    BuildingCount=x.BuildingCount,
                    SlumCount=x.SlumCount,
                    CommercialCount=x.CommercialCount,
                    //StartDate = (string.IsNullOrEmpty(x.StartDate.ToString()) ? "" : Convert.ToDateTime(x.StartDate).ToString("dd/MM/yyyy")) + " " + x.StartTime,
                    //StartDate = (x.StartDate == null ? "" : Convert.ToDateTime(x.StartDate).ToString("dd/MM/yyyy")) + " " + x.StartTime,
                    // EndDate = (x.EndDate == null ? " " : Convert.ToDateTime(x.EndDate).ToString("dd/MM/yyyy")) + " " +  x.EndTime,
                    // EndDate = string.IsNullOrEmpty(x.EndDate) ? " " : x.EndDate + " " + string.IsNullOrEmpty(x.EndTime) ? " " : x.EndTime
                    // StartDate = string.IsNullOrEmpty(Convert.ToString(x.StartDate)) ? " " :Convert.ToDateTime(x.StartDate).ToString("dd/MM/yyyy")  + " " + string.IsNullOrEmpty (x.StartTime) ? "" :x.StartTime,
                    //  EndDate = string.IsNullOrEmpty(Convert.ToString(x.EndDate)) ? " " : Convert.ToDateTime(x.EndDate).ToString("dd/MM/yyyy") + " " + string.IsNullOrEmpty((x.EndTime) ? "" : x.EndTime
                    //StartDate = x.StartDate == null ? "" : Convert.ToDateTime(x.StartDate).ToString("dd/MM/yyyy"),
                    //StartTime = x.StartTime,
                    //EndTime = x.EndTime
                    //  string.IsNullOrEmpty(c.userNameMar) ? " " : c.userNameMar

                }).ToList();


                //if (!string.IsNullOrEmpty(SearchString))
                //{
                //    var model = data.Where(c => c.userMobileNumber.ToString().Contains(SearchString) || c.userEmployeeNo.ToString().Contains(SearchString)
                //    || c.userAddress.ToString().Contains(SearchString) || c.userName.ToString().Contains(SearchString) || c.userNameMar.ToString().Contains(SearchString) || c.bloodGroup.ToString().Contains(SearchString)

                //    || c.userMobileNumber.Contains(SearchString) || c.userAddress.ToLower().ToString().Contains(SearchString) || c.userName.ToLower().ToString().Contains(SearchString) || c.userNameMar.ToLower().ToString().Contains(SearchString)
                //    || c.userEmployeeNo.ToLower().ToString().Contains(SearchString) || c.bloodGroup.ToLower().ToString().Contains(SearchString)

                //    || c.userMobileNumber.ToUpper().ToString().Contains(SearchString) || c.userNameMar.ToUpper().ToString().Contains(SearchString) || c.userName.ToUpper().ToString().Contains(SearchString) || c.bloodGroup.ToUpper().ToString().Contains(SearchString) || c.userAddress.ToUpper().ToString().Contains(SearchString) || c.userEmployeeNo.ToUpper().ToString().Contains(SearchString)).ToList();

                //    data = model.ToList();
                //}
                return data.OrderByDescending(c => c.LiquidCount).OrderByDescending(c=>c.HouseCount).OrderByDescending(c=>c.StreetCount);
            }
        }

        public IEnumerable<SBAHSAttendanceGrid> GetHSAttendeceData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        {
            List<SBAHSAttendanceGrid> obj = new List<SBAHSAttendanceGrid>();
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {

                //var data1 = (from t1 in db.Qr_Employee_Daily_Attendance
                //             join t2 in db.QrEmployeeMasters on t1.qrEmpId equals t2.qrEmpId
                //             select new
                //             {
                //                 t1.qrEmpDaId,
                //                 t1.qrEmpId,
                //                 t1.startDate,
                //                 t1.startTime,
                //                 t1.endDate,
                //                 t1.endTime,
                //                 t1.startNote,
                //                 t1.endNote,
                //                 t1.startLat,
                //                 t1.startLong,
                //                 t1.endLat,
                //                 t1.endLong,
                //                 t2.qrEmpName
                //             }).ToList();

                //var data1 = db.Locations.Where(l => l.datetime >= fdate && l.datetime <= tdate)

                var data = (from t1 in db.Qr_Employee_Daily_Attendance
                            join t2 in db.QrEmployeeMasters on t1.qrEmpId equals t2.qrEmpId
                            select new
                            {
                                t1.qrEmpDaId,
                                t1.qrEmpId,
                                t1.startDate,
                                t1.endDate,
                                t1.startTime,
                                t1.endTime,
                                t1.startLat,
                                t1.startLong,
                                t1.endLat,
                                t1.endLong,
                                t1.startNote,
                                t1.endNote,
                                t2.qrEmpName,

                            }).OrderByDescending(c => c.qrEmpDaId).ToList();//.OrderByDescending(c => c.startTime).ToList();

                //return obj.OrderBy(c => c.Date).ThenByDescending(c => c.StartTime);

                //var data = db.Qr_Employee_Daily_Attendance.OrderByDescending(c => c.qrEmpDaId).ToList();

                if (Convert.ToDateTime(fdate).ToString("dd/MM/yyyy") == Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy"))
                {
                    data = data.Where(c => (c.startDate == fdate || c.endDate == fdate || c.endTime == "")).ToList();
                }
                else
                {

                    data = data.Where(c => (c.startDate >= fdate && c.startDate <= tdate) || (c.startDate >= fdate && c.startDate <= tdate)).ToList();
                }

                if (userId > 0)
                {
                    var model = data.Where(c => c.qrEmpId == userId).ToList();

                    data = model.ToList();
                }

                foreach (var x in data)
                {

                    DateTime cDate = DateTime.Now;

                    TimeSpan timespan = new TimeSpan(00, 00, 00);
                    DateTime time = DateTime.Now.Add(timespan);
                    string displayTime = time.ToString("hh:mm tt");


                    string displayTime1 = Convert.ToDateTime(x.startDate).ToString("MM/dd/yyyy");
                    string sTime = Convert.ToDateTime(x.startTime).ToString("HH:mm:ss");

                    var a = (Convert.ToDateTime(x.startDate).ToString("MM/dd/yyyy"));
                    var b = x.endDate == null ? Convert.ToDateTime(cDate).ToString("MM/dd/yyyy") : Convert.ToDateTime(x.endDate).ToString("MM/dd/yyyy");

                    string Time1 = (x.startTime).ToString();
                    string Time2 = ((x.endTime == "" ? displayTime : x.endTime).ToString());

                    DateTime startDate = Convert.ToDateTime(a + " " + Time1);
                    DateTime endDate = Convert.ToDateTime(b + " " + Time2);
                    var houseCount = db.HouseMasters.Where(c => c.modified >= startDate && c.modified <= endDate && c.userId == x.qrEmpId).Count();
                    var liquidCount = db.LiquidWasteDetails.Where(c => c.lastModifiedDate >= startDate && c.lastModifiedDate <= endDate && c.userId == x.qrEmpId).Count();
                    var streetCount = db.StreetSweepingDetails.Where(c => c.lastModifiedDate >= startDate && c.lastModifiedDate <= endDate && c.userId == x.qrEmpId).Count();
                    var dumpyardcount = db.DumpYardDetails.Where(c => c.lastModifiedDate >= startDate && c.lastModifiedDate <= endDate && c.userId == x.qrEmpId).Count();
                    ///x.daDate = checkNull(x.daDate.tp);
                    //x.endLat = checkNull(x.endLat);
                    //x.endLong = checkNull(x.endLong);
                    //x.endTime = checkNull(x.endTime);
                    //x.startLat = checkNull(x.startLat);
                    //x.startLong = checkNull(x.startLong);
                    //x.startTime = checkNull(x.startTime);

                    //x.endNote = checkNull(x.endNote);
                    //x.startNote = checkNull(x.startNote);
                    string endate = "";
                    if (x.endDate == null)
                    {
                        endate = "";
                    }
                    else
                    {
                        endate = Convert.ToDateTime(x.endDate).ToString("dd/MM/yyyy");
                    }
                    obj.Add(new SBAHSAttendanceGrid()
                    {
                        qrEmpDaId = x.qrEmpDaId,
                        qrEmpId = Convert.ToInt32(x.qrEmpId),
                        userName = x.qrEmpName,
                        startDate = Convert.ToDateTime(x.startDate).ToString("dd/MM/yyyy"),
                        endDate = endate,
                        startTime = checkNull(x.startTime),
                        endTime = checkNull(x.endTime),
                        startLat = checkNull(x.startLat),
                        startLong = checkNull(x.startLong),
                        endLat = checkNull(x.endLat),
                        endLong = checkNull(x.endLong),
                        startNote = checkNull(x.startNote),
                        endNote = checkNull(x.endNote),
                        CompareDate = x.startDate,
                        HouseCount = houseCount,
                        LiquidCount=liquidCount,
                        StreetCount= streetCount,
                        DumpYardCount=dumpyardcount,
                        daDateTIme = (displayTime1 + " " + sTime)



                    });
                }

                //if (!string.IsNullOrEmpty(SearchString))
                //{
                //    var model = obj.Where(c => c.vehicleNumber.Contains(SearchString) || c.startDate.Contains(SearchString) || c.endTime.Contains(SearchString) || c.startLat.Contains(SearchString) || c.endLat.Contains(SearchString) || c.startTime.Contains(SearchString) || c.userName.Contains(SearchString) || c.vtId.Contains(SearchString)

                //    || c.vehicleNumber.ToLower().Contains(SearchString) || c.vtId.ToLower().Contains(SearchString) || c.startDate.ToLower().Contains(SearchString) || c.endTime.ToLower().Contains(SearchString) || c.startLat.ToLower().Contains(SearchString) || c.endLat.ToLower().Contains(SearchString) || c.startTime.ToLower().Contains(SearchString) || c.userName.ToLower().Contains(SearchString)).ToList();

                //    obj = model.ToList();
                //}

                //if (!string.IsNullOrEmpty(fdate.ToString()))
                //{
                //    DateTime? dt1 = null;
                //    if (!string.IsNullOrEmpty(tdate.ToString()))
                //    { dt1 = tdate; }
                //    else { dt1 = fdate; }
                //    obj = obj.Where(fullEntry => fullEntry.CompareDate >= fdate && fullEntry.CompareDate <= dt1).OrderByDescending(c => c.CompareDate).ToList();
                //}

                return obj.OrderByDescending(c => c.qrEmpDaId).ToList();
            }
        }

        #endregion
        //Added By Nishikant (2 Jully 2019)
        public IEnumerable<GPLeagueGridRow> GetLeagueDetailsData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        {
            List<GPLeagueGridRow> objData = new List<GPLeagueGridRow>();
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.UserProfile + "/";
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                //string json = new WebClient().DownloadString(appDetails.Grampanchayat_Pro + "/api/Get/AnswerDetailsList?appId=1");
                //string json = new WebClient().DownloadString("http://localhost:6067/api/Get/AnswerDetailsList?appId=1");

                WebClient WebClient = new WebClient();
                WebClient.Encoding = System.Text.Encoding.UTF8;

                var json = WebClient.DownloadString(appDetails.Grampanchayat_Pro + "/api/Get/AnswerDetailsList?appId=" + appDetails.GramPanchyatAppID + "&LangId=1&Fdate=" + fdate + "&Tdate=" + tdate);
                List<GPLeagueGridRow> obj = JsonConvert.DeserializeObject<List<GPLeagueGridRow>>(json).Where(c => Convert.ToDateTime(c.AnsDate) >= fdate & Convert.ToDateTime(c.AnsDate) <= tdate).ToList();

                //List<GPLeagueGridRow> obj = JsonConvert.DeserializeObject<List<GPLeagueGridRow>>(json).ToList();

                foreach (var x in obj)
                {
                    var q = db.HouseDetails_ReferanceId(x.ReferenceId).FirstOrDefault();

                    if (q != null)
                    {
                        objData.Add(new GPLeagueGridRow()
                        {
                            ID = x.AnswerDetailId,
                            HouseId = (string.IsNullOrEmpty(x.ReferenceId) ? "NA" : x.ReferenceId),
                            Name = (string.IsNullOrEmpty(q.Name) ? x.Name : q.Name),
                            Zone = (string.IsNullOrEmpty(q.Zone) ? "NA" : q.Zone),
                            Ward = (string.IsNullOrEmpty(q.Ward) ? "NA" : q.Ward),
                            Area = (string.IsNullOrEmpty(q.Area) ? "NA" : q.Area),
                            AnsDate = Convert.ToDateTime(x.AnsDate).ToString("dd/MM/yyyy hh:mm tt"),
                            JSon = x.JSon
                        });
                    }
                    else
                    {
                        objData.Add(new GPLeagueGridRow()
                        {
                            ID = x.AnswerDetailId,
                            HouseId = "NA",
                            Name = x.Name,
                            Zone = "NA",
                            Ward = "NA",
                            Area = "NA",
                            AnsDate = Convert.ToDateTime(x.AnsDate).ToString("dd/MM/yyyy hh:mm tt"),
                            JSon = x.JSon
                        });
                    }

                }

                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = objData.Where(c => (
                          (string.IsNullOrEmpty(c.AnswerDetailId.ToString()) ? " " : c.AnswerDetailId.ToString()) + " "
                        + (string.IsNullOrEmpty(c.Name.ToString()) ? " " : c.Name) + " "
                        + (string.IsNullOrEmpty(c.Zone.ToString()) ? " " : c.Zone) + " "
                        + (string.IsNullOrEmpty(c.Ward.ToString()) ? "" : c.Ward) + " "
                        + (string.IsNullOrEmpty(c.Area.ToString()) ? " " : c.Area)
                    ).ToUpper().Contains(SearchString.ToUpper())).ToList();

                    objData = model.ToList();
                }

                return objData.OrderByDescending(c => c.AnsDate);

            }
        }

        # region Sauchalay 
        public IEnumerable<SBASauchalayDetailsGridRow> GetSauchalayDetailsData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        {

            //var byid = db.gameplayerdetails
            //                   .where(g => g.created >= fdate && g.created <= tdate)
            //                   .tolist();

            //var gamemaster = dbmain.gamemasters.tolist();

            //var data = (from t1 in byid
            //            join t2 in gamemaster on t1.gameid equals t2.gameid
            //            select new { t1.id, t1.name, t1.mobile, t1.score, t1.created, t2.gameid, t2.gamename }).tolist();



            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();
            List<SBASauchalayDetailsGridRow> obj = new List<SBASauchalayDetailsGridRow>();

            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {


                var mData = dbMain.Sauchalay_feedback.Where(x => x.AppId == appId && x.Date >= fdate && x.Date <= tdate).ToList();


                var cData = db.SauchalayAddresses.ToList();

                //var data = (from t1 in mData
                //            join t2 in cData on t1.SauchalayID equals t2.SauchalayID
                //            select new { t1.SauchalayFeedback_ID, t1.ULB, t1.SauchalayID, t1.AppId, t1.Fullname, t1.MobileNo, t1.que1, t1.que2, t1.que3,                            t1.Rating,t1.Feedback,t1.Date,t2.Address });

                var data = (from t1 in mData
                            join t2 in cData on t1.SauchalayID equals t2.SauchalayID into ps
                            from t3 in ps.DefaultIfEmpty()
                            select new { t1.SauchalayFeedback_ID, t1.ULB, t1.SauchalayID, t1.AppId, t1.Fullname, t1.MobileNo, t1.que1, t1.que2, t1.que3, t1.Rating, t1.Feedback, t1.Date, Address = (t3 == null ? string.Empty : t3.Address) }).ToList();


                if (data != null)
                {
                    foreach (var x in data)
                    {
                        ///x.daDate = checkNull(x.daDate.tp);
                        //x.SauchalayFeedback_ID = x.SauchalayFeedback_ID;
                        //x.ULB = checkNull(x.ULB);
                        //x.SauchalayID = x.SauchalayID;
                        //x.AppId = x.AppId;
                        //x.Fullname = checkNull(x.Fullname);
                        //x.MobileNo = checkNull(x.MobileNo);
                        //x.que1 = checkNull(x.que1);
                        //x.que2 = checkNull(x.que2);
                        //x.que3 = checkNull(x.que3);
                        //x.Rating = checkNull(x.Rating);
                        //x.Feedback = checkNull(x.Feedback);
                        //x.Date = x.Date;
                        //  Convert.ToDateTime(x.daEndDate).ToString("dd/MM/yyyy");
                        obj.Add(new SBASauchalayDetailsGridRow()
                        {
                            SauchalayFeedback_ID = x.SauchalayFeedback_ID,
                            ULB = x.ULB,
                            SauchalayID = x.SauchalayID,
                            Address = x.Address,
                            AppId = x.AppId,
                            Fullname = x.Fullname,
                            MobileNo = x.MobileNo,
                            que1 = x.que1,
                            que2 = x.que2,
                            que3 = x.que3,
                            Rating = x.Rating,
                            Feedback = x.Feedback,
                            Date = Convert.ToDateTime(x.Date).ToString("dd/MM/yyyy  hh:mm tt"),
                            // date = Convert.ToDateTime(x.createdDate2).ToString("dd/MM/yyyy hh:mm tt"),
                        });


                    }
                }

            }
            return obj.OrderByDescending(c => c.Date);
        }


        public IEnumerable<SauchalayRegistrationGridRow> GetSauchalayRegistrationDetailsData(long wildcard, string SearchString, int appId)
        {
            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + appDetails.HouseQRCode + "/";
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {

                var data = db.SauchalayAddresses.AsEnumerable().Select(x => new SauchalayRegistrationGridRow
                {
                    Id = x.Id,
                    SauchalayID = x.SauchalayID,
                    Name = x.Name,
                    Address = x.Address,
                    Image = (string.IsNullOrEmpty(x.ImageUrl) ? "/Images/default_not_upload.png" : x.ImageUrl),
                    QrImage = (string.IsNullOrEmpty(x.QrImageUrl) ? "/Images/default_not_upload.png" : x.QrImageUrl),
                    Mobile = x.Mobile,
                    CreatedDate = Convert.ToDateTime(x.CreatedDate).ToString("dd/MM/yyyy h:mm tt"),
                    Tot = (string.IsNullOrEmpty(x.Tot)) ? "" : GetTot(x.Tot),
                    Tns = x.Tns.HasValue ? x.Tns.ToString() : ""

                }).ToList();
                if (!string.IsNullOrEmpty(SearchString))
                {


                    var model = data.Where(c => ((string.IsNullOrEmpty(c.SauchalayID) ? " " : c.SauchalayID) + " " +
                                        (string.IsNullOrEmpty(c.Address) ? " " : c.Address) + " " +
                                        (string.IsNullOrEmpty(c.Tot) ? " " : c.Tot)).ToUpper().Contains(SearchString.ToUpper())).ToList();


                    data = model.ToList();
                }
                return data.OrderByDescending(c => c.SauchalayID);
            }
        }


        public string GetTot(string tot)
        {
            if (!string.IsNullOrEmpty(tot))
            {
                if (tot.ToUpper() == "CT")
                    return "Community Toilet";
                else if (tot.ToUpper() == "PT")
                    return "Public Toilet";
                else if (tot.ToUpper() == "U")
                    return "Urinal";
                else
                    return "";
            }
            else
                return "";
        }
        #endregion
        public IEnumerable<OnePointfourGridRow> GetOnepointfourEditData(long wildcard, string SearchString, int appId, string INSERT_ID)
        {
            int I_ID = Convert.ToInt32(INSERT_ID);
            using (var dbMain = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = dbMain.SS_1_4_ANSWER.Where(m => m.INSERT_ID == I_ID).
                    Select(x => new OnePointfourGridRow
                    {
                        INSERT_DATE = x.INSERT_DATE.Value,
                        INSERT_ID = x.INSERT_ID.Value,
                        ANS_ID = x.ANS_ID,
                        Q_ID = x.Q_ID,
                        TOTAL_COUNT = x.TOTAL_COUNT
                    }).ToList();

                return data;
            }
        }
        public IEnumerable<OnePointfourGridRow> GetOnepointfiveEditData(long wildcard, string SearchString, int appId, string INSERT_ID)
        {
            int I_ID = Convert.ToInt32(INSERT_ID);
            using (var dbMain = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = dbMain.SS_1_4_ANSWER.Where(m => m.INSERT_ID == I_ID).
                    Select(x => new OnePointfourGridRow
                    {
                        INSERT_DATE = x.INSERT_DATE.Value,
                        INSERT_ID = x.INSERT_ID.Value,
                        ANS_ID = x.ANS_ID,
                        Q_ID = x.Q_ID,
                        TOTAL_COUNT = x.TOTAL_COUNT
                    }).ToList();

                return data;
            }
        }
        public IEnumerable<OnePointfourGridRow> GetOnepointfourData(long wildcard, string SearchString, int appId)
        {
            using (var dbMain = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = dbMain.RPT_ONE_POINT_FOUR_SHOW().
                    Select(x => new OnePointfourGridRow
                    {
                        INSERT_DATE = x.insert_date.Value,
                        INSERT_ID = x.insert_id.Value
                    }).ToList();

                return data;
            }
        }
        public IEnumerable<OnePointfourGridRow> GetOnepointfiveData(long wildcard, string SearchString, int appId)
        {
            using (var dbMain = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = dbMain.RPT_ONE_POINT_Five_SHOW().
                    Select(x => new OnePointfourGridRow
                    {
                        INSERT_DATE = x.insert_date.Value,
                        INSERT_ID = x.insert_id.Value
                    }).ToList();

                return data;
            }
        }
        public IEnumerable<OnePointfourGridRow> GetOnepointsixData(long wildcard, string SearchString, int appId)
        {
            using (var dbMain = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = dbMain.RPT_ONE_POINT_Six_SHOW().
                    Select(x => new OnePointfourGridRow
                    {
                        INSERT_DATE = x.insert_date.Value,
                        INSERT_ID = x.insert_id.Value
                    }).ToList();

                return data;
            }
        }
        //by neha 13 june 2019
        public IEnumerable<EmployeeHouseCollectionType> getEmployeeHouseCollectionType(int appId)
        {
            List<EmployeeHouseCollectionType> obj = new List<EmployeeHouseCollectionType>();
            using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                var data = db.SP_EmployeeHouseCollectionType().ToList();
                //var data = "1";

                foreach (var x in data)
                {
                    obj.Add(new EmployeeHouseCollectionType()
                    {
                        inTime = x.inTime,
                        Count = x.Count,
                        ToDate = x.TodayDate.ToString(),
                        MixedCount = x.MixedCount,
                        Bifur = x.Bifur,
                        NotCollected = x.NotCollected,
                        gcTarget = x.gcTarget,
                        NotSpecidfied = x.NotSpecidfied,
                        userId = x.userId,
                        userName = x.userName,
                        ConstructionAndDemolition = x.CDW,
                        Horticulture = x.HW,
                        WetWaste = x.WetWaste,
                        DryWaste = x.DryWaste,
                        DomesticHazardous = x.DHW,
                        Sanitary = x.SW,
                        Commercial = x.CW

                        //inTime = "10.30 AM",
                        //Count = 10,
                        //ToDate = "2022-02-16",
                        //MixedCount = 10,
                        //Bifur = 10,
                        //NotCollected = 0,
                        //gcTarget = "90",
                        //NotSpecidfied = 0,
                        //userId = 1,
                        //userName = "XYZ",
                        //ConstructionAndDemolition = 10,
                        //Horticulture = 10,
                        //WetWaste = 10,
                        //DryWaste = 10,
                        //DomesticHazardous = 10,
                        //Sanitary = 10,
                        //Commercial = 10


                    });
                }
                return obj.OrderBy(c => c.userName);
            }
        }

        #region Infotainment 
        public IEnumerable<InfotainmentDetailsGridRow> GetInfotainmentDetailsData(long wildcard, string SearchString, int appId)
        {
            List<InfotainmentDetailsGridRow> obj = new List<InfotainmentDetailsGridRow>();

            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            //var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            //string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + "/GameImages/";
            //using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            //{
            var data = dbMain.GameDetails.ToList();
            if (data != null)
            {
                foreach (var x in data)
                {
                    obj.Add(new InfotainmentDetailsGridRow()
                    {
                        GameDetailsId = x.GameDetailsID,
                        GameName = dbMain.GameMasters.Where(c => c.GameId == x.GameMasterID).FirstOrDefault().GameName,
                        GameNameMar = dbMain.GameMasters.Where(c => c.GameId == x.GameMasterID).FirstOrDefault().GameName,
                        SloganId = Convert.ToInt16(x.SloganID),
                        Slogan = dbMain.Game_Slogan.Where(c => c.ID == x.SloganID).FirstOrDefault().Slogan,
                        //Image = (string.IsNullOrEmpty(x.ImageUrl) ? "/Images/default_not_upload.png" : ThumbnaiUrlCMS + x.ImageUrl),
                        Image = (string.IsNullOrEmpty(x.ImageUrl) ? "/Images/default_not_upload.png" : x.ImageUrl),
                        RightAnswer = dbMain.Game_AnswerType.Where(c => c.AnswerTypeId == x.RightAnswerID).FirstOrDefault().AnswerType,
                        Points = Convert.ToInt16(x.Point)
                    });
                }

            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                var model = obj.Where(c => ((string.IsNullOrEmpty(c.GameName) ? " " : c.GameName) + " " +
                                     (string.IsNullOrEmpty(c.Slogan) ? " " : c.Slogan) + " " +
                                     (string.IsNullOrEmpty(c.RightAnswer) ? " " : c.RightAnswer)).ToUpper().Contains(SearchString.ToUpper())).ToList();

                obj = model.ToList();
            }

            return obj.OrderByDescending(c => c.GameDetailsId);

            //}
        }

        public IEnumerable<InfotainmentPlayerDetailsGridRow> GetInfotainmentPlayerDetailsData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId)
        {
            List<InfotainmentPlayerDetailsGridRow> obj = new List<InfotainmentPlayerDetailsGridRow>();

            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();


            if (appId == 0)
            {
                var data = (from t1 in dbMain.GamePlayerDetails.Where(g => g.Created >= fdate && g.Created <= tdate)
                            join t2 in dbMain.GameMasters on t1.GameId equals t2.GameId
                            select new { t1.ID, t1.Name, t1.Mobile, t1.Score, t1.Created, t2.GameId, t2.GameName }).ToList();

                //var data = dbMain.GamePlayerDetails.Where(c => c.Created >= fdate &&  c.Created <= tdate).ToList();
                //var data1 = data.Where(c => c.GameId == userId).ToList();

                if (userId > 0)
                {
                    var model = data.Where(c => c.GameId == userId).ToList();

                    data = model.ToList();
                }
                if (data != null)
                {
                    foreach (var x in data)
                    {
                        obj.Add(new InfotainmentPlayerDetailsGridRow()
                        {
                            ID = x.ID,
                            //PlayerId = x.PlayerId,
                            Name = x.Name,
                            GameId = x.GameName,
                            Mobile = x.Mobile,
                            Score = x.Score,
                            Created = Convert.ToDateTime(x.Created).ToString("dd/MM/yyyy hh:mm tt"),
                            DisplayDateTime = Convert.ToDateTime(x.Created).ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)
                            //string time = Convert.ToDateTime(x.startTime).ToString("HH:mm:ss")
                            //Image = (string.IsNullOrEmpty(x.ImageUrl) ? "/Images/default_not_upload.png" : ThumbnaiUrlCMS + x.ImageUrl),

                        });
                    }

                }
            }
            else
            {
                using (var db = new DevChildSwachhBharatNagpurEntities(appId))
                {

                    //var data = (from t1 in dbMain.GamePlayerDetails.Where(g => g.Created >= fdate && g.Created <= tdate)
                    //            join t2 in dbMain.GameMasters on t1.GameId equals t2.GameId
                    //            select new { t1.ID, t1.Name, t1.Mobile, t1.Score, t1.Created, t2.GameId, t2.GameName }).ToList();

                    var byId = db.GamePlayerDetails
                               .Where(g => g.Created >= fdate && g.Created <= tdate)
                               .ToList();

                    var gameMaster = dbMain.GameMasters.ToList();

                    var data = (from t1 in byId
                                join t2 in gameMaster on t1.GameId equals t2.GameId
                                select new { t1.ID, t1.Name, t1.Mobile, t1.Score, t1.Created, t2.GameId, t2.GameName, t1.PlayerId }).ToList();

                    //var byId = db.GamePlayerDetails
                    //           .Where(g => g.Created >= fdate && g.Created <= tdate && g.PlayerId != "External")
                    //           .ToDictionary(s => s.ID);

                    //var ids = byId.Keys.ToList();

                    //var data = dbMain.GameMasters
                    //                .Where(y => ids.Contains(y.GameId))
                    //                .AsEnumerable() 
                    //                .Select(y => new {
                    //                   ID = byId[y.GameId].ID,
                    //                   Name = byId[y.GameId].Name,
                    //                   GameName = y.GameName,
                    //                   GameId = y.GameId,
                    //                   Mobile = byId[y.GameId].Mobile,
                    //                   Score = byId[y.GameId].Score,
                    //                   Created = byId[y.GameId].Created,

                    //                    //,   Translation = y.translation
                    //                });

                    //var data = (from t1 in db.GamePlayerDetails.Where(g => g.Created >= fdate && g.Created <= tdate)
                    //            join t2 in dbMain.GameMasters on t1.GameId equals t2.GameId
                    //            select new { t1.ID, t1.Name, t1.Mobile, t1.Score, t1.Created, t2.GameId, t2.GameName }).ToList();

                    //var data = db.GamePlayerDetails.Where(c => c.Created >= fdate && c.Created <= tdate).ToList();
                    if (userId > 0)
                    {
                        var model = data.Where(c => c.GameId == userId).ToList();

                        data = model.ToList();
                    }
                    foreach (var x in data)
                    {
                        obj.Add(new InfotainmentPlayerDetailsGridRow()
                        {
                            ID = x.ID,
                            //PlayerId = x.PlayerId,
                            Name = x.PlayerId == "External" ? x.Name : db.HouseMasters.Where(c => c.ReferanceId == x.PlayerId).FirstOrDefault().houseOwner,
                            GameId = x.GameName,
                            Mobile = x.Mobile,
                            Score = x.Score,
                            Created = Convert.ToDateTime(x.Created).ToString("dd/MM/yyyy hh:mm tt"),
                            DisplayDateTime = Convert.ToDateTime(x.Created).ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)
                            //Image = (string.IsNullOrEmpty(x.ImageUrl) ? "/Images/default_not_upload.png" : ThumbnaiUrlCMS + x.ImageUrl),
                        });
                    }

                }
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                var model = obj.Where(c => ((string.IsNullOrEmpty(c.Name) ? " " : c.Name) + " " +
                                     (string.IsNullOrEmpty(c.GameId) ? " " : c.GameId)).ToUpper().Contains(SearchString.ToUpper())).ToList();

                obj = model.ToList();
            }

            return obj.OrderByDescending(c => c.DisplayDateTime);

            //}
        }

        #endregion

        #region WasteManagement

        public IEnumerable<WM_WasteDetailsGridRow> GetWasteDetailsData(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId, int? param1, int? param2)
        {
            using (DevChildSwachhBharatNagpurEntities db = new DevChildSwachhBharatNagpurEntities(appId))
            {
                List<WM_WasteDetailsGridRow> data = new List<WM_WasteDetailsGridRow>();


                //var data1 = (from t1 in db.WM_Garbage_Details.Where(g => g.CreatedDate >= fdate && g.CreatedDate <= tdate)
                //             join t2 in db.WM_GarbageSubCategory on t1.SubCategoryID equals t2.SubCategoryID
                //             join t3 in db.UserMasters on t1.UserId equals t3.userId
                //             where t2.CategoryID == 2 && t1.UserId == t3.userId
                //             select new { t1.GarbageDetailsID, t1.SubCategoryID, t1.Weight, userId = (t1.UserId == 0 ? null : t1.UserId), t1.CreatedDate,
                //                          t2.SubCategory,t3.userName}).ToList();
                // int Param1 = (param1 == null ?Convert.ToInt32(param1 =0):param1);


                int Param2 = Convert.ToInt32(param2 == null ? 0 : param2);
                var data1 = (from t1 in db.WM_Garbage_Details.Where(g => g.CreatedDate >= fdate && g.CreatedDate <= tdate)
                             join t2 in db.WM_GarbageSubCategory on t1.SubCategoryID equals t2.SubCategoryID
                             join t3 in db.WM_GarbageCategory on t2.CategoryID equals t3.CategoryID
                             join t4 in db.UserMasters
                             on t1.UserId equals t4.userId into g
                             from t4 in g.DefaultIfEmpty()
                             where ((param1 == null && param2 == null) ? (t2.CategoryID == 1) || (t2.CategoryID == 2) :
                                    param1 != 1 && param2 == null ? t2.CategoryID == 1 :
                                    param1 == 1 && param2 != null ? t2.SubCategoryID == param2 :
                                    param1 == 2 && param2 == null ? t2.CategoryID == 2 :
                                    param1 == 2 && param2 != null ? t2.SubCategoryID == param2 : t2.CategoryID == 0)
                             select new
                             {
                                 t1.GarbageDetailsID,
                                 t1.SubCategoryID,
                                 t1.Weight,
                                 userId = (t1.UserId == 0 ? null : t1.UserId),
                                 t1.CreatedDate,
                                 t1.Source,
                                 t2.SubCategory,
                                 t3.Category,
                                 t4.userName
                             }).ToList();

                if (userId > 0)
                {
                    var model = data1.Where(x => x.userId == userId).ToList();

                    data1 = model.ToList();
                }
                if (userId == -3) // Admin = -3
                {
                    var model = data1.Where(x => x.Source == 1).ToList();
                    data1 = model.ToList();
                }
                if (userId == -2) // onlyALLMobileAppUsers = -2
                {
                    var model = data1.Where(x => x.Source == 2).ToList();
                    data1 = model.ToList();
                }

                foreach (var x in data1)
                {
                    data.Add(new WM_WasteDetailsGridRow
                    {
                        GarbageDetailsID = x.GarbageDetailsID,
                        SubCategoryID = x.SubCategoryID,
                        SubCategoryName = x.SubCategory,
                        CategoryName = x.Category,
                        //userName = db.UserMasters.FirstOrDefault(c => c.userId == x.userId).userName,
                        UserId = Convert.ToInt32(x.userId),
                        UserName = (x.userName == null ? "Admin" : x.userName),
                        Weight = x.Weight,
                        CreatedDate = Convert.ToDateTime(x.CreatedDate).ToString("dd/MM/yyyy h:mm tt"),
                        DisplayTime = Convert.ToDateTime(x.CreatedDate).ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)
                    });
                }
                //foreach (var item in data)
                //{
                //    if (item.userName != null && item.userName == "")
                //        item.userName = "";
                //    item.latlong = checkNull(item.latlong);
                //    item.date = checkNull(item.date);
                //    item.time = checkNull(item.time);


                //}
                if (!string.IsNullOrEmpty(SearchString))
                {
                    var model = data.Where(c => ((string.IsNullOrEmpty(c.UserName) ? " " : c.UserName) + " " +
                                     (string.IsNullOrEmpty(c.CreatedDate) ? " " : c.CreatedDate) + " " +
                                     (string.IsNullOrEmpty(c.CategoryName) ? " " : c.CategoryName) + " " +
                                     (string.IsNullOrEmpty(c.SubCategoryName) ? " " : c.SubCategoryName)).ToUpper().Contains(SearchString.ToUpper())).ToList();

                    data = model.OrderByDescending(c => c.DisplayTime).ToList();
                }
                return data.OrderByDescending(c => c.DisplayTime).ToList();
            }
        }

        #endregion

        #region RFID
        public IEnumerable<SBAGrabageCollectionGridRow> GetRfidDetailsData(long wildcard, string SearchString, int appId)
        {
            List<SBAGrabageCollectionGridRow> obj = new List<SBAGrabageCollectionGridRow>();

            DevSwachhBharatMainEntities dbMain = new DevSwachhBharatMainEntities();
            //var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

            //string ThumbnaiUrlCMS = appDetails.baseImageUrlCMS + appDetails.basePath + "/GameImages/";
            //using (var db = new DevChildSwachhBharatNagpurEntities(appId))
            //{
            var data = dbMain.AppDetails.FirstOrDefault();
            using (var db = new DevChildSwachhBharatNagpurEntities(data.AppId))
            {
                var data1 = db.GarbageCollectionDetails.Where(c => c.SourceId == 2).OrderByDescending(c => c.gcDate).ToList();

                foreach (var x in data1)
                {
                    obj.Add(new SBAGrabageCollectionGridRow()
                    {
                        RFIDReaderId = x.RFIDReaderId,
                        RFIDTagId = x.RFIDTagId,
                        Lat = x.Lat,
                        Long = x.Long,
                        type1 = x.garbageType.ToString(),
                        attandDate = (Convert.ToDateTime(x.gcDate) == null ? "" : Convert.ToDateTime(x.gcDate).ToString("dd/MM/yyyy hh:mm tt"))
                        //((c.Name == null ? " " : c.Name)
                    });
                }


            }

            return obj.OrderByDescending(c => c.gcDate).ToList();

            //}
        }

        #endregion
    }
}
#region old Code
//public IEnumerable<GPBusinessAppliedGridRow> GetGPBusinessAppliedData(long wildcard, string SearchString, int appId)
//{
//    DevGramPanchayatAppyMainEntities dbMain = new DevGramPanchayatAppyMainEntities();
//    var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

//    string ThumbnaiUrl = appDetails.baseImageUrl + appDetails.basePath + appDetails.BusinessDetail + "/";
//    using (DevGramPanchayatAppyNagpurEntities db = new DevGramPanchayatAppyNagpurEntities(appId))
//    {
//        var data = db.GYuvaBussinessApplieds.Select(x => new GPBusinessAppliedGridRow
//        {
//            YBAppliedId = x.YBAppliedId,
//            Name = x.Name,
//            Number = x.Number.ToString(),
//            Address = x.Address,
//            AdharrNo = x.AdharrNo,
//            Email = x.Email,
//            AboutYourSelf = x.AboutYourSelf,
//            AvailableResources = x.AvailableResources,
//            Education = x.Education,
//            Skill = x.Skill,
//            BusinessField = x.BusinessField,
//            Bid = x.YBId,
//            Description = x.Description,
//            Date = x.CreatedDate.ToString(),
//            languageId = x.languageId,
//            Image = x.Image,
//            Image1 = x.Image1,
//            Image2 = x.Image2,
//            Image3 = x.Image3,
//            Lat_Log = x.Lat_Log,
//            Lat_Log1 = x.Lat_Log1,
//            Lat_Log2 = x.Lat_Log2,
//            Lat_Log3 = x.Lat_Log3

//        }).ToList();
//        foreach (var item in data)
//        {
//            item.language = db.LanguageInfoes.Where(x => x.id == item.languageId).Select(x => x.languageType).FirstOrDefault();
//            item.Date = Convert.ToDateTime(item.Date).ToString("dd MMM yyyy h:mm tt");
//            if (item.Bid != 0 && item.Bid != null)
//            { item.Buisness = db.GYuvaBussniessDetails.Where(x => x.YBusniessId == item.Bid).Select(x => x.Title).FirstOrDefault(); }
//            else
//            { item.Buisness = null; }

//            if (item.Image != "" && item.Image != null)
//            {
//                item.Image = ThumbnaiUrl + item.Image;
//                item.Lat_Log = item.Lat_Log;
//            }
//            else
//            {
//                item.Image = "default.png";
//                item.Lat_Log = "";
//            }
//            if (item.Image1 != "" && item.Image1 != null)
//            {
//                item.Image1 = ThumbnaiUrl + item.Image1;
//                item.Lat_Log1 = item.Lat_Log1;
//            }
//            else
//            {
//                item.Image1 = "default.png";
//                item.Lat_Log1 = "";
//            }

//            if (item.Image2 != "" && item.Image2 != null)
//            {
//                item.Image2 = ThumbnaiUrl + item.Image2;
//                item.Lat_Log2 = item.Lat_Log2;
//            }
//            else
//            {
//                item.Image2 = "default.png";
//                item.Lat_Log2 = "";
//            }
//            if (item.Image3 != "" && item.Image3 != null)
//            {
//                item.Image3 = ThumbnaiUrl + item.Image3;
//                item.Lat_Log3 = item.Lat_Log3;
//            }
//            else
//            {
//                item.Image3 = "default.png";
//                item.Lat_Log3 = "";
//            }
//        }

//        return data;
//    }
////}
//public IEnumerable<GPJobGridRow> GetGPJobData(long wildcard, string SearchString, int appId)
//{
//    DevGramPanchayatAppyMainEntities dbMain = new DevGramPanchayatAppyMainEntities();
//    var appDetails = dbMain.AppDetails.Where(x => x.AppId == appId).FirstOrDefault();

//    string ThumbnaiUrl = appDetails.baseImageUrl + appDetails.basePath + appDetails.Job + "/";
//    using (DevGramPanchayatAppyNagpurEntities db = new DevGramPanchayatAppyNagpurEntities(appId))
//    {
//        var data = db.GramYuvaJobs.Select(x => new GPJobGridRow
//        {
//            JobId = x.JobId,
//            Name = x.Name,
//            Address = x.Address,
//            Email = x.Email,
//            Number = x.Number.ToString(),
//            AdhaarNo = x.AdhaarNo,
//            Education = x.Education,
//            AboutYourSelf = x.AboutYourSelf,
//            JobField = x.JobField,
//            // Stream=x.Stream,
//            Skill = x.Skills,
//            //  Specialization =x.Specialization,
//            ResumeUrl = x.ResumeUrl,
//            date = x.CreatedDate.ToString(),
//            languageId = x.languageId,
//            Image = x.ImageUrl,
//            Image1 = x.ImageUrl1,
//            Image2 = x.ImageUrl2,
//            Image3 = x.ImageUrl3,
//            Lat_Log = x.Lat_Log,
//            Lat_Log1 = x.Lat_Log1,
//            Lat_Log2 = x.Lat_Log2,
//            Lat_Log3 = x.Lat_Log3
//        }).ToList();
//        foreach (var item in data)
//        {
//            item.language = db.LanguageInfoes.Where(x => x.id == item.languageId).Select(x => x.languageType).FirstOrDefault();
//            item.date = Convert.ToDateTime(item.date).ToString("dd MMM yyyy h:mm tt");
//            if (item.ResumeUrl != "" && item.ResumeUrl != null)
//                item.ResumeUrl = ThumbnaiUrl + item.ResumeUrl;
//            else
//                item.ResumeUrl = "None";



//            if (item.Image != "" && item.Image != null)
//            {
//                item.Image = ThumbnaiUrl + item.Image;
//                item.Lat_Log = item.Lat_Log;
//            }
//            else
//            {
//                item.Image = "default.png";
//                item.Lat_Log = "";
//            }

//            if (item.Image1 != "" && item.Image1 != null)
//            {
//                item.Image1 = ThumbnaiUrl + item.Image1;
//                item.Lat_Log1 = item.Lat_Log1;
//            }
//            else
//            {
//                item.Image1 = "default.png";
//                item.Lat_Log1 = "";
//            }

//            if (item.Image2 != "" && item.Image2 != null)
//            {
//                item.Image2 = ThumbnaiUrl + item.Image2;
//                item.Lat_Log2 = item.Lat_Log2;
//            }

//            else
//            {
//                item.Image2 = "default.png";
//                item.Lat_Log2 = "";
//            }

//            if (item.Image3 != "" && item.Image3 != null)
//            {
//                item.Image3 = ThumbnaiUrl + item.Image3;
//                item.Lat_Log3 = item.Lat_Log3;
//            }
//            else
//            {
//                item.Image3 = "default.png";
//                item.Lat_Log3 = "";
//            }
//        }
//        return data;
//    }
//}
#endregion