using System;

namespace UserManagement.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public  Person Person { get; set; }
        public  int PersonId { get; set; }
    }
}