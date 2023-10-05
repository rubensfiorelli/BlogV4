using BlogV4.Domain.Primitives;

namespace BlogV4.Domain.Entities
{
    public sealed class Tag : BaseEntity
    {
        public Tag(string name, string slug)
        {
            Name = name;
            Slug = slug;

            Posts = new List<Post>();
        }

        public Guid Id { get; init; }
        public string Name { get; private set; }
        public string Slug { get; private set; }

        public List<Post> Posts { get; set; }
    }
}