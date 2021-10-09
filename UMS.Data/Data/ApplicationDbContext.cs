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
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<ClassTime> ClassTimes { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Claims> Claims { get; set; }
        public DbSet<AssignPreRegistrationCourse> PreRegistrationCourses { get; set; }
        public DbSet<PreregistrationCourses> CourseforPreregistration { get; set; }
        public DbSet<AssignRegistrationCourse> AssignRegistrationCourses { get; set; }
        public DbSet<StudentRegisteationCourse> StudentRegisteationCourses { get; set; }
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

            builder.Entity<AssignPreRegistrationCourse>().HasKey(x => new { x.PreCourseId, x.StudentId });

            builder.Entity<AssignPreRegistrationCourse>().
                HasOne<PreregistrationCourses>(x => x.Courses).
                WithMany(x => x.PrereristrationCourses).
                HasForeignKey(x => x.PreCourseId);

            builder.Entity<AssignPreRegistrationCourse>().
                HasOne<ApplicationUser>(x => x.ApplicationUser).
                WithMany(x => x.PreRegistrationCourses).
                HasForeignKey(x => x.StudentId);


            builder.Entity<StudentRegisteationCourse>().HasKey(x => new { x.AssignRegiCourseId, x.StudentId });

            builder.Entity<StudentRegisteationCourse>().
                HasOne<AssignRegistrationCourse>(x => x.AssignRegistrationCourse).
                WithMany(x => x.StudentRegisteationCourses).
                HasForeignKey(x => x.AssignRegiCourseId);

            builder.Entity<StudentRegisteationCourse>().
                HasOne<ApplicationUser>(x => x.ApplicationUser).
                WithMany(x => x.StudentRegisteationCourses).
                HasForeignKey(x => x.StudentId);

        }
    }
}
