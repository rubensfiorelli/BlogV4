using BlogV4.Data.DataContext;
using BlogV4.Domain.Entities;
using BlogV4.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogV4.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) => _context = context;

        public async Task AddAsync(User user)
        {
            try
            {
                await _context.AddAsync(user);

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateException)
            {

                throw new Exception("Erro ao inserir Usuário");
            }
        }

        public async Task<User> Delete(Guid userId)
        {
            try
            {
                var existing = await _context.Users
                     .AsTracking()
                     .FirstOrDefaultAsync(x => x.Id.Equals(userId));

                if (existing is null)
                    return null;

                existing.Delete();
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return existing;
            }
            catch (DbUpdateException)
            {

                throw new Exception("Erro ao remover Usuário");
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var existing = await _context.Users
               .Include(x => x.Roles)
               .FirstOrDefaultAsync(x => x.Email.Equals(email));

            if (existing is null)
                return null;

            return existing;
        }

        public async Task<User> GetUserId(Guid userId)
        {
            var existing = await _context.Users
               .AsTracking()
               .SingleOrDefaultAsync(x => x.Id.Equals(userId));

            if (existing is null)
                return null;

            return existing;
        }

        public async Task<List<User>> GetUsers()
        {
            try
            {
                var listUsers = await _context.Users
               .Where(x => !x.IsDeleted)
               .ToListAsync();

                return listUsers;
            }
            catch (DbUpdateException)
            {
                throw new Exception("Erro desconhecido ao acessar banco de dados");
            }
        }

        public async Task<User> Update(Guid userId, User user)
        {
            try
            {
                var existing = await _context.Users
                    .AsTracking()
                    .SingleOrDefaultAsync(x => x.Id.Equals(user.Id));

                if (existing is null)
                    return null;

                user.UpdatedAt = DateTimeOffset.UtcNow;
                user.CreatedAt = existing.CreatedAt;

                existing.SetupUser(user.Name, user.Email);

                _context.Update(existing);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return existing;
            }
            catch (DbUpdateException)
            {
                throw new Exception("Erro ao atualizar User");
            }

        }
    }
}

