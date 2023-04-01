using Core.Dtos.Courses;
using Core.Dtos.CourseSection;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/course-section")]
    public class CourseSectionController : Controller
    {
        public CourseSectionService _courseSectionService { get; set; }
        public CourseSectionController(CourseSectionService courseSectionService)
        {
            _courseSectionService = courseSectionService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateCourseSection([FromBody] CreateCourseSectionDto payload)
        {
            try
            {
                await _courseSectionService.CreateCourseSectionAsync(payload);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("get-by-id/{courseSectionId}")]
        public async Task<ActionResult> GetCourseSectionAsync([FromRoute] int courseSectionId)
        {
            try
            {
                var dto = await _courseSectionService.GetCourseSectionByIdAsync(courseSectionId);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("delete/{courseSectionId}")]
        public async Task<ActionResult> DeleteCourseAsync([FromRoute] int courseSectionId)
        {
            try
            {
                await _courseSectionService.DeleteCourseSectionAsync(courseSectionId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateCourseSectionAsync([FromBody] UpdateCourseSection payload)
        {
            try
            {
                await _courseSectionService.UpdateCourseSectionAsync(payload);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
