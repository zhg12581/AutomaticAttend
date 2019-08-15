using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticAttend.Models
{
    public class Login
    {
        public int ID { get; set; }

        //用户注册微信的唯一标识，目前暂时用微信昵称代替
        public string OpenId { get; set; }
    }
}