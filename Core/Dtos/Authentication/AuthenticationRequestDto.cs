﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Authentication
{
    public class AuthenticationRequestDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
