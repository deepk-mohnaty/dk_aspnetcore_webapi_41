using System;
using System.Collections.Generic;
using System.Text;
using ProviderEdge_V3_Core.GblService.InterFace;
using ProviderEdge_V3_Core.GblService.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ProviderEdge_V3_Core.GblService.InterFace
{
   public interface ISecurityService
    {
         Task<LoginViewModelRes> AuthenticateUser(LoginViewModel objLoginViewModel);
    }
}
