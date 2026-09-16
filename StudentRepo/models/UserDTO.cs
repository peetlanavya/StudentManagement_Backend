namespace StudentRepo.models
{
    public class UserDTO
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }

        // frontend may send it, but backend ignores it
        public string? Role { get; set; }
    }
}