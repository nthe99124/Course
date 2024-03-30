using CourseProject.Model.ViewModel;
using MediaInfoLib;
using Microsoft.AspNetCore.Hosting.Server;


namespace CourseProject.API.Common.Ulti
{
    public interface IFileUlti
    {
        Task<string> SaveFile(IFormFile file);
        Task<FileStream> ReadFile(string fileName);
        Task<string> SaveFileBase64(FileBase64Infor fileBase64);
        bool DeleteFile(string fileName);
    }
    public class FileUlti : IFileUlti
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public FileUlti(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> SaveFileBase64(FileBase64Infor fileBase64)
        {
            byte[] bytes = Convert.FromBase64String(fileBase64.Base64File);
            MemoryStream stream = new(bytes);

            //item.File = new FormFile(stream, 0, bytes.Length, item.SeoFilename, item.SeoFilename);
            var fileItem = new FormFile(stream, 0, stream.Length, null, fileBase64.FileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = fileBase64.ContentType
            };
            var fileName = await SaveFile(fileItem);
            return fileName;
        }
        public async Task<string> SaveFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file.");
            }

            // Tạo một tên file duy nhất bằng cách kết hợp tên và định dạng mở rộng
            var fileName = file.FileName.ToLower();

            // Đường dẫn đến thư mục lưu trữ file (ví dụ: wwwroot/uploads)
            var uploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");

            // Tạo thư mục lưu trữ file nếu nó không tồn tại
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Đường dẫn đến file lưu trữ trên server
            var filePath = Path.Combine(uploadFolder, fileName);

            if (!File.Exists(filePath))
            {
                // Mở luồng để ghi dữ liệu file từ yêu cầu vào file trên server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return fileName; // Trả về tên file đã lưu
        }

        public async Task<FileStream> ReadFile(string fileName)
        {
            // Đường dẫn đến file trên server
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads", fileName);
            var imageFileStream = System.IO.File.OpenRead(filePath);
            // Đọc dữ liệu từ file
            return imageFileStream;
        }

        public bool DeleteFile(string fileName)
        {
            string filePath = Path.Combine("uploads", fileName);
            if (File.Exists(filePath))
            {
                // Xóa video nếu tồn tại
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
