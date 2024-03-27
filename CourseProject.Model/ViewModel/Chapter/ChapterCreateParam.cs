using static CourseProject.Model.Enum.DataType;

namespace CourseProject.Model.ViewModel.Course;

public class ChapterCreateParam
{
    public string ChapterName { get; set; }
    public Guid CourseId { get; set; }
}
