using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class SauchalayDetailsVM : BaseVM
    {
        public int Id { get; set; }
        public string SauchalayID { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> WardNo { get; set; }
        public Nullable<int> ZoneId { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string QrImage { get; set; }
        public string Mobile { get; set; }
        public string Tot { get; set; }
        public int? Tns { get; set; }
        public string SauchalayQRCode { get; set; }
        public string ReferanceId { get; set; }
        public string SerielNo { get; set; }
    }
}
