using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Base;
using Core.Services;
using Org.BouncyCastle.Tls.Crypto.Impl.BC;
using Core.Dtos;

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
        public async Task<ActionResult> Predict([FromBody]ObosealaDto dto)
        { 
            var result = await obosealaService.MakeCall(dto.Data,dto.Type);
            return Ok(result);
        }
    }
}
