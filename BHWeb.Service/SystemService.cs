using BHWeb.Dao;
using BHWeb.Dao.Model;
using BHWeb.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHWeb.Service
{
    public static class SystemService
    {
        public static ArticleDataObject GetArticle()
        {
            using (BHWebEntity entity = new BHWebEntity())
            {
                Article article = entity.Article.FirstOrDefault();
                if (article == null)
                    return new ArticleDataObject();
                else
                    return BHMapper.Map<Article,ArticleDataObject>(article);
            }
        }        
    }
}
