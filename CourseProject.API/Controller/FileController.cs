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

        [HttpGet]
        [Route("video/{videoName}")]
        public async Task<IActionResult> GetVideo(string videoName)
        {
            var imageFileStream = await _fileUlti.ReadFile(videoName);
            return File(imageFileStream, "video/mp4");
        }

        [HttpPost("video/upload")]
        public async Task<IActionResult> UploadVideo([FromForm]IFormFile videoFile)
        {
            var fileName = await _fileUlti.SaveFile(videoFile);
            _res.SuccessEventHandler(fileName);
            return Ok(_res);
        }

        // API để xóa video
        [HttpPost("video/{videoName}")]
        public IActionResult DeleteVideo(string videoName)
        {
            var isSuccess = _fileUlti.DeleteFile(videoName);
            
            if (isSuccess)
            {
                _res.SuccessEventHandler();
            }
            else
            {
                _res.ErrorEventHandler();
            }
            return Ok(_res);
        }
    }
}
