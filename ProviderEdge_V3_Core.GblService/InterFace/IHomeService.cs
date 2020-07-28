using System;
using System.Collections.Generic;
using System.Text;
using ProviderEdge_V3_Core.GblService.ViewModels;

namespace ProviderEdge_V3_Core.GblService.InterFace
{
   public interface IHomeService
    {
        HomeVMRes GetHomeData();
    }
}
