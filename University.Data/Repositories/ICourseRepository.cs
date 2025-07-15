using University.Data.Entities;

namespace University.Data.Repositories;

public interface ICourseRepository
{
    List<Course> GetAll();

    Course? GetById(int id);

    void Add(Course course);
    
    void Update(Course course);

    void Delete(Course course);

    bool NameExists(string name);

    public bool ClassroomNumberExists(int classroomNumber);
}