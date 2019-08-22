using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticAttend.ViewModel
{
    public class ViewModelChooseCourse
    {
        //老师课程关系表里的ID
        public int TeacherCourseId { get; set; }
        //课程唯一编号
        public string CourseId { get; set; }
        //课程名称
        public string CourseName { get; set; }
        //上课地点
        public string CoursePlace { get; set; }
        //课程星期
        public string WeekDay { get; set; }
        //上课开始时间
        public string StartTime { get; set; }
        //课程结束时间
        public string OverTime { get; set; }
        //课程的老师名字
        public string Teachername { get; set; }
    }
}