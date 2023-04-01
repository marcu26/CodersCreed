using Core.Database.Entities;
using Core.Dtos.Rewards;
using Core.Services;
using Core.Utils.Pageable;
using Infrastructure.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/reward")]
    public class RewardController : BaseController
    {
        public RewardService _rewardService { get; set; }

        public RewardController(RewardService rewardService)
        {
            _rewardService = rewardService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateReward([FromBody] CreateRewardDto payload)
        {
            try
            {
                await _rewardService.CreateRewardAsync(payload);

                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("get-by-id/{rewardId}")]
        public async Task<ActionResult> GetRewardAsync([FromRoute] int rewardId)
        {
            try
            {
                var dto = await _rewardService.GetRewardsByIdAsync(rewardId);

                return Ok(dto);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPut("delete/{rewardId}")]
        public async Task<ActionResult> DeleteRewardAsync([FromRoute] int rewardId)
        {
            try
            {
                await _rewardService.DeleteRewardAsync(rewardId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateRewardsAsync([FromBody] UpdateRewardDto payload)
        {
            try
            {
                await _rewardService.UpdateRewardAsync(payload);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost("get-pagina")]
        public async Task<ActionResult> GetRewardsPaginaAsync([FromBody] PageablePostModelReward request)
        {
            try
            {
                var dto = await _rewardService.GetPaginaAsync(request);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
