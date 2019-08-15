using AutomaticAttend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutomaticAttend.DAL
{
    class AccountInitializer :
       DropCreateDatabaseIfModelChanges<AccountContext>
    {
        protected override void Seed(AccountContext context)
        {
            var sysRole = new List<SysRole>
            {
               new SysRole {RoleName ="学生",RoleDec ="登录学生窗口"},
               new SysRole {RoleName ="教师",RoleDec ="登录教师窗口"},
               new SysRole{RoleName="admin",RoleDec="admin登录用户管理界面" }
            };
            sysRole.ForEach(s => context.SysRoles.Add(s));
            context.SaveChanges();

            var sysUser = new List<SysUser>
            {
               new SysUser {OpenId ="s.s",Name ="朱鸿光"},
            };
            sysUser.ForEach(s => context.SysUsers.Add(s));
            context.SaveChanges();

            var login = new List<Login>
            {
               new Login {OpenId ="s.s"},
            };
            login.ForEach(s => context.Logins.Add(s));
            context.SaveChanges();

            var loginRole = new List<LoginRole>
            {
               new LoginRole {UserID=1,PrimaryRoleID=3,ConfirmRoleID=3},
            };
            loginRole.ForEach(s => context.LoginRoles.Add(s));
            context.SaveChanges();



        }
    }
}