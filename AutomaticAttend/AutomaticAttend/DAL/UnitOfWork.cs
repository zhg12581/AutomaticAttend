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