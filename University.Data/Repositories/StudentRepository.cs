using University.Data.Contexts;
using University.Data.Entities;

namespace University.Data.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly UniversityDbContext _context;

    public StudentRepository(UniversityDbContext context)
    {
        _context = context;
    }

    public List<Student> GetAll()
    {
        return _context.Students.ToList();
    }

    public Student? GetById(int id)
    {
        return _context.Students.Find(id);
    }

    public void Add(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);
        _context.Add(student);
        _context.SaveChanges();
    }

    public void Update(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);
        _context.Update(student);
        _context.SaveChanges();
    }

    public void Delete(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);
        _context.Remove(student);
        _context.SaveChanges();
    }

    public bool EmailExists(string email)
    {
        return _context.Students.Any(t => t.Email.Equals(email));
    }
}