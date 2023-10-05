using BlogV4.Domain.Primitives;

namespace BlogV4.Domain.Entities
{
    public sealed class Post : BaseEntity
    {
        public Post(string title, string summary, string body, string slug)
        {
            Title = title;
            Summary = summary;
            Body = body;
            Slug = slug;

            Tags = new List<Tag>();
        }

        public Guid Id { get; init; }
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string Body { get; private set; }
        public string Slug { get; private set; }
        public Category Category { get; private set; }
        public User Author { get; private set; }

        public List<Tag> Tags { get; set; }
    }
}