using AutomaticAttend.DAL;
using AutomaticAttend.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace AutomaticAttend.Controllers
{
    public class LoginController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostLogin")]
        public ViewModelInformation Login(ViewModelLogin viewModelLogin)
        {
            ViewModelInformation viewModelInformation = new ViewModelInformation();
            viewModelInformation = new ViewModelInformation();
            string js_code = viewModelLogin.Code.ToString();
            string serviceAddress = "https://api.weixin.qq.com/sns/jscode2session?appid=wxece27e98fc59b527&secret=3efec00e6fe037602aeae3a317608942&js_code=" + js_code + "&grant_type=authorization_code";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            string key = "\"openid\":\"";
            int startIndex = retString.IndexOf(key);
            int endIndex = retString.IndexOf("\"}", startIndex);
            string openid = retString.Substring(startIndex + key.Length, endIndex - startIndex - key.Length);

            var user = unitOfWork.LoginRepository.Get().Where(s => s.OpenId.Equals(openid)).FirstOrDefault();
            if (user != null)
            {
                var UserRole = (from u in unitOfWork.LoginRepository.Get()
                                join ur in unitOfWork.LoginRoleRepository.Get() on u.ID equals ur.UserID
                                join r in unitOfWork.SysRoleRepository.Get() on ur.PrimaryRoleID equals r.ID
                                where u.OpenId.Equals(openid)
                                select new { PrimaryRoleID = ur.PrimaryRoleID, RoleName = r.RoleName, ConfirmRoleID = ur.ConfirmRoleID })
                               .FirstOrDefault();
                if (UserRole.ConfirmRoleID == 0)
                {
                    viewModelInformation.Message = "角色待确认";
                    return viewModelInformation;                
                }
                else
                {
                    if (UserRole.RoleName == "学生")
                    {
                        var student = unitOfWork.StudentRepository.Get().Where(s => s.OpenId.Equals(openid)).FirstOrDefault();
                        viewModelInformation.ID = student.ID;
                        viewModelInformation.Message = "学生";
                        return viewModelInformation;
                    }
                    else if(UserRole.RoleName == "教师")
                    {
                        var teacher = unitOfWork.TeacherRepository.Get().Where(s => s.OpenId.Equals(openid)).FirstOrDefault();
                        viewModelInformation.ID = teacher.ID;
                        viewModelInformation.Message = "教师";
                        return viewModelInformation;
                    }
                    else
                    {
                        viewModelInformation.Message = "admin";
                        return viewModelInformation;
                    }                    
                }
            }
            else
            {
                viewModelInformation.Message = "还没注册";
                return viewModelInformation;
            }


        }

    }
}
