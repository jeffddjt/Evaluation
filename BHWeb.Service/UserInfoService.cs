using BHWeb.Dao.Model;
using BHWeb.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BHWeb.Service
{
    public class UserInfoService : BaseService<UserInfo, UserInfoDataObject>
    {
        public List<UserInfoDataObject> GetList(int pageSize, int pageIndex, Expression<Func<UserInfo, bool>> condition = null)
        {
            List<UserInfo> list;
            if (condition == null)
                list = this.DataEntity.OrderBy(p=>p.WorkNo).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            else
                list = this.DataEntity.Where(condition).OrderBy(p=>p.WorkNo).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return BHMapper.Map<List<UserInfo>, List<UserInfoDataObject>>(list);
        }
        public override UserInfoDataObject Update(UserInfoDataObject model)
        {
            if (model.ID == 0)
                return Add(model);
            UserInfo userinfo = this.DataEntity.FirstOrDefault(p => p.ID == model.ID);
            Department dept;
            if (model.DepartmentID != 0)
            {
                dept = this.entity.Department.FirstOrDefault(p => p.ID == model.DepartmentID);
                userinfo.Department = dept;
            }
            else
            {
                userinfo.Department?.UserList.Remove(userinfo);
            }

            Duties duties;
            if (model.DutiesID != 0)
            {
                duties = this.entity.Duties.FirstOrDefault(p => p.ID == model.DutiesID);
                userinfo.Duties = duties;
            }
            else
            {
                userinfo.Duties?.UserList.Remove(userinfo);
            }
            userinfo.UserName = model.UserName;
            userinfo.Professional = model.Professional;
            userinfo.WorkNo = model.WorkNo;
            userinfo.MajorLeader = model.MajorLeader;
            userinfo.Secretary = model.Secretary;
            userinfo.Director = model.Director;
            userinfo.Instructor = model.Instructor;
            this.entity.SaveChanges();
            return BHMapper.Map<UserInfo, UserInfoDataObject>(userinfo);
        }

        public void ChangePassword(int id, string password)
        {
            UserInfo userInfo = this.DataEntity.Single(p => p.ID == id);
            userInfo.Password = password;
            this.entity.SaveChanges();
        }

        public override UserInfoDataObject Add(UserInfoDataObject model)
        {
            UserInfo userinfo = this.DataEntity.Create();
            Department dept = this.entity.Department.FirstOrDefault(p => p.ID == model.DepartmentID);
            Duties duties = this.entity.Duties.FirstOrDefault(p => p.ID == model.DutiesID);
            userinfo.UserName = model.UserName;
            userinfo.Password = "12345";
            userinfo.Professional = model.Professional;
            userinfo.WorkNo = model.WorkNo;
            userinfo.MajorLeader = model.MajorLeader;
            userinfo.Department = dept;
            userinfo.Duties = duties;
            userinfo.Secretary = model.Secretary;
            userinfo.Director = model.Director;
            userinfo.Instructor = model.Instructor;
            this.DataEntity.Add(userinfo);
            this.entity.SaveChanges();
            return BHMapper.Map<UserInfo, UserInfoDataObject>(userinfo);
        }

        public int Import(List<UserInfoDataObject> list)
        {
            foreach (UserInfoDataObject model in list)
            {
                UserInfo userinfo = this.DataEntity.Create();
                Department dept = this.entity.Department.FirstOrDefault(p => p.ID == model.DepartmentID);
                Duties duties = this.entity.Duties.FirstOrDefault(p => p.ID == model.DutiesID);
                userinfo.UserName = model.UserName;
                userinfo.Password = model.Password;
                userinfo.Professional = model.Professional;
                userinfo.WorkNo = model.WorkNo;
                userinfo.MajorLeader = model.MajorLeader;
                userinfo.Department = dept;
                userinfo.Duties = duties;
                userinfo.Secretary = model.Secretary;
                userinfo.Director = model.Director;
                userinfo.Instructor = model.Instructor;
                this.DataEntity.Add(userinfo);
            }
            this.entity.SaveChanges();
            return list.Count();
        }
        public override int RemoveAll()
        {
            int[] ids = this.DataEntity.Where(p => p.WorkNo != "admin").Select(p => p.ID).ToArray();
            return RemoveByIds(ids);
        }
    }
}
