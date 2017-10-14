using System;
using System.Data.Entity;
using BHWeb.Dao;
using System.Collections.Generic;
using BHWeb.Dao.Model;
using System.Linq;

namespace BHWeb.Service
{
    internal class BHEntityInitializerForCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<BHWebEntity>
    {
        protected override void Seed(BHWebEntity context)
        {
            List<FuncModule> funcModuleList = new List<FuncModule> {
            new FuncModule() { ModuleName = "测评管理" },
                new FuncModule() { ModuleName = "测评人员查询" },
                new FuncModule() { ModuleName = "民主测评" },
                new FuncModule() { ModuleName = "德和作风" },
                new FuncModule() { ModuleName = "民主测评结果" },
                new FuncModule() { ModuleName = "德和作风反向测评结果" }
                };
            funcModuleList.ForEach(item =>
            {
                context.FuncModule.Add(item);
            });
            context.SaveChanges();
            UserRole userRole = new UserRole { Name = "系统管理员", FunctionList = context.FuncModule.ToList() };
            context.UserRole.Add(userRole);
            context.SaveChanges();
            UserInfo userInfo = new UserInfo
            {
                WorkNo = "admin",
                UserName = "admin",
                Password = "123",
                UserRole = context.UserRole.ToList()
            };
            context.UserInfo.Add(userInfo);
            context.SaveChanges();
            context.Department.Add(new Department { Name = "党政群团教辅" });
            context.Department.Add(new Department { Name = "教学系(部)" });
            context.TimeOver.Add(new TimeOver { Year = DateTime.Now.Year, DateTime = DateTime.Now.AddDays(-5) });
            context.SaveChanges();
            base.Seed(context);
        }
    }
}