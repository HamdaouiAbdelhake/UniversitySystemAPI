namespace University.Core.DTOs;

public class CourseDTO
{
    public CourseDTO(string courseName, DateTime startDate, DateTime endDate, int classroomNumber)
    {
        CourseName = courseName;
        StartDate = startDate;
        EndDate = endDate;
        ClassroomNumber = classroomNumber;
    }

    public string CourseName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ClassroomNumber { get; set; } 
}