using CourseProject.Common.Cache;
using CourseProject.Model.ViewModel;
using CourseProject.Model.ViewModel.Course;
using CourseProject.Services.ApiUrldefinition;
using CourseProject.Services.Base;
using Microsoft.JSInterop;

namespace CourseProject.Services
{
    public interface ILessionService : IBaseService
    {
        Task<ResponseOutput<Guid?>> CreateLession(LessionCreateParam lession);
        Task<ResponseOutput<string>> EditLession(LessionEditParam lession);
        Task<ResponseOutput<string>> DeleteLession(LessionDeleteParam lession);
        Task<ResponseOutput<string>> EditLessionName(LessionCreateParam lession);
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
        public async Task<ResponseOutput<string>> EditLession(LessionEditParam lession)
        {
            var url = LessionApiUrlDef.EditLession();
            return await RequestFullAuthenPostAsync<string>(url, lession);
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

        /// <summary>
        /// Hàm xử lý sửa tên bài học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> EditLessionName(LessionCreateParam lession)
        {
            var url = LessionApiUrlDef.CreateLession();
            return await RequestFullAuthenPostAsync<string>(url, lession);
        }

        
    }
}
