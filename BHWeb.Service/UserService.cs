using BHWeb.Dao;
using BHWeb.Dao.Model;
using BHWeb.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHWeb.Service
{
    public class UserService:BaseService<UserInfo,UserInfoDataObject>
    {

        public UserInfoDataObject CheckLogin(string workNo, string password)
        {
            UserInfo userInfo = this.DataEntity.FirstOrDefault(p => p.WorkNo == workNo && p.Password == password);            
            return BHMapper.Map<UserInfo,UserInfoDataObject>(userInfo);
        }

        public List<string> GetFunctionList(UserInfoDataObject userInfo)
        {
            List<string> functionList = new List<string>();
            UserInfo userInfoEntity = this.DataEntity.FirstOrDefault(p => p.ID == userInfo.ID);
            foreach (UserRole userRole in userInfoEntity.UserRole)
            {
                functionList.AddRange(userRole.FunctionList.Select(p => p.ModuleName).ToList());
            }
            return functionList.Distinct().ToList();
        }
    }
}
