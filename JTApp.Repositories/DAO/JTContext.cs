using JTApp.Domain.Model;
using JTApp.Repositories.DAO.EntityTypeConfiguration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Repositories.DAO
{
    public class JTContext:DbContext
    {
        public JTContext():base("Server=127.0.0.1;Database=Evaluation;User ID=sa;Password=aaaa1111!;")
        { }

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<FuncModule> FuncModule { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<TimeOver> TimeOver { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Duties> Duties { get; set; }
        public DbSet<BeMeasured> BeMeasured { get; set; }
        public DbSet<Measured> Measured { get; set; }
        public DbSet<EvaluationTable> EvaluationTable { get; set; }
        public DbSet<EvaluationTableDetail> EvaluationTableDetail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserInfoTypeConfiguration())
                .Add(new UserRoleTypeConfiguration())
                .Add(new DepartmentTypeConfiguration())
                .Add(new FuncModuleTypeConfiguration())
                .Add(new ArticleTypeConfiguration())
                .Add(new TimeOverTypeConfiguration())
                .Add(new ReviewTypeConfiguration())
                .Add(new DutiesTypeConfiguration())
                .Add(new BeMeasuredTypeConfiguration())
                .Add(new MeasuredTypeConfiguration())
                .Add(new EvaluationTableTypeConfiguration())
                .Add(new EvaluationTableDetailTypeConfiguration());
                
            base.OnModelCreating(modelBuilder);
        }

    }
}
