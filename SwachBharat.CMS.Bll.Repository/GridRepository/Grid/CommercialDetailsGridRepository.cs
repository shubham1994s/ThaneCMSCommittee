using SwachBharat.CMS.Bll.Repository.GridRepository;
using System.Collections.Generic;
using System.Collections.Specialized;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Grid;
using SwachBharat.CMS.Bll.Repository.GridRepository.Grid;
using System;


namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
    public class CommercialDetailsGridRepository : IDataTableRepository
    {
        IEnumerable<SBAHouseDetailsGridRow> dataSet;
        DashBoardRepository objRep = new DashBoardRepository();

        public CommercialDetailsGridRepository(long wildcard, string SearchString, int appId)
        {
            dataSet = objRep.GetCommercialDetailsData(wildcard, SearchString, appId);
        }

        public string GetDataTabelJson(string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {
            var json = dataSet.GetDataTableJson(sortColumn, sortColumnDir, draw, length, searchValue, start);
            return json;
        }
    }
}
