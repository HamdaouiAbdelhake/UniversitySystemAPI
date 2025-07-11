using University.Core.DTOs;
using University.Core.Forms;
using University.Data.Entities;

namespace University.Core.Services;

public interface IStudentService
{
    List<StudentDTO> GetAll();

    StudentDTO GetById(int id);

    void Create(CreateStudentForm form);

    void Update(int id, UpdateStudentForm form);

    void Delete(int id);
}