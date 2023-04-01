using Core.Dtos.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Quiz
{
    public class UpdateQuizDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Points { get; set; }
        public List<CreateQuestionDto>? Questions { get; set; }
    }
}
