using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class EmployeeHouseCollectionType
    {

        public Nullable<int> MixedCount { get; set; }
        public Nullable<int> Bifur { get; set; }
        public Nullable<int> NotCollected { get; set; }
        public Nullable<int> NotSpecidfied { get; set; }
        public Nullable<int> ConstructionAndDemolition { get; set; }
        public Nullable<int> Horticulture { get; set; }

        public Nullable<int> CommercialConstructionAndDemolition { get; set; }
        public Nullable<int> CommercialHorticulture { get; set; }

        public Nullable<int> DryWaste { get; set; }
        public Nullable<int> WetWaste { get; set; }
        public Nullable<int> DomesticHazardous { get; set; }
        public Nullable<int> Sanitary { get; set; }
        public string inTime { get; set; }
        public Nullable<int> Count { get; set; }

        public Nullable<int> CommercialWasteNotSpecified { get; set; }
        public Nullable<int> CommercialMixed { get; set; }
        public Nullable<int> CommercialNotReceived { get; set; }
        public Nullable<int> CommercialWet { get; set; }
        public Nullable<int> CommercialDry { get; set; }

        public Nullable<int> CommercialSegregeted { get; set; }

        public int userId { get; set; }
        public string userName { get; set; }
        public string gcTarget { get; set; }
        public string gcTarget2 { get; set; }
        public string ToDate { get; set; }
    }
}
