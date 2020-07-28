using System;
using System.Collections.Generic;
using System.Text;

namespace ProviderEdge_V3_Core.GblRepository.Model
{

    public class ModelResponse
    {
        public object ReturnObj { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserLoginId { get; set; }
        public  string UserPassword { get; set; }

        public string UserName { get; set; }
        public int RoleId { get; set; }

        public string RoleName { get; set; }
        public string Status { get; set; }
        public string EmailId { get; set; }
        public string  MobileNo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }

    public class RoleModel
    {
        public int RoleId { get; set; }
        public string  RoleName { get; set; }
    }

    public class TenantModel
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
    }

    public class UserTenantModel
    {
        public int UserId { get; set; }
        public int TenantId { get; set; }
    }

    


}
