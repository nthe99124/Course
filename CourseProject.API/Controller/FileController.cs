using CourseProject.API.Common.Ulti;
using CourseProject.API.Controller.Base;
using CourseProject.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.API.Controller
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
