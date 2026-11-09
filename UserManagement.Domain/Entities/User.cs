
using System;

namespace UserManagement.Domain.Entities
{
    public class User
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; } = "User";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
