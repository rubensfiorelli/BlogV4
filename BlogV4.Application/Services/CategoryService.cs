using BlogV4.Application.Repositories;
using BlogV4.Domain.DTOs.Input;
using BlogV4.Domain.DTOs.Output;
using BlogV4.Domain.Entities;
using BlogV4.Domain.Repositories;

namespace BlogV4.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository) => _repository = repository;

        public async Task<CategoryOutputDto> Add(CreateCategoryDto model)
        {
            
            var addEntity = (Category)model; 

            await _repository.AddAsync(addEntity);

            return (CategoryOutputDto)addEntity;

        }

        public async Task<CategoryOutputDto> Delete(Guid categoryId)
        {
            var existing = await _repository.Delete(categoryId);
            
            return existing;
        }

        public async Task<List<CategoryOutputDto>> GetCategories(CancellationToken cancellationToken)
        {
           var listtCategories = await _repository.GetCategories();

            return listtCategories
                .Select(x => new CategoryOutputDto(x.Name, x.Slug))
                .ToList();
        }

        public async Task<CategoryOutputDto> GetCategoryId(Guid categoryId, CancellationToken cancellationToken)
        {

            var existing = await _repository.GetCategoryId(categoryId);

            if (existing is null)
                return null;

            return (CategoryOutputDto)existing;

        }

        public async Task<CategoryOutputDto> Update(Guid categoryId, CreateCategoryDto model)
        {
            var existing = await _repository.GetCategoryId(categoryId);

            if (existing is null)
                return null;

            existing.SetupCategory(model.Name, model.Slug);

            await _repository.Update(categoryId, existing);

            return (CategoryOutputDto)existing;
          
            
        }
        
    }
}
