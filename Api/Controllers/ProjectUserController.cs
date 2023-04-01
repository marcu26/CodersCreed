using Core.Dtos.ProjectUser;
using Core.Services;
using Infrastructure.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/project-user")]
    public class ProjectUserController : BaseController
    {
        public ProjectUserService _projectUserService { get; set; }
        public ProjectUserController(ProjectUserService projectUserService)
        {
            _projectUserService = projectUserService;
        }

        [HttpPost("assign-user-to-project/{projectId}-{userId}")]
        public async Task<ActionResult> AssignUserToProject([FromRoute]int projectId, [FromRoute] int userId)
        {
            try
            {
                await _projectUserService.AssignUserToProject(projectId, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("assign-manager-to-project/{projectId}-{userId}")]
        public async Task<ActionResult> AssignManagerToProject([FromRoute] int projectId, [FromRoute] int userId)
        {
            try
            {
                await _projectUserService.AssignManagerToProject(projectId, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("get-pagina")]
        public async Task<ActionResult> GetProjectUsersPaginaAsync([FromBody] PageablePostModelProjectUsers payload)
        {
            try
            {
                var dto = await _projectUserService.GetProjectUsersDtoPaginaAsync(payload);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
