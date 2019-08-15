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
    public class DeleteTeacherRoleController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostDeleteTeacher")]
        public ViewModelInformation DeleteTeacher(ViewModelConfirmOrDeleteRole viewModelDeleteRole)
        {
            ViewModelInformation viewModelInformation = null;
            try
            {
                viewModelInformation = new ViewModelInformation();
                var user = unitOfWork.TeacherRepository.Get().Where(s => s.TeacherId.Equals(viewModelDeleteRole.UserId)).FirstOrDefault();
                var users = unitOfWork.LoginRepository.Get().Where(s => s.OpenId.Equals(user.OpenId)).FirstOrDefault();
                var userss = unitOfWork.LoginRoleRepository.Get().Where(s => s.UserID.Equals(users.ID)).FirstOrDefault();
                unitOfWork.LoginRoleRepository.Delete(userss);
                unitOfWork.LoginRepository.Delete(users);
                unitOfWork.TeacherRepository.Delete(user);
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
