using System.ComponentModel.DataAnnotations;
using University.Core.Validations;
using University.Data.Repositories;

namespace University.Core.Forms;

public class CreateCourseForm
{
    
    private ICourseRepository _courseRepository;

    public void SetCreateCourseForm(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }
    

    [Required]
    public string CourseName { get; set; }
    
    [Required]
    public DateTime? StartDate { get; set; }
    
    [Required]
    public DateTime? EndDate { get; set; }
    
    [Required]  
    [Range(minimum:1,maximum:100)]
    public int? ClassroomNumber { get; set; }
    
}