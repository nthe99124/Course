using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Model.BaseEntity;

public partial class Lession
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Mã chương")]
    public Guid? ChapterId { get; set; }

    [Description("Tên bài học")]
    public string LessionName { get; set; }

    [Description("Video bài học")]
    public string VideoLink { get; set; }

    [Description("Link bài test")]
    public string TestLink { get; set; }

    [Description("Link bài học")]
    public string LessionLink { get; set; }

    [Description("Text bài học")]
    public string Text { get; set; }

    [Description("Tài liệu đính kèm")]
    public string AttachmentsLink { get; set; }
    [Description("Tổng thời gian học của bài học")]
    public long TotalTimeLession { get; set; }

    public virtual Chapter Chapter { get; set; }
}
