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
    public class ReturnElectiveCourseController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostElectiveCourse")]
        public List<ViewModelChooseCourse> ChooseCourse(ViewModelTeacherOfCheckboxGroup viewModelTeacherOfCheckboxGroup)
        {
            int studentid = int.Parse(viewModelTeacherOfCheckboxGroup.StudentId);
            var data = (from u in unitOfWork.TeacherRepository.Get()
                        join ur in unitOfWork.TeacherCourseRepository.Get() on u.ID equals ur.TeacherId
                        join r in unitOfWork.CourseRepository.Get() on ur.CourseId equals r.ID
                        select new ViewModelChooseCourse { CourseId = r.CourseId, CourseName = r.CourseName, CoursePlace = r.CoursePlace, WeekDay = r.WeekDay, StartTime = r.StartTime, OverTime = r.OverTime, Teachername=u.Name, TeacherCourseId =ur.ID}).ToList();
            var data2 = (from u in unitOfWork.TeacherRepository.Get()
                        join ur in unitOfWork.TeacherCourseRepository.Get() on u.ID equals ur.TeacherId
                        join r in unitOfWork.CourseRepository.Get() on ur.CourseId equals r.ID
                        select new ViewModelChooseCourse { CourseId = r.CourseId, CourseName = r.CourseName, CoursePlace = r.CoursePlace, WeekDay = r.WeekDay, StartTime = r.StartTime, OverTime = r.OverTime, Teachername = u.Name, TeacherCourseId = ur.ID }).ToList();

            for (int i = 0; i < data.Count; i++)
            {
                var courseSelectInformation = unitOfWork.CourseSelectInformationRepository.Get().Where(s => s.TeacherCourseId.Equals(data[i].TeacherCourseId)).ToList();
                for (int j = 0; j < courseSelectInformation.Count; j++)
                {
                    if (courseSelectInformation[j].StudentId.Equals(studentid))
                    {
                        int index = data2.FindIndex(item => item.TeacherCourseId.Equals(data[i].TeacherCourseId));
                        data2.Remove(data2[index]);
                        break;
                    }
                }
            }
            return data2;
        }
    }
}
