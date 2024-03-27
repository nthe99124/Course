using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static CourseProject.Model.Enum.DataType;

namespace CourseProject.Model.BaseEntity;

public partial class Course
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Tên khóa học")]
    public string CourseName { get; set; }

    [Description("Số sao đánh giá")]
    public double? Rating { get; set; }

    [Description("Người tạo")]
    public Guid? CreatedBy { get; set; }

    [Description("Ngày tạo")]
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    [Description("Ngôn ngữ")]
    public string Language { get; set; }

    [Description("Người thay đổi")]
    public Guid? ModifiedBy { get; set; }

    [Description("Ngày thay đổi")]
    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

    [Description("Giá")]
    public decimal Price { get; set; } = 0;

    [Description("Giá sau khuyến mãi")]
    public decimal? PriceAfterDiscount { get; set; } = 0;

    [Description("Ảnh khóa học")]
    public string ImgCourse { get; set; }

    [Description("Tổng thời gian học")]
    public long TotalTime { get; set; } = 0;

    [Description("Tổng bài học")]
    public long TotalLectures { get; set; } = 0;

    [Description("Tổng số người đánh giá")]
    public long TotalPerRating { get; set; } = 0;

    [Description("Mã khóa học")]
    public string CourseCode { get; set; }

    [Description("Mô tả")]
    public string Description { get; set; }

    [Description("Giảng viên")]
    public Guid? Teacher { get; set; }

    [Description("Video mô tả")]
    public string VideoDescription { get; set; }

    [Description("Giới thiệu")]
    public string Introduce { get; set; }

    [Description("Loại thanh toán")]
    public TypeOfPurchase TypeOfPurchase { get; set; }

    [Description("Thời hạn học")]
    public TypeOfTerm TypeOfTerm { get; set; }

    [Description("Số ngày/ tháng học")]
    public int DateMonthLearn { get; set; } = 0;

    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();

    public virtual ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();

    public virtual Account CreatedByNavigation { get; set; }
    public virtual ICollection<CourseAccount> CourseAccountCourses { get; set; } = new List<CourseAccount>();
}
