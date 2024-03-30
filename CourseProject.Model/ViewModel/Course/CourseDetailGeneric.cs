namespace CourseProject.Model.ViewModel.Course;

public class CourseDetailGeneric
{
    public string ChapterName { get; set; }
    public Guid ChapterId { get; set; }
    public Guid LessionId { get; set; }
    public string LessionName { get; set; }
    public long TotalTimeLession { get; set; }
    public string VideoLink { get; set; }
    public string TotalHourTimeLession
    {
        get
        {
            int hours = (int)TotalTimeLession / 3600;
            int minutes = (int)TotalTimeLession % 3600 / 60;
            int seconds = (int)TotalTimeLession % 60;

            return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
        }
        private set { }
    }
    public string TestLink { get; set; }
    public string LessionLink { get; set; }
    public string Text { get; set; }
    public string AttachmentsLink { get; set; }
}
