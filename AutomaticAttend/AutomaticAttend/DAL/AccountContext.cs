using AutomaticAttend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AutomaticAttend.DAL
{
    public class AccountContext : DbContext
    {
        public AccountContext()
            : base("AccountContext")
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<SysUser> SysUsers { get; set; }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }
        public DbSet<LoginRole> LoginRoles { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }
        public DbSet<CourseSelectInformation> CourseSelectInformations { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}