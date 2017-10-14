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
            return JTMapper.Map<IList<Review>,IList<ReviewDataObject>>(this.Repository.Get(p => p.Year == year).ToList());
        }
    }
}
