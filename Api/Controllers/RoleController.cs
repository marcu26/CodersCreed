using Core.Services;
using Infrastructure.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/roles")]
    public class RoleController:BaseController
    {
        public RoleService roleService { get; set; }
        public RoleController(RoleService roleService)
        {
            this.roleService = roleService;
        }
        [Authorize(Roles ="Administrator")]
        [HttpGet("get-roles")]
        public async Task<ActionResult> GetRolesDto()
        {
            try
            {
                var result = await roleService.GetRolesDtoAsync();
                return Ok(result);
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("add-role-to-user/{roleName}/{userId}")]
        public async Task<ActionResult> AddRoleToUser([FromRoute] int userId, [FromRoute] string roleName)
        {
            try
            {
                await roleService.AddRoleToUserAsync(roleName, userId);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        return Ok();
        }
    }
}
