using Infrastructure.Base;

namespace Core.Database.Entities
{
    public class Role: BaseEntity
    {
        public string Title { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
