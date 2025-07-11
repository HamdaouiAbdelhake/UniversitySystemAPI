namespace University.Core.DTOs;

public class StudentDTO
{
    public StudentDTO(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}