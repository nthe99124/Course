using AutoMapper;
using CourseProject.API.Common.Cache;
using CourseProject.API.Common.Constant;
using CourseProject.API.Common.Repository;
using CourseProject.API.Services.Base;
using CourseProject.Model.BaseEntity;
using CourseProject.Model.DTO;
using CourseProject.Model.ViewModel.Course;
using System.Text.Json;

namespace CourseProject.API.Services
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllTag();
    }
    public class TagService : BaseService, ITagService
    {
        public TagService(IHttpContextAccessor httpContextAccessor,
                                IDistributedCacheCustom cache,
                                IUnitOfWork unitOfWork, IMapper mapper) : base(httpContextAccessor, cache, unitOfWork, mapper)
        {
            
        }

        /// <summary>
        /// Hàm xử lý lấy tất cả tag
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Tag>> GetAllTag()
        {
            return await _unitOfWork.TagRepository.GetAll();
        }

        #region Private Method
        #endregion
    }
}
