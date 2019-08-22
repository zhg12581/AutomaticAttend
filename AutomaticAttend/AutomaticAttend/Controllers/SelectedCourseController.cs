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
    public class SelectedCourseController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostSelectedCourse")]
        public List<ViewModelChooseCourse> SelectedCourse(ViewModelTeacherOfCheckboxGroup viewModelTeacherOfCheckboxGroup)
        {
            var data = (from u in unitOfWork.StudentRepository.Get()
                        join ur in unitOfWork.CourseSelectInformationRepository.Get() on u.ID equals ur.StudentId
                        join r in unitOfWork.TeacherCourseRepository.Get() on ur.TeacherCourseId equals r.ID
                        join vr in unitOfWork.CourseRepository.Get() on r.CourseId equals vr.ID
                        join wr in unitOfWork.TeacherRepository.Get() on r.TeacherId equals wr.ID
                        where ur.StudentId.Equals(int.Parse(viewModelTeacherOfCheckboxGroup.StudentId))
                        select new ViewModelChooseCourse { CourseId = vr.CourseId, CourseName = vr.CourseName, CoursePlace = vr.CoursePlace, WeekDay = vr.WeekDay, StartTime = vr.StartTime, OverTime = vr.OverTime, Teachername = wr.Name, TeacherCourseId = r.ID }).ToList();
            return data;
        }
    }
}
