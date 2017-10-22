using JTApp.DataObject;
using JTApp.Domain.Model;
using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTApp.Domain;
using JTApp.Application.AutoMap;

namespace JTApp.Application.Impl
{
    public class EvaluationTableService : ServiceBase<EvaluationTableDataObject, EvaluationTable>, IEvaluationTableService
    {
        public EvaluationTableService(IRepository<EvaluationTable> repository) : base(repository)
        {
        }

        public EvaluationTableDataObject Add(int beMeasuredID, int userInfoID)
        {
            BeMeasured beMeasured = this.Repository.Context.DoGet<BeMeasured>(p => p.UserInfo.ID == beMeasuredID).FirstOrDefault();
            UserInfo userInfo = this.Repository.Context.DoGet<UserInfo>(p => p.ID == userInfoID).FirstOrDefault();
            EvaluationTable entity = this.Repository.Create();
            entity.BeMeasured = beMeasured;
            entity.UserInfo = userInfo;
            entity.Ratio = userInfo.MeasuredList.FirstOrDefault(p => p.BeMeasured.ID == beMeasured.ID).Ratio;
            List<EvaluationTableDetail> detailList = new List<EvaluationTableDetail>();
            TimeOver timeOver = this.Repository.Context.DoGetFirst<TimeOver>();
            int year = timeOver == null ? DateTime.Now.Year : timeOver.Year;
            entity.Year = year;
            List<Review> reviewList = this.Repository.Context.DoGet<Review>(p => p.Year == year).ToList();
            foreach (Review review in reviewList)
            {
                EvaluationTableDetail detail = this.Repository.Context.DoCreate<EvaluationTableDetail>();
                detail.EvaluationTable = entity;
                detail.Review = review;
                detailList.Add(detail);
            }
            entity.EvaluationTableDetail = detailList;
            this.Repository.Add(entity);
            this.Repository.Commit();
            return JTMapper.Map<EvaluationTable, EvaluationTableDataObject>(entity);
        }

        public EvaluationTableDataObject GetOne(int beMeasuredUserInfoID, int userInfoID, int year)
        {
            EvaluationTable entity = this.Repository.Get(p => p.BeMeasured.UserInfo.ID == beMeasuredUserInfoID && p.UserInfo.ID == userInfoID && p.Year == year).FirstOrDefault();
            return JTMapper.Map<EvaluationTable, EvaluationTableDataObject>(entity);
        }

        public void Update(int evalID)
        {
            EvaluationTable entity = this.Repository.FindByID(evalID);
            entity.Submit = true;
            this.Repository.Update(entity);
            this.Repository.Commit();
        }
    }
}
