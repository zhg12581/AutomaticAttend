using AutomaticAttend.DAL;
using AutomaticAttend.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            ViewModelInformation viewModelInformation = null;
            try
            {
                viewModelInformation = new ViewModelInformation();
                var user= unitOfWork.LoginRepository.Get().Where(s => s.OpenId.Equals(viewModelLogin.OpenId)).FirstOrDefault();
                if (user != null)
                {
                    var UserRole = (from u in unitOfWork.LoginRepository.Get()
                                    join ur in unitOfWork.LoginRoleRepository.Get() on u.ID equals ur.UserID
                                    join r in unitOfWork.SysRoleRepository.Get() on ur.PrimaryRoleID equals r.ID
                                    where u.OpenId.Equals(viewModelLogin.OpenId)
                                    select new { PrimaryRoleID = ur.PrimaryRoleID, RoleName = r.RoleName, ConfirmRoleID=ur.ConfirmRoleID })
                                   .FirstOrDefault();
                    if(UserRole.ConfirmRoleID==0)
                    {
                        throw new Exception("角色待确认");
                    }
                    else
                    {
                        throw new Exception(UserRole.RoleName);
                    }

                }
                else
                {
                    throw new Exception("还没注册");
                }
                
            }
            catch (Exception ex)
            {
                viewModelInformation.Message = ex.Message;
                return viewModelInformation;
            }
        }

    }
}
