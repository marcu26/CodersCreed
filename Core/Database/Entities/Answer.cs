using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Entities
{
    public class Answer : BaseEntity
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
