using Core.Dtos.Courses;
using Core.Services;
using Core.Utils.Pageable;
using Infrastructure.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/course")]
    public class CourseController : BaseController
    {
        public CourseService _courseService { get; set; }
        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateCourse([FromBody] CreateCourseDto payload)
        {
            try
            {
                await _courseService.CreateCourseAsync(payload);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("get-by-id/{courseId}")]
        public async Task<ActionResult> GetCourseAsync([FromRoute] int courseId)
        {
            try
            {
                var dto = await _courseService.GetCourseByIdAsync(courseId);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("delete/{courseId}")]
        public async Task<ActionResult> DeleteCourseAsync([FromRoute] int courseId)
        {
            try
            {
                await _courseService.DeleteCourseAsync(courseId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateCourseAsync([FromBody] UpdateCourseDto payload)
        {
            try
            {
                await _courseService.UpdateCourseAsync(payload);
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
                var dto = await _courseService.GetPaginaAsync(request);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("add-course/{courseId}-{userId}")]
        public async Task<ActionResult> MarkCourseAsDone([FromRoute]int courseId,[FromRoute] int userId)
        {
            await _courseService.AddUserToCourse(userId, courseId);
            return Ok();    
        }

    }
}
