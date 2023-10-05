using BlogV4.Domain.Entities;

namespace BlogV4.Domain.DTOs.Output
{
    public record CategoryOutputDto
    {
        public CategoryOutputDto(string name, string slug)
        {
            Name = name;
            Slug = slug;
        }

        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Slug { get; init; }

        public static implicit operator CategoryOutputDto(Category entity)
            => new(entity.Name, entity.Slug);


    }
}
