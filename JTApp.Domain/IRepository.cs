using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Domain
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        IRepositoryContext Context { get; set; }
        void Add(TAggregateRoot entity);
        void Update(TAggregateRoot entity);
        void Remove(TAggregateRoot entity);
        TAggregateRoot FindByID(int id);
        TAggregateRoot Create();
        bool Exists(Expression<Func<TAggregateRoot, bool>> condition);
        int GetCount();
        int GetCount(Expression<Func<TAggregateRoot, bool>> condition);
        IList<TAggregateRoot> GetAll();
        IQueryable<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> condition);
        IQueryable<TAggregateRoot> Get(int pageIndex, int pageSize);
        IQueryable<TAggregateRoot> Get<TKey>(Expression<Func<TAggregateRoot, TKey>> keySelector, int pageIndex, int pageSize);
        IQueryable<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> condition, int pageIndex, int pageSize);
        IQueryable<TAggregateRoot> Get<TKey>(Expression<Func<TAggregateRoot, bool>> condition, Expression<Func<TAggregateRoot, TKey>> keySelector, int pageIndex, int pageSize);
        TAggregateRoot GetFirst();
        int Commit();

    }
}
