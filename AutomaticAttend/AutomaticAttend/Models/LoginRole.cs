using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticAttend.Models
{
    public class LoginRole
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        //预选角色ID(用户在微信小程序注册时选择的角色）
        public int PrimaryRoleID { get; set; }

        //角色确认ID(系统管理员确认后的角色)
        public int ConfirmRoleID { get; set; }
    }
}