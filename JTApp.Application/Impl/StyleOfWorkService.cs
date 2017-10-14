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
    public class StyleOfWorkService : ServiceBase<StyleOfWorkDataObject, StyleOfWork>, IStyleOfWorkService
    {
        public StyleOfWorkService(IRepository<StyleOfWork> repository) : base(repository)
        {
        }

        public StyleOfWorkDataObject Add(int beMeasuredID, int userInfoID)
        {
            BeMeasured beMeasured = this.Repository.Context.DoGet<BeMeasured>(p => p.UserInfo.ID == beMeasuredID).FirstOrDefault();
            UserInfo userInfo = this.Repository.Context.DoGet<UserInfo>(p => p.ID == userInfoID).FirstOrDefault();
            StyleOfWork styleOfWork = this.Repository.Create();
            styleOfWork.BeMeasured = beMeasured;
            styleOfWork.BeMeasuredID = beMeasured.ID;
            styleOfWork.UserInfo = userInfo;
            styleOfWork.Ratio = userInfo.MeasuredList.FirstOrDefault(p => p.BeMeasured.ID == beMeasured.ID).Ratio;
            int year = this.Repository.Context.DoGetFirst<TimeOver>().Year;
            styleOfWork.Year = year;
            this.Repository.Add(styleOfWork);
            this.Repository.Commit();
            return JTMapper.Map<StyleOfWork, StyleOfWorkDataObject>(styleOfWork);
        }

        public StyleOfWorkDataObject GetOne(int beMeasuredUserInfoID, int userInfoID, int year)
        {
            return JTMapper.Map<StyleOfWork, StyleOfWorkDataObject>(this.Repository.Get(p => p.BeMeasured.UserInfo.ID == beMeasuredUserInfoID && p.UserInfo.ID == userInfoID && p.Year == year).FirstOrDefault());
        }

        public StyleOfWorkDataObject Update(int id, int score)
        {
            StyleOfWork styleOfWork = this.Repository.FindByID(id);
            styleOfWork.Score = score;
            this.Repository.Commit();
            return JTMapper.Map<StyleOfWork, StyleOfWorkDataObject>(styleOfWork);
        }
    }
}
