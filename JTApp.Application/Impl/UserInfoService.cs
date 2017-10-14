﻿using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTApp.DataObject;
using JTApp.Infrastructure.Common;
using JTApp.Domain.Repository;
using JTApp.Domain.Model;
using JTApp.Application.AutoMap;
using JTApp.Domain;

namespace JTApp.Application.Impl
{
    public class UserInfoService : ServiceBase<UserInfoDataObject,UserInfo>, IUserInfoService
    {
        public UserInfoService(IUserInfoRepository userinfoRepository):base(userinfoRepository)
        {
        }

        public void ChangePassword(int id, string password)
        {
            UserInfo userInfo = this.Repository.FindByID(id);
            userInfo.Password = password;
            this.Repository.Update(userInfo);
            this.Repository.Commit();
        }

        public UserInfoDataObject CheckLogin(string workNo, string password)
        {
            UserInfo userInfo = this.Repository.Get(p => p.WorkNo == workNo && p.Password == password).FirstOrDefault();
            return JTMapper.Map<UserInfo, UserInfoDataObject>(userInfo);
        }

        public IList<UserInfoDataObject> GetAlready(JTPager pager)
        {
            pager.Total = this.Repository.GetCount(p => p.MeasuredList.Count == p.EvaluationTable.Where(k => k.Submit).Count()
                && p.MeasuredList.Count > 0);
            pager.Count = (pager.Total + pager.Size - 1) / pager.Size;
            if (pager.Index > pager.Count)
                pager.Index = pager.Count;
            if (pager.Index < 1)
                pager.Index = 1;
            IList<UserInfo> userList = this.Repository.Get(p => p.MeasuredList.Count == p.EvaluationTable.Where(k => k.Submit).Count()
              && p.MeasuredList.Count > 0,t=>t.WorkNo,pager.Index,pager.Size).ToList();
            return JTMapper.Map<IList<UserInfo>, IList<UserInfoDataObject>>(userList);
        }
        public IList<UserInfoDataObject> GetHavent(JTPager pager)
        {            
            pager.Total = this.Repository.GetCount(p => p.MeasuredList.Count != p.EvaluationTable.Where(k => k.Submit).Count() && p.MeasuredList.Count > 0);
            pager.Count = (pager.Total + pager.Size - 1) / pager.Size;
            if (pager.Index > pager.Count)
                pager.Index = pager.Count;
            if (pager.Index < 1)
                pager.Index = 1;
            IList<UserInfo> userList = this.Repository.Get(p => p.MeasuredList.Count != p.EvaluationTable.Where(k => k.Submit).Count() && p.MeasuredList.Count > 0, t => t.WorkNo, pager.Index, pager.Size).ToList();
            return JTMapper.Map<IList<UserInfo>, IList<UserInfoDataObject>>(userList);

        }
        public IList<UserInfoDataObject> StyleOfWorkAlready(JTPager pager)
        {
            pager.Total = this.Repository.GetCount(p => p.MeasuredList.Count == p.StyleOfWork.Where(k => k.Score > 0).Count() && p.MeasuredList.Count > 0);
            pager.Count = (pager.Total + pager.Size - 1) / pager.Size;
            if (pager.Index > pager.Count)
                pager.Index = pager.Count;
            if (pager.Index < 1)
                pager.Index = 1;
            IList<UserInfo> userList = this.Repository.Get(p => p.MeasuredList.Count == p.StyleOfWork.Where(k => k.Score > 0).Count() && p.MeasuredList.Count > 0, t => t.WorkNo, pager.Index, pager.Size).ToList();
            return JTMapper.Map<IList<UserInfo>, IList<UserInfoDataObject>>(userList);

        }

        public IList<UserInfoDataObject> StyleOfWorkHavent(JTPager pager)
        {
            pager.Total = this.Repository.GetCount(p => p.MeasuredList.Count != p.StyleOfWork.Where(k => k.Score > 0).Count() && p.MeasuredList.Count > 0);
            pager.Count = (pager.Total + pager.Size - 1) / pager.Size;
            if (pager.Index > pager.Count)
                pager.Index = pager.Count;
            if (pager.Index < 1)
                pager.Index = 1;
            IList<UserInfo> userList = this.Repository.Get(p => p.MeasuredList.Count != p.StyleOfWork.Where(k => k.Score > 0).Count() && p.MeasuredList.Count > 0, t => t.WorkNo, pager.Index, pager.Size).ToList();
            return JTMapper.Map<IList<UserInfo>, IList<UserInfoDataObject>>(userList);

        }

        public UserInfoDataObject GetEvaUser(int userInfoID)
        {
            UserInfo userInfo = this.Repository.Get(p => p.MeasuredList.Count == p.EvaluationTable.Where(k => k.Submit).Count()
              && p.MeasuredList.Count > 0 && p.ID == userInfoID && p.MeasuredList.Count == p.StyleOfWork.Where(k => k.Score > 0).Count()).FirstOrDefault();
            return JTMapper.Map<UserInfo, UserInfoDataObject>(userInfo);

        }

        public List<string> GetFunctionList(UserInfoDataObject userInfo)
        {
            List<string> functionList = new List<string>();
            UserInfo userInfoEntity = this.Repository.FindByID(userInfo.ID);
            foreach (UserRole userRole in userInfoEntity.UserRole)
            {
                functionList.AddRange(userRole.FunctionList.Select(p => p.ModuleName).ToList());
            }
            return functionList.Distinct().ToList();
        }


        public List<UserInfoDataObject> GetList(JTPager pager)
        {
            pager.Count = (pager.Total + pager.Size - 1) / pager.Size;
            if (pager.Index > pager.Count)
                pager.Index = pager.Count;
            if (pager.Index < 1)
                pager.Index = 1;
            return JTMapper.Map<IList<UserInfo>, List<UserInfoDataObject>>(this.Repository.Get(pager.Index, pager.Size).ToList());
        }

        public IList<UserInfoDataObject> GetList(int[] ids, JTPager pager)
        {
            pager.Total = this.Repository.GetCount(p => !ids.Contains(p.ID));
            pager.Count = (pager.Total + pager.Size - 1) / pager.Size;
            if (pager.Index > pager.Count)
                pager.Index = pager.Count;
            if (pager.Index < 1)
                pager.Index = 1;
            IList<UserInfo> list = this.Repository.Get(p => !ids.Contains(p.ID), pager.Index, pager.Size).ToList();
            return JTMapper.Map<IList<UserInfo>, IList<UserInfoDataObject>>(list);
        }

        public int Import(IList<UserInfoDataObject> list)
        {
            foreach (UserInfoDataObject model in list)
            {
                UserInfo userinfo = this.Repository.Create();
                Department dept = this.Repository.Context.DoGet<Department>(p => p.ID == model.DepartmentID).FirstOrDefault();
                Duties duties = this.Repository.Context.DoGet<Duties>(p => p.ID == model.DutiesID).FirstOrDefault();
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
                this.Repository.Add(userinfo);
            }
            return this.Repository.Commit();
        }

    }
}