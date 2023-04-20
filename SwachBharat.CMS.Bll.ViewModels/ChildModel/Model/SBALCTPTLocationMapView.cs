using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
  public  class SBALCTPTLocationMapView : BaseVM
    {
        public int CTPTId { get; set; }

        public string CTPTName { get; set; }
        public string ReferanceId { get; set; }
        public string CTPTAddress { get; set; }
        public string CTPTLat { get; set; }
        public string CTPTLong { get; set; }
        public string gcDate { get; set; }
        public string gcTime { get; set; }
        public Nullable<int> garbageType { get; set; }

        public string TOT { get; set; }

        public Nullable<int> TotalNoSeat { get; set; }
    }
}
