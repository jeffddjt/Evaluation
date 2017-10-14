using JTApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Domain.Model
{
    public class Article:AggregateRoot
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
