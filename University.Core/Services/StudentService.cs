using University.Core.DTOs;
using University.Core.Forms;
using University.Data.Entities;
using University.Data.Repositories;


namespace University.Core.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public List<StudentDTO> GetAll()
    {
        return _repository.GetAll().Select(t=>new StudentDTO(t.Id,t.Name,t.Email)).ToList();
    }

    public StudentDTO GetById(int id)
    {
        Student? student = _repository.GetById(id);
        if (student == null)
            throw new KeyNotFoundException("Student not found");
        return new StudentDTO(student.Id,student.Name,student.Email);
    }

    public void Create(CreateStudentForm form)
    {
        ArgumentNullException.ThrowIfNull(form);
        if (string.IsNullOrEmpty(form.Name))
        {
            throw new ArgumentException("Name Can't Be Empty");
        }
        if (string.IsNullOrEmpty(form.Email))
        {
            throw new ArgumentException("Email Can't Be Empty");
        }
        if (!Helper.Helper.EmailIsValid(form.Email))
            throw new InvalidDataException("Email Is Not Valid");

        if (_repository.EmailExists(form.Email))
            throw new InvalidDataException("Email Already Exists");
         
        _repository.Add(new Student(form.Name,form.Email));
    }

    public void Update(int id, UpdateStudentForm form)
    {
        {
            ArgumentNullException.ThrowIfNull(form);
            if (string.IsNullOrEmpty(form.Name))
            {
                throw new ArgumentException("Name Can't Be Empty");
            }
            
            Student? student = _repository.GetById(id);
            if (student == null)
                throw new KeyNotFoundException("Student Not Found");

            student.Name = form.Name;
            _repository.Update(student);
        }
    }

    public void Delete(int id)
    {
        Student? student = _repository.GetById(id);
        if (student == null)
            throw new KeyNotFoundException("Student Not Found");
        
        _repository.Delete(student);
    }
}