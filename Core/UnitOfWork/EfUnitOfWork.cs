using Core.Database.Context;
using Core.Repositories;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UnitOfWork
{
    public class EfUnitOfWork
    {
        private HackatonDbContext _context;

        public RolesRepository _rolesRepository { get; set; }
        public UsersRepository _usersRepository { get; set; }
        public UserRoleRepository _userRoleRepository { get; set; }
        public CoursesRepository _coursesRepository { get; set; }
        public AnswersRepository _answersRepository { get; set; }
        public QuestionsRepitory _questionsRepitory { get; set; }
        public RewardsRepository _rewardsRepository { get; set; }
 
        public EfUnitOfWork(
            HackatonDbContext context,
            RolesRepository rolesRepository,
            UserRoleRepository userRolesRepository,
            UsersRepository usersRepository,
            CoursesRepository coursesRepository,
            AnswersRepository answersRepository,
            QuestionsRepitory questionsRepitory
            CoursesRepository coursesRepository,
            RewardsRepository rewardsRepository
            )
        {
            _context = context;
            _rolesRepository = rolesRepository;
            _usersRepository = usersRepository;
            _userRoleRepository = userRolesRepository;
            _coursesRepository = coursesRepository;
            _answersRepository = answersRepository;
            _questionsRepitory = questionsRepitory;
            _rewardsRepository = rewardsRepository;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
