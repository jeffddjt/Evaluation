using BHWeb.Dao.Model;
using BHWeb.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHWeb.Service
{
    public class StyleOfWorkService : BaseService<StyleOfWork, StyleOfWorkDataObject>
    {
        public StyleOfWorkDataObject Create(int id, int userInfoID)
        {
            BeMeasured beMeasured = this.entity.BeMeasured.FirstOrDefault(p => p.UserInfo.ID == id);
            UserInfo userInfo = this.entity.UserInfo.FirstOrDefault(p => p.ID == userInfoID);
            StyleOfWork styleOfWork = this.DataEntity.Create();
            styleOfWork.BeMeasured = beMeasured;
            styleOfWork.BeMeasuredID = beMeasured.ID;
            styleOfWork.UserInfo = userInfo;
            styleOfWork.Ratio = userInfo.MeasuredList.FirstOrDefault(p => p.BeMeasured.ID == beMeasured.ID).Ratio;
            int year= this.entity.TimeOver.FirstOrDefault().Year;
            styleOfWork.Year = year;
            this.DataEntity.Add(styleOfWork);
            this.entity.SaveChanges();
            styleOfWork = this.DataEntity.FirstOrDefault(p => p.BeMeasured.UserInfo.ID == id && p.UserInfo.ID == userInfoID && p.Year == year);
            return BHMapper.Map<StyleOfWork, StyleOfWorkDataObject>(styleOfWork);
        }

        public StyleOfWorkDataObject Update(int id, int score)
        {
            StyleOfWork styleOfWork = this.DataEntity.FirstOrDefault(p => p.ID == id);
            styleOfWork.Score = score;
            this.entity.SaveChanges();
            return BHMapper.Map<StyleOfWork, StyleOfWorkDataObject>(styleOfWork);
        }
    }
}
