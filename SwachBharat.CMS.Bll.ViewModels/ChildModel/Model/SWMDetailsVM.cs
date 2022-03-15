using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
   public  class SWMDetailsVM  : BaseVM
    {
        public int swmId { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> WardNo { get; set; }
        public Nullable<int> ZoneId { get; set; }
        public string swmNumber { get; set; }
        public string swmOwnerMar { get; set; }
        public string swmName { get; set; }
        public string swmManager { get; set; }
        public string swmMobile { get; set; }
        public string swmAddress { get; set; }
        public string swmLat { get; set; }
        public string swmLong { get; set; }
        public string swmQRCode { get; set; }
        public string ReferanceId { get; set; }
        public string JavascriptToRun { get; set; }
        public string areaName { get; set; }
        public string wardName { get; set; }

        public Nullable<int> userId { get; set; }

        public string SerielNo { get; set; }

        public string WasteType { get; set; }
        public string swmType { get; set; }

        public string swmSubType { get; set; }

    }
}
