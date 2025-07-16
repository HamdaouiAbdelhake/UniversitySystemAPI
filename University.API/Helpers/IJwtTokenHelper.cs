using University.Core.DTOs;

namespace University.API.Helpers;

public interface IJwtTokenHelper
{
    string generateToken(UserDTO user);
}

