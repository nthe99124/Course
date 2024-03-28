using Microsoft.AspNetCore.Components.Forms;

namespace CourseProject.Model.ViewModel.Course;

public class LessionEditParam
{
    public Guid Id { get; set; }
    public string LessionName { get; set; }
    public string VideoLink { get; set; }
    public string TestLink { get; set; }
    public string LessionLink { get; set; }
    public string Text { get; set; }
    public string AttachmentsLink { get; set; }
    public long TotalTimeLession { get; set; }
    public Guid CourseId { get; set; }
}
