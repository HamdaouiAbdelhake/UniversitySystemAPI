using University.Core.DTOs;
using University.Core.Forms;

namespace University.Core.Services;

public interface IAuthService
{
    Task<UserDTO> Login(LoginForm form);
    Task<UserDTO> Register(RegisterForm form);
}