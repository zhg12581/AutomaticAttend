﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticAttend.Models
{
    public class Course
    {
        public int ID { get; set; }
        //课程唯一编号
        public string CourseId { get; set; }
        //课程名称
        public string CourseName { get; set; }
        //上课地点
        public string CoursePlace { get; set; }
        //课程地点的经度
        public float Longitude { get; set; }
        //课程地点的纬度
        public float Latitude { get; set; }
        //课程星期
        public string WeekDay{ get; set; }
        //上课开始时间
        public string StartTime { get; set; }

        //课程结束时间
        public string OverTime { get; set; }


    }
}