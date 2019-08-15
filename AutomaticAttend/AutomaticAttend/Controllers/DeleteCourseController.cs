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
    public class DeleteCourseController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostDeleteCourse")]
        public ViewModelInformation DeleteCourse(ViewModelAllCourse viewModelAllCourse)
        {
            ViewModelInformation viewModelInformation = null;
            try
            {
                viewModelInformation = new ViewModelInformation();
                var user = unitOfWork.CourseRepository.Get().Where(s => s.CourseId.Equals(viewModelAllCourse.CourseId)).FirstOrDefault();
                unitOfWork.CourseRepository.Delete(user);
                unitOfWork.Save();
                throw new Exception("删除成功");              
             
            }
            catch (Exception ex)
            {
                viewModelInformation.Message = ex.Message;
                return viewModelInformation;
            }
        }
    }
}
