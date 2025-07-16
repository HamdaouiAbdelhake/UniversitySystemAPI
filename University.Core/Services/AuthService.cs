using Microsoft.AspNetCore.Identity;
using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms;
using University.Core.Validations;
using University.Data.Entities.Identity;

namespace University.Core.Services;

public class AuthService : IAuthService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public AuthService(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }


    public async Task<UserDTO> Login(LoginForm form)
    {
        ArgumentNullException.ThrowIfNull(form);
        var validation = FormValidator.Validate(form);
        if (!validation.IsValid)
            throw new BusinessException(validation.Errors);

        var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, true, false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(form.Email);
            if (user == null)
                throw new NotFoundException($"Unable to find a user with email {form.Email}");
            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            };
        }

        if (result.IsLockedOut)
            throw new BusinessException("Account is locked");
        if (result.IsNotAllowed)
            throw new BusinessException("Account is not allowed to login");

        throw new BusinessException("Login Failed");
    }

    public async Task<UserDTO> Register(RegisterForm form)
    {
        ArgumentNullException.ThrowIfNull(form);
        var validation = FormValidator.Validate(form);
        if (!validation.IsValid)
            throw new BusinessException(validation.Errors);

        var userExists = await _userManager.FindByEmailAsync(form.Email);
        if (userExists != null)
            throw new BusinessException("Email Already Used By A User");

        User user = new User()
        {
            UserName = form.Email,
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email
        };
        var result = await _userManager.CreateAsync(user, form.Password);
        if (!result.Succeeded)
        {
            throw new BusinessException(result.Errors
                .GroupBy(e => e.Code)
                .ToDictionary(e => e.Key, g => g.Select(d => d.Description).ToList()));
        }

        if (!await _roleManager.RoleExistsAsync(form.Role.ToString()))
            await _roleManager.CreateAsync(new IdentityRole<int>() { Name = form.Role.ToString() });
        await _userManager.AddToRoleAsync(user, form.Role.ToString());

        return new UserDTO()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumber = user.PhoneNumber,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
        };

    }
}