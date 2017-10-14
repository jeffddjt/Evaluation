using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Domain.Model
{
    public class Review:AggregateRoot
    {
        public int Year { get; set; }
        public string Name { get; set; }
        public double Score { get; set; }
        public string Content { get; set; }
        public int Sort { get; set; }
        public virtual Review Parent { get; set; }
        public virtual List<Review> Children { get; set; }
    }
}
