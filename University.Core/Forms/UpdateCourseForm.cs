using System.ComponentModel.DataAnnotations;

namespace University.Core.Forms;

public class UpdateCourseForm
{
    [Required]
    public DateTime? StartDate { get; set; }
    [Required]
    public DateTime? EndDate { get; set; }
    [Required]
    public int? ClassroomNumber { get; set; } 
}