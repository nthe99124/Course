namespace CourseProject.Model.ViewModel.Course
{
    public class CourseDetailVM
    {
        public CourseGeneric CourseMaster { get; set; }
        public List<ChapterDetail> CourseDetailList { get; set; }
    }

    public class ChapterDetail
    {
        public Guid ChapterId { get; set; }
        public string ChapterName { get; set; }
        public List<LessionDetail> LessionDetailList { get; set; }

    }

    public class LessionDetail
    {
        public Guid LessionId { get; set; }
        public string LessionName { get; set; }
        public string TotalHourTimeLession { get; set; }
        public string VideoLink { get; set; }
    }
}
