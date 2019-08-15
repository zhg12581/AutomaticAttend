using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticAttend.ViewModel
{
    public class ViewModelRegisterStudent
    {
        //用户注册微信的唯一标识，目前暂时用微信昵称代替
        public string OpenId { get; set; }

        //学生学号
        public string StudentId { get; set; }

        //学生姓名
        public string Name { get; set; }

        //学生专业班级
        public string ProfessionalClass { get; set; }

        //预选角色：学生(用户在微信小程序注册时选择的角色）
        public string PrimaryRole { get; set; }

    }
}