namespace BlogV4.Domain.DTOs.Input
{
    public record class CreateLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
