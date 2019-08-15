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
    public class ReturnAllCourseController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpGet]
        [ActionName("GetAllCourse")]
        public List<ViewModelAllCourse> AllCourse()
        {
            var data = (from us in unitOfWork.CourseRepository.Get()
                        select new ViewModelAllCourse { CourseId = us.CourseId, CourseName = us.CourseName,CoursePlace=us.CoursePlace,StartTime=us.StartTime,OverTime=us.OverTime,WeekDay=us.WeekDay }).ToList();
            return data;
        }
    }
}
