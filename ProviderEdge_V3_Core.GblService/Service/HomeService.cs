using System;
using System.Collections.Generic;
using System.Text;
using ProviderEdge_V3_Core.GblService.InterFace;
using ProviderEdge_V3_Core.GblService.ViewModels;

namespace ProviderEdge_V3_Core.GblService.Service
{
   public class HomeService :IHomeService
    {
        public HomeVMRes GetHomeData()
        {
            HomeVMRes objHomeVMRes = new HomeVMRes()
            {
                EmpId=1,
                EmpName="Deepak Mohanty",
                DepartmentId=1,
                DepartmentName="Product Engineering",
                DesignationId=2,
                DesignationName="Technial Architect"
            };

            return objHomeVMRes;
        
        }
    }
}
