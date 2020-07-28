using System;
using System.Collections.Generic;
using System.Text;

namespace ProviderEdge_V3_Core.Common.CommonEntities
{
   public class UserOnlineContext
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserLoginId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int OnlineId { get; set; }
        public DateTime LoginDatetime { get; set; }
        public GblTenantContext objGblTenantContext { get; set; }

        public ApplicationSettings objApplicationSettings { get; set; }

    }
}
