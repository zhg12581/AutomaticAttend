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
    public class DeleteStudentRoleController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostDeleteStudent")]
        public ViewModelInformation DeleteStudent(ViewModelConfirmOrDeleteRole viewModelDeleteRole)
        {
            ViewModelInformation viewModelInformation = null;
            try
            {
                viewModelInformation = new ViewModelInformation();
                var user = unitOfWork.StudentRepository.Get().Where(s => s.StudentId.Equals(viewModelDeleteRole.UserId)).FirstOrDefault();
                var users = unitOfWork.LoginRepository.Get().Where(s => s.OpenId.Equals(user.OpenId)).FirstOrDefault();
                var userss = unitOfWork.LoginRoleRepository.Get().Where(s => s.UserID.Equals(users.ID)).FirstOrDefault();
                unitOfWork.LoginRoleRepository.Delete(userss);
                unitOfWork.LoginRepository.Delete(users);
                unitOfWork.StudentRepository.Delete(user);
                unitOfWork.Save();
                throw new Exception("删除成功");
            }
            catch (Exception ex)
            {
                viewModelInformation.Message = ex.Message;
                return viewModelInformation;
            }
        }
    }
}
