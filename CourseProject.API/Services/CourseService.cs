using AutoMapper;
using CourseProject.API.Common.Cache;
using CourseProject.API.Common.Repository;
using CourseProject.API.Common.Ulti;
using CourseProject.API.Services.Base;
using CourseProject.Model.DTO;
using CourseProject.Model.ViewModel.Course;
using System.Linq.Expressions;

namespace CourseProject.API.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseGeneric>> GetTop10GoodCourse();
        Task<IEnumerable<CourseGeneric>> GetTop10NewCourse();
        IEnumerable<MyCourseVM> GetListCourseByUser();
        Task<CourseDetailVM> GetDetailCourse(Guid courseId);
        Task<IEnumerable<CourseGeneric>> GetCourseSearchCourseByCondition(SearchCourseParam searchCourseParam);
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
            // 2 luồng này nên tách riêng, nhưng EF lại không cho dùng context 1 lúc 2 bên nên phải wait

            // lấy thông tin master của khóa học 
            var courseMaster = await _unitOfWork.CourseRepository.GetCourseByCondition(1, null, item => item.Id == courseId);
            // lấy thông tin của detail khóa học
            var courseDetail = _unitOfWork.CourseRepository.GetDetailCourse(courseId);

            var result = new CourseDetailVM();
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
                        var lessionDetailList = courseDetail.Where(item => item.ChapterId == item.ChapterId).Select(item => new LessionDetail
                        {
                            LessionId = item.LessionId,
                            LessionName = item.LessionName,
                            TotalHourTimeLession = item.TotalHourTimeLession,
                            VideoLink = item.VideoLink,
                        }).ToList();
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

        /// <summary>
        /// Hàm xử lý lấy 
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CourseGeneric>> GetCourseSearchCourseByCondition(SearchCourseParam searchCourseParam)
        {
            var sortedList = new List<SortedPaging>();
            Expression<Func<CourseGeneric, bool>> predicateFilter = null;
            if (searchCourseParam.TypeOfPurchase != null)
            {
                predicateFilter = (item => item.TypeOfPurchase == searchCourseParam.TypeOfPurchase);
            }

            if (searchCourseParam.TagId != null)
            {
                predicateFilter = predicateFilter != null? ExtensionUlti.PedicateAnd(predicateFilter, (item => item.TagIdList.Contains((Guid)searchCourseParam.TagId) )): (item => item.TagIdList.Contains((Guid)searchCourseParam.TagId));
            }

            if (searchCourseParam.Rating != null)
            {
                predicateFilter = predicateFilter != null ? ExtensionUlti.PedicateAnd(predicateFilter, (item => item.Rating == searchCourseParam.Rating)) : (item => item.Rating == searchCourseParam.Rating);
            }

            if (searchCourseParam.SortField != null)
            {
                sortedList = new List<SortedPaging>()
                {
                    searchCourseParam.SortField
                };
            }
            var result = await _unitOfWork.CourseRepository.GetCourseByCondition(null, sortedList, predicateFilter);
            return result;
        }

        /// <summary>
        /// Hàm xử lý thêm mới master khóa học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<MyCourseVM> CreateCourseMaster()
        //{
        //    var accId = GetUserAuthen()?.AccoutantId;

        //    if (accId != null)
        //    {
        //        //return _unitOfWork.CourseRepository.Create((Guid)accId);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        #region Private Method

        #endregion
    }
}
