using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Base;
using Core.Services;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/projects")]
    public class ObosealaController : BaseController
    {
        public ObosealaService obosealaService { get; set; }

        public ObosealaController(ObosealaService obosealaService)
        {
            this.obosealaService = obosealaService;
        }

        [HttpPost("predict")]
        public async Task<ActionResult> Predict(IFormFile pic)
        { 
            var result = await obosealaService.MakeCall(pic);
            return Ok(result);
        }
    }
}
