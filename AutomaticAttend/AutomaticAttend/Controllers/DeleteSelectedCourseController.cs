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
    public class DeleteSelectedCourseController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostDeleteSelectedCourse")]
        public ViewModelInformation DeleteSelectedCourse(string[] choosecourse)
        {
            ViewModelInformation viewModelInformation = new ViewModelInformation();
            string studentid = choosecourse[0];
            var courseselectinformation = unitOfWork.CourseSelectInformationRepository.Get().Where(s => s.StudentId.Equals(int.Parse(studentid))).ToList();
            for (int i = 1; i < choosecourse.Length; i++)
            {
                var course = courseselectinformation.Where(s => s.TeacherCourseId.Equals(int.Parse(choosecourse[i]))).FirstOrDefault();
                unitOfWork.CourseSelectInformationRepository.Delete(course);
                unitOfWork.Save();
            }
            viewModelInformation.Message = "解绑成功";
            return viewModelInformation;

        }
    }
}
