using CourseProject.Common.Cache;
using CourseProject.Model.ViewModel;
using CourseProject.Model.ViewModel.Course;
using CourseProject.Services.ApiUrldefinition;
using CourseProject.Services.Base;
using Microsoft.JSInterop;

namespace CourseProject.Services
{
    public interface ICourseService : IBaseService
    {
        Task<List<CourseGeneric>> GetTop10GoodCourse();
        Task<List<CourseGeneric>> GetTop10NewCourse();
        Task<List<MyCourseVM>> GetListCourseByUser();
        Task<CourseDetailVM> GetDetailCourse(Guid courseId);
        Task<List<CourseGeneric>> GetCourseSearchCourseByCondition(SearchCourseParam searchCourseParam);
        Task<ResponseOutput<Guid?>> CreateCourseMaster(CreateCourseVM createCourseParam);
        Task<bool> CheckUserHasPermissionCourse(Guid courseId);
    }

    public class CourseService : BaseService, ICourseService
    {
        public CourseService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory, IConfiguration config, IJSRuntime js) : base(cache, httpClientFactory, config, js)
        {

        }

        /// <summary>
        /// Hàm xử lý lấy top 10 khóa học hằng đầu (tính theo lượt đánh giá và lượt người đánh giá cao nhất)
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<CourseGeneric>> GetTop10GoodCourse()
        {
            var url = CourseApiUrlDef.GetTop10GoodCourse();
            return await RequestGetAsync<List<CourseGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy top 10 khóa học mới nhất
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<CourseGeneric>> GetTop10NewCourse()
        {
            var url = CourseApiUrlDef.GetTop10NewCourse();
            return await RequestGetAsync<List<CourseGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy tất cả khóa học user đã đăng ký
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<MyCourseVM>> GetListCourseByUser()
        {
            var url = CourseApiUrlDef.GetListCourseByUser();
            return await RequestAuthenGetAsync<List<MyCourseVM>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy lấy chi tiết khóa học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<CourseDetailVM> GetDetailCourse(Guid courseId)
        {
            var url = CourseApiUrlDef.GetDetailCourse(courseId);
            return await RequestAuthenGetAsync<CourseDetailVM>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách khóa học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<CourseGeneric>> GetCourseSearchCourseByCondition(SearchCourseParam searchCourseParam)
        {
            var url = CourseApiUrlDef.GetCourseSearchCourseByCondition();
            return await RequestPostAsync<List<CourseGeneric>>(url,searchCourseParam);
        }

        /// <summary>
        /// Hàm xử lý thêm khóa học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<Guid?>> CreateCourseMaster(CreateCourseVM createCourseParam)
        {
            var url = CourseApiUrlDef.CreateCourseMaster();
            return await RequestFullAuthenPostAsync<Guid?>(url, createCourseParam);
        }

        /// <summary>
        /// Hàm kiểm tra xem user có quyền xem khóa học không
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckUserHasPermissionCourse(Guid courseId)
        {
            var url = CourseApiUrlDef.CheckUserHasPermissionCourse(courseId);
            return await RequestAuthenGetAsync<bool>(url);
        }
        
    }
}
