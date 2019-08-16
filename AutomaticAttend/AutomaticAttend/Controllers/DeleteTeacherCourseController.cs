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
    public class DeleteTeacherCourseController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostDeleteTeacherCourse")]
        public ViewModelInformation DeleteTeacherCourse(string[] choosecourse)
        {
            ViewModelInformation viewModelInformation = new ViewModelInformation();
            string teachername = choosecourse[0];
            var teacher = unitOfWork.TeacherRepository.Get().Where(s => s.Name.Equals(teachername)).FirstOrDefault();
            var teachercourse = unitOfWork.TeacherCourseRepository.Get().Where(s => s.TeacherId.Equals(teacher.ID)).ToList();
            for (int i = 1; i < choosecourse.Length; i++)
            {
                var course = unitOfWork.CourseRepository.Get().Where(s => s.CourseId.Equals(choosecourse[i])).FirstOrDefault();
                for (int j = 0; j < teachercourse.Count; j++)
                {
                    if (course.ID.Equals(teachercourse[j].CourseId))
                    {
                        unitOfWork.TeacherCourseRepository.Delete(teachercourse[j]);
                        unitOfWork.Save();
                        break;
                    }
                }

            }
            viewModelInformation.Message = "解绑成功";
            return viewModelInformation;

        }

    }
}
