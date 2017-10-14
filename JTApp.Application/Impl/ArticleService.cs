using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTApp.DataObject;
using JTApp.Domain.Repository;
using JTApp.Domain.Model;
using JTApp.Application.AutoMap;
using JTApp.Domain;

namespace JTApp.Application.Impl
{
    public class ArticleService :ServiceBase<ArticleDataObject,Article>, IArticleService
    {

        public ArticleService(IArticleRepository articleRepository):base(articleRepository)
        {
        }

    }
}
