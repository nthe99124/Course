using CourseProject.API.Common.Ulti;
using CourseProject.API.Controller.Base;
using CourseProject.API.Services;
using CourseProject.Model.ViewModel;
using CourseProject.Model.ViewModel.Course;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.API.Controller
{
    public class CourseController : BaseController
    {
        private ICourseService _courseService;
        public CourseController(IRestOutput res, IHttpContextAccessor httpContextAccessor, ICourseService courseService) : base(res, httpContextAccessor)
        {
            _courseService = courseService;
        }

        /// <summary>
        /// Hàm xử lý lấy top 10 khóa học hằng đầu (tính theo lượt đánh giá và lượt người đánh giá cao nhất)
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTop10GoodCourse")]
        public async Task<IActionResult> GetTop10GoodCourse()
        {
            var dataResult = await _courseService.GetTop10GoodCourse();
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý lấy top 10 khóa học mới nhất
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTop10NewCourse")]
        public async Task<IActionResult> GetTop10NewCourse()
        {
            var dataResult = await _courseService.GetTop10NewCourse();
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý lấy tất cả khóa học user đã đăng ký
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetListCourseByUser")]
        public IActionResult GetListCourseByUser()
        {
            var dataResult = _courseService.GetListCourseByUser();
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý lấy chi tiết khóa học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDetailCourse")]
        public async Task<IActionResult> GetDetailCourse(Guid courseId)
        {
            var dataResult = await _courseService.GetDetailCourse(courseId);
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý tìm kiếm khóa học theo param truyền xuống
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCourseSearchCourseByCondition")]
        public async Task<IActionResult> GetCourseSearchCourseByCondition(SearchCourseParam searchCourseParam)
        {
            var dataResult = await _courseService.GetCourseSearchCourseByCondition(searchCourseParam);
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý thêm mới master khóa học
        /// CreatedBy ntthe 27.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateCourseMaster")]
        public async Task<IActionResult> CreateCourseMaster(CreateCourseVM createCourseParam)
        {
            _res = await _courseService.CreateCourseMaster(createCourseParam);
            return Ok(_res);
        }
    }
}
