using AutomaticAttend.Models;
using AutomaticAttend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticAttend.DAL
{
    public class UnitOfWork : IDisposable
    {
        private AccountContext context = new AccountContext();

        private GenericRepository<Student> studentRepository;
        private GenericRepository<SysUser> sysUserRepository;
        private GenericRepository<Teacher> teacherRepository;
        private GenericRepository<SysRole> sysRoleRepository;
        private GenericRepository<LoginRole> loginRoleRepository;
        private GenericRepository<Login> loginRepository;
        private GenericRepository<Course> courseRepository;
        private GenericRepository<TeacherStudent> teacherStudentRepository;
        private GenericRepository<TeacherCourse> teacherCourseRepository;

        public GenericRepository<Student> StudentRepository
        {
            get
            {
                if (this.studentRepository == null)
                {
                    this.studentRepository = new GenericRepository<Student>(context);
                }
                return studentRepository;
            }
        }

        public GenericRepository<SysUser> SysUserRepository
        {
            get
            {
                if (this.sysUserRepository == null)
                {
                    this.sysUserRepository = new GenericRepository<SysUser>(context);
                }
                return sysUserRepository;
            }
        }

        public GenericRepository<Teacher> TeacherRepository
        {
            get
            {
                if (this.teacherRepository == null)
                {
                    this.teacherRepository = new GenericRepository<Teacher>(context);
                }
                return teacherRepository;
            }
        }

        public GenericRepository<SysRole> SysRoleRepository
        {
            get
            {
                if (this.sysRoleRepository == null)
                {
                    this.sysRoleRepository = new GenericRepository<SysRole>(context);
                }
                return sysRoleRepository;
            }
        }


        public GenericRepository<LoginRole> LoginRoleRepository
        {
            get
            {
                if (this.loginRoleRepository == null)
                {
                    this.loginRoleRepository = new GenericRepository<LoginRole>(context);
                }
                return loginRoleRepository;
            }
        }

        public GenericRepository<Login> LoginRepository
        {
            get
            {
                if (this.loginRepository == null)
                {
                    this.loginRepository = new GenericRepository<Login>(context);
                }
                return loginRepository;
            }
        }


        public GenericRepository<Course> CourseRepository
        {
            get
            {
                if (this.courseRepository == null)
                {
                    this.courseRepository = new GenericRepository<Course>(context);
                }
                return courseRepository;
            }
        }


        public GenericRepository<TeacherStudent> TeacherStudentRepository
        {
            get
            {
                if (this.teacherStudentRepository == null)
                {
                    this.teacherStudentRepository = new GenericRepository<TeacherStudent>(context);
                }
                return teacherStudentRepository;
            }
        }

        public GenericRepository<TeacherCourse> TeacherCourseRepository
        {
            get
            {
                if (this.teacherCourseRepository == null)
                {
                    this.teacherCourseRepository = new GenericRepository<TeacherCourse>(context);
                }
                return teacherCourseRepository;
            }
        }


        #region Save & Dispose
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}