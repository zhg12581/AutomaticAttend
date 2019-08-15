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
    public class DeleteTeacherStudentController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostDeleteTeacherStudent")]
        public ViewModelInformation DeleteTeacherStudent(string[] choosestudent)
        {
            ViewModelInformation viewModelInformation = new ViewModelInformation();
            string teachername = choosestudent[0];
            var teacher = unitOfWork.TeacherRepository.Get().Where(s => s.Name.Equals(teachername)).FirstOrDefault();
            var teacherstudent = unitOfWork.TeacherStudentRepository.Get().Where(s => s.TeacherId.Equals(teacher.ID)).ToList();
            for (int i = 1; i < choosestudent.Length; i++)
            {
                var student = unitOfWork.StudentRepository.Get().Where(s => s.StudentId.Equals(choosestudent[i])).FirstOrDefault();
                for (int j = 0; j < teacherstudent.Count; j++)
                {
                    if (student.ID.Equals(teacherstudent[j].StudentId))
                    {                     
                        unitOfWork.TeacherStudentRepository.Delete(teacherstudent[j]);                      
                        unitOfWork.Save();
                        break;
                    }
                }

            }
            viewModelInformation.Message = "绑定成功";
            return viewModelInformation;

        }
    }
}
