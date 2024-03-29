﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomaticAttend.Models
{
    public class SysUser
    {
        public int ID { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 用户发送的消息
        /// </summary>
        public string Name { get; set; }
    }
}