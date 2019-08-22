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
    public class ChooseCourseController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostChooseCourse")]
        public ViewModelInformation ChooseCourse(string[] choosecourse)
        {
            ViewModelInformation viewModelInformation = new ViewModelInformation();
            int studentid = int.Parse(choosecourse[0]);
            for (int index = 1; index < choosecourse.Length; index++)
            {
                var courseSelectInformation = new CourseSelectInformation();
                courseSelectInformation.StudentId = studentid;
                courseSelectInformation.TeacherCourseId = int.Parse(choosecourse[index]);
                unitOfWork.CourseSelectInformationRepository.Insert(courseSelectInformation);
                unitOfWork.Save();
            }
            viewModelInformation.Message = "选课成功";
            return viewModelInformation;
        }
    }
}
