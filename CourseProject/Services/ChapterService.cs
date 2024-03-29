using CourseProject.Common.Cache;
using CourseProject.Model.BaseEntity;
using CourseProject.Model.ViewModel;
using CourseProject.Model.ViewModel.Course;
using CourseProject.Services.ApiUrldefinition;
using CourseProject.Services.Base;
using Microsoft.JSInterop;

namespace CourseProject.Services
{
    public interface IChapterService : IBaseService
    {
        Task<ResponseOutput<Guid?>> CreateChapter(ChapterCreateParam chapter);
        Task<ResponseOutput<string>> EditChapter(ChapterEditParam chapter);
    }

    public class ChapterService : BaseService, IChapterService
    {
        public ChapterService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory, IConfiguration config, IJSRuntime js) : base(cache, httpClientFactory, config, js)
        {

        }

        /// <summary>
        /// Hàm xử lý thêm mới chương
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<Guid?>> CreateChapter(ChapterCreateParam chapter)
        {
            var url = ChapterApiUrlDef.CreateChapter();
            return await RequestFullAuthenPostAsync<Guid?>(url, chapter);
        }

        /// <summary>
        /// Hàm xử lý đổi tên chương
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> EditChapter(ChapterEditParam chapter)
        {
            var url = ChapterApiUrlDef.EditChapter();
            return await RequestFullAuthenPostAsync<string>(url, chapter);
        }
    }
}
