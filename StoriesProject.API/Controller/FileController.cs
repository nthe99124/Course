using Microsoft.AspNetCore.Mvc;
using CourseProject.API.Common.Attribute;
using CourseProject.API.Common.Constant;
using CourseProject.API.Common.Ulti;
using CourseProject.API.Services;
using CourseProject.Model.ViewModel;

namespace CourseProject.API.Controller.Base
{
    public class FileController : BaseController
    {
        private readonly IFileUlti _fileUlti;
        public FileController(IRestOutput res, IHttpContextAccessor httpContextAccessor, IFileUlti fileUlti) : base(res, httpContextAccessor)
        {
            _fileUlti = fileUlti;
        }

        [HttpGet]
        [Route("images/{imageName}")]
        public async Task<IActionResult> GetImage(string imageName)
        {
            var imageFileStream = await _fileUlti.ReadFile(imageName);
            return File(imageFileStream, "image/jpeg");
        }
    }
}
