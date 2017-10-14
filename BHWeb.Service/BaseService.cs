using BHWeb.Dao;
using BHWeb.Dao.Model;
using BHWeb.DataObject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BHWeb.Service
{
    public class BaseService<TEntity,TTarget>:IDisposable where TEntity:TModel where TTarget:BaseDataObject
    {
        protected BHWebEntity entity;
        protected DbSet<TEntity> DataEntity;
        public BaseService()
        {
            entity = new BHWebEntity();
            this.DataEntity = entity.Set<TEntity>();
            Database.SetInitializer<BHWebEntity>(new BHEntityInitializerForCreateDatabaseIfNotExists());
        }
        public virtual List<TTarget> GetList(Expression<Func<TEntity,bool>> condition=null)
        {
            if (condition == null)
            {
                List<TEntity> list = this.DataEntity.ToList();
                return BHMapper.Map<List<TEntity>, List<TTarget>>(list);
            }
            else
            {
                List<TEntity> list = this.DataEntity.Where(condition).ToList();
                return BHMapper.Map<List<TEntity>, List<TTarget>>(list);
            }
        }

        public virtual TTarget GetOne(Expression<Func<TEntity, bool>> condition = null)
        {
            TEntity temp ;
            if (condition == null)
                temp = this.DataEntity.FirstOrDefault();
            else
                temp = this.DataEntity.Where(condition).FirstOrDefault();
            return BHMapper.Map<TEntity, TTarget>(temp);            
        }
        public virtual TTarget Update(TTarget model)
        {
            if (model.ID == 0)
                return this.Add(model);
            TEntity ent = BHMapper.Map<TTarget, TEntity>(model);
            TEntity data = this.DataEntity.FirstOrDefault(p => p.ID == ent.ID);
            this.entity.Entry(data).CurrentValues.SetValues(ent);
            this.entity.SaveChanges();
            return model;
        }

        public virtual TTarget Add(TTarget model)
        {
            TEntity ent = BHMapper.Map<TTarget, TEntity>(model);
            TEntity result = this.DataEntity.Add(ent);
            this.entity.SaveChanges();
            return BHMapper.Map<TEntity, TTarget>(result);
        }
        public virtual int RemoveById(int id)
        {
            TEntity ent = DataEntity.Find(id);
            DataEntity.Remove(ent);
            entity.SaveChanges();
            return ent.ID;
        }
        public virtual int GetCount(Expression<Func<TEntity,bool>> condition=null)
        {
            if (condition == null)
                return this.DataEntity.Count();
            else
                return this.DataEntity.Where(condition).Count();
        }

        public virtual int RemoveByIds(int[] ids)
        {
            List<TEntity> list = this.DataEntity.Where(p => ids.Contains(p.ID)).ToList();
            foreach (TEntity ent in list)
            {
                this.DataEntity.Remove(ent);
            }
            return this.entity.SaveChanges();
        }
        public virtual int RemoveAll()
        {
            int[] ids = this.DataEntity.Select(p => p.ID).ToArray();
            return RemoveByIds(ids);
        }

        public void Dispose()
        {
            this.entity.Dispose();
        }
    }
}
