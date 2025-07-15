using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms;
using University.Core.Validations;
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
            throw new NotFoundException("Student Doesn't exist");
        return new StudentDTO(student.Id,student.Name,student.Email);
    }

    public void Create(CreateStudentForm form)
    {
        ArgumentNullException.ThrowIfNull(form);

        var validation = FormValidator.Validate(form);
        if (!validation.IsValid)
        {
            throw new BusinessException(validation.Errors);
        }

        if (_repository.EmailExists(form.Email))
        {
            throw new BusinessException("Email Already Exists");
        }
         
        _repository.Add(new Student(form.Name,form.Email));
    }

    public void Update(int id, UpdateStudentForm form)
    {
        ArgumentNullException.ThrowIfNull(form);
        var validation = FormValidator.Validate(form);
        if (!validation.IsValid)
        {
            throw new BusinessException(validation.Errors);
        }
        Student? student = _repository.GetById(id);
        if (student == null)
            throw new NotFoundException("Student Doesn't exist");

        student.Name = form.Name;
        _repository.Update(student);
    }

    public void Delete(int id)
    {
        Student? student = _repository.GetById(id);
        if (student == null)
            throw new NotFoundException("Student Doesn't exist");
        
        _repository.Delete(student);
    }
}