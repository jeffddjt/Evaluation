using JTApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTApp.Domain;
using JTApp.Domain.Repository;

namespace JTApp.Repositories.Repository
{
    public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IRepositoryContext context) : base(context)
        {
        }
    }
}
