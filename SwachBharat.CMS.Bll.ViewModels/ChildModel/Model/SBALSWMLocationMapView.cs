using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
  public  class SBALSWMLocationMapView : BaseVM
    {

        public int SWMId { get; set; }
        public string ReferanceId { get; set; }

        public string swmname { get; set; }
        public string swmOwnerMobile { get; set; }
        public string SWMAddress { get; set; }
        public string SWMLat { get; set; }
        public string SWMLong { get; set; }
        public Nullable<DateTime> gcDate { get; set; }
        public string gcTime { get; set; }
        public Nullable<int> garbageType { get; set; }

        public string SWMType { get; set; }
    }
}
