using JTApp.DataObject;
using JTApp.Domain.Model;
using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTApp.Domain;
using JTApp.Application.AutoMap;

namespace JTApp.Application.Impl
{
    public class ReviewService : ServiceBase<ReviewDataObject, Review>, IReviewService
    {
        public ReviewService(IRepository<Review> repository) : base(repository)
        {
        }

        public IList<ReviewDataObject> GetList(int year)
        {
            return JTMapper.Map<IList<Review>,IList<ReviewDataObject>>(this.Repository.Get(p => p.Year == year&&p.Parent==null).ToList());
        }
        public override ReviewDataObject Add(ReviewDataObject dataObject)
        {
            Review entity = this.Repository.Create();
            entity = JTMapper.Map(dataObject, entity);
            if (dataObject.ParentID != 0)
            {
                entity.ParentID = dataObject.ParentID;                
            }
            else
            {
                entity.ParentID = null;
            }
            this.Repository.Add(entity);
            this.Repository.Commit();
            return JTMapper.Map<Review, ReviewDataObject>(entity);
        }
        public override ReviewDataObject Update(ReviewDataObject dataObject)
        {
            Review entity = this.Repository.FindByID(dataObject.ID);
            entity.Name = dataObject.Name;
            entity.Content = dataObject.Content;
            entity.Score = dataObject.Score;
            entity.Sort = dataObject.Sort;
            if (dataObject.ParentID != 0)
                entity.ParentID = dataObject.ParentID;
            else
                entity.ParentID = null; this.Repository.Update(entity);
            this.Repository.Update(entity);
            this.Repository.Commit();
            return JTMapper.Map<Review, ReviewDataObject>(entity);
        }
        public override int RemoveById(int id)
        {
            Review entity = this.Repository.FindByID(id);
            this.RemoveByIds(entity.Children.Select(p => p.ID).ToArray());
            this.Repository.Remove(entity);
            return this.Repository.Commit();
        }

    }
}
