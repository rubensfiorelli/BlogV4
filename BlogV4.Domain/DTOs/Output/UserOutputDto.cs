using BlogV4.Domain.Entities;

namespace BlogV4.Domain.DTOs.Output
{
    public record UserOutputDto
    {
        public UserOutputDto(string name, string email, string password)
        {
            Name = name;
            Email = email;
            PasswordHash = password;
        }

        public string Name { get; init; }
        public string Email { get; init; }
        public string PasswordHash { get; init; }

        public static implicit operator UserOutputDto(User entity)
            => new UserOutputDto(entity.Name, entity.Email, entity.PasswordHash);

        public static implicit operator User(UserOutputDto dto)
            => new User(dto.Name, dto.Email, dto.PasswordHash);
    }
}
