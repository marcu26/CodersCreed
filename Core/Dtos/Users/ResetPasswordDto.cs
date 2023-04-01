using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Users
{
    public class ResetPasswordDto
    {
        public string ResetPasswordCode { get; set; }
        public string Password { get; set; }
    }
}
