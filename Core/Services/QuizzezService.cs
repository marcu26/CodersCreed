using Core.Database.Entities;
using Core.Dtos.Courses;
using Core.Dtos.Question;
using Core.Dtos.Quiz;
using Core.UnitOfWork;
using Core.Utils.Pageable;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class QuizzezService
    {
        public EfUnitOfWork _efUnitOfWork { get; set; }

        public QuizzezService(EfUnitOfWork efUnitOfWork)
        {
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task CreateQuiz(CreateQuizDto payload)
        {
            var exist = await _efUnitOfWork._quizzesRepository.DoesNameExist(payload.Name);

            if (exist)
                throw new WrongInputException($"Quiz with name {payload.Name}");

            var course = await _efUnitOfWork._coursesRepository.GetByIdAsync(payload.CourseId);

            if (course == null)
                throw new WrongInputException($"Course with id {payload.CourseId} does not exist.");

            if (course.IsDeleted)
                throw new WrongInputException($"Course with id {payload.CourseId} does not exist.");

            var quiz = new Quiz { Name = payload.Name, Points = payload.Points, CourseId = payload.CourseId };

            quiz.Questions = new List<Question>();

            foreach (var q in payload.Questions)
            {
                var question = new Question { Content = q.Content };

                question.Answers = new List<Answer>();

                foreach (var answer in q.Answers)
                {
                    question.Answers.Add(new Answer { Content = answer.Content, IsCorrect = answer.IsCorrect });
                }

                quiz.Questions.Add(question);
            }

            _efUnitOfWork._quizzesRepository.Add(quiz);

            await _efUnitOfWork.SaveAsync();
        }

        public async Task<GetQuizDto> GetQuizAsync(int quizId)
        {
            var quiz = await _efUnitOfWork._quizzesRepository.GetQuizWithPropertiesAsync(quizId);


            if (quiz == null)
                throw new WrongInputException($"Quiz with id {quizId} does not exist.");

            if (quiz.IsDeleted)
                throw new WrongInputException($"Quiz with id {quizId} does not exist.");

            return new GetQuizDto
            {
                Id = quiz.Id,
                Name = quiz.Name,
                CourseId = quiz.CourseId,
                Points = quiz.Points,
                Questions = quiz.Questions.Select(q => new GetQuestionDto
                {
                    Id = q.Id,
                    Content = q.Content,
                    Answers = q.Answers.Select(a => new Dtos.Answer.GetAnswerDto
                    {
                        Id = a.Id,
                        Content = a.Content,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()
            };
        }

        public async Task DeleteQuizAsync(int quizId)
        {
            var quiz = await _efUnitOfWork._quizzesRepository.GetQuizWithPropertiesAsync(quizId);

            if (quiz == null)
                throw new WrongInputException($"Quiz with id {quizId} does not exist.");

            if (quiz.IsDeleted)
                throw new WrongInputException($"Quiz with id {quizId} does not exist.");

            quiz.IsDeleted = true;

            await _efUnitOfWork.SaveAsync();
        }

        public async Task<PageableResponse> GetQuizDtoPaginaAsync(PageablePostModelQuiz request)
        {
            var dto = await _efUnitOfWork
                     ._quizzesRepository
                     .GetQuizzesPaginaAsync(request);

            return new PageableResponse()
            {
                draw = request.draw,
                recordTotal = dto.NumarTotalRanduri,
                recordsFiltered = dto.NumarRanduriFiltrate,
                data = dto.Pagina.Select(e => new GetQuizDtoPagina
                {
                    QuizId = e.Id,
                    Name = e.Name,
                    Points = e.Points
                })
            };
        }

        public async Task UpdateQuizAsync(UpdateQuizDto payload)
        {
            if (payload.Name != null)
            {
                var exist = await _efUnitOfWork._quizzesRepository.DoesNameExist(payload.Name);

                if (exist)
                    throw new WrongInputException($"Quiz with name {payload.Name}");
            }

            var quiz = await _efUnitOfWork._quizzesRepository.GetQuizWithPropertiesAsync(payload.Id);

            if (quiz == null)
                throw new WrongInputException($"Quiz with id {payload.Id} does not exist.");

            if (quiz.IsDeleted)
                throw new WrongInputException($"Quiz with id {payload.Id} does not exist.");

            quiz.Name = String.IsNullOrEmpty(payload.Name) ? quiz.Name : payload.Name;
            quiz.Points = payload.Points == null ? quiz.Points : payload.Points.Value;


            if (payload.Questions != null)
            {
                quiz.Questions.RemoveAll(q=>q.QuizId==payload.Id);

                foreach (var q in payload.Questions)
                {
                    var question = new Question { Content = q.Content };

                    question.Answers = new List<Answer>();

                    foreach (var answer in q.Answers)
                    {
                        question.Answers.Add(new Answer { Content = answer.Content, IsCorrect = answer.IsCorrect });
                    }

                    quiz.Questions.Add(question);
                }
            }

            await _efUnitOfWork.SaveAsync();
        }
    }
}
