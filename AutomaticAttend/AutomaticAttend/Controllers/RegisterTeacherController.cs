using AutomaticAttend.DAL;
using AutomaticAttend.Models;
using AutomaticAttend.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutomaticAttend.Controllers
{
    public class RegisterTeacherController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostRegisterTeacher")]
        public ViewModelInformation RegisterTeacher(ViewModelRegisterTeacher viewModelRegisterTeacher)
        {
            ViewModelInformation viewModelInformation = null;
            try
            {
                viewModelInformation = new ViewModelInformation();
                var user = new Teacher();
                user.OpenId = viewModelRegisterTeacher.OpenId;
                user.TeacherId = viewModelRegisterTeacher.TeacherId;
                user.Name = viewModelRegisterTeacher.Name;
                user.Subject= viewModelRegisterTeacher.Subject;
                unitOfWork.TeacherRepository.Insert(user);    //增加新User
                unitOfWork.Save();

                var users = new Login();
                users.OpenId = viewModelRegisterTeacher.OpenId;
                unitOfWork.LoginRepository.Insert(users);
                unitOfWork.Save();

                var sysRole = unitOfWork.SysRoleRepository.Get().Where(s => s.RoleName.Equals("教师")).FirstOrDefault();    //寻找用户所选择角色在UserRole里的实例，返回对象
                var userRole = new LoginRole();
                userRole.UserID = users.ID;
                userRole.PrimaryRoleID = sysRole.ID;
                userRole.ConfirmRoleID = 0;
                unitOfWork.LoginRoleRepository.Insert(userRole);
                unitOfWork.Save();

                throw new Exception("提交成功");
            }
            catch (Exception ex)
            {
                viewModelInformation.Message = ex.Message;
                return viewModelInformation;
            }
        }

    }
}
