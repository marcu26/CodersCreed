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
    public class QuestionsRepsitory : BaseRepository<Question>
    {
        public HackatonDbContext _DbContext { get; set; }
        public QuestionsRepsitory(HackatonDbContext DbContext) : base(DbContext)
        {
            _DbContext = DbContext;
        }


    }
}
