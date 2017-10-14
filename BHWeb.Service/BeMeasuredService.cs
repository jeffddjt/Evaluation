using JTApp.DataObject;
using JTApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BHWeb.Service
{
    public class BeMeasuredService : BaseService<BeMeasured, BeMeasuredDataObject>
    {
        public List<BeMeasuredDataObject> GetList(int pageSize, int pageIndex, Expression<Func<BeMeasured, bool>> condition = null)
        {
            List<BeMeasured> list;
            if (condition == null)
                list = this.DataEntity.OrderBy(p => p.UserInfo.WorkNo).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            else
                list = this.DataEntity.Where(condition).OrderBy(p => p.UserInfo.WorkNo).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return BHMapper.Map<List<BeMeasured>, List<BeMeasuredDataObject>>(list);
        }

        public int Add(int[] ids)
        {
            foreach (int id in ids)
            {
                BeMeasured bm = this.DataEntity.Create();
                UserInfo userInfo = this.entity.UserInfo.FirstOrDefault(p => p.ID == id);
                bm.UserInfo = userInfo;
                this.DataEntity.Add(bm);
            }
            this.entity.SaveChanges();
            return ids.Length;
        }

        public int AddUserInfo(int[] selected, int ratio,int id)
        {
            BeMeasured bm = this.DataEntity.FirstOrDefault(p => p.ID == id);
            List<UserInfo> userList = this.entity.UserInfo.Where(p => selected.Contains(p.ID)).ToList();

            if (bm.MeasuredList.FirstOrDefault(p => p.Ratio == ratio) == null)
            {
                bm.MeasuredList.Add(new Measured() { Ratio = ratio, UserList = userList });
            }
            else
                bm.MeasuredList.FirstOrDefault(p => p.Ratio == ratio).UserList.AddRange(userList);
            return this.entity.SaveChanges();
        }

        public void RemoveMeasuredUser(int[] selected, int beMeasuredID)
        {
            BeMeasured bm = this.DataEntity.FirstOrDefault(p => p.ID == beMeasuredID);
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
            this.entity.SaveChanges();
        }
    }
}
