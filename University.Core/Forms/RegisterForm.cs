using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace University.Core.Forms;

public class RegisterForm
{
    
    public enum UserRole
    {
        Student,
        Teacher,
    }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
  
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    [Length(minimumLength:8, maximumLength:255)]
    public string Password { get; set; }
    
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserRole Role { get; set; }
}