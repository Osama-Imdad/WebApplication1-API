using AspNetIdentity.Shared;
using DataAccessLayers;

namespace WebApplication1_API.Repository
{
    public interface IUserInterface
    {
        Task<UserMangerResponse> RegisterUser(RegisterModel registerModel);
        Task<UserMangerResponse> Login(loginModel loginModel);
    }
}
