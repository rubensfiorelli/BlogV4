using BlogV4.Domain.Entities;

namespace BlogV4.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserId(Guid userId);
        Task<User> GetUserByEmail(string email);
        Task AddAsync(User user);
        Task<User> Update(Guid userId, User user);
        Task<User> Delete(Guid userId);
    }
}
