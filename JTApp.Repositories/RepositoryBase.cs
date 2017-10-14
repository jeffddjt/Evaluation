using JTApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Repositories
{
    public class RepositoryBase<TAggregateRoot> : IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        public IRepositoryContext Context { get; set; }
        public RepositoryBase(IRepositoryContext context)
        {
            this.Context = context;
        }

        public void Add(TAggregateRoot entity)
        {
            this.Context.DoAdd(entity);
        }

        public int Commit()
        {
            return this.Context.DoCommit();
        }

        public TAggregateRoot Create()
        {
            return this.Context.DoCreate<TAggregateRoot>();
        }

        public bool Exists(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return this.Context.DoExists(condition);
        }

        public TAggregateRoot FindByID(int id)
        {
            return this.Context.DoFindByID<TAggregateRoot>(id);
        }

        public IQueryable<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return this.Context.DoGet(condition);
        }

        public IQueryable<TAggregateRoot> Get(int pageIndex, int pageSize)
        {
            return this.Context.DoGet<TAggregateRoot>(pageIndex, pageSize);
        }
        public IQueryable<TAggregateRoot> Get<TKey>(Expression<Func<TAggregateRoot, TKey>> keySelector, int pageIndex, int pageSize)
        {
            return this.Context.DoGet<TAggregateRoot, TKey>(keySelector, pageIndex, pageSize);
        }

        public IQueryable<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> condition, int pageIndex, int pageSize)
        {
            return this.Context.DoGet(condition, pageIndex, pageSize);
        }
        public TAggregateRoot GetFirst()
        {
            return this.Context.DoGetFirst<TAggregateRoot>();
        }
        public IList<TAggregateRoot> GetAll()
        {
            return this.Context.DoGetAll<TAggregateRoot>();
        }

        public int GetCount()
        {
            return this.Context.DoGetCount<TAggregateRoot>();
        }

        public int GetCount(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return this.Context.DoGetCount(condition);
        }

        public void Remove(TAggregateRoot entity)
        {
            this.Context.DoRemove(entity);
        }

        public void Update(TAggregateRoot entity)
        {
            this.Context.DoUpdate(entity);
        }

        public IQueryable<TAggregateRoot> Get<TKey>(Expression<Func<TAggregateRoot, bool>> condition, Expression<Func<TAggregateRoot, TKey>> keySelector, int pageIndex, int pageSize)
        {
            return this.Context.DoGet(condition, keySelector, pageIndex, pageSize);
        }
    }
}
