using System.ComponentModel.DataAnnotations;

namespace University.Core.Forms;

public class LoginForm
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [Length(minimumLength:8, maximumLength:255)]
    public string Password { get; set; }
}