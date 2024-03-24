using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Model.BaseEntity;

public partial class Chapter
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Mã khóa học")]
    public Guid? CourseId { get; set; }

    [Description("Tên chương")]
    public string ChapterName { get; set; }
    public virtual Course Course { get; set; }

    public virtual ICollection<Lession> Lessions { get; set; } = new List<Lession>();
}
