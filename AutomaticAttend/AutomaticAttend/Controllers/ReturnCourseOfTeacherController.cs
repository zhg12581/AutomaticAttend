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
    public class ReturnCourseOfTeacherController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostCourseOfTeacher")]
        public List<ViewModelAllCourse> CourseOfTeacher(ViewModelTeacherOfCheckboxGroup viewModelTeacherOfCheckboxGroup)
        {
            var teacher = unitOfWork.TeacherRepository.Get().Where(s => s.Name.Equals(viewModelTeacherOfCheckboxGroup.TeacherName)).FirstOrDefault();
            var data = (from u in unitOfWork.TeacherRepository.Get()
                        join ur in unitOfWork.TeacherCourseRepository.Get() on u.ID equals ur.TeacherId
                        join r in unitOfWork.CourseRepository.Get() on ur.CourseId equals r.ID
                        where ur.TeacherId.Equals(teacher.ID)
                        select new ViewModelAllCourse { CourseId = r.CourseId, CourseName = r.CourseName,CoursePlace = r.CoursePlace,WeekDay=r.CoursePlace,StartTime=r.StartTime,OverTime=r.OverTime}).ToList();
            return data;
        }
    }
}
