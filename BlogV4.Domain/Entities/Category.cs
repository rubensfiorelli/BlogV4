using BlogV4.Domain.Primitives;

namespace BlogV4.Domain.Entities
{
    public sealed class Category : BaseEntity
    {
        public Category(string name, string slug)
        {
            Name = name;
            Slug = slug;
            IsDeleted = false;

            Posts = new List<Post>();
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Slug { get; private set; }
        public bool IsDeleted { get; set; }

        public List<Post> Posts { get; set; }

        public void SetupCategory(string name, string slug)
        {
            Name = name;
            Slug = slug;
        }

        public void Delete() => IsDeleted = true;



    }
}