using Core.Dtos.Authentication;
using Core.Dtos.Users;
using Core.Services;
using Core.Utils.Pageable;
using Infrastructure.Base;
using ISoft.Travel.Core.Dtos.EmailDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : BaseController
    {
        public UserService userService { get; set; }
        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("create-user")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDto payload)
        {
            try
            {
                var user = await userService.CreateUserAsync(payload);
                GetUserId();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate-user")]
        public async Task<ActionResult> AuthenticateUserAsync([FromBody] AuthenticationRequestDto payload)
        {
            try
            {
                var response = await userService.AuthenticateUserAsync(payload);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("get-user/{userId}")]
        public async Task<ActionResult> GetUserDto([FromRoute] int userId)
        {
            try
            {

                var user = await userService.GetUserDtoByIdAsync(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("get-users-pagina")]
        public async Task<ActionResult> GetPaginaAsync([FromBody] PageablePostModel request)
        {
            try
            {
                var dto = await userService.GetPaginaAsync(request);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("update-user")]
        public async Task<ActionResult> UpdateUserById([FromBody] UpdateUserDto payload)
        {
            try
            {
                await userService.UpdateUserByIdAsync(payload);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangeUserPasswordDto payload)
        {
            try
            {
                var userId = GetUserId();
                await userService.UpdatePasswordAsync(userId, payload);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("send-forget-password-email")]
        public async Task<ActionResult> SendForgetPasswordEmail([FromBody] ForgetPasswordEmailDto payload)
        {
            try
            {
                await userService.SendForgetPasswordEmail(payload);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto payload)
        {
            await userService.ResetPasswordAsync(payload);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("add-reward/{rewardId}")]
        public async Task<ActionResult> AddRewardToUser([FromRoute] int rewardId)
        {
            await userService.AddRewardToUser(GetUserId(), rewardId);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("add-points")]
        public async Task<ActionResult> AddXp([FromBody] int xpPoints)
        {

            int userId = GetUserId();
            await userService.AddXpToUserAsync(userId, xpPoints);
            return Ok();

        }
    }
}
