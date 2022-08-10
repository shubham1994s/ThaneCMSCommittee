using SwachBharat.CMS.Bll.ViewModels.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
 public   class StreetSweepBeatPointGrid: IDataTableRepository
    {
        IEnumerable<SBAStreeSweepBeatDetailsGridRowNew> dataset;

        DashBoardRepository objRep = new DashBoardRepository();

        public StreetSweepBeatPointGrid(long wildcard, DateTime? fdate, DateTime? tdate, int? userId, string SearchString, int AppId,int PId)
        {
            dataset = objRep.GetStreetBeatSweepData(wildcard, fdate, tdate,userId, SearchString, AppId,PId);
        }
        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataset.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
