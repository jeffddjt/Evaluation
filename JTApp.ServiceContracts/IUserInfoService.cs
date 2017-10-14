using JTApp.DataObject;
using JTApp.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.ServiceContracts
{
    public interface IUserInfoService:IServiceBase<UserInfoDataObject>
    {
        IList<UserInfoDataObject> GetList(int[] ids, JTPager pager);
        void ChangePassword(int id, string password);
        UserInfoDataObject CheckLogin(string workNo, string password);
        List<string> GetFunctionList(UserInfoDataObject userInfo);
        UserInfoDataObject GetEvaUser(int userInfoID);
        List<UserInfoDataObject> GetList(JTPager pager);
        int Import(IList<UserInfoDataObject> list);
        IList<UserInfoDataObject> GetAlready(JTPager pager);
        IList<UserInfoDataObject> GetHavent(JTPager pager);
        IList<UserInfoDataObject> StyleOfWorkAlready(JTPager pager);
        IList<UserInfoDataObject> StyleOfWorkHavent(JTPager pager);
    }
}
