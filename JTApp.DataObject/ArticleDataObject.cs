using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.DataObject
{
    public class ArticleDataObject:DataObjectBase
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
