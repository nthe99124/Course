namespace CourseProject.Model.BaseEntity;

public class MyCourseVM
{
    public Guid Id { get; set; }
    public string CourseName { get; set; }
    public double? Rating { get; set; }
    public string ImgCourse { get; set; }
    public long? TotalPerRating { get; set; }
    public string CourseCode { get; set; }
}
