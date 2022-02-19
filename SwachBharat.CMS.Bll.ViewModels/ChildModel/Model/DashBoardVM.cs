using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class DashBoardVM : BaseVM
    {
        public Nullable<int> TodayAttandence { get; set; }
        public Nullable<int> TotalAttandence { get; set; }
        public Nullable<int> HouseCollection { get; set; }
        public Nullable<int> commercialCollection { get; set; }
        public Nullable<int> LiquidCollection { get; set; }

        public Nullable<int> StreetCollection { get; set; }
        public Nullable<int> PointCollection { get; set; }
        public Nullable<int> TotalComplaint { get; set; }
        public Nullable<int> DumpYardCount { get; set; }

        public Nullable<int> TotalHouseCount { get; set; }
        public Nullable<int> TotalcommercialCount { get; set; }
        public Nullable<int> TotalLiquidCount { get; set; }
        public Nullable<int> TotalStreetCount { get; set; }
        public Nullable<int> TotalStreetPropertyCount { get; set; }
        public Nullable<int> TotalLiquidPropertyCount { get; set; }

        public Nullable<int> TotalHousePropertyCount { get; set; }
        public Nullable<int> TotalcommercialPropertyCount { get; set; }
        public Nullable<int> TotalDumpPropertyCount { get; set; }
        public Nullable<int> MixedCount { get; set; }
        public Nullable<int> BifurgatedCount { get; set; }
        public Nullable<int> NotCollected { get; set; }
        public Nullable<double> TotalGcWeightCount { get; set; }
        public Nullable<double> TotalDryWeightCount { get; set; }
        public Nullable<double> TotalWetWeightCount { get; set; }
        public Nullable<double> GcWeightCount { get; set; }
        public Nullable<double> DryWeightCount { get; set; }
        public Nullable<double> WetWeightCount { get; set; }

        public Nullable<int> NotSpecified { get; set; }

        public Nullable<int> TotalDryWaste { get; set; }

        public Nullable<int> TotalWetWaste { get; set; }

        public Nullable<double> TotalCDWCount { get; set; }
        public Nullable<double> TotalHWCount { get; set; }

        public Nullable<double> TotalDHWCount { get; set; }
        public Nullable<double> TotalSWCount { get; set; }

        public Nullable<double> TotalCWCount { get; set; }

        public Nullable<double> CommercialMixCount { get; set; }
        public Nullable<double> CommercialWetCount { get; set; }
        public Nullable<double> CommercialDryCount { get; set; }

        public Nullable<double> TotalCommercialCount { get; set; }

        public Nullable<double> TotalCommercial { get; set; }

        public Nullable<double> TotalCommercialCurrent { get; set; }




        public int userId { get; set; }
        public string userName { get; set; }
        public string gcTarget { get; set; }

        public string UserName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int _Count { get; set; }



        public string Target { get; set; }

        //public Nullable<int> TotalHouseCount { get; set; }
        public Nullable<int> TotalHouseLatLongCount { get; set; }
        public Nullable<int> TotalcommercialLatLongCount { get; set; }
        public Nullable<int> TotalScanHouseCount { get; set; }
        public Nullable<int> TotalScancommercialCount { get; set; }


        public string areaName { get; set; }

        // public List<SelectListItem> AreaList { get; set; }

        public Nullable<int> ZoneId { get; set; }
        public Nullable<int> WardNo { get; set; }
        public Nullable<int> AreaId { get; set; }

        public Nullable<int> LiquidWasteCollection { get; set; }

        public Nullable<int> LiquidWasteScanedHouse { get; set; }

        public Nullable<int> StreetWasteCollection { get; set; }

        public Nullable<int> StreetWasteScanedHouse { get; set; }
        public Nullable<int> LiquidCollectionCount { get; set; }



        public Nullable<double> LWTotalGcWeightCount { get; set; }
        public Nullable<double> LWTotalDryWeightCount { get; set; }
        public Nullable<double> LWTotalWetWeightCount { get; set; }
        public Nullable<double> LWGcWeightCount { get; set; }
        public Nullable<double> LWDryWeightCount { get; set; }
        public Nullable<double> LWWetWeightCount { get; set; }

        public Nullable<double> SSTotalGcWeightCount { get; set; }
        public Nullable<double> SSTotalDryWeightCount { get; set; }
        public Nullable<double> SSTotalWetWeightCount { get; set; }
        public Nullable<double> SSGcWeightCount { get; set; }
        public Nullable<double> SSDryWeightCount { get; set; }
        public Nullable<double> SSWetWeightCount { get; set; }
        public Nullable<double> GcHWWeightCount { get; set; }
        public Nullable<double> GcCDWWeightCount { get; set; }


        public Nullable<int> ResidentialCollection { get; set; }
        public Nullable<int> ResidentialBuildingCollection { get; set; }
        public Nullable<int> ResidentialSlumCollection { get; set; }
        public Nullable<int> CommercialCollection { get; set; }

        public Nullable<int> ConstructionDemolitionCount { get; set; }
        public Nullable<int> HorticultureCount { get; set; }
        public Nullable<int> WetWasteCount { get; set; }
        public Nullable<int> DryWasteCount { get; set; }
        public Nullable<int> DemosticHazardousCount { get; set; }
        public Nullable<int> SanitaryCount { get; set; }

    }
}
