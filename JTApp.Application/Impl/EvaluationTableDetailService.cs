using JTApp.DataObject;
using JTApp.Domain.Model;
using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTApp.Domain;

namespace JTApp.Application.Impl
{
    public class EvaluationTableDetailService : ServiceBase<EvaluationTableDetailDataObject, EvaluationTableDetail>, IEvaluationTableDetailService
    {
        public EvaluationTableDetailService(IRepository<EvaluationTableDetail> repository) : base(repository)
        {
        }

        public void Update(int detailID, double score)
        {
            EvaluationTableDetail detail = this.Repository.FindByID(detailID);
            if (detail == null)
                return;
            detail.Score = score;
            if (detail.Review.Parent != null)
            {
                EvaluationTableDetail etd = detail.EvaluationTable.EvaluationTableDetail.FirstOrDefault(p => p.Review.ID == detail.Review.Parent.ID);
                List<EvaluationTableDetail> detailList = etd.EvaluationTable.EvaluationTableDetail.Where(p => p.Review.Parent != null && p.Review.Parent.ID == etd.Review.ID).ToList();
                etd.Score = detailList.Sum(p => p.Score);
            }this.Repository.Update(detail);
            this.Repository.Commit();
        }
    }
}
