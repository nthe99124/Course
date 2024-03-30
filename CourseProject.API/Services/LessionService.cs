using AutoMapper;
using CourseProject.API.Common.Cache;
using CourseProject.API.Common.Repository;
using CourseProject.API.Common.Ulti;
using CourseProject.API.Services.Base;
using CourseProject.Model.BaseEntity;
using CourseProject.Model.ViewModel;
using CourseProject.Model.ViewModel.Course;
using Microsoft.AspNetCore.Components.Forms;

namespace CourseProject.API.Services
{
    public interface ILessionService
    {
        Task<RestOutput> CreateLession(LessionCreateParam lession);
        Task<RestOutput> EditLession(LessionEditParam lession);
        Task<RestOutput> DeleteLession(LessionDeleteParam lession);
        Task<RestOutput> EditLessionName(LessionCreateParam lession);
    }
    public class LessionService : BaseService, ILessionService
    {
        private IFileUlti _fileUlti;
        public LessionService(IHttpContextAccessor httpContextAccessor,
                                IDistributedCacheCustom cache,
                                IUnitOfWork unitOfWork, IMapper mapper, IFileUlti fileUlti) : base(httpContextAccessor, cache, unitOfWork, mapper)
        {
            _fileUlti = fileUlti;
        }

        /// <summary>
        /// Hàm xử lý thêm mới bài học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<RestOutput> CreateLession(LessionCreateParam lession)
        {
            var res = new RestOutput();
            if (lession != null)
            {
                if (string.IsNullOrEmpty(lession.LessionName))
                {
                    res.ErrorEventHandler("Tên bài học không được để trống");
                    return res;
                }

                // insert bài học
                var lessionInsert = new Lession()
                {
                    LessionName = lession.LessionName,
                    ChapterId = lession.ChapterId,
                };
                _unitOfWork.LessionRepository.Create(lessionInsert);

                // insert số bài học trong khóa học
                var courseCurrent = await _unitOfWork.CourseRepository.FirstOrDefault(item => item.Id == lession.CourseId);
                if (courseCurrent != null)
                {
                    courseCurrent.TotalLectures += courseCurrent.TotalLectures;
                }

                await _unitOfWork.CommitAsync();
                res.SuccessEventHandler(lessionInsert.Id);
            }
            else
            {
                res.ErrorEventHandler("Thông tin không hợp lệ");
                return res;
            }
            return res;
        }

        /// <summary>
        /// Hàm xử lý sửa tên bài học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<RestOutput> EditLessionName(LessionCreateParam lession)
        {
            var res = new RestOutput();
            if (lession != null)
            {
                if (string.IsNullOrEmpty(lession.LessionName))
                {
                    res.ErrorEventHandler("Tên bài học không được để trống");
                    return res;
                }

                var lessionCurrent = await _unitOfWork.LessionRepository.FirstOrDefault(item => item.Id == lession.LessionId);
                if (lessionCurrent != null)
                {
                    var oldTotalTimeLession = lessionCurrent.TotalTimeLession;
                    // cập nhật bài học
                    lessionCurrent.LessionName = lession.LessionName;

                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    res.ErrorEventHandler("Bài học không tồn tại.");
                    return res;
                }
            }
            return res;
        }

        /// <summary>
        /// Hàm xử lý sửa bài học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<RestOutput> EditLession(LessionEditParam lession)
        {
            var res = new RestOutput();
            if (lession != null)
            {
                var lessionCurrent = await _unitOfWork.LessionRepository.FirstOrDefault(item => item.Id == lession.Id);
                if (lessionCurrent != null)
                {
                    var oldTotalTimeLession = lessionCurrent.TotalTimeLession;
                    // cập nhật bài học
                    lessionCurrent.VideoLink = lession.VideoLink;
                    lessionCurrent.TestLink = lession.TestLink;
                    lessionCurrent.LessionLink = lession.LessionLink;
                    lessionCurrent.Text = lession.Text;
                    lessionCurrent.AttachmentsLink = lession.AttachmentsLink;
                    lessionCurrent.TotalTimeLession = lession.TotalTimeLession;

                    // cập nhật tổng thời gian khóa học
                    var courseCurrent = await _unitOfWork.CourseRepository.FirstOrDefault(item => item.Id == lession.CourseId);
                    if (courseCurrent != null) {
                        courseCurrent.TotalLectures = courseCurrent.TotalLectures - oldTotalTimeLession + lessionCurrent.TotalTimeLession;
                    }

                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    res.ErrorEventHandler("Bài học không tồn tại.");
                    return res;
                }
            }

            res.SuccessEventHandler();
            return res;
        }

        /// <summary>
        /// Hàm xử lý xóa bài học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<RestOutput> DeleteLession(LessionDeleteParam lession)
        {
            var res = new RestOutput();
            if (lession != null)
            {
                var lessionCurrent = await _unitOfWork.LessionRepository.FirstOrDefault(item => item.Id == lession.LessionId);
                if (lessionCurrent != null)
                {
                    // xóa bài học
                    _unitOfWork.LessionRepository.Delete(lessionCurrent);

                    // xóa số lượng bài học + tổng thời gian học ở khóa học
                    var courseCurrent = await _unitOfWork.CourseRepository.FirstOrDefault(item => item.Id == lession.CourseId);
                    if (courseCurrent != null)
                    {
                        courseCurrent.TotalLectures = courseCurrent.TotalLectures - 1;
                        courseCurrent.TotalTime = courseCurrent.TotalTime - lessionCurrent.TotalTimeLession;
                    }

                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    res.ErrorEventHandler("Bài học không tồn tại.");
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
