using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticAttend.Models
{
    public class TeacherCourse
    {
        public int ID { get; set; }
        //Teacher表里的ID
        public int TeacherId { get; set; }
        //Course表里的ID
        public int CourseId { get; set; }

    }
}