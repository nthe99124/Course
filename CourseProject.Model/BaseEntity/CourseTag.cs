using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Model.BaseEntity;

public partial class CourseTag
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Mã khóa học")]
    public Guid CourseId { get; set; }

    [Description("Mã tag")]
    public Guid? TagId { get; set; }

    public virtual Course Course { get; set; }

    public virtual Tag Tag { get; set; }
}
