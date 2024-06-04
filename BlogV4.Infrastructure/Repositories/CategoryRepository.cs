using BlogV4.Data.DataContext;
using BlogV4.Domain.Entities;
using BlogV4.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogV4.Data.Repositories
{

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) => _context = context;
        
        public async Task AddAsync(Category model)
        {
            try
            {
                await _context.AddAsync(model);

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateException)
            {

                throw new Exception("Erro ao inserir Categoria");
            }

        }

        public async Task<Category> Delete(Guid categoryId)
        {
            try
            {
                var existing = await _context.Categories
                     .AsTracking()
                     .FirstOrDefaultAsync(x => x.Id.Equals(categoryId));

                if (existing is null)
                    return null;

                existing.Delete();
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return existing;
            }
            catch (DbUpdateException)
            {

                throw new Exception("Erro ao remover Categoria");
            }
           
        }

        public async Task<List<Category>> GetCategories()
        {
            try
            {
                var listCategories = await _context.Categories
               .Where(x => !x.IsDeleted)
               .ToListAsync();

                return listCategories;
            }
            catch (Exception)
            {
                throw new Exception("Erro desconhecido ao acessar banco de dados");
            }
                
        }

        public async Task<Category> GetCategoryId(Guid categoryId)
        {
            var existing = await _context.Categories
                .AsTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(categoryId));

            if (existing is null)
                return null;

            return existing;
        }

        public async Task<Category> Update(Category category)
        {
            try
            {
                var existing = await _context.Categories
                    .AsTracking()
                    .SingleOrDefaultAsync(x => x.Id.Equals(category.Id));

                if (existing is null)
                    return null;

                category.UpdatedAt = DateTimeOffset.UtcNow;
                category.CreatedAt = existing.CreatedAt;

                existing.SetupCategory(category.Name, category.Slug);

                _context.Update(existing);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return existing;
            }
            catch (DbUpdateException)
            {
                throw new Exception("Erro ao localizar Categoria");
            }
            
        }
    }
}
