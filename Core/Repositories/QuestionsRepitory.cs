using Core.Database.Context;
using Core.Database.Entities;
using Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class QuestionsRepitory : BaseRepository<Question>
    {
        public HackatonDbContext _DbContext { get; set; }
        public QuestionsRepitory(HackatonDbContext DbContext) : base(DbContext)
        {
            _DbContext = DbContext;
        }
    }
}
