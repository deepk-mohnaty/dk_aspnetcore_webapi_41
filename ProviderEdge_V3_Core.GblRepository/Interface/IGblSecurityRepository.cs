using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProviderEdge_V3_Core.GblRepository.Model;

namespace ProviderEdge_V3_Core.GblRepository.Interface
{
    public interface IGblSecurityRepository
    {
        Task<ModelResponse> UserAuthenticationAsync(string UserLoginId, string UserPassword);
    }
}
