using AutoMapper;
using CourseProject.API.Common.Cache;
using CourseProject.API.Common.Repository;
using CourseProject.API.Services.Base;
using CourseProject.Model.BaseEntity;
using CourseProject.Model.ViewModel;
using CourseProject.Model.ViewModel.Course;

namespace CourseProject.API.Services
{
    public interface IChapterService
    {
        Task<RestOutput> CreateChapter(ChapterCreateParam chapter);
        Task<RestOutput> EditChapter(ChapterCreateParam chapter);
    }
    public class ChapterService : BaseService, IChapterService
    {
        public ChapterService(IHttpContextAccessor httpContextAccessor,
                                IDistributedCacheCustom cache,
                                IUnitOfWork unitOfWork, IMapper mapper) : base(httpContextAccessor, cache, unitOfWork, mapper)
        {
            
        }

        /// <summary>
        /// Hàm xử lý thêm mới chương
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<RestOutput> CreateChapter(ChapterCreateParam chapter)
        {
            var res = new RestOutput();
            if (chapter != null)
            {
                if (string.IsNullOrEmpty(chapter.ChapterName))
                {
                    res.ErrorEventHandler("Tên chapter không được để trống");
                    return res;
                }
                else if (chapter.CourseId == default)
                {
                    res.ErrorEventHandler("Mã khóa học không được để trống");
                    return res;
                }

                // insert chương
                var chapterInsert = new Chapter()
                {
                    ChapterName = chapter.ChapterName,
                    CourseId = chapter.CourseId,
                };
                _unitOfWork.ChapterRepository.Create(chapterInsert);

                await _unitOfWork.CommitAsync();

                res.SuccessEventHandler(chapterInsert.Id);
            }
            else
            {
                res.ErrorEventHandler("Thông tin không hợp lệ");
                return res;
            }
            
            return res;
        }

        /// <summary>
        /// Hàm xử lý đổi tên chương
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<RestOutput> EditChapter(ChapterCreateParam chapter)
        {
            var res = new RestOutput();
            if (chapter != null)
            {
                if (string.IsNullOrEmpty(chapter.ChapterName))
                {
                    res.ErrorEventHandler("Tên chapter không được để trống");
                    return res;
                }


                var currentChapter = await _unitOfWork.ChapterRepository.FirstOrDefault(item => item.Id == chapter.ChapterId);
                if (currentChapter != null)
                {
                    currentChapter.ChapterName = chapter.ChapterName;
                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    res.ErrorEventHandler("Chương không tồn tại.");
                    return res;
                }
            }

            res.SuccessEventHandler();
            return res;
        }

        #region Private Method

        #endregion
    }
}
