using System;
using System.Collections.Generic;
using System.Text;
using ProviderEdge_V3_Core.GblRepository.Model;

namespace ProviderEdge_V3_Core.GblService.ViewModels
{
   public class LoginViewModel
    {
        public string UserLoginId  { get; set; }
        public string UserPassword { get; set; }
    }
    public class ViewModelResponse
    {
        public string Status { get; set; }

        public string Msg { get; set; }

        public object ReturnObj { get; set; }
    }

    public class LoginViewModelRes: ViewModelResponse
    {
        public string SecurityToken { get; set; }

        public string UserName { get; set; }

        public string UserLoginId { get; set; } 

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public LoginViewModelRes()
        {
            Status = "F";
            Msg = "";
        }

        public LoginViewModelRes(UserModel objUserModel)
        {
            this.UserName = objUserModel.UserName;
            this.UserLoginId = objUserModel.UserLoginId;
            this.RoleId = objUserModel.RoleId;
            this.RoleName = objUserModel.RoleName;
        }

    }



}
