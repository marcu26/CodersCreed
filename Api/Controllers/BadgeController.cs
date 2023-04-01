
using Core.Dtos.Badges;
using Core.Services;
using Core.Utils.Pageable;
using Infrastructure.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/badge")]
    public class BadgeController : BaseController
    {
        public BadgeService _badgeService { get; set; }
        public BadgeController(BadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateBadge([FromBody] CreateBadgeDto payload)
        {
            try
            {
                await _badgeService.CreateBadgeAsync(payload);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("get-by-id/{bagdeId}")]
        public async Task <ActionResult> GetBadgeAsync([FromRoute] int bagdeId)
        {
           try
           {
                var dto = await _badgeService.GetBadgeByIdAsync(bagdeId);

                return Ok(dto);
           }
           catch (Exception ex) 
           {
                return BadRequest(ex.Message);
           }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPut("delete/{badgeId}")]
        public async Task<ActionResult> DeleteBadgeAsync([FromRoute] int badgeId)
        {
            try
            {
                await _badgeService.DeleteBadgeAsync(badgeId);

                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateBadgeAsync([FromBody] UpdateBadgeDto payload)
        {
            try
            {
                await _badgeService.UpdateBadgeAsync(payload);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("get-pagina")]
        public async Task<ActionResult> GetCoursesPaginaAsync([FromBody] PageablePostModel request)
        {
            try
            {
                var dto = await _badgeService.GetPaginaAsync(request);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
