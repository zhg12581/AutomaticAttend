using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticAttend.Models
{
    public class TeacherStudent
    {
        public int ID { get; set; }
        //Teacher表里的ID
        public int TeacherId{ get; set; }
        //Student表里的ID
        public int StudentId{ get; set; }

    }
}