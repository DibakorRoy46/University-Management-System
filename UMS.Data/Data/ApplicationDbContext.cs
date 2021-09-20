using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UMS.Models.Models;

namespace UMS.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<CourseProtoType> CourseProtoTypes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CoursePrerequisite> CoursePrerequisites { get; set; }
        public DbSet<CourseToCoursePrerequisite> CourseToCoursePrerequisites { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CourseToCoursePrerequisite>().HasKey(x => new { x.CourseId, x.CoursePreId });

            builder.Entity<CourseToCoursePrerequisite>().
                HasOne<Course>(x => x.Course).
                WithMany(x => x.CourseToCoursePrerequisites).
                HasForeignKey(x => x.CourseId);

            builder.Entity<CourseToCoursePrerequisite>().
                HasOne<CoursePrerequisite>(x => x.CoursePrerequisite).
                WithMany(x => x.CourseToCoursePrerequisites).
                HasForeignKey(x => x.CoursePreId);

        }
    }
}
