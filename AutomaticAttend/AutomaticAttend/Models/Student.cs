﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticAttend.Models
{
    public class Student
    {
        public int ID { get; set; }

        //用户注册微信的唯一标识，目前暂时用微信昵称代替
        public string OpenId { get; set; }

        //学生学号
        public string StudentId { get; set; }

        //学生姓名
        public string Name { get; set; }

        //学生专业班级
        public string ProfessionalClass { get; set; }

        //该学生总共的出勤次数
        public int TotalSignIn { get; set; }

        //该学生遇到的总考勤次数
        public int TotalAttendance { get; set; }

    }
}