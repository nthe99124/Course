using CourseProject.API.Common.Repository;
using CourseProject.API.Repositories.Base;
using CourseProject.Model.BaseEntity;
using CourseProject.Model.DTO;
using CourseProject.Model.ViewModel.Course;
using System.Linq.Expressions;

namespace CourseProject.API.Repositories
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<IEnumerable<CourseGeneric>> GetCourseByCondition(int? courseNumber, List<SortedPaging> sortedList, Expression<Func<CourseGeneric, bool>> predicateFilter = null);
        IEnumerable<MyCourseVM> GetListCourseByUser(Guid accId);
        IEnumerable<CourseDetailGeneric> GetDetailCourse(Guid courseId);
    }
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(CourseContext context) : base(context)
        {

        }

        /// <summary>
        /// Hàm xử lý lấy tất cả khóa học user đã đăng ký
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public IEnumerable<MyCourseVM> GetListCourseByUser(Guid accId)
        {
            var myCourse = from c in _context.Courses
                            join ca in _context.CourseAccounts on c.Id equals ca.CourseId
                            where ca.CourseId == accId
                            select new MyCourseVM
                            {
                                Id = c.Id,
                                CourseName = c.CourseName,
                                CourseCode = c.CourseCode,
                                Rating = c.Rating,
                                ImgCourse = c.ImgCourse,
                                TotalPerRating = c.TotalPerRating,
                            };
            return myCourse.ToList();
        }

        /// <summary>
        /// Hàm xử lý lấy top khóa học theo số lượng vào và điều kiện sort
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CourseGeneric>> GetCourseByCondition(int? courseNumber, List<SortedPaging> sortedList, Expression<Func<CourseGeneric, bool>> predicateFilter = null)
        {
            var courseGeneric = ExecuteStoredProcedureObject<CourseGeneric>("GetAllCourseNotCondition").AsQueryable();
            if (predicateFilter != null)
            {
                courseGeneric = courseGeneric.Where(predicateFilter);
            }
            var result = await GetDataBySorted(courseGeneric, sortedList);
            
            if (result != null && result.Count() > 0 && courseNumber != null && courseNumber > 0)
            {
                result = result.Take((int)courseNumber);
            }
            return result.ToList();
        }

        /// <summary>
        /// Hàm xử lý lấy chi tiết khóa học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CourseDetailGeneric> GetDetailCourse(Guid courseId)
        {
            var courseDetailGeneric = from c in _context.Chapters
                                join l in _context.Lessions on c.Id equals l.ChapterId into LessionGroup
                                from l2 in LessionGroup
                                where c.CourseId == courseId
                                select new CourseDetailGeneric
                                {
                                    ChapterId = c.Id,
                                    LessionId = l2.Id,
                                    LessionName = l2.LessionName,
                                    TotalTimeLession = l2.TotalTimeLession,
                                    ChapterName = c.ChapterName,
                                    VideoLink = l2.VideoLink,
                                    TestLink = l2.TestLink,
                                    Text = l2.Text,
                                    AttachmentsLink = l2.AttachmentsLink,
                                };
            return courseDetailGeneric.ToList();

        }
    }
}
