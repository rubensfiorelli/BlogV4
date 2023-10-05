using BlogV4.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlogV4.Domain.DTOs.Input
{
    public record CreateUserDto
    {
        public CreateUserDto(string name, string email)
        {
            Name = name;
            Email = email;

        }
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }
        public string Password { get; set; }

        public static implicit operator User(CreateUserDto dto)
            => new(dto.Name, dto.Email);


    }
}
