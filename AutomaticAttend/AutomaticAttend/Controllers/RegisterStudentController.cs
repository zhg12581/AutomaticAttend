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
    public class RegisterStudentController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostRegisterStudent")]
        public ViewModelInformation RegisterStudent(ViewModelRegisterStudent viewModelRegisterStudent)
        {
            ViewModelInformation viewModelInformation = null;
            try
            {
                viewModelInformation = new ViewModelInformation();
                var user = new Student();
                user.OpenId = viewModelRegisterStudent.OpenId;
                user.StudentId = viewModelRegisterStudent.StudentId;
                user.Name = viewModelRegisterStudent.Name;
                user.ProfessionalClass = viewModelRegisterStudent.ProfessionalClass;
                user.TotalSignIn = 0;
                user.TotalAttendance = 0;
                unitOfWork.StudentRepository.Insert(user);    //增加新User
                unitOfWork.Save();

                var users = new Login();
                users.OpenId = viewModelRegisterStudent.OpenId;
                unitOfWork.LoginRepository.Insert(users);
                unitOfWork.Save();

                var sysRole = unitOfWork.SysRoleRepository.Get().Where(s => s.RoleName.Equals("学生")).FirstOrDefault();    //寻找用户所选择角色在UserRole里的实例，返回对象
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
