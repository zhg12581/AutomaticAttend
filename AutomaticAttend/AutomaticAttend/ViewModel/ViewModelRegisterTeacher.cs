using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticAttend.ViewModel
{
    public class ViewModelRegisterTeacher
    {
        //用户注册微信的唯一标识，目前暂时用微信昵称代替
        public string OpenId { get; set; }

        //老师唯一编号
        public string TeacherId { get; set; }

        //老师姓名
        public string Name { get; set; }

        //老师教学科目
        public string Subject { get; set; }

        //预选角色：老师(用户在微信小程序注册时选择的角色）
        public string PrimaryRole { get; set; }

    }
}