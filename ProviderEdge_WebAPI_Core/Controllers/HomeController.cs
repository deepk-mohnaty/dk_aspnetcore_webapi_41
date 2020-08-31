using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProviderEdge_V3_Core.GblService.InterFace;
using Microsoft.Extensions.Options;
using ProviderEdge_V3_Core.Common.CommonEntities;
using ProviderEdge_V3_Core.GblService.ViewModels;
using ProviderEdge_WebAPI_Core.Middleware;

namespace ProviderEdge_WebAPI_Core.Controllers
{
    [Route("api/[controller]")]
    [DkAuthorize]
    [ApiController] 
    public class HomeController : ControllerBase
    {
        private IHomeService _objHomeService;
        private readonly ApplicationSettings _objConfigSettings; 
        public HomeController(IHomeService objHomeService, IOptions<ApplicationSettings> objConfigSettings)
        {
            _objHomeService = objHomeService;
            _objConfigSettings = objConfigSettings.Value;
        }

       
        [HttpGet("GetHomeData")]    
        public async Task<IActionResult> GetHomeData()
        {
            ViewModelResponse objViewModelResponse = new ViewModelResponse();
            objViewModelResponse.ReturnObj = _objHomeService.GetHomeData();
            await Task.Delay(3000);
            objViewModelResponse.Status = "Y";
            return Ok(objViewModelResponse);
        }
    }
}