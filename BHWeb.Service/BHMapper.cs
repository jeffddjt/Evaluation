using AutoMapper;
using AutoMapper.Configuration;
using BHWeb.Dao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHWeb.Service
{
    public static class BHMapper
    {
        static BHMapper()
        {
            MapperConfigurationExpression cfg = new MapperConfigurationExpression();
            cfg.CreateMap<UserInfo, UserInfoDataObject>()
                .ForMember(dest => dest.DepartmentID, source => source.MapFrom(src => src.Department.ID))
                .ForMember(dest => dest.DepartmentName, source => source.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.DutiesID, source => source.MapFrom(src => src.Duties.ID))
                .ForMember(dest => dest.DutiesName, source => source.MapFrom(src => src.Duties.Name))
                .ForMember(dest => dest.BeMeasuredUserInfoIDs, source => source.MapFrom(src => src.MeasuredList.Select(p => p.BeMeasured.UserInfo.ID).ToArray()))
                .ForMember(dest => dest.Submited, source => source.MapFrom(src => src.EvaluationTable.Where(p => p.Submit).Count()))
                .ForMember(dest => dest.StyleOfWorkSubmit, source => source.MapFrom(src => src.StyleOfWork.Where(p => p.Score > 0).Count()));
            cfg.CreateMap<UserRole, UserRoleDataObject>();
            cfg.CreateMap<UserRoleDataObject, UserRole>();
            cfg.CreateMap<Article, ArticleDataObject>();
            cfg.CreateMap<Department, DepartmentDataObject>().ForMember(dest => dest.ParentID, source => source.MapFrom(src => src.Parent.ID));
            cfg.CreateMap<FuncModule, FuncModuleDataObject>();
            cfg.CreateMap<Duties, DutiesDataObject>();
            cfg.CreateMap<DutiesDataObject, Duties>();
            cfg.CreateMap<DepartmentDataObject, TreeNodeDataObject>()
                .ForMember(dest => dest.text, source => source.MapFrom(src =>src.Name))
                .ForMember(dest => dest.nodes, source => source.MapFrom(src => src.Children));

            cfg.CreateMap<DepartmentDataObject, Department>();
            cfg.CreateMap<ArticleDataObject, Article>();
            cfg.CreateMap<TimeOver, TimeOverDataObject>()
                .ForMember(dest => dest.Date, source => source.MapFrom(src => src.DateTime.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Hour, source => source.MapFrom(src => src.DateTime.Hour));
            cfg.CreateMap<TimeOverDataObject, TimeOver>()
                .ForMember(dest => dest.DateTime, source => source.MapFrom(src => DateTime.Parse(src.Date).AddHours(src.Hour)));
            cfg.CreateMap<Review, ReviewDataObject>()
                .ForMember(dest => dest.ParentID, source => source.MapFrom(src => src.Parent.ID));
            cfg.CreateMap<ReviewDataObject, Review>();
            cfg.CreateMap<BeMeasured, BeMeasuredDataObject>();                
            cfg.CreateMap<BeMeasuredDataObject, BeMeasured>();
            cfg.CreateMap<Measured, MeasuredDataObject>()
                .ForMember(dest => dest.BeMeasuredID, source => source.MapFrom(src => src.BeMeasured.ID));
            cfg.CreateMap<MeasuredDataObject, Measured>();
            cfg.CreateMap<EvaluationTable, EvaluationTableDataObject>()
                .ForMember(dest => dest.BeMeasuredUserInfoID, source => source.MapFrom(src => src.BeMeasured.UserInfo.ID));
            cfg.CreateMap<EvaluationTableDataObject, EvaluationTable>();
            cfg.CreateMap<EvaluationTableDetail, EvaluationTableDetailDataObject>()
                .ForMember(dest => dest.EvaluationID, source => source.MapFrom(src => src.EvaluationTable.ID))
                .ForMember(dest => dest.ReviewID, source => source.MapFrom(src => src.Review.ID))
                .ForMember(dest => dest.ReviewName, source => source.MapFrom(src => src.Review.Name))
                .ForMember(dest => dest.ReviewParentID, source => source.MapFrom(src => src.Review.Parent == null ? 0 : src.Review.Parent.ID))
                .ForMember(dest => dest.ReviewScore, source => source.MapFrom(src => src.Review.Score))
                .ForMember(dest => dest.ReviewContent, source => source.MapFrom(src => src.Review.Content))
                .ForMember(dest => dest.Sort, source => source.MapFrom(src => src.Review.Sort));
            cfg.CreateMap<EvaluationTableDetailDataObject, EvaluationTableDetail>();
            cfg.CreateMap<StyleOfWork, StyleOfWorkDataObject>()
                .ForMember(dest => dest.BeMeasuredUserInfoID, source => source.MapFrom(src => src.BeMeasured.UserInfo.ID))
                .ForMember(dest => dest.BeMeasuredUserName, source => source.MapFrom(src => src.BeMeasured.UserInfo.UserName));
            Mapper.Initialize(cfg);
        }
        public static Target Map<Source, Target>(Source source)
        {
            if (source == null)
                return default(Target);
            return Mapper.Map<Source, Target>(source);
        }
    }
}
