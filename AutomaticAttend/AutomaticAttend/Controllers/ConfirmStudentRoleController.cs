using AutomaticAttend.DAL;
using AutomaticAttend.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutomaticAttend.Controllers
{
    public class ConfirmStudentRoleController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostConfirmStudent")]
        public ViewModelInformation ConfirmStudent(ViewModelConfirmOrDeleteRole viewModelConfirmRole)
        {
            ViewModelInformation viewModelInformation = null;
            try
            {
                viewModelInformation = new ViewModelInformation();
                var user = unitOfWork.StudentRepository.Get().Where(s => s.StudentId.Equals(viewModelConfirmRole.UserId)).FirstOrDefault();
                var users = unitOfWork.LoginRepository.Get().Where(s => s.OpenId.Equals(user.OpenId)).FirstOrDefault();
                var userss = unitOfWork.LoginRoleRepository.Get().Where(s => s.UserID.Equals(users.ID)).FirstOrDefault();
                userss.ConfirmRoleID = userss.PrimaryRoleID;
                unitOfWork.Save();
                throw new Exception("确认角色成功");
            }
            catch (Exception ex)
            {
                viewModelInformation.Message = ex.Message;
                return viewModelInformation;
            }
        }
    }
}
