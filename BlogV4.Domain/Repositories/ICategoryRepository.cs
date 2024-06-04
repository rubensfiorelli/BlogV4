using BlogV4.Domain.Entities;

namespace BlogV4.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryId(Guid categoryId);
        Task AddAsync(Category model);
        Task<Category> Update(Category category);
        Task<Category> Delete(Guid categoryId);
    }
}
