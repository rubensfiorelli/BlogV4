using BlogV4.Domain.Entities;

namespace BlogV4.Domain.DTOs.Input
{
    public record CreateCategoryDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }

        public static implicit operator Category(CreateCategoryDto dto)
           => new(dto.Name, dto.Slug);


    }
}
