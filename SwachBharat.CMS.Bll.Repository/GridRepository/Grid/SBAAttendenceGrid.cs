using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.Grid
{
  public  class SBAAttendenceGrid
    {
        public int daID { get; set; }
        public string userName { get; set; }
        public string daDate { get; set; }
        public string daEndDate { get; set; }
        public string startLat { get; set; }
        public string startLong { get; set; }
        public string endLat { get; set; }
        public string endLong { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }       
        public string vtId { get; set; }      
        public string vehicleNumber { get; set; }
        public int userId { get; set; }
        public Nullable<DateTime> CompareDate { get; set; }
        public string Date { get; set; }
        public string daDateTIme { get; set; }
         public string CType { get; set; }

        public string wardId { get; set; }
        public int PrabhagId { get; set; }

    }
}
