using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
 public class SBALCommercialLocationMapView : BaseVM
    {

        public int ssid { get; set; }
        public int lwid { get; set; }
        public int commercialId { get; set; }
        public string ReferanceId { get; set; }
        public string commercialOwnerName { get; set; }
        public string commercialOwnerMobile { get; set; }
        public string commercialAddress { get; set; }
        public string commercialLat { get; set; }
        public string commercialLong { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string lat { get; set; }
        public string log { get; set; }
        public string address { get; set; }
        public string vehcileNumber { get; set; }
        public string userMobile { get; set; }

        public string gcDate { get; set; }
        public string gcTime { get; set; }
        public Nullable<int> garbageType { get; set; }

        public int areaId { get; set; }

        public string Ctype { get; set; }

    }
}
