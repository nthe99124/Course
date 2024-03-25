using AutoMapper;
using CourseProject.API.Common.Cache;
using CourseProject.API.Common.Repository;
using CourseProject.API.Services.Base;
using CourseProject.Model.BaseEntity;
using CourseProject.Model.DTO;
using CourseProject.Model.ViewModel.Course;

namespace CourseProject.API.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseGeneric>> GetTop10GoodCourse();
        Task<IEnumerable<CourseGeneric>> GetTop10NewCourse();
        IEnumerable<MyCourseVM> GetListCourseByUser();
        Task<CourseDetailVM> GetDetailCourse(Guid courseId);
    }
    public class CourseService : BaseService, ICourseService
    {
        public CourseService(IHttpContextAccessor httpContextAccessor,
                                IDistributedCacheCustom cache,
                                IUnitOfWork unitOfWork, IMapper mapper) : base(httpContextAccessor, cache, unitOfWork, mapper)
        {
            
        }

        /// <summary>
        /// Hàm xử lý lấy top 10 khóa học hàng đầu
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CourseGeneric>> GetTop10GoodCourse()
        {
            var sortedList = new List<SortedPaging>()
            {
                new SortedPaging
                {
                    Field = "Rating",
                    IsAsc = false
                },
                new SortedPaging
                {
                    Field = "TotalPerRating",
                    IsAsc = false
                }
            };
            return await _unitOfWork.CourseRepository.GetCourseByCondition(10, sortedList);
        }

        /// <summary>
        /// Hàm xử lý lấy top 10 khóa học mới nhất
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CourseGeneric>> GetTop10NewCourse()
        {
            var sortedList = new List<SortedPaging>()
            {
                new SortedPaging
                {
                    Field = "CreatedDate",
                    IsAsc = false
                }
            };
            return await _unitOfWork.CourseRepository.GetCourseByCondition(10, sortedList);
        }

        /// <summary>
        /// Hàm xử lý lấy tất cả khóa học user đã đăng ký
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MyCourseVM> GetListCourseByUser()
        {
            var accId = GetUserAuthen()?.AccoutantId;
            if (accId != null)
            {
                return _unitOfWork.CourseRepository.GetListCourseByUser((Guid)accId);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Hàm xử lý lấy chi tiết khóa học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<CourseDetailVM> GetDetailCourse(Guid courseId)
        {
            // 2 luồng này cho riêng cũng được, tách task oke

            // lấy thông tin master của khóa học 
            var taskGetCourseMaster = _unitOfWork.CourseRepository.GetCourseByCondition(1, null, item => item.Id == courseId);
            // lấy thông tin của detail khóa học
            var taskGetCourseDetail = Task.Run(() => _unitOfWork.CourseRepository.GetDetailCourse(courseId));

            await Task.WhenAll(taskGetCourseMaster, taskGetCourseDetail);

            var result = new CourseDetailVM();
            var courseMaster = await taskGetCourseMaster;
            var courseDetail = await taskGetCourseDetail;
            if (courseMaster != null && courseMaster.Count() > 0)
            {
                result.CourseMaster = courseMaster.First();
            }

            if (courseDetail != null && courseDetail.Count() > 0)
            {
                // dùng dictionary tìm nhanh
                var courseDetailList = new Dictionary<Guid, ChapterDetail>();
                foreach (var item in courseDetail)
                {
                    // nếu không gồm key thì add vào
                    if (!courseDetailList.ContainsKey(item.ChapterId))
                    {
                        var lessionDetailList = courseDetail.Where(item => item.ChapterId == item.ChapterId)
                                                            .Cast<LessionDetail>().ToList();
                        courseDetailList.Add(item.ChapterId, new ChapterDetail()
                        {
                            ChapterId = item.ChapterId,
                            ChapterName = item.ChapterName,
                            LessionDetailList = lessionDetailList
                        });
                    }
                }
                // gom nhóm lại detail
                result.CourseDetailList = courseDetailList.Values.ToList();
            }

            return result;
        }

        #region Private Method
        #endregion
    }
}
