using CourseProject.API.Common.Attribute;
using CourseProject.API.Common.Constant;
using CourseProject.API.Controller.Base;
using CourseProject.API.Services;
using CourseProject.Model.ViewModel;
using CourseProject.Model.ViewModel.Course;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.API.Controller
{
    public class LessionController : BaseController
    {
        private ILessionService _lessionService;
        public LessionController(IRestOutput res, IHttpContextAccessor httpContextAccessor, ILessionService lessionService) : base(res, httpContextAccessor)
        {
            _lessionService = lessionService;
        }

        /// <summary>
        /// Hàm xử lý thêm mới bài học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateLession")]
        [Roles(RoleConstant.Admin, RoleConstant.Teacher)]
        public async Task<IActionResult> CreateLession(LessionCreateParam lession)
        {
            _res = await _lessionService.CreateLession(lession);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý sửa bài học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("EditLession")]
        [Roles(RoleConstant.Admin, RoleConstant.Teacher)]
        public async Task<IActionResult> EditLession([FromForm] Dictionary<string, IFormFile> listFile, [FromForm] LessionEditParam lession)
        {
            _res = await _lessionService.EditLession(listFile, lession);
            return Ok(_res);
        }
        /// <summary>
        /// Hàm xử lý xóa bài học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteLession")]
        [Roles(RoleConstant.Admin, RoleConstant.Teacher)]
        public async Task<IActionResult> DeleteLession(LessionDeleteParam lession)
        {
            _res = await _lessionService.DeleteLession(lession);
            return Ok(_res);
        }

    }
}
