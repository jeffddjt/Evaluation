using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Domain
{
    public interface IRepositoryContext
    {
        void DoAdd<TAggregateRoot>(TAggregateRoot entity) where TAggregateRoot : AggregateRoot;
        void DoUpdate<TAggregateRoot>(TAggregateRoot entity) where TAggregateRoot : AggregateRoot;
        void DoRemove<TAggregateRoot>(TAggregateRoot entity) where TAggregateRoot : AggregateRoot;
        IList<TAggregateRoot> DoGetAll<TAggregateRoot>() where TAggregateRoot : AggregateRoot;
        int DoCommit();
        int DoGetCount<TAggregateRoot>() where TAggregateRoot : AggregateRoot;
        IQueryable<TAggregateRoot> DoGet<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> condition) where TAggregateRoot : AggregateRoot;
        TAggregateRoot DoCreate<TAggregateRoot>() where TAggregateRoot : AggregateRoot;
        int DoGetCount<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> condition) where TAggregateRoot : AggregateRoot;
        IQueryable<TAggregateRoot> DoGet<TAggregateRoot>(int pageIndex, int pageSize) where TAggregateRoot : AggregateRoot;
        IQueryable<TAggregateRoot> DoGet<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> condition, int pageIndex, int pageSize) where TAggregateRoot : AggregateRoot;
        TAggregateRoot DoFindByID<TAggregateRoot>(int id) where TAggregateRoot : AggregateRoot;
        bool DoExists<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> condition) where TAggregateRoot : AggregateRoot;
        void DoClear<TAggregateRoot>() where TAggregateRoot : AggregateRoot;
        IQueryable<TAggregateRoot> DoGet<TAggregateRoot, TKey>(Expression<Func<TAggregateRoot, TKey>> keySelector, int pageIndex, int pageSize) where TAggregateRoot : AggregateRoot;
        TAggregateRoot DoGetFirst<TAggregateRoot>() where TAggregateRoot : AggregateRoot;
        IQueryable<TAggregateRoot> DoGet<TAggregateRoot,Tkey>(Expression<Func<TAggregateRoot, bool>> condition, Expression<Func<TAggregateRoot, Tkey>> keySelector, int pageIndex, int pageSize) where TAggregateRoot : AggregateRoot;
    }
}
