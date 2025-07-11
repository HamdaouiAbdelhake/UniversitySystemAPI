namespace University.Data.Entities;

public class Student
{
    public Student(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}