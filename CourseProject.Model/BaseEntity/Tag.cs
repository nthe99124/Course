using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Model.BaseEntity;

public partial class Tag
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Tên thẻ")]
    public string TagName { get; set; }

    public virtual ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();
}
