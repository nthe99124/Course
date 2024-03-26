using CourseProject.API.Common.Ulti;
using CourseProject.API.Controller.Base;
using CourseProject.API.Services;
using CourseProject.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.API.Controller
{
    public class TagController : BaseController
    {
        private ITagService _tagService;
        public TagController(IRestOutput res, IHttpContextAccessor httpContextAccessor, ITagService tagService) : base(res, httpContextAccessor)
        {
            _tagService = tagService;
        }

        /// <summary>
        /// Hàm xử lý lấy tất cả các tag
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllTag")]
        public async Task<IActionResult> GetAllTag()
        {
            var dataResult = await _tagService.GetAllTag();
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }
    }
}
