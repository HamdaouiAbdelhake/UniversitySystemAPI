using University.Core.DTOs;
using University.Core.Forms;

namespace University.Core.Services;

public interface ICourseService
{
    List<CourseDTO> GetAll();

    CourseDTO GetById(int id);

    void Create(CreateCourseForm form);

    void Update(int id, UpdateCourseForm form);

    void Delete(int id);
}