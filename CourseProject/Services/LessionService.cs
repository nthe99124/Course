using Amazon.Runtime.Internal.Transform;
using CourseProject.Common.Cache;
using CourseProject.Model.BaseEntity;
using CourseProject.Model.ViewModel;
using CourseProject.Model.ViewModel.Course;
using CourseProject.Services.ApiUrldefinition;
using CourseProject.Services.Base;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace CourseProject.Services
{
    public interface ILessionService : IBaseService
    {
        Task<ResponseOutput<Guid?>> CreateLession(LessionCreateParam lession);
        Task<ResponseOutput<string>> EditLession(Dictionary<string, IBrowserFile> listFile, LessionEditParam lession);
        Task<ResponseOutput<string>> DeleteLession(LessionDeleteParam lession);
    }

    public class LessionService : BaseService, ILessionService
    {
        public LessionService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory, IConfiguration config, IJSRuntime js) : base(cache, httpClientFactory, config, js)
        {

        }

        /// <summary>
        /// Hàm xử lý thêm mới bài học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<Guid?>> CreateLession(LessionCreateParam lession)
        {
            var url = LessionApiUrlDef.CreateLession();
            return await RequestFullAuthenPostAsync<Guid?>(url, lession);
        }

        /// <summary>
        /// Hàm xử lý sửa bài học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> EditLession(Dictionary<string, IBrowserFile>  listFile, LessionEditParam lession)
        {
            //var listFile = new Dictionary<string, IBrowserFile>()
            //{
            //    { "VideoFile", lession.VideoFile },
            //    { "TestFile", lession.TestFile },
            //    { "LessionFile", lession.LessionFile },
            //    { "AttachmentsFile", lession.AttachmentsFile },
            //};
            var url = LessionApiUrlDef.EditLession();
            return await RequestFileAsync<string>(url, listFile, lession);
        }

        /// <summary>
        /// Hàm xử lý xóa bài học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> DeleteLession(LessionDeleteParam lession)
        {
            var url = LessionApiUrlDef.DeleteLession();
            return await RequestFullAuthenPostAsync<string>(url, lession);
        }
    }
}
