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

        [HttpPost("video/upload")]
        public async Task<IActionResult> UploadVideo([FromForm]IFormFile videoFile)
        {
            try
            {
                string UploadsDirectory = "uploads";
                // Tạo thư mục lưu trữ nếu nó chưa tồn tại
                if (!Directory.Exists(UploadsDirectory))
                {
                    Directory.CreateDirectory(UploadsDirectory);
                }

                // Tạo đường dẫn lưu trữ cho video
                var filePath = Path.Combine(UploadsDirectory, Guid.NewGuid().ToString() + Path.GetExtension(videoFile.FileName));

                // Mở một luồng để ghi dữ liệu từ tệp tải lên
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    // Ghi dữ liệu từ tệp tải lên vào luồng
                    await videoFile.CopyToAsync(stream);
                }

                // Trả về đường dẫn tới video đã tải lên
                return Ok(new { filePath });
            }
            catch (Exception ex)
            {
                // Trả về lỗi nếu có bất kỳ vấn đề nào xảy ra
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Failed to upload video: {ex.Message}" });
            }
        }
    }
}
