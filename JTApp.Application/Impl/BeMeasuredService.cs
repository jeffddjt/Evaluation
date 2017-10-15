using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTApp.DataObject;
using JTApp.Domain.Model;
using JTApp.Application.AutoMap;
using JTApp.Domain.Repository;
using JTApp.Domain;
using JTApp.Infrastructure.Common;

namespace JTApp.Application.Impl
{
    public class BeMeasuredService :ServiceBase<BeMeasuredDataObject,BeMeasured>, IBeMeasuredService
    {

        public BeMeasuredService(IBeMeasuredRepository beMeasuredRepository):base(beMeasuredRepository)
        {
        }

        public int Add(int[] selected)
        {
            IList<UserInfo> userList = this.Repository.Context.DoGet<UserInfo>(p => selected.Contains(p.ID)).ToList();
            foreach (UserInfo user in userList)
            {
                BeMeasured bm = this.Repository.Create();
                bm.UserInfo = user;
                this.Repository.Add(bm);
            }
            return this.Repository.Commit();

        }

        public int AddUserInfo(int[] selected, int ratio, int beMeasuredID)
        {
            BeMeasured entity = this.Repository.FindByID(beMeasuredID);
            List<UserInfo> userInfoList = this.Repository.Context.DoGet<UserInfo>(p => selected.Contains(p.ID)).ToList();
            var bm = entity.MeasuredList.FirstOrDefault(p => p.Ratio == ratio);
            if (bm == null)
            {
                entity.MeasuredList.Add(new Measured() { Ratio = ratio, UserList = userInfoList });
            }
            else
            {
                bm.UserList.AddRange(userInfoList);
            }
            return this.Repository.Commit();
        }

        public IList<BeMeasuredDataObject> GetList(JTPager pager)
        {
            pager.Total = this.Repository.GetCount();
            pager.Count = (pager.Total + pager.Size - 1) / pager.Size;
            if (pager.Index > pager.Count)
                pager.Index = pager.Count;
            if (pager.Index < 1)
                pager.Index = 1;
            return JTMapper.Map<IList<BeMeasured>, IList<BeMeasuredDataObject>>(this.Repository.Get(pager.Index, pager.Size).ToList());

        }

        public List<BeMeasuredDataObject> GetList(int[] ids, int year)
        {
            IList<BeMeasured> beMeasuredList = this.Repository.Get(p => ids.Contains(p.UserInfo.Department.ID) && p.EvaluationTableList.Where(k => k.Year == year).Count() > 0).ToList();
            return JTMapper.Map<IList<BeMeasured>, List<BeMeasuredDataObject>>(beMeasuredList);
        }

        public IList<BeMeasuredDataObject> GetTotal(int[] ids, int year, JTPager pager)
        {
            pager.Total = this.Repository.GetCount(p => ids.Contains(p.UserInfo.Department.ID) && p.EvaluationTableList.Where(k => k.Year == year&&k.Submit).Count() > 0);
            pager.Count = (pager.Total + pager.Size - 1) / pager.Size;
            if (pager.Index > pager.Count)
                pager.Index = pager.Count;
            if (pager.Index < 1)
                pager.Index = 1;
            IList<BeMeasured> beMeasuredList = this.Repository.Get(p => ids.Contains(p.UserInfo.Department.ID) && p.EvaluationTableList.Where(k => k.Year == year && k.Submit).Count() > 0, pager.Index, pager.Size).ToList();
            return JTMapper.Map<IList<BeMeasured>, IList<BeMeasuredDataObject>>(beMeasuredList);
        }

        public void RemoveMeasuredUser(int[] selected, int beMeasuredID)
        {
            BeMeasured bm = this.Repository.FindByID(beMeasuredID);
            for (int i = bm.MeasuredList.Count - 1; i >= 0; i--)
            {
                for (int j = bm.MeasuredList[i].UserList.Count - 1; j >= 0; j--)
                {
                    if (selected.Contains(bm.MeasuredList[i].UserList[j].ID))
                    {
                        bm.MeasuredList[i].UserList.RemoveAt(j);
                    }
                }
                if (bm.MeasuredList[i].UserList.Count <= 0)
                    bm.MeasuredList.RemoveAt(i);
            }
            this.Repository.Commit();
        }
    }
}
