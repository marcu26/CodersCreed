using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Base
{
  
    public class BaseController: ControllerBase
    {
        protected int GetUserId()
        {
            string strUserId = User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            try
            {
                var userId = int.Parse(strUserId);
                return userId;
            }
            catch
            {
                throw new InvalidJwtException("Invalid jwt.");
            }
        }
    }
}
