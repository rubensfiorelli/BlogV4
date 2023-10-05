using BlogV4.Domain.DTOs.Input;
using BlogV4.Domain.DTOs.Output;

namespace BlogV4.Application.Repositories
{
    public interface IUserService
    {
        Task<List<UserOutputDto>> GetUsers(CancellationToken cancellationToken);
        Task<UserOutputDto> GetUserId(Guid userId, CancellationToken cancellationToken);
        Task<UserOutputDto> GetUserEmail(string email, CancellationToken cancellationToken);
        Task<UserOutputDto> Add(CreateUserDto model);
        Task<UserOutputDto> Update(Guid userId, CreateUserDto model);
        Task<UserOutputDto> Delete(Guid userId);
    }
}
