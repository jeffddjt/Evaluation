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
    public class BeMeasuredRepository : RepositoryBase<BeMeasured>, IBeMeasuredRepository
    {
        public BeMeasuredRepository(IRepositoryContext context) : base(context)
        {
        }
    }
}
