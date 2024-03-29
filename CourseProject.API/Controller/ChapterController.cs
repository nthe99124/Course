using CourseProject.API.Common.Attribute;
using CourseProject.API.Common.Constant;
using CourseProject.API.Controller.Base;
using CourseProject.API.Services;
using CourseProject.Model.ViewModel;
using CourseProject.Model.ViewModel.Course;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.API.Controller
{
    public class ChapterController : BaseController
    {
        private IChapterService _chapterService;
        public ChapterController(IRestOutput res, IHttpContextAccessor httpContextAccessor, IChapterService chapterService) : base(res, httpContextAccessor)
        {
            _chapterService = chapterService;
        }

        /// <summary>
        /// Hàm xử lý thêm mới chương
        /// CreatedBy ntthe 27.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateChapter")]
        //[Roles(RoleConstant.Admin, RoleConstant.Teacher)]
        public async Task<IActionResult> CreateChapter(ChapterCreateParam chapter)
        {
            _res = await _chapterService.CreateChapter(chapter);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý đổi tên chương
        /// CreatedBy ntthe 27.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("EditChapter")]
        [Roles(RoleConstant.Admin, RoleConstant.Teacher)]
        public async Task<IActionResult> EditChapter(ChapterEditParam chapter)
        {
            _res = await _chapterService.EditChapter(chapter);
            return Ok(_res);
        }
    }
}
