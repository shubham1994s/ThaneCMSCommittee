using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class CurrentCTPTCollectionCount
    {
        public int? userId { get; set; }
        public string userName { get; set; }
        public string ToDate { get; set; }
        public int? TotalPTCount { get; set; }
        public int? TotalCTCount { get; set; }
        public int? TotalUCount { get; set; }

    }
}
