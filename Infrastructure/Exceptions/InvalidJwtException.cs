using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class InvalidJwtException : Exception
    {
        public InvalidJwtException(string message) : base(message)
        {

        }
    }
}
