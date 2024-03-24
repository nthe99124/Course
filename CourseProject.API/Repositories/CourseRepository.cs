using CourseProject.API.Common.Repository;
using CourseProject.API.Repositories.Base;
using CourseProject.Model.BaseEntity;
using CourseProject.Model.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.API.Repositories
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<IEnumerable<CourseGeneric>> GetTopCourse(int courseNumber, List<SortedPaging> sortedList);
        IEnumerable<MyCourseVM> GetListCourseByUser(Guid accId);
        
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
        public async Task<IEnumerable<CourseGeneric>> GetTopCourse(int courseNumber, List<SortedPaging> sortedList)
        {
            var courseGeneric = from c in _context.Courses
                                join a in _context.Accounts on c.Teacher equals a.Id
                                join ct in _context.CourseTags on c.Id equals ct.CourseId
                                join t in _context.Tags on ct.TagId equals t.Id
                                group new { c, t } by new
                                {
                                    c.Id,
                                    c.CourseName,
                                    c.Rating,
                                    c.Price,
                                    c.PriceAfterDiscount,
                                    c.ImgCourse,
                                    c.TotalTime,
                                    c.TotalLectures,
                                    c.TotalPerRating,
                                    c.CourseCode,
                                    a.ImgAvatar,
                                    a.FirstName,
                                    c.ModifiedDate,
                                    c.Description,
                                    c.BenefitsOfCourse,
                                } into grp
                                select new CourseGeneric
                                {
                                    Id = grp.Key.Id,
                                    CourseName = grp.Key.CourseName,
                                    Rating = grp.Key.Rating,
                                    Price = grp.Key.Price,
                                    PriceAfterDiscount = grp.Key.PriceAfterDiscount,
                                    ImgCourse = grp.Key.ImgCourse,
                                    TotalTime = grp.Key.TotalTime,
                                    TotalLectures = grp.Key.TotalLectures,
                                    TotalPerRating = grp.Key.TotalPerRating,
                                    CourseCode = grp.Key.CourseCode,
                                    TagString = string.Join(", ", grp.Select(x => x.t.TagName)),
                                    ImgTeacher = grp.Key.ImgAvatar,
                                    TeacherName = grp.Key.FirstName,
                                    ModifiedDate = grp.Key.ModifiedDate,
                                    Description = grp.Key.Description,
                                    BenefitsOfCourse = grp.Key.BenefitsOfCourse
                                };
            var result = await GetDataBySorted(courseGeneric, sortedList);
            if (result != null && result.Count() > 0)
            {
                result = result.Take(courseNumber);
            }
            return result;
        }

        
    }
}
