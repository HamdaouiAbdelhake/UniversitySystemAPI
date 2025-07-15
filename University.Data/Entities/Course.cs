namespace University.Data.Entities;

public class Course
{
    public int Id { get; set; }
    public string CourseName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ClassroomNumber { get; set; }

    public Course(string courseName, DateTime startDate, DateTime endDate, int classroomNumber)
    {
        CourseName = courseName;
        StartDate = startDate;
        EndDate = endDate;
        ClassroomNumber = classroomNumber;
    }
}