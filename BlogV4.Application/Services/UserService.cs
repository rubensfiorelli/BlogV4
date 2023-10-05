using BlogV4.Application.Repositories;
using BlogV4.Domain.DTOs.Input;
using BlogV4.Domain.DTOs.Output;
using BlogV4.Domain.Entities;
using BlogV4.Domain.Repositories;
using SecureIdentity.Password;

namespace BlogV4.Application.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<UserOutputDto> Add(CreateUserDto userDto)
        {
            var addEntity = (User)userDto;

            var password = userDto.Password;

            addEntity.PasswordHash = password;

            addEntity.PasswordHash = PasswordHasher.Hash(password);

            await _userRepository.AddAsync(addEntity);

            return (UserOutputDto)addEntity;

        }

        public async Task<UserOutputDto> Delete(Guid userId)
        {
            var existing = await _userRepository.Delete(userId);

            return existing;
        }

        public async Task<UserOutputDto> GetUserEmail(string email, CancellationToken cancellationToken)
        {
            var existing = await _userRepository.GetUserByEmail(email);

            if (existing is not null)
                return (UserOutputDto)existing;

            return null;
        }

        public async Task<UserOutputDto> GetUserId(Guid userId, CancellationToken cancellationToken)
        {

            var existing = await _userRepository.GetUserId(userId);

            if (existing is null)
                return null;

            return (UserOutputDto)existing; ;
        }

        public async Task<List<UserOutputDto>> GetUsers(CancellationToken cancellationToken)
        {
            var listUsers = await _userRepository.GetUsers();

            return listUsers
                .Select(x => new UserOutputDto(x.Name, x.Email, x.PasswordHash))
                .ToList();
        }

        public async Task<UserOutputDto> Update(Guid userId, CreateUserDto model)
        {
            var existing = await _userRepository.GetUserId(userId);

            if (existing is null)
                return null;

            existing.SetupUser(model.Name, model.Email);

            await _userRepository.Update(userId, existing);

            return (UserOutputDto)existing;
        }
    }
}
