using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticAttend.ViewModel
{
    public class ViewModelConfirmStudent
    {
        //该学生在学生表里的ID
        public int ID { get; set; }

        //学生学号
        public string StudentId { get; set; }

        //学生姓名
        public string Name { get; set; }

        //学生专业班级
        public string ProfessionalClass { get; set; }

   
    }
}