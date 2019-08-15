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
    public class ReturnBindingStudentOfTeacherController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostCheckboxGroupStudent")]
        public List<ViewModelConfirmStudent> CheckboxGroupStudent(ViewModelTeacherOfCheckboxGroup viewModelTeacherOfCheckboxGroup)
        {
            var teacher = unitOfWork.TeacherRepository.Get().Where(s => s.Name.Equals(viewModelTeacherOfCheckboxGroup.TeacherName)).FirstOrDefault();
          //  List<ViewModelConfirmStudent> viewModelConfirmStudent = new List<ViewModelConfirmStudent>();
            var data = (from u in unitOfWork.StudentRepository.Get()
                        join ur in unitOfWork.LoginRepository.Get() on u.OpenId equals ur.OpenId
                        join r in unitOfWork.LoginRoleRepository.Get() on ur.ID equals r.UserID
                        where r.ConfirmRoleID.Equals(r.PrimaryRoleID)
                        select new ViewModelConfirmStudent { StudentId = u.StudentId, Name = u.Name, ProfessionalClass = u.ProfessionalClass, ID = u.ID }).ToList();
            var data2 = (from u in unitOfWork.StudentRepository.Get()
                         join ur in unitOfWork.LoginRepository.Get() on u.OpenId equals ur.OpenId
                         join r in unitOfWork.LoginRoleRepository.Get() on ur.ID equals r.UserID
                         where r.ConfirmRoleID.Equals(r.PrimaryRoleID)
                         select new ViewModelConfirmStudent { StudentId = u.StudentId, Name = u.Name, ProfessionalClass = u.ProfessionalClass, ID = u.ID }).ToList();
            for (int i=0;i<data.Count;i++)
            {
                var teacherstudent = unitOfWork.TeacherStudentRepository.Get().Where(s => s.StudentId.Equals(data[i].ID)).ToList();
                for (int j = 0; j < teacherstudent.Count; j++)
                {
                    if (teacherstudent[j].TeacherId.Equals(teacher.ID))
                    {
                       // var data3 = (from p in data2
                           //          where p.ID.Equals(data[i].ID)
                           //          select new ViewModelConfirmStudent { StudentId = p.StudentId, Name = p.Name, ProfessionalClass = p.ProfessionalClass, ID = p.ID }).FirstOrDefault();
                        int index = data2.FindIndex(item => item.ID.Equals(data[i].ID));
                        data2.Remove(data2[index]);
                        break;
                    }
                }
            }
            return data2;
        }
    }
}
