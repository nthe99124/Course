using CourseProject.Model.DTO;
using static CourseProject.Model.Enum.DataType;

namespace CourseProject.Model.ViewModel.Course;

public class SearchCourseParam
{
    /// <summary>
    /// Giá
    /// </summary>
    public TypeOfPurchase? TypeOfPurchase { get; set; }
    /// <summary>
    /// Cấp độ
    /// </summary>
    public Guid? TagId { get; set; }

    /// <summary>
    /// Xếp hạng
    /// </summary>
    public double? Rating { get; set; }

    /// <summary>
    /// Trường xác định sort theo
    /// </summary>
    public SortedPaging SortField { get; set; }
}
