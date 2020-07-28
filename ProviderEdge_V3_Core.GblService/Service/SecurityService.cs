using System;
using System.Collections.Generic;
using System.Text;
using ProviderEdge_V3_Core.GblService.InterFace;
using ProviderEdge_V3_Core.GblService.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using ProviderEdge_V3_Core.GblRepository.Interface;
using ProviderEdge_V3_Core.GblRepository.Model;

namespace ProviderEdge_V3_Core.GblService.Service
{
   public class SecurityService: ISecurityService
    {

        private IGblSecurityRepository _objIGblSecurityRepsitory;
        public SecurityService(IGblSecurityRepository objIGblSecurityRepsitory)
        {
            _objIGblSecurityRepsitory = objIGblSecurityRepsitory;
        }

        public async Task<LoginViewModelRes> AuthenticateUser(LoginViewModel objLoginViewModel)
        {
            LoginViewModelRes objLoginViewModelRes = new LoginViewModelRes();

            Task<ModelResponse> objTask=  _objIGblSecurityRepsitory.UserAuthenticationAsync(objLoginViewModel.UserLoginId, objLoginViewModel.UserPassword);

            ModelResponse objModelResponse = await objTask;

            if(objModelResponse.Status)
            {
                objLoginViewModelRes = new LoginViewModelRes((UserModel)objModelResponse.ReturnObj);
                objLoginViewModelRes.Status = "Y";
                objLoginViewModelRes.Msg = "user credentails validated successfully.";
            }
            else
            {
                objLoginViewModelRes.Status = "F";
                objLoginViewModelRes.Msg = "user credentails validation failed.";
            }
            

            return objLoginViewModelRes;
        }

    }
}
