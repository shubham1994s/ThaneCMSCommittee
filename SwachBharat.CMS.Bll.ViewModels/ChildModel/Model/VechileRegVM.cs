using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.ViewModels.ChildModel.Model
{
    public class VechileRegVM: BaseVM
    {
        public int vechileId { get; set; }
        public Nullable<int> vechileType { get; set; }
        public string vechileNumber { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<bool> isActive { get; set; }


    }
}
