using Core.Dtos.Project;
using Core.Dtos.ProjectUser;
using Core.Dtos.Tasks;
using Core.Services;
using Infrastructure.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : BaseController
    {
        public ProjectService _projectService { get; set; }
        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateProject([FromBody] CreateProjectDto payload)
        {
            int managerId = GetUserId();
            await _projectService.CreateProjectAsync(payload,managerId);
            return Ok();
        }

        [HttpGet("get-one/{projectId}")]
        public async Task<ActionResult> GetProject([FromRoute] int projectId)
        {
            var task = await _projectService.GetProjectById(projectId);
            return Ok(task);
        }

        [HttpPut("edit/{projectId}")]
        public async Task<ActionResult> EditProject([FromRoute] int projectId, [FromBody] EditProjectDto payload)
        {
            await _projectService.EditProject(projectId, payload);
            return Ok();
        }

        [HttpPut("delete/{projectId}")]
        public async Task<ActionResult> DeleteProject([FromRoute] int projectId)
        {
            await _projectService.DeleteProject(projectId);
            return Ok();
        }

        [HttpPost("get-pagina")]
        public async Task<ActionResult> GetProjectsPaginaAsync([FromBody] PageablePostModelProject payload)
        {
            try
            {
                var dto = await _projectService.GetProjectsPaginaAsync(payload);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
