using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class HSDashBoardVM
    {
        public int AppId { get; set; }
        public string AppName { get; set; }
        public Nullable<int> TotalHouse { get; set; }
        public Nullable<int> TotalHouseUpdated { get; set; }
        public Nullable<int> TotalHouseUpdated_CurrentDay { get; set; }

        public Nullable<int> TotalPoint { get; set; }
        public Nullable<int> TotalPointUpdated { get; set; }
        public Nullable<int> TotalPointUpdated_CurrentDay { get; set; }
        public Nullable<int> TotalDump { get; set; }
        public Nullable<int> TotalDumpUpdated { get; set; }
        public Nullable<int> TotalDumpUpdated_CurrentDay { get; set; }


        public Nullable<int> TotalLiquid { get; set; }
        public Nullable<int> TotalLiquidUpdated { get; set; }
        public Nullable<int> TotalLiquidUpdated_CurrentDay { get; set; }


        public Nullable<int> TotalStreet { get; set; }
        public Nullable<int> TotalStreetUpdated { get; set; }
        public Nullable<int> TotalStreetUpdated_CurrentDay { get; set; }

     
        public Nullable<int> TotalBuildingUpdated { get; set; }
        public Nullable<int> TotalBuildingUpdated_CurrentDay { get; set; }

        public Nullable<int> TotalSlumUpdated { get; set; }
        public Nullable<int> TotalSlumUpdated_CurrentDay { get; set; }

        public Nullable<int> TotalResidentialUpdated { get; set; }
        public Nullable<int> TotalResidentialUpdated_CurrentDay { get; set; }

        public Nullable<int> TotalCommercial { get; set; }
        public Nullable<int> TotalCommercialUpdated { get; set; }
        public Nullable<int> TotalCommercialUpdated_CurrentDay { get; set; }

        public Nullable<int> TotalSWM { get; set; }
        public Nullable<int> TotalSWMUpdated { get; set; }
        public Nullable<int> TotalSWMUpdated_CurrentDay { get; set; }

        public Nullable<int> TotalCTPT { get; set; }
        public Nullable<int> TotalCTPTUpdated { get; set; }
        public Nullable<int> TotalCTPTUpdated_CurrentDay { get; set; }

        public Nullable<int> HouseMinutes { get; set; }
        public Nullable<int> DumpYardMinutes { get; set; }
        public Nullable<int> LiquidMinutes { get; set; }
        public Nullable<int> StreetMinutes { get; set; }
        public Nullable<int> CommercialMinutes { get; set; }
        public Nullable<int> SWMMinutes { get; set; }
        public Nullable<int> CTPTMinutes { get; set; }



        public Nullable<int> TotalHouse_DataEntry { get; set; }
        public Nullable<int> TodayHouse_DataEntry { get; set; }
        public Nullable<int> TotalCommercial_DataEntry { get; set; }
        public Nullable<int> TodayCommercial_DataEntry { get; set; }
        public Nullable<int> TotalLiquid_DataEntry { get; set; }
        public Nullable<int> TodayLiquid_DataEntry { get; set; }
        public Nullable<int> TotalStreet_DataEntry { get; set; }


        public Nullable<int> TodayStreet_DataEntry { get; set; }
        public Nullable<int> TotalSWM_DataEntry { get; set; }
        public Nullable<int> TodaySWM_DataEntry { get; set; }
        public Nullable<int> TotalCTPT_DataEntry { get; set; }
        public Nullable<int> TodayCTPT_DataEntry { get; set; }

        public Nullable<int> TotalSlum_DataEntry { get; set; }
        public Nullable<int> TodaySlum_DataEntry { get; set; }
        public Nullable<int> TotalBuilding_DataEntry { get; set; }
        public Nullable<int> TodayBuilding_DataEntry { get; set; }







    }

}
