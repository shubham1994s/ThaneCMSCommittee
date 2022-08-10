﻿using SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
  public  class LGarbageCollectionGridRepository : IDataTableRepository
    {
        IEnumerable<SBAGrabageCollectionGridRow> dataSet;

        DashBoardRepository objRep = new DashBoardRepository();

        public LGarbageCollectionGridRepository(long wildcard, string SearchString, DateTime? fdate, DateTime? tdate, int userId, int appId, int? param1, int? param2, int? param3,int PId)
        {
            dataSet = objRep.GetLiquidGarbageCollectionData(wildcard, SearchString, fdate, tdate, userId, appId, param1, param2, param3,PId);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataSet.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
