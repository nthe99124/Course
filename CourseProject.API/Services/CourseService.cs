using AutoMapper;
using CourseProject.API.Common.Cache;
using CourseProject.API.Common.Constant;
using CourseProject.API.Common.Repository;
using CourseProject.API.Services.Base;
using CourseProject.Model.BaseEntity;
using CourseProject.Model.DTO;
using System.Text.Json;

namespace CourseProject.API.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseGeneric>> GetTop10GoodCourse();
        Task<IEnumerable<CourseGeneric>> GetTop10NewCourse();
        IEnumerable<MyCourseVM> GetListCourseByUser();
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
            return await _unitOfWork.CourseRepository.GetTopCourse(10, sortedList);
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
            return await _unitOfWork.CourseRepository.GetTopCourse(10, sortedList);
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

        #region Private Method
        #endregion
    }
}
