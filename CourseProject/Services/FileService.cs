using CourseProject.Common.Cache;
using CourseProject.Model.ViewModel;
using CourseProject.Services.ApiUrldefinition;
using CourseProject.Services.Base;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace CourseProject.Services
{
    public interface IFileService : IBaseService
    {
        Task<ResponseOutput<string>> UploadVideo(IBrowserFile file);
        Task<ResponseOutput<string>> DeleteFile(string videoName);
    }

    public class FileService : BaseService, IFileService
    {
        public FileService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory, IConfiguration config, IJSRuntime js) : base(cache, httpClientFactory, config, js)
        {

        }

        /// <summary>
        /// Hàm xử lý upload video
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> UploadVideo(IBrowserFile file)
        {
            var url = FileApiUrlDef.UploadVideo();
            var selectedFile = new Dictionary<string, IBrowserFile>
            {
                { "videoFile", file },
            };
            return await RequestFileAsync<string>(url, selectedFile);
        }

        /// <summary>
        /// Hàm xử lý xóa video
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> DeleteFile(string videoName)
        {
            var url = FileApiUrlDef.DeleteVideo(videoName);
            return await RequestFullAuthenPostAsync<string>(url);
        }
    }
}
