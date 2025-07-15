using University.Data.Contexts;
using University.Data.Entities;

namespace University.Data.Repositories;

public class CourseRepository : ICourseRepository
{
    
    private readonly UniversityDbContext _context;

    public CourseRepository(UniversityDbContext context)
    {
        _context = context;
    }
    
    public List<Course> GetAll()
    {
        return _context.Courses.ToList();
    }

    public Course? GetById(int id)
    {
        return _context.Courses.Find(id);
    }

    public void Add(Course course)
    {
        ArgumentNullException.ThrowIfNull(course);
        _context.Add(course);
        _context.SaveChanges();
    }

    public void Update(Course course)
    {
        ArgumentNullException.ThrowIfNull(course);
        _context.Update(course);
        _context.SaveChanges();
    }

    public void Delete(Course course)
    {
        ArgumentNullException.ThrowIfNull(course);
        _context.Remove(course);
        _context.SaveChanges();
    }

    public bool NameExists(string name)
    {
        return _context.Courses.Any(t => t.CourseName.Equals(name));
    }
    
    public bool ClassroomNumberExists(int classroomNumber)
    {
        return _context.Courses.Any(t => t.ClassroomNumber == classroomNumber);
    }
}