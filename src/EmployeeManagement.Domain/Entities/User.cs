using EmployeeManagement.Domain.Enums;

namespace EmployeeManagement.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        // Stored hashed password (for demo purposes we use SHA256 hashing)
        public string PasswordHash { get; set; }

        public Role Role { get; set; }
    }
}
