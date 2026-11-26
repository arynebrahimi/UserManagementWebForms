namespace UserManagement.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public PersonDto Person { get; set; }
    }

    public class PersonDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class RegisterUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserSearchDto
    {
        public string Keyword { get; set; }
        public int PageNumber { get; set; }
    }
}
