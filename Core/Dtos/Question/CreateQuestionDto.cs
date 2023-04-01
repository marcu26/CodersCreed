using Core.Dtos.Answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Question
{
    public class CreateQuestionDto
    {
        public string Content { get; set; }
        public List<CreateAnswerDto> Answers { get; set; }
    }
}
