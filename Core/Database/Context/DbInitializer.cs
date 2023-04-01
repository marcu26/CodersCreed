using Core.Database.Entities;
using Core.Database.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Context
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<Role>().HasData(
                   new Role() { Id = 1, Title = RolesEnum.Administrator },
                   new Role() { Id = 2, Title = RolesEnum.SimpleUser }
            );

            modelBuilder.Entity<User>().HasData(
                   new User()
                   {
                       Id = 1,
                       Email = "admin",
                       Username = "admin",
                       Password = "YWRtaW4="
                   }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole()
                {
                    RoleId = 1,
                    UserId = 1
                }
            );
        }
    }
}
