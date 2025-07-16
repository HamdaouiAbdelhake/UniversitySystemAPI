using University.Core.Forms;

namespace University.Core.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }

    public override string ToString()
    {
        return
            $"{nameof(Id)}: {Id}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Email)}: {Email}, {nameof(EmailConfirmed)}: {EmailConfirmed}, {nameof(PhoneNumber)}: {PhoneNumber}, {nameof(PhoneNumberConfirmed)}: {PhoneNumberConfirmed}, {nameof(Role)}: {Role}";
    }

    public string LastName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    
    public RegisterForm.UserRole Role { get; set; }
}