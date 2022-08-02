using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
    public class SauchalayRegistrationGridRow
    {
        public int Id { get; set; }
        public string SauchalayID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CreatedDate { get; set; }
        public string Image { get; set; }
        public string QrImage { get; set; }
        public string Mobile { get; set; }
        public string Tot { get; set; }
        public string Tns { get; set; }
        public string QRCode { get; set; }

        public string TOEMC { get; set; }

        public string TOC { get; set; }

        public Nullable<int> PrabhagId { get; set; }


    }
}
