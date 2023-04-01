﻿using Core.Database.Context;
using Core.Database.Entities;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class CourseSectionRepository : BaseRepository<CourseSection>
    {
        public HackatonDbContext _DbContext { get; set; }
        public CourseSectionRepository(HackatonDbContext DbContext) : base(DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<bool> DoesNameExist(string name)
        {
            return await _DbContext.CourseSections.AnyAsync(c => c.Name == name && !c.IsDeleted);
        }

    }
}
