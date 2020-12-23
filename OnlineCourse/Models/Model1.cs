namespace OnlineCourse.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=ModelContext")
        {
        }

        public virtual DbSet<CourseLevel> CourseLevels { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<CourseSelection> CourseSelections { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Price> Prices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseLevel>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.CourseLevel)
                .HasForeignKey(e => e.LevelId);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.Cours)
                .HasForeignKey(e => e.CourseId);
        }
    }
}
