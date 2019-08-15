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
    public class CreateCourseController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostCreateCourse")]
        public ViewModelInformation CreateCourse(ViewModelCreateCourse viewModelCreateCourse)
        {
            ViewModelInformation viewModelInformation = null;
            try
            {
                viewModelInformation = new ViewModelInformation();
                var user = unitOfWork.CourseRepository.Get().Where(s => s.CourseId.Equals(viewModelCreateCourse.CourseId)).FirstOrDefault();
                if (user == null)
                {
                    var course = new Course();
                    course.CourseId = viewModelCreateCourse.CourseId;
                    course.CourseName = viewModelCreateCourse.CourseName;
                    course.CoursePlace = viewModelCreateCourse.CoursePlace;
                    course.Longitude = viewModelCreateCourse.Longitude;
                    course.Latitude = viewModelCreateCourse.Latitude;
                    course.StartTime = viewModelCreateCourse.StartTime;
                    course.OverTime = viewModelCreateCourse.OverTime;
                    course.WeekDay = viewModelCreateCourse.WeekDay;
                    unitOfWork.CourseRepository.Insert(course);
                    unitOfWork.Save();
                    throw new Exception("创建课程成功");

                }
                else
                {
                    throw new Exception("该编号已存在");
                }

            }
            catch (Exception ex)
            {
                viewModelInformation.Message = ex.Message;
                return viewModelInformation;
            }
        }
    }
}
