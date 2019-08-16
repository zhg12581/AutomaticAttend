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
    public class BindingTeacherCourseController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostBindingTeacherCourse")]
        public ViewModelInformation BindingCourse(string[] choosecourse)
        {
            ViewModelInformation viewModelInformation = new ViewModelInformation();
            string teachername = choosecourse[0];
            var teacher = unitOfWork.TeacherRepository.Get().Where(s => s.Name.Equals(teachername)).FirstOrDefault();
            for (int index = 1; index < choosecourse.Length; index++)
            {
                var course = unitOfWork.CourseRepository.Get().Where(s => s.CourseId.Equals(choosecourse[index])).FirstOrDefault();
                var teachercourse = new TeacherCourse();
                teachercourse.TeacherId = teacher.ID;
                teachercourse.CourseId = course.ID;
                unitOfWork.TeacherCourseRepository.Insert(teachercourse);
                unitOfWork.Save();
            }
            viewModelInformation.Message = "绑定成功";
            return viewModelInformation;

        }
    }
}
