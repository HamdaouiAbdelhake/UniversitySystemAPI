using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms;
using University.Core.Validations;
using University.Data.Entities;
using University.Data.Repositories;

namespace University.Core.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _repository;

    public CourseService(ICourseRepository repository)
    {
        _repository = repository;
    }

    public List<CourseDTO> GetAll()
    {
        return _repository.GetAll().Select(t=>new CourseDTO(t.CourseName,t.StartDate,t.EndDate,t.ClassroomNumber)).ToList();
    }

    public CourseDTO GetById(int id)
    {
        Course? course = _repository.GetById(id);
        if (course == null)
            throw new NotFoundException("Course Doesn't exist");
        return new CourseDTO(course.CourseName,course.StartDate,course.EndDate,course.ClassroomNumber);
    }

    public void Create(CreateCourseForm form)
    {
        ArgumentNullException.ThrowIfNull(form);

        var validation = FormValidator.Validate(form);
        if (!validation.IsValid)
        {
            throw new BusinessException(validation.Errors);
        }

        if (_repository.NameExists(form.CourseName))
        {
            throw new BusinessException("Class Already Exists");
        }
        if (_repository.ClassroomNumberExists(form.ClassroomNumber.Value))
        {
            throw new BusinessException("Classroom Already Full");
        }
         
        _repository.Add(new Course(form.CourseName,form.StartDate.Value,form.EndDate.Value,form.ClassroomNumber.Value));
    }

    public void Update(int id, UpdateCourseForm form)
    {
        ArgumentNullException.ThrowIfNull(form);
        var validation = FormValidator.Validate(form);
        if (!validation.IsValid)
        {
            throw new BusinessException(validation.Errors);
        }
        Course? course = _repository.GetById(id);
        if (course == null)
            throw new NotFoundException("Course Doesn't exist");
        
        
        if (_repository.ClassroomNumberExists(form.ClassroomNumber.Value))
        {
            throw new BusinessException("Classroom Already Full");
        }

        course.StartDate = form.StartDate.Value;
        course.EndDate = form.EndDate.Value;
        course.ClassroomNumber = form.ClassroomNumber.Value;
        _repository.Update(course);
    }

    public void Delete(int id)
    {
        Course? course = _repository.GetById(id);
        if (course == null)
            throw new NotFoundException("Course Doesn't exist");
        
        _repository.Delete(course);
    }
}