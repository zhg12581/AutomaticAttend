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
    public class ReturnBindingCourseOfTeacherController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostCheckboxGroupCourse")]
        public List<ViewModelAllCourse> CheckboxGroupCourse(ViewModelTeacherOfCheckboxGroup viewModelTeacherOfCheckboxGroup)
        {
            var teacher = unitOfWork.TeacherRepository.Get().Where(s => s.Name.Equals(viewModelTeacherOfCheckboxGroup.TeacherName)).FirstOrDefault();
            var data = (from u in unitOfWork.CourseRepository.Get()                        
                        select new ViewModelAllCourse { CourseId = u.CourseId, CourseName = u.CourseName,CoursePlace = u.CoursePlace, StartTime = u.StartTime,OverTime=u.OverTime,WeekDay=u.WeekDay,ID=u.ID}).ToList();
            var data2 = (from u in unitOfWork.CourseRepository.Get()
                        select new ViewModelAllCourse { CourseId = u.CourseId, CourseName = u.CourseName, CoursePlace = u.CoursePlace, StartTime = u.StartTime, OverTime = u.OverTime, WeekDay = u.WeekDay, ID = u.ID }).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                var teacherscourse = unitOfWork.TeacherCourseRepository.Get().Where(s => s.CourseId.Equals(data[i].ID)).ToList();
                for (int j = 0; j < teacherscourse.Count; j++)
                {
                    if (teacherscourse[j].TeacherId.Equals(teacher.ID))
                    {
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
