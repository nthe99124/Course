using CourseProject.Common.Cache;
using CourseProject.Model.BaseEntity;
using CourseProject.Services.ApiUrldefinition;
using CourseProject.Services.Base;
using Microsoft.JSInterop;

namespace CourseProject.Services
{
    public interface ITagService : IBaseService
    {
        Task<List<Tag>> GetAllTag();
    }

    public class TagService : BaseService, ITagService
    {
        public TagService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory, IConfiguration config, IJSRuntime js) : base(cache, httpClientFactory, config, js)
        {

        }

        /// <summary>
        /// Hàm xử lý lấy tất cả các tag
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<Tag>> GetAllTag()
        {
            var url = TagApiUrlDef.GetAllTag();
            return await RequestGetAsync<List<Tag>>(url);
        }
    }
}
