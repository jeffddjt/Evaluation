namespace JTApp.Repositories.Migrations
{
    using Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JTApp.Repositories.DAO.JTContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(JTApp.Repositories.DAO.JTContext context)
        {
            List<FuncModule> funcModuleList = new List<FuncModule> {
            new FuncModule() { ModuleName = "��������" },
                new FuncModule() { ModuleName = "������Ա��ѯ" },
                new FuncModule() { ModuleName = "��������" },
                new FuncModule() { ModuleName = "�º�����" },
                new FuncModule() { ModuleName = "�����������" },
                new FuncModule() { ModuleName = "�º����練��������" }
                };
            funcModuleList.ForEach(item =>
            {
                context.FuncModule.Add(item);
            });
            context.SaveChanges();
            UserRole userRole = new UserRole { Name = "ϵͳ����Ա", FunctionList = context.FuncModule.ToList() };
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
            context.Department.Add(new Department { Name = "����Ⱥ�Ž̸�" });
            context.Department.Add(new Department { Name = "��ѧϵ(��)" });
            context.TimeOver.Add(new TimeOver { Year = DateTime.Now.Year, DateTime = DateTime.Now.AddDays(-5) });
            context.SaveChanges();
        }
    }
}
