﻿namespace CourseProject.Model.BaseEntity;

public class CourseGeneric
{
    public Guid Id { get; set; }
    public string CourseName { get; set; }
    public double? Rating { get; set; }
    public decimal? Price { get; set; }
    public decimal? PriceAfterDiscount { get; set; }
    public string ImgCourse { get; set; }
    public long? TotalTime { get; set; }
    public long? TotalLectures { get; set; }
    public long? TotalPerRating { get; set; }
    public string CourseCode { get; set; }
    public List<string> TagList
    {
        get
        {
            return TagString?.Split(',').ToList() ?? new List<string>();
        }
        private set { }
    }
    public string TagString { get; set; }
    public string ImgTeacher { get; set; }
    public string TeacherName { get; set; }
    public string TotalHourTime
    {
        get
        {
            int hours = (int)TotalTime / 3600;
            int minutes = ((int)TotalTime % 3600) / 60;
            int seconds = (int)TotalTime % 60;

            return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
        }
        private set { }
    }
    public DateTime ModifiedDate { get; set; }
    public string Description { get; set; }
    public string BenefitsOfCourse { get; set; }

}