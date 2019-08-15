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
    public class ReturnPrimaryStudentController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpGet]
        [ActionName("GetPrimaryStudent")]
        public List<ViewModelRegisterStudent> PrimaryStudent()
        {
            var data = (from u in unitOfWork.StudentRepository.Get()
                        join ur in unitOfWork.LoginRepository.Get() on u.OpenId equals ur.OpenId
                        join r in unitOfWork.LoginRoleRepository.Get() on ur.ID equals r.UserID
                        where r.ConfirmRoleID.Equals(0)
                        select new ViewModelRegisterStudent { StudentId =u.StudentId, Name = u.Name, ProfessionalClass = u.ProfessionalClass }).ToList();
            return data;
        }
    }
    
}
