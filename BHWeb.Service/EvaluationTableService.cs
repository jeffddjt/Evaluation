using BHWeb.Dao.Model;
using BHWeb.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHWeb.Service
{
    public class EvaluationTableService : BaseService<EvaluationTable, EvaluationTableDataObject>
    {
        public EvaluationTableDataObject Create(int beMeasuredUserInfoID, int userInfoID)
        {
            BeMeasured beMeasured = this.entity.BeMeasured.FirstOrDefault(p => p.UserInfo.ID == beMeasuredUserInfoID);
            UserInfo userInfo = this.entity.UserInfo.FirstOrDefault(p => p.ID == userInfoID);
            EvaluationTable evaluationTable = this.DataEntity.Create();
            evaluationTable.BeMeasured = beMeasured;
            evaluationTable.UserInfo = userInfo;
            evaluationTable.Ratio = userInfo.MeasuredList.FirstOrDefault(p => p.BeMeasured.ID == beMeasured.ID).Ratio;
            List<EvaluationTableDetail> detailList = new List<EvaluationTableDetail>();
            TimeOver timeOver = this.entity.TimeOver.FirstOrDefault();
            int year = timeOver == null ? DateTime.Now.Year : timeOver.Year;
            evaluationTable.Year = year;
            List<Review> reviewList = this.entity.Review.Where(p => p.Year == year).ToList();
            foreach (Review review in reviewList)
            {
                EvaluationTableDetail detail = new EvaluationTableDetail();
                detail.EvaluationTable = evaluationTable;
                detail.Review = review;
                detailList.Add(detail);
            }
            evaluationTable.EvaluationTableDetail = detailList;
            this.DataEntity.Add(evaluationTable);
            this.entity.SaveChanges();
            evaluationTable = this.DataEntity.FirstOrDefault(p => p.BeMeasured.UserInfo.ID == beMeasuredUserInfoID && p.UserInfo.ID == userInfoID);
            return BHMapper.Map<EvaluationTable, EvaluationTableDataObject>(evaluationTable);
        }

        public void Update(int id)
        {
            EvaluationTable evaluation = this.DataEntity.FirstOrDefault(p => p.ID == id);
            if (evaluation == null)
                return;
            evaluation.Submit = true;
            evaluation.Score = evaluation.EvaluationTableDetail.Sum(p => p.Score);
            this.entity.SaveChanges();
        }
    }
}
