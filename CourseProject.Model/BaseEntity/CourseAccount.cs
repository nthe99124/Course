using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static CourseProject.Model.Enum.DataType;

namespace CourseProject.Model.BaseEntity;

/// <summary>
/// Bảng lưu liên kết nhiều nhiều giữa user và khóa học
/// </summary>
public partial class CourseAccount
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Id của khóa học")]
    public Guid CourseId { get; set; }

    [Description("Id của user")]
    public Guid AccountId { get; set; }
    [Description("Ngày tạo")]
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    public CourseAccountStatus Status { get; set; }

    public virtual Account Account { get; set; }

    public virtual Course Course { get; set; }
}
