using System;
using System.Collections.Generic;
using System.Text;
using ProviderEdge_V3_Core.GblRepository.Interface;
using ProviderEdge_V3_Core.GblRepository.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ProviderEdge_V3_Core.GblRepository.Repository
{
   public class GblSecurityRepository: IGblSecurityRepository
    {

        private List<UserModel> _userList;

        public GblSecurityRepository()
        {
            InitializeUserList();
        }

        public void InitializeUserList()
        {
            _userList = new List<UserModel>();
            _userList.Add(new UserModel
            {
                UserId = 1,
                UserName = "Deepak Mohanty",
                UserLoginId = "deepak",
                UserPassword="deepak@123",
                RoleId=1,
                EmailId="deepak@gmail.com",
                MobileNo ="9820023456",
                RoleName="Admin"
            });
            _userList.Add(new UserModel
            {
                UserId = 2,
                UserName = "Karan Panchal",
                UserLoginId = "karan",
                UserPassword = "karan@123",
                RoleId = 1,
                EmailId = "karan@gmail.com",
                MobileNo = "232323233",
                RoleName = "SuperAdmin"
            });

            _userList.Add(new UserModel
            {
                UserId = 3,
                UserName = "Nikunj Dani",
                UserLoginId = "nikunj",
                UserPassword = "nikunj@123",
                RoleId = 1,
                EmailId = "nikunj@gmail.com",
                MobileNo = "232323261",
                RoleName = "User"
            });
            _userList.Add(new UserModel
            {
                UserId = 4,
                UserName = "Mehul Ajmera",
                UserLoginId = "mehul",
                UserPassword = "mehul@123",
                RoleId = 1,
                EmailId = "mehul@gmail.com",
                MobileNo = "232323245",
                RoleName = "User"
            });
        }

        public async  Task<ModelResponse> UserAuthenticationAsync(string UserLoginId, string UserPassword)
        {
            ModelResponse objModelResponse = new ModelResponse();
            await Task.Delay(100);

            var objUser=_userList.Where(rec => rec.UserLoginId == UserLoginId && rec.UserPassword == UserPassword).FirstOrDefault();

            if(objUser != null)
            {
                objModelResponse.Status = true;
                objModelResponse.ReturnObj = objUser;
            }

            return objModelResponse;
        }





    }
}
