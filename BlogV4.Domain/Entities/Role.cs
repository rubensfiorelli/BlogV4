using BlogV4.Domain.Primitives;

namespace BlogV4.Domain.Entities
{
    public sealed class Role : BaseEntity
    {
        public Role(string name, string slug)
        {
            Name = name;
            Slug = slug;

            Users = new List<User>();
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Slug { get; private set; }

        public List<User> Users { get; set; }
    }
}