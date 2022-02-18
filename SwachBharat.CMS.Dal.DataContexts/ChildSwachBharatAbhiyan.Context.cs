﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SwachBharat.CMS.Dal.DataContexts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DevChildSwachhBharatNagpurEntities : DbContext
    {
        public DevChildSwachhBharatNagpurEntities(int AppId)
                : base(SwachBharatAppConnection.GetConnectionString(AppId))
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<GarbagePointDetail> GarbagePointDetails { get; set; }
        public virtual DbSet<Qr_Employee_Daily_Attendance> Qr_Employee_Daily_Attendance { get; set; }
        public virtual DbSet<GamePlayerDetail> GamePlayerDetails { get; set; }
        public virtual DbSet<Qr_Location> Qr_Location { get; set; }
        public virtual DbSet<WM_GarbageCategory> WM_GarbageCategory { get; set; }
        public virtual DbSet<WM_GarbageSubCategory> WM_GarbageSubCategory { get; set; }
        public virtual DbSet<WM_Garbage_Details> WM_Garbage_Details { get; set; }
        public virtual DbSet<WM_Garbage_Summary> WM_Garbage_Summary { get; set; }
        public virtual DbSet<ZoneMaster> ZoneMasters { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        public virtual DbSet<TeritoryMaster> TeritoryMasters { get; set; }
        public virtual DbSet<WardNumber> WardNumbers { get; set; }
        public virtual DbSet<QrEmployeeMaster> QrEmployeeMasters { get; set; }
        public virtual DbSet<SS_1_4_ANSWER> SS_1_4_ANSWER { get; set; }
        public virtual DbSet<SS_1_4_QUESTION> SS_1_4_QUESTION { get; set; }
        public virtual DbSet<GramCleaningComplient> GramCleaningComplients { get; set; }
        public virtual DbSet<SS_1_7_ANSWER> SS_1_7_ANSWER { get; set; }
        public virtual DbSet<WM_Garbage_Sales> WM_Garbage_Sales { get; set; }
        public virtual DbSet<HouseMaster> HouseMasters { get; set; }
        public virtual DbSet<Vw_MsgNotification> Vw_MsgNotification { get; set; }
        public virtual DbSet<LiquidWasteDetail> LiquidWasteDetails { get; set; }
        public virtual DbSet<StreetSweepingDetail> StreetSweepingDetails { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<UserMaster> UserMasters { get; set; }
        public virtual DbSet<GarbageCollectionDetail> GarbageCollectionDetails { get; set; }
        public virtual DbSet<DumpYardDetail> DumpYardDetails { get; set; }
        public virtual DbSet<Daily_Attendance> Daily_Attendance { get; set; }
        public virtual DbSet<CommercialMaster> CommercialMasters { get; set; }
        public virtual DbSet<SauchalayAddress> SauchalayAddresses { get; set; }
        public virtual DbSet<SWMMaster> SWMMasters { get; set; }
    
        public virtual ObjectResult<GetAttendenceDetailsTotal_Result> GetAttendenceDetailsTotal(Nullable<int> userId, Nullable<int> year, Nullable<int> month)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(int));
    
            var monthParameter = month.HasValue ?
                new ObjectParameter("month", month) :
                new ObjectParameter("month", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAttendenceDetailsTotal_Result>("GetAttendenceDetailsTotal", userIdParameter, yearParameter, monthParameter);
        }
    
        public virtual ObjectResult<CollecctionArea_Result> CollecctionArea(Nullable<int> type)
        {
            var typeParameter = type.HasValue ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CollecctionArea_Result>("CollecctionArea", typeParameter);
        }
    
        public virtual ObjectResult<CurrentAllUserLocation_Result3> CurrentAllUserLocation()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CurrentAllUserLocation_Result3>("CurrentAllUserLocation");
        }
    
        public virtual ObjectResult<SP_Dashboard_Details_Result2> SP_Dashboard_Details()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Dashboard_Details_Result2>("SP_Dashboard_Details");
        }
    
        public virtual ObjectResult<PointDetails_Result> PointDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PointDetails_Result>("PointDetails");
        }
    
        public virtual ObjectResult<CurrentAllUserLocationTest1_Result> CurrentAllUserLocationTest1()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CurrentAllUserLocationTest1_Result>("CurrentAllUserLocationTest1");
        }
    
        public virtual ObjectResult<SP_Collection_Count_Result> SP_Collection_Count(Nullable<System.DateTime> fdate, Nullable<System.DateTime> tdate, Nullable<int> userid)
        {
            var fdateParameter = fdate.HasValue ?
                new ObjectParameter("fdate", fdate) :
                new ObjectParameter("fdate", typeof(System.DateTime));
    
            var tdateParameter = tdate.HasValue ?
                new ObjectParameter("tdate", tdate) :
                new ObjectParameter("tdate", typeof(System.DateTime));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Collection_Count_Result>("SP_Collection_Count", fdateParameter, tdateParameter, useridParameter);
        }
    
        public virtual ObjectResult<SP_DumpYardDetails_Result> SP_DumpYardDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_DumpYardDetails_Result>("SP_DumpYardDetails");
        }
    
        public virtual ObjectResult<SP_EmployeeTarget_Result> SP_EmployeeTarget(Nullable<System.DateTime> fdate, Nullable<System.DateTime> tdate, Nullable<int> userid)
        {
            var fdateParameter = fdate.HasValue ?
                new ObjectParameter("fdate", fdate) :
                new ObjectParameter("fdate", typeof(System.DateTime));
    
            var tdateParameter = tdate.HasValue ?
                new ObjectParameter("tdate", tdate) :
                new ObjectParameter("tdate", typeof(System.DateTime));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_EmployeeTarget_Result>("SP_EmployeeTarget", fdateParameter, tdateParameter, useridParameter);
        }
    
        public virtual ObjectResult<RPT_ONE_POINT_FOUR_SHOW_Result> RPT_ONE_POINT_FOUR_SHOW()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RPT_ONE_POINT_FOUR_SHOW_Result>("RPT_ONE_POINT_FOUR_SHOW");
        }
    
        public virtual ObjectResult<RPT_ONE_POINT_Five_SHOW_Result> RPT_ONE_POINT_Five_SHOW()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RPT_ONE_POINT_Five_SHOW_Result>("RPT_ONE_POINT_Five_SHOW");
        }
    
        public virtual ObjectResult<RPT_ONE_POINT_Six_SHOW_Result> RPT_ONE_POINT_Six_SHOW()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RPT_ONE_POINT_Six_SHOW_Result>("RPT_ONE_POINT_Six_SHOW");
        }
    
        public virtual ObjectResult<HouseDetails_ReferanceId_Result> HouseDetails_ReferanceId(string referanceId)
        {
            var referanceIdParameter = referanceId != null ?
                new ObjectParameter("ReferanceId", referanceId) :
                new ObjectParameter("ReferanceId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<HouseDetails_ReferanceId_Result>("HouseDetails_ReferanceId", referanceIdParameter);
        }
    
        public virtual ObjectResult<RPT_1POINT7_DETAILS_Result> RPT_1POINT7_DETAILS(Nullable<int> iNSERT_ID)
        {
            var iNSERT_IDParameter = iNSERT_ID.HasValue ?
                new ObjectParameter("INSERT_ID", iNSERT_ID) :
                new ObjectParameter("INSERT_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RPT_1POINT7_DETAILS_Result>("RPT_1POINT7_DETAILS", iNSERT_IDParameter);
        }
    
        public virtual ObjectResult<RPT_1point7_questions_Result> RPT_1point7_questions()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RPT_1point7_questions_Result>("RPT_1point7_questions");
        }
    
        public virtual ObjectResult<RPT_ONE_POINT_Eight_SHOW_Result> RPT_ONE_POINT_Eight_SHOW()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RPT_ONE_POINT_Eight_SHOW_Result>("RPT_ONE_POINT_Eight_SHOW");
        }
    
        public virtual ObjectResult<RPT_ONE_POINT_SEVEN_SHOW_Result> RPT_ONE_POINT_SEVEN_SHOW()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RPT_ONE_POINT_SEVEN_SHOW_Result>("RPT_ONE_POINT_SEVEN_SHOW");
        }
    
        public virtual ObjectResult<RPT_1POINT9_DETAILS_Result> RPT_1POINT9_DETAILS(Nullable<int> iNSERT_ID)
        {
            var iNSERT_IDParameter = iNSERT_ID.HasValue ?
                new ObjectParameter("INSERT_ID", iNSERT_ID) :
                new ObjectParameter("INSERT_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RPT_1POINT9_DETAILS_Result>("RPT_1POINT9_DETAILS", iNSERT_IDParameter);
        }
    
        public virtual ObjectResult<RPT_ONE_POINT_NINE_SHOW_Result> RPT_ONE_POINT_NINE_SHOW()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RPT_ONE_POINT_NINE_SHOW_Result>("RPT_ONE_POINT_NINE_SHOW");
        }
    
        public virtual ObjectResult<SP_GetLiveTracking_Result> SP_GetLiveTracking()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetLiveTracking_Result>("SP_GetLiveTracking");
        }
    
        public virtual ObjectResult<SP_EmployeeSummary_Result> SP_EmployeeSummary(Nullable<System.DateTime> from, Nullable<System.DateTime> to, Nullable<int> userid)
        {
            var fromParameter = from.HasValue ?
                new ObjectParameter("from", from) :
                new ObjectParameter("from", typeof(System.DateTime));
    
            var toParameter = to.HasValue ?
                new ObjectParameter("to", to) :
                new ObjectParameter("to", typeof(System.DateTime));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_EmployeeSummary_Result>("SP_EmployeeSummary", fromParameter, toParameter, useridParameter);
        }
    
        public virtual ObjectResult<SP_IdelTime_Result> SP_IdelTime(Nullable<int> userId, Nullable<System.DateTime> fdate, Nullable<System.DateTime> tdate)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var fdateParameter = fdate.HasValue ?
                new ObjectParameter("fdate", fdate) :
                new ObjectParameter("fdate", typeof(System.DateTime));
    
            var tdateParameter = tdate.HasValue ?
                new ObjectParameter("tdate", tdate) :
                new ObjectParameter("tdate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_IdelTime_Result>("SP_IdelTime", userIdParameter, fdateParameter, tdateParameter);
        }
    
        public virtual ObjectResult<SP_LiquidWasteDetails_Result> SP_LiquidWasteDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_LiquidWasteDetails_Result>("SP_LiquidWasteDetails");
        }
    
        public virtual ObjectResult<SP_StreetSweepDetails_Result> SP_StreetSweepDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_StreetSweepDetails_Result>("SP_StreetSweepDetails");
        }
    
        public virtual ObjectResult<SP_LSEmployeeSummary_Result> SP_LSEmployeeSummary(Nullable<System.DateTime> from, Nullable<System.DateTime> to, Nullable<int> userid, string emptype)
        {
            var fromParameter = from.HasValue ?
                new ObjectParameter("from", from) :
                new ObjectParameter("from", typeof(System.DateTime));
    
            var toParameter = to.HasValue ?
                new ObjectParameter("to", to) :
                new ObjectParameter("to", typeof(System.DateTime));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            var emptypeParameter = emptype != null ?
                new ObjectParameter("Emptype", emptype) :
                new ObjectParameter("Emptype", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_LSEmployeeSummary_Result>("SP_LSEmployeeSummary", fromParameter, toParameter, useridParameter, emptypeParameter);
        }
    
        public virtual ObjectResult<SP_IdelTimeLiquid_Result> SP_IdelTimeLiquid(Nullable<int> userId, Nullable<System.DateTime> fdate, Nullable<System.DateTime> tdate)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var fdateParameter = fdate.HasValue ?
                new ObjectParameter("fdate", fdate) :
                new ObjectParameter("fdate", typeof(System.DateTime));
    
            var tdateParameter = tdate.HasValue ?
                new ObjectParameter("tdate", tdate) :
                new ObjectParameter("tdate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_IdelTimeLiquid_Result>("SP_IdelTimeLiquid", userIdParameter, fdateParameter, tdateParameter);
        }
    
        public virtual ObjectResult<LiquidCurrentAllUserLocationTest1_Result> LiquidCurrentAllUserLocationTest1()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LiquidCurrentAllUserLocationTest1_Result>("LiquidCurrentAllUserLocationTest1");
        }
    
        public virtual ObjectResult<SP_LWaste_Count_Result> SP_LWaste_Count()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_LWaste_Count_Result>("SP_LWaste_Count");
        }
    
        public virtual ObjectResult<SP_LiquidWasteOnMapDetails_Result> SP_LiquidWasteOnMapDetails(Nullable<System.DateTime> gcDate, Nullable<int> userId, Nullable<int> zoneId, Nullable<int> areaId, Nullable<int> wardNo, Nullable<int> garbageType, Nullable<int> filterType)
        {
            var gcDateParameter = gcDate.HasValue ?
                new ObjectParameter("gcDate", gcDate) :
                new ObjectParameter("gcDate", typeof(System.DateTime));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var zoneIdParameter = zoneId.HasValue ?
                new ObjectParameter("ZoneId", zoneId) :
                new ObjectParameter("ZoneId", typeof(int));
    
            var areaIdParameter = areaId.HasValue ?
                new ObjectParameter("AreaId", areaId) :
                new ObjectParameter("AreaId", typeof(int));
    
            var wardNoParameter = wardNo.HasValue ?
                new ObjectParameter("WardNo", wardNo) :
                new ObjectParameter("WardNo", typeof(int));
    
            var garbageTypeParameter = garbageType.HasValue ?
                new ObjectParameter("GarbageType", garbageType) :
                new ObjectParameter("GarbageType", typeof(int));
    
            var filterTypeParameter = filterType.HasValue ?
                new ObjectParameter("FilterType", filterType) :
                new ObjectParameter("FilterType", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_LiquidWasteOnMapDetails_Result>("SP_LiquidWasteOnMapDetails", gcDateParameter, userIdParameter, zoneIdParameter, areaIdParameter, wardNoParameter, garbageTypeParameter, filterTypeParameter);
        }
    
        public virtual ObjectResult<SP_SSEmployeeSummary_Result> SP_SSEmployeeSummary(Nullable<System.DateTime> from, Nullable<System.DateTime> to, Nullable<int> userid, string emptype)
        {
            var fromParameter = from.HasValue ?
                new ObjectParameter("from", from) :
                new ObjectParameter("from", typeof(System.DateTime));
    
            var toParameter = to.HasValue ?
                new ObjectParameter("to", to) :
                new ObjectParameter("to", typeof(System.DateTime));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            var emptypeParameter = emptype != null ?
                new ObjectParameter("Emptype", emptype) :
                new ObjectParameter("Emptype", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_SSEmployeeSummary_Result>("SP_SSEmployeeSummary", fromParameter, toParameter, useridParameter, emptypeParameter);
        }
    
        public virtual ObjectResult<SP_IdelTimestreet_Result> SP_IdelTimestreet(Nullable<int> userId, Nullable<System.DateTime> fdate, Nullable<System.DateTime> tdate)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var fdateParameter = fdate.HasValue ?
                new ObjectParameter("fdate", fdate) :
                new ObjectParameter("fdate", typeof(System.DateTime));
    
            var tdateParameter = tdate.HasValue ?
                new ObjectParameter("tdate", tdate) :
                new ObjectParameter("tdate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_IdelTimestreet_Result>("SP_IdelTimestreet", userIdParameter, fdateParameter, tdateParameter);
        }
    
        public virtual ObjectResult<SP_SSweeping_Count_Result> SP_SSweeping_Count()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_SSweeping_Count_Result>("SP_SSweeping_Count");
        }
    
        public virtual ObjectResult<SP_LiquidEmployeeTarget_Result> SP_LiquidEmployeeTarget(Nullable<System.DateTime> fdate, Nullable<System.DateTime> tdate, Nullable<int> userid)
        {
            var fdateParameter = fdate.HasValue ?
                new ObjectParameter("fdate", fdate) :
                new ObjectParameter("fdate", typeof(System.DateTime));
    
            var tdateParameter = tdate.HasValue ?
                new ObjectParameter("tdate", tdate) :
                new ObjectParameter("tdate", typeof(System.DateTime));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_LiquidEmployeeTarget_Result>("SP_LiquidEmployeeTarget", fdateParameter, tdateParameter, useridParameter);
        }
    
        public virtual ObjectResult<SP_StreetEmployeeTarget_Result> SP_StreetEmployeeTarget(Nullable<System.DateTime> fdate, Nullable<System.DateTime> tdate, Nullable<int> userid)
        {
            var fdateParameter = fdate.HasValue ?
                new ObjectParameter("fdate", fdate) :
                new ObjectParameter("fdate", typeof(System.DateTime));
    
            var tdateParameter = tdate.HasValue ?
                new ObjectParameter("tdate", tdate) :
                new ObjectParameter("tdate", typeof(System.DateTime));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_StreetEmployeeTarget_Result>("SP_StreetEmployeeTarget", fdateParameter, tdateParameter, useridParameter);
        }
    
        public virtual ObjectResult<SP_EmployeeLiquidCollectionType_Result> SP_EmployeeLiquidCollectionType()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_EmployeeLiquidCollectionType_Result>("SP_EmployeeLiquidCollectionType");
        }
    
        public virtual ObjectResult<SP_EmployeeStreetCollectionType_Result> SP_EmployeeStreetCollectionType()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_EmployeeStreetCollectionType_Result>("SP_EmployeeStreetCollectionType");
        }
    
        public virtual ObjectResult<SP_LiquidDashboard_Details_Result> SP_LiquidDashboard_Details()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_LiquidDashboard_Details_Result>("SP_LiquidDashboard_Details");
        }
    
        public virtual ObjectResult<SP_StreetDashboard_Details_Result> SP_StreetDashboard_Details()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_StreetDashboard_Details_Result>("SP_StreetDashboard_Details");
        }
    
        public virtual ObjectResult<StreetCurrentAllUserLocationTest1_Result> StreetCurrentAllUserLocationTest1()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<StreetCurrentAllUserLocationTest1_Result>("StreetCurrentAllUserLocationTest1");
        }
    
        public virtual ObjectResult<SP_StreetSweepingOnMapDetails_Result> SP_StreetSweepingOnMapDetails(Nullable<System.DateTime> gcDate, Nullable<int> userId, Nullable<int> zoneId, Nullable<int> areaId, Nullable<int> wardNo, Nullable<int> garbageType, Nullable<int> filterType)
        {
            var gcDateParameter = gcDate.HasValue ?
                new ObjectParameter("gcDate", gcDate) :
                new ObjectParameter("gcDate", typeof(System.DateTime));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var zoneIdParameter = zoneId.HasValue ?
                new ObjectParameter("ZoneId", zoneId) :
                new ObjectParameter("ZoneId", typeof(int));
    
            var areaIdParameter = areaId.HasValue ?
                new ObjectParameter("AreaId", areaId) :
                new ObjectParameter("AreaId", typeof(int));
    
            var wardNoParameter = wardNo.HasValue ?
                new ObjectParameter("WardNo", wardNo) :
                new ObjectParameter("WardNo", typeof(int));
    
            var garbageTypeParameter = garbageType.HasValue ?
                new ObjectParameter("GarbageType", garbageType) :
                new ObjectParameter("GarbageType", typeof(int));
    
            var filterTypeParameter = filterType.HasValue ?
                new ObjectParameter("FilterType", filterType) :
                new ObjectParameter("FilterType", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_StreetSweepingOnMapDetails_Result>("SP_StreetSweepingOnMapDetails", gcDateParameter, userIdParameter, zoneIdParameter, areaIdParameter, wardNoParameter, garbageTypeParameter, filterTypeParameter);
        }
    
        public virtual ObjectResult<SP_HouseScanifyDetails_Result> SP_HouseScanifyDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_HouseScanifyDetails_Result>("SP_HouseScanifyDetails");
        }
    
        public virtual ObjectResult<SP_HouseScanify_Result> SP_HouseScanify(Nullable<System.DateTime> fdate, Nullable<System.DateTime> tdate, Nullable<int> userid)
        {
            var fdateParameter = fdate.HasValue ?
                new ObjectParameter("fdate", fdate) :
                new ObjectParameter("fdate", typeof(System.DateTime));
    
            var tdateParameter = tdate.HasValue ?
                new ObjectParameter("tdate", tdate) :
                new ObjectParameter("tdate", typeof(System.DateTime));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_HouseScanify_Result>("SP_HouseScanify", fdateParameter, tdateParameter, useridParameter);
        }
    
        public virtual ObjectResult<SP_GarbageCollection_Result> SP_GarbageCollection(Nullable<int> appId, Nullable<int> userid, Nullable<System.DateTime> fdate, Nullable<System.DateTime> tdate, Nullable<int> zoneId, Nullable<int> areaId, Nullable<int> wardNo, Nullable<int> segid)
        {
            var appIdParameter = appId.HasValue ?
                new ObjectParameter("appId", appId) :
                new ObjectParameter("appId", typeof(int));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            var fdateParameter = fdate.HasValue ?
                new ObjectParameter("fdate", fdate) :
                new ObjectParameter("fdate", typeof(System.DateTime));
    
            var tdateParameter = tdate.HasValue ?
                new ObjectParameter("tdate", tdate) :
                new ObjectParameter("tdate", typeof(System.DateTime));
    
            var zoneIdParameter = zoneId.HasValue ?
                new ObjectParameter("ZoneId", zoneId) :
                new ObjectParameter("ZoneId", typeof(int));
    
            var areaIdParameter = areaId.HasValue ?
                new ObjectParameter("AreaId", areaId) :
                new ObjectParameter("AreaId", typeof(int));
    
            var wardNoParameter = wardNo.HasValue ?
                new ObjectParameter("WardNo", wardNo) :
                new ObjectParameter("WardNo", typeof(int));
    
            var segidParameter = segid.HasValue ?
                new ObjectParameter("Segid", segid) :
                new ObjectParameter("Segid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GarbageCollection_Result>("SP_GarbageCollection", appIdParameter, useridParameter, fdateParameter, tdateParameter, zoneIdParameter, areaIdParameter, wardNoParameter, segidParameter);
        }
    
        public virtual ObjectResult<SP_HouseScanify_Count_Result> SP_HouseScanify_Count()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_HouseScanify_Count_Result>("SP_HouseScanify_Count");
        }
    
        public virtual ObjectResult<SP_HouseOnMapDetails_Result> SP_HouseOnMapDetails(Nullable<System.DateTime> gcDate, Nullable<int> userId, Nullable<int> zoneId, Nullable<int> areaId, Nullable<int> wardNo, Nullable<int> garbageType, Nullable<int> filterType)
        {
            var gcDateParameter = gcDate.HasValue ?
                new ObjectParameter("gcDate", gcDate) :
                new ObjectParameter("gcDate", typeof(System.DateTime));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var zoneIdParameter = zoneId.HasValue ?
                new ObjectParameter("ZoneId", zoneId) :
                new ObjectParameter("ZoneId", typeof(int));
    
            var areaIdParameter = areaId.HasValue ?
                new ObjectParameter("AreaId", areaId) :
                new ObjectParameter("AreaId", typeof(int));
    
            var wardNoParameter = wardNo.HasValue ?
                new ObjectParameter("WardNo", wardNo) :
                new ObjectParameter("WardNo", typeof(int));
    
            var garbageTypeParameter = garbageType.HasValue ?
                new ObjectParameter("GarbageType", garbageType) :
                new ObjectParameter("GarbageType", typeof(int));
    
            var filterTypeParameter = filterType.HasValue ?
                new ObjectParameter("FilterType", filterType) :
                new ObjectParameter("FilterType", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_HouseOnMapDetails_Result>("SP_HouseOnMapDetails", gcDateParameter, userIdParameter, zoneIdParameter, areaIdParameter, wardNoParameter, garbageTypeParameter, filterTypeParameter);
        }
    
        public virtual ObjectResult<HouseDetails_Result> HouseDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<HouseDetails_Result>("HouseDetails");
        }
    
        public virtual ObjectResult<CommercialDetails_Result> CommercialDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CommercialDetails_Result>("CommercialDetails");
        }
    
        public virtual ObjectResult<SP_EmployeeHouseCollectionType_Result> SP_EmployeeHouseCollectionType()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_EmployeeHouseCollectionType_Result>("SP_EmployeeHouseCollectionType");
        }
    
        public virtual ObjectResult<SP_CommercialGarbageCollection_Result> SP_CommercialGarbageCollection(Nullable<int> appId, Nullable<int> userid, Nullable<System.DateTime> fdate, Nullable<System.DateTime> tdate, Nullable<int> zoneId, Nullable<int> areaId, Nullable<int> wardNo, Nullable<int> segid)
        {
            var appIdParameter = appId.HasValue ?
                new ObjectParameter("appId", appId) :
                new ObjectParameter("appId", typeof(int));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            var fdateParameter = fdate.HasValue ?
                new ObjectParameter("fdate", fdate) :
                new ObjectParameter("fdate", typeof(System.DateTime));
    
            var tdateParameter = tdate.HasValue ?
                new ObjectParameter("tdate", tdate) :
                new ObjectParameter("tdate", typeof(System.DateTime));
    
            var zoneIdParameter = zoneId.HasValue ?
                new ObjectParameter("ZoneId", zoneId) :
                new ObjectParameter("ZoneId", typeof(int));
    
            var areaIdParameter = areaId.HasValue ?
                new ObjectParameter("AreaId", areaId) :
                new ObjectParameter("AreaId", typeof(int));
    
            var wardNoParameter = wardNo.HasValue ?
                new ObjectParameter("WardNo", wardNo) :
                new ObjectParameter("WardNo", typeof(int));
    
            var segidParameter = segid.HasValue ?
                new ObjectParameter("Segid", segid) :
                new ObjectParameter("Segid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_CommercialGarbageCollection_Result>("SP_CommercialGarbageCollection", appIdParameter, useridParameter, fdateParameter, tdateParameter, zoneIdParameter, areaIdParameter, wardNoParameter, segidParameter);
        }
    
        public virtual ObjectResult<SP_TotalHouseCollection_Count_Result> SP_TotalHouseCollection_Count(Nullable<System.DateTime> gcdate)
        {
            var gcdateParameter = gcdate.HasValue ?
                new ObjectParameter("gcdate", gcdate) :
                new ObjectParameter("gcdate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_TotalHouseCollection_Count_Result>("SP_TotalHouseCollection_Count", gcdateParameter);
        }
    
        public virtual ObjectResult<SWMDetails_Result> SWMDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SWMDetails_Result>("SWMDetails");
        }
    }
}
