using System;
using System.Collections.Generic;
using System.Linq;
using static CourseProject.Model.Enum.DataType;

namespace CourseProject.Model.ViewModel.Course
{
    public class CourseGeneric
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public double? Rating { get; set; } = 0;
        public decimal? Price { get; set; } = 0;
        public decimal? PriceAfterDiscount { get; set; }
        public string ImgCourse { get; set; }
        public long? TotalTime { get; set; } = 0;
        public long? TotalLectures { get; set; }
        public long? TotalPerRating { get; set; } = 0;
        public string CourseCode { get; set; }
        public List<string> TagList => TagString?.Split(',').ToList() ?? new List<string>();

        public List<Guid> TagIdList => TagId?.Split(',').Select(Guid.Parse).ToList() ?? new List<Guid>();

        private string _tagId;
        public string TagId
        {
            get { return _tagId; }
            set { _tagId = value; }
        }

        public string TagString { get; set; }
        public string ImgTeacher { get; set; }
        public string TeacherName { get; set; }

        public string TotalHourTime
        {
            get
            {
                int hours = (int)(TotalTime ?? 0) / 3600;
                int minutes = (int)(TotalTime ?? 0) % 3600 / 60;
                int seconds = (int)(TotalTime ?? 0) % 60;

                return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
            }
        }

        public DateTime? ModifiedDate { get; set; }
        public string Description { get; set; }
        public int? CourseOfTeacher { get; set; }
        public TypeOfPurchase? TypeOfPurchase { get; set; }
    }
}
