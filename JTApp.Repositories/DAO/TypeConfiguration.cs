using JTApp.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Repositories.DAO
{
    public class TypeConfiguration<TAggregateRoot>:EntityTypeConfiguration<TAggregateRoot> where TAggregateRoot:AggregateRoot
    {
        public TypeConfiguration()
        {
            HasKey(p => p.ID);
            Property(p => p.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            ToTable(typeof(TAggregateRoot).Name);
        }
    }
}
