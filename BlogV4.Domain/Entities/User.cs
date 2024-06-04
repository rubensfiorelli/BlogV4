using BlogV4.Domain.Primitives;
using System.Text.Json.Serialization;

namespace BlogV4.Domain.Entities
{
    public sealed class User : BaseEntity
    {

        public User(string name, string email, string passwordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;

        }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
            Slug = email.Replace("@", "-").Replace(".", "-");
        }

        public User(Guid id,
                    string name,
                    string email,
                    string passwordHash,
                    string slug,
                    string bio)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Slug = slug;
            Bio = bio;
            IsDeleted = false;

            Roles = [];
            Posts = [];
        }

        public Guid Id { get; init; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string PasswordHash { get; set; }
        public string Slug { get; private set; }
        public string Bio { get; private set; }
        public bool IsDeleted { get; private set; }

        public List<Post> Posts { get; set; }
        public List<Role> Roles { get; set; }


        public void Delete()
        {
            IsDeleted = true;
        }

        public void SetupUser(string name, string email)
        {
            Name = name;
            Email = email;
        }

    }
}