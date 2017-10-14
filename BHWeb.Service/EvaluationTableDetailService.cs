using BHWeb.Dao.Model;
using BHWeb.DataObject;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHWeb.Service
{
    public class EvaluationTableDetailService : BaseService<EvaluationTableDetail, EvaluationTableDetailDataObject>
    {
        public void Update(int id, double score)
        {
            EvaluationTableDetail detail = this.DataEntity.FirstOrDefault(p => p.ID == id);
            if (detail == null)
                return ;
            detail.Score = score;
            this.entity.SaveChanges();
            if (detail.Review.Parent != null)
            {
                EvaluationTableDetail etd= detail.EvaluationTable.EvaluationTableDetail.FirstOrDefault(p => p.Review.ID == detail.Review.Parent.ID);
                List<EvaluationTableDetail> detailList = etd.EvaluationTable.EvaluationTableDetail.Where(p =>p.Review.Parent!=null &&p.Review.Parent.ID == etd.Review.ID).ToList();
                etd.Score = detailList.Sum(p => p.Score);                
            }                
            this.entity.SaveChanges();            
        }
    }
}
