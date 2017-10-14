using BHWeb.Dao.Model;
using BHWeb.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHWeb.Service
{
    public class ReviewService:BaseService<Review,ReviewDataObject>
    {
        public override ReviewDataObject Update(ReviewDataObject model)
        {
            if (model.ID == 0)
                return Add(model);
            Review parent = this.DataEntity.FirstOrDefault(p => p.ID == model.ParentID);
            Review curr = this.DataEntity.FirstOrDefault(p => p.ID == model.ID);
            curr.Year = model.Year;
            curr.Name = model.Name;
            curr.Parent = parent;
            curr.Score = model.Score;
            curr.Content = model.Content;
            curr.Sort = model.Sort;
            if(parent!=null)
                parent.Score = parent.Children.Sum(p => p.Score);
            this.entity.SaveChanges();
            return model;
        }
        public override ReviewDataObject Add(ReviewDataObject model)
        {
            if (model.ParentID == 0)
                return base.Add(model);
            Review parent = this.DataEntity.FirstOrDefault(p => p.ID == model.ParentID);
            Review newOne = this.DataEntity.Create();
            newOne.Year = model.Year;
            newOne.Name = model.Name;
            newOne.Score = model.Score;
            newOne.Content = model.Content;
            newOne.Sort = model.Sort;
            parent.Children.Add(newOne);
            parent.Score = parent.Children.Sum(p => p.Score);
            this.entity.SaveChanges();
            return BHMapper.Map<Review, ReviewDataObject>(newOne);
        }
        public override int RemoveById(int id)
        {
            Review curr = this.DataEntity.SingleOrDefault(p => p.ID == id);
            Review parent = curr.Parent;
            for (int i = curr.Children.Count-1; i >=0; i--)
            {
                this.DataEntity.Remove(curr.Children[i]);                
            }
            this.DataEntity.Remove(curr);
            if (parent!=null && parent.Children.Count > 0)
                parent.Score = parent.Children.Sum(p => p.Score);
            else if(parent!=null)
                parent.Score = 0;
            this.entity.SaveChanges();
            return parent == null ? 0 : parent.ID;
        }
    }
}
