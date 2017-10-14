using JTApp.Domain.Model;
using JTApp.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTApp.Domain;

namespace JTApp.Repositories.Repository
{
    public class TimeOverRepository : RepositoryBase<TimeOver>, ITimeOverRepository
    {
        public TimeOverRepository(IRepositoryContext context) : base(context)
        {
        }
    }
}
