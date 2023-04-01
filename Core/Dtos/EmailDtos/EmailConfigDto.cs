using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISoft.Travel.Core.Dtos.EmailDtos
{
    public class EmailConfigDto
    {
        public string EmailHost { get; set; }
        public string EmailUserName { get; set; }
        public string EmailPassword { get; set; }
        public int Port { get; set; }


    }
}
