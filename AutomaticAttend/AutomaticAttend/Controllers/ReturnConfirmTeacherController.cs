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
    public class ReturnConfirmTeacherController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpGet]
        [ActionName("GetConfirmTeacher")]
        public List<ViewModelRegisterTeacher> PrimaryTeacher()
        {
            var data = (from u in unitOfWork.TeacherRepository.Get()
                        join ur in unitOfWork.LoginRepository.Get() on u.OpenId equals ur.OpenId
                        join r in unitOfWork.LoginRoleRepository.Get() on ur.ID equals r.UserID
                        where r.ConfirmRoleID.Equals(r.PrimaryRoleID)
                        select new ViewModelRegisterTeacher { TeacherId = u.TeacherId, Name = u.Name, Subject = u.Subject }).ToList();
            return data;
        }
    }
}
