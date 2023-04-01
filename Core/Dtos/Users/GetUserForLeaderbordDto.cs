using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Users
{
    public class GetUserForLeaderbordDto
    {
        public string Username { get; set; }
        public string Function { get; set; }
        public string Image { get; set; }
        public int Experience { get; set; }
    }
}
