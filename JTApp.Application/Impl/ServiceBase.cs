using JTApp.Application.AutoMap;
using JTApp.DataObject;
using JTApp.Domain;
using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Application.Impl
{
    public class ServiceBase<DataObject, Entity> : IServiceBase<DataObject> where DataObject : DataObjectBase where Entity : AggregateRoot
    {
        protected IRepository<Entity> Repository;

        public ServiceBase(IRepository<Entity> repository)
        {
            this.Repository = repository;
        }
        public virtual DataObject Add(DataObject dataObject)
        {
            Entity entity = this.Repository.Create();
            entity = JTMapper.Map(dataObject, entity);
            this.Repository.Add(entity);
            this.Repository.Commit();
            return JTMapper.Map<Entity, DataObject>(entity);
        }

        public bool Exists(int id)
        {
            return this.Repository.Exists(p => p.ID == id);
        }

        public virtual int GetCount()
        {
            return this.Repository.GetCount();
        }

        public DataObject GetFirst()
        {
            return JTMapper.Map<Entity, DataObject>(this.Repository.GetFirst());
        }

        public virtual IList<DataObject> GetList()
        {
            return JTMapper.Map<IList<Entity>, IList<DataObject>>(this.Repository.GetAll());
        }

        public virtual IList<DataObject> GetList(int[] ids)
        {
            return JTMapper.Map<IList<Entity>, IList<DataObject>>(this.Repository.Get(p => ids.Contains(p.ID)).ToList());
        }

        public virtual DataObject GetOne(int id)
        {
            return JTMapper.Map<Entity, DataObject>(this.Repository.FindByID(id));
        }

        public virtual int RemoveAll()
        {
            IList<Entity> list = this.Repository.GetAll();
            foreach (Entity item in list)
            {
                this.Repository.Remove(item);
            }
            return this.Repository.Commit();
        }

        public virtual int RemoveById(int id)
        {
            Entity entity = this.Repository.FindByID(id);
            this.Repository.Remove(entity);
            return this.Repository.Commit();
        }

        public virtual int RemoveByIds(int[] ids)
        {
            List<Entity> list = this.Repository.Get(p => ids.Contains(p.ID)).ToList();
            list.ForEach((item) => 
            {
                this.Repository.Remove(item);
            });
            return this.Repository.Commit();
        }

        public virtual DataObject Update(DataObject dataObject)
        {
            Entity entity = this.Repository.FindByID(dataObject.ID);
            entity = JTMapper.Map(dataObject, entity);
            this.Repository.Update(entity);
            this.Repository.Commit();
            return JTMapper.Map<Entity, DataObject>(entity);
        }
    }
}
