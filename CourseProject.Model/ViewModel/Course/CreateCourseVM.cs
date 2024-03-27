using static CourseProject.Model.Enum.DataType;

namespace CourseProject.Model.ViewModel.Course;

public class CreateCourseVM
{
    public string CourseName { get; set; }
    public Guid? Teacher { get; set; }
    public List<Guid> TagList { get; set; }
    public string Description { get; set; }
    public string Introduce { get; set; }
    public TypeOfPurchase TypeOfPurchase { get; set; }
    public decimal? Price { get; set; }
    public decimal? PriceAfterDiscount { get; set; }
    public TypeOfTerm TypeOfTerm { get; set; }
    public int DateMonthLearn { get; set; } = 0;
}
