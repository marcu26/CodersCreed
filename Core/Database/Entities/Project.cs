﻿using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public List<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
        public List<Category> Categories { get; set; }
        public List<TaskToDo> Tasks { get; set; }

    }
}
