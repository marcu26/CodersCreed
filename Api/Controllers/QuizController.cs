using Core.Dtos.Quiz;
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
    [Route("api/quiz")]
    public class QuizController : BaseController
    {
        public QuizzezService _quizzezService { get; set; }

        public QuizController(QuizzezService quizzezService)
        {
            _quizzezService = quizzezService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateReward([FromBody] CreateQuizDto payload)
        {
            try
            {
                await _quizzezService.CreateQuiz(payload);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet("get-by-id/{quizId}")]
        public async Task<ActionResult> GetByIdAsync([FromRoute] int quizId)
        {
            try
            {
                var dto = await _quizzezService.GetQuizAsync(quizId);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpPut("delete/{quizId}")]
        public async Task<ActionResult> DeleteQuizAsync([FromRoute] int quizId)
        {
            try
            {
                await _quizzezService.DeleteQuizAsync(quizId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost("get-quizzes-pagina")]
        public async Task<ActionResult> GetQuizzesPagina([FromBody] PageablePostModelQuiz request)
        {
            try
            {
                var dto = await _quizzezService.GetQuizDtoPaginaAsync(request);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpPut("update")]
        public async Task<ActionResult> DeleteQuizAsync([FromBody]UpdateQuizDto payload)
        {
            try
            {
                await _quizzezService.UpdateQuizAsync(payload);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
