using BlogV4.Domain.DTOs.Input;
using BlogV4.Domain.DTOs.Output;

namespace BlogV4.Application.Repositories
{
    public interface ICategoryService
    {
        Task<List<CategoryOutputDto>> GetCategories(CancellationToken cancellationToken);
        Task<CategoryOutputDto> GetCategoryId(Guid categoryId, CancellationToken cancellationToken);
        Task<CategoryOutputDto> Add(CreateCategoryDto model);
        Task<CategoryOutputDto> Update(Guid categoryId, CreateCategoryDto model);
        Task<CategoryOutputDto> Delete(Guid categoryId);
    }
}
