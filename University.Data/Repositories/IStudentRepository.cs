using University.Data.Entities;

namespace University.Data.Repositories;

//Methods: GetAll, GetById, Add, Update, Delete
public interface IStudentRepository
{
    List<Student> GetAll();

    Student? GetById(int id);

    void Add(Student student);
    
    
    void Update(Student student);

    void Delete(Student student);

    bool EmailExists(string email);
}