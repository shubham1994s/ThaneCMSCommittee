using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
    public class SBASWMDetailsGridRow
    {
        public int swmId { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public string zone { get; set; }
        public string WardNo { get; set; }
        public string Area { get; set; }
        public string swmNo { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string QRCode { get; set; }
        public string swmOwnerMar { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> WardNoId { get; set; }
        public Nullable<int> zoneId { get; set; }
        public string ReferanceId { get; set; }
        public string swmType { get; set; }
        public string swmSubType { get; set; }
        public int PrabhagId { get; set; }
    }
}
