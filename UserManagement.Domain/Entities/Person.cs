namespace UserManagement.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public virtual User User { get; set; }
    }
}