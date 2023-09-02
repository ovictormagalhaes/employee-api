using Auth.API.Application.Inputs;
using Auth.API.Domain;
using Auth.API.Infrastructure.Repositories;
using Auth.API.Infrastructure.Securities;

namespace Auth.API.Application.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(CreateUserInput input);
        Task<User?> LoginAsync(LoginInput input);
    }
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User?> LoginAsync(LoginInput input)
        {
            var passwordEncrypted = Cryptography.GenerateMD5(input.Password!);
            var user = _userRepository.FirstOrDefaultByEmailAndPasswordAsync(input.Email!, passwordEncrypted);
            return user;
        }

        public async Task<User> RegisterAsync(CreateUserInput input)
        {
            var passwordEncrypted = Cryptography.GenerateMD5(input.Password!);

            var user = new User(input.Name!, input.Email!, passwordEncrypted);

            await _userRepository.CreateAsync(user);

            return user;
        }
    }
}