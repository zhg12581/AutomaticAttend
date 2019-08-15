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
    public class BindingTeacherStudentController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostBindingTeacherStudent")]
        public ViewModelInformation BindingStudent(string[] choosestudent)
        {
            ViewModelInformation viewModelInformation = new ViewModelInformation();
            string teachername = choosestudent[0];
            var teacher = unitOfWork.TeacherRepository.Get().Where(s => s.Name.Equals(teachername)).FirstOrDefault();
            for (int index = 1; index < choosestudent.Length; index++)
            {
                var student = unitOfWork.StudentRepository.Get().Where(s => s.StudentId.Equals(choosestudent[index])).FirstOrDefault();
                var teacherstudent = new TeacherStudent();
                teacherstudent.TeacherId = teacher.ID;
                teacherstudent.StudentId = student.ID;
                unitOfWork.TeacherStudentRepository.Insert(teacherstudent);
                unitOfWork.Save();
            }
            viewModelInformation.Message = "绑定成功";
            return viewModelInformation;

        }
    }
}
