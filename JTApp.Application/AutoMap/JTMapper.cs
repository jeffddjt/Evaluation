using AutoMapper;
using AutoMapper.Configuration;
using JTApp.DataObject;
using JTApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Application.AutoMap
{
    public static class JTMapper
    {
        static JTMapper()
        {
            MapperConfigurationExpression cfg = new MapperConfigurationExpression();

            cfg.CreateMap<UserInfo, UserInfoDataObject>()
                .ForMember(dest => dest.BeMeasuredUserInfoIDs, source => source.MapFrom(src => src.MeasuredList.Select(p => p.BeMeasured.UserInfo.ID).ToArray()))
                .ForMember(dest => dest.Submited, source => source.MapFrom(src => src.EvaluationTable.Where(p => p.Submit).Count()))
                .ForMember(dest => dest.StyleOfWorkSubmit, source => source.MapFrom(src => src.StyleOfWork.Where(p => p.Score > 0).Count()));

            cfg.CreateMap<UserInfoDataObject, UserInfo>();

            cfg.CreateMap<UserRole, UserRoleDataObject>();
            cfg.CreateMap<UserRoleDataObject, UserRole>();

            cfg.CreateMap<Article, ArticleDataObject>();
            cfg.CreateMap<ArticleDataObject, Article>();

            cfg.CreateMap<Department, DepartmentDataObject>();
            cfg.CreateMap<DepartmentDataObject, Department>();
            cfg.CreateMap<DepartmentDataObject, TreeNodeDataObject>()
                .ForMember(dest => dest.text, source => source.MapFrom(src => src.Name))
                .ForMember(dest => dest.nodes, source => source.MapFrom(src => src.Children));

            cfg.CreateMap<FuncModule, FuncModuleDataObject>();

            cfg.CreateMap<Duties, DutiesDataObject>();
            cfg.CreateMap<DutiesDataObject, Duties>();


            cfg.CreateMap<TimeOver, TimeOverDataObject>()
                .ForMember(dest => dest.Date, source => source.MapFrom(src => src.DateTime.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Hour, source => source.MapFrom(src => src.DateTime.Hour));
            cfg.CreateMap<TimeOverDataObject, TimeOver>()
                .ForMember(dest => dest.DateTime, source => source.MapFrom(src => DateTime.Parse(src.Date).AddHours(src.Hour)));

            cfg.CreateMap<Review, ReviewDataObject>()
                .ForMember(dest => dest.Score, src => src.MapFrom(p => p.Children != null && p.Children.Count > 0 ? p.Children.Sum(k => k.Score) : p.Score));
            cfg.CreateMap<ReviewDataObject, Review>();

            cfg.CreateMap<BeMeasured, BeMeasuredDataObject>();
            cfg.CreateMap<BeMeasuredDataObject, BeMeasured>();

            cfg.CreateMap<Measured, MeasuredDataObject>()
                .ForMember(dest => dest.BeMeasuredID, source => source.MapFrom(src => src.BeMeasured.ID));
            cfg.CreateMap<MeasuredDataObject, Measured>();

            cfg.CreateMap<EvaluationTable, EvaluationTableDataObject>();
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

            cfg.CreateMap<Department, TreeNodeDataObject>()
                .ForMember(dest => dest.text, src => src.MapFrom(p => p.Name))
                .ForMember(dest => dest.nodes, src => src.MapFrom(p => p.Children));

            cfg.CreateMap<EvaluationLevel, EvaluationLevelDataObject>();
            cfg.CreateMap<EvaluationLevelDataObject, EvaluationLevel>();

            Mapper.Initialize(cfg);
        }
        public static Target Map<Source, Target>(Source source)
        {
            if (source == null)
                return default(Target);
            return Mapper.Map<Source, Target>(source);
        }
        public static Target Map<Source, Target>(Source source, Target target)
        {
            return Mapper.Map(source, target);
        }
    }
}
