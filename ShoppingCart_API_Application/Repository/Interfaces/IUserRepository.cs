using ShoppingCart_API_Application.Models;
using ShoppingCart_API_Application.Models.DTO.LoginRequest;

namespace ShoppingCart_API_Application.Repository.Interfaces
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
