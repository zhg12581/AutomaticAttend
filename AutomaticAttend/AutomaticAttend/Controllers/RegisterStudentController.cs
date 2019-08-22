using AutomaticAttend.DAL;
using AutomaticAttend.Models;
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
    public class RegisterStudentController : ApiController
    {
        private AccountContext db = new AccountContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        [HttpPost]
        [ActionName("PostRegisterStudent")]
        public ViewModelInformation RegisterStudent(ViewModelRegisterStudent viewModelRegisterStudent)
        {
            ViewModelInformation viewModelInformation = null;
            try
            {
                viewModelInformation = new ViewModelInformation();
                string js_code = viewModelRegisterStudent.Code.ToString();
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

                var user = new Student();
                user.OpenId = openid;
                user.StudentId = viewModelRegisterStudent.StudentId;
                user.Name = viewModelRegisterStudent.Name;
                user.ProfessionalClass = viewModelRegisterStudent.ProfessionalClass;
                user.TotalSignIn = 0;
                user.TotalAttendance = 0;
                unitOfWork.StudentRepository.Insert(user);    //增加新User
                unitOfWork.Save();

                var users = new Login();
                users.OpenId = openid;
                unitOfWork.LoginRepository.Insert(users);
                unitOfWork.Save();

                var sysRole = unitOfWork.SysRoleRepository.Get().Where(s => s.RoleName.Equals("学生")).FirstOrDefault();    //寻找用户所选择角色在UserRole里的实例，返回对象
                var userRole = new LoginRole();
                userRole.UserID = users.ID;
                userRole.PrimaryRoleID = sysRole.ID;
                userRole.ConfirmRoleID = 0;
                unitOfWork.LoginRoleRepository.Insert(userRole);
                unitOfWork.Save();

                throw new Exception("提交成功");
            }
            catch (Exception ex)
            {
                viewModelInformation.Message = ex.Message;
                return viewModelInformation;
            }
        }
    }
}
