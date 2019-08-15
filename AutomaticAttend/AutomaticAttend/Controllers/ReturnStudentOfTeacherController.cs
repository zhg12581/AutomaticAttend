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
    public class ReturnStudentOfTeacherController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostStudentOfTeacher")]
        public List<ViewModelConfirmStudent> StudentOfTeacher(ViewModelTeacherOfCheckboxGroup viewModelTeacherOfCheckboxGroup)
        {
            var teacher = unitOfWork.TeacherRepository.Get().Where(s => s.Name.Equals(viewModelTeacherOfCheckboxGroup.TeacherName)).FirstOrDefault();
            var data = (from u in unitOfWork.TeacherRepository.Get()
                        join ur in unitOfWork.TeacherStudentRepository.Get() on u.ID equals ur.TeacherId
                        join r in unitOfWork.StudentRepository.Get() on ur.StudentId equals r.ID
                        where ur.TeacherId.Equals(teacher.ID)
                        select new ViewModelConfirmStudent { StudentId = r.StudentId, Name = r.Name, ProfessionalClass = r.ProfessionalClass }).ToList();
            return data;
        }
    }
}
