﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
