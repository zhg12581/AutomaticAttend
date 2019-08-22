using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticAttend.Models
{
    public class CourseSelectInformation
    {
        public int ID { get; set; }
        //学生表里的ID
        public int StudentId { get; set; }
        //老师课程关系表里的ID
        public int TeacherCourseId { get; set; }
    }
}