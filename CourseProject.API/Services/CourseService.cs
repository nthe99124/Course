using AutoMapper;
using CourseProject.API.Common.Cache;
using CourseProject.API.Common.Repository;
using CourseProject.API.Common.Ulti;
using CourseProject.API.Services.Base;
using CourseProject.Model.BaseEntity;
using CourseProject.Model.DTO;
using CourseProject.Model.ViewModel;
using CourseProject.Model.ViewModel.Course;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CourseProject.API.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseGeneric>> GetTop10GoodCourse();
        Task<IEnumerable<CourseGeneric>> GetTop10NewCourse();
        IEnumerable<MyCourseVM> GetListCourseByUser();
        Task<CourseDetailVM> GetDetailCourse(Guid courseId);
        Task<IEnumerable<CourseGeneric>> GetCourseSearchCourseByCondition(SearchCourseParam searchCourseParam);
        Task<RestOutput> CreateCourseMaster(CreateCourseVM createCourseParam);
        RestOutput CheckUserHasPermissionCourse(Guid courseId);
    }
    public class CourseService : BaseService, ICourseService
    {
        private IFileUlti _fileUlti;
        public CourseService(IHttpContextAccessor httpContextAccessor,
                                IDistributedCacheCustom cache,
                                IUnitOfWork unitOfWork, IMapper mapper, IFileUlti fileUlti) : base(httpContextAccessor, cache, unitOfWork, mapper)
        {
            _fileUlti = fileUlti;
        }

        /// <summary>
        /// Hàm xử lý lấy top 10 khóa học hàng đầu
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CourseGeneric>> GetTop10GoodCourse()
        {
            var sortedList = new List<SortedPaging>()
            {
                new SortedPaging
                {
                    Field = "Rating",
                    IsAsc = false
                },
                new SortedPaging
                {
                    Field = "TotalPerRating",
                    IsAsc = false
                }
            };
            return await _unitOfWork.CourseRepository.GetCourseByCondition(10, sortedList);
        }

        /// <summary>
        /// Hàm xử lý lấy top 10 khóa học mới nhất
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CourseGeneric>> GetTop10NewCourse()
        {
            var sortedList = new List<SortedPaging>()
            {
                new SortedPaging
                {
                    Field = "CreatedDate",
                    IsAsc = false
                }
            };
            return await _unitOfWork.CourseRepository.GetCourseByCondition(10, sortedList);
        }

        /// <summary>
        /// Hàm xử lý lấy tất cả khóa học user đã đăng ký
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MyCourseVM> GetListCourseByUser()
        {
            var accId = GetUserAuthen()?.AccoutantId;
            if (accId != null)
            {
                return _unitOfWork.CourseRepository.GetListCourseByUser((Guid)accId);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Hàm xử lý lấy chi tiết khóa học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<CourseDetailVM> GetDetailCourse(Guid courseId)
        {
            // 2 luồng này nên tách riêng, nhưng EF lại không cho dùng context 1 lúc 2 bên nên phải wait

            // lấy thông tin master của khóa học 
            var courseMaster = await _unitOfWork.CourseRepository.GetCourseByCondition(1, null, item => item.Id == courseId);
            // lấy thông tin của detail khóa học
            var courseDetail = _unitOfWork.CourseRepository.GetDetailCourse(courseId);

            var result = new CourseDetailVM();
            if (courseMaster != null && courseMaster.Count() > 0)
            {
                result.CourseMaster = courseMaster.First();
            }

            if (courseDetail != null && courseDetail.Count() > 0)
            {
                // dùng dictionary tìm nhanh
                var courseDetailList = new Dictionary<Guid, ChapterDetail>();
                foreach (var item in courseDetail)
                {
                    // nếu không gồm key thì add vào
                    if (!courseDetailList.ContainsKey(item.ChapterId))
                    {
                        var lessionDetailList = courseDetail.Where(itemChild => item.ChapterId == itemChild.ChapterId).Select(item => new LessionDetail
                        {
                            LessionId = item.LessionId,
                            LessionName = item.LessionName,
                            TotalHourTimeLession = item.TotalHourTimeLession,
                            TotalTimeLession = item.TotalTimeLession,
                            VideoLink = item.VideoLink,
                            TestLink = item.TestLink,
                            LessionLink = item.LessionLink,
                            Text = item.Text,
                            AttachmentsLink = item.AttachmentsLink,
                            CourseId = courseId,
                        }).ToList();
                        courseDetailList.Add(item.ChapterId, new ChapterDetail()
                        {
                            ChapterId = item.ChapterId,
                            ChapterName = item.ChapterName,
                            LessionDetailList = lessionDetailList
                        });
                    }
                }
                // gom nhóm lại detail
                result.CourseDetailList = courseDetailList.Values.ToList();
            }

            return result;
        }

        /// <summary>
        /// Hàm xử lý lấy 
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CourseGeneric>> GetCourseSearchCourseByCondition(SearchCourseParam searchCourseParam)
        {
            var sortedList = new List<SortedPaging>();
            Expression<Func<CourseGeneric, bool>> predicateFilter = null;
            if (searchCourseParam.TypeOfPurchase != null)
            {
                predicateFilter = (item => item.TypeOfPurchase == searchCourseParam.TypeOfPurchase);
            }

            if (searchCourseParam.TagId != null)
            {
                predicateFilter = predicateFilter != null? ExtensionUlti.PedicateAnd(predicateFilter, (item => item.TagIdList.Contains((Guid)searchCourseParam.TagId) )): (item => item.TagIdList.Contains((Guid)searchCourseParam.TagId));
            }

            if (searchCourseParam.Rating != null)
            {
                predicateFilter = predicateFilter != null ? ExtensionUlti.PedicateAnd(predicateFilter, (item => item.Rating == searchCourseParam.Rating)) : (item => item.Rating == searchCourseParam.Rating);
            }

            if (searchCourseParam.SortField != null)
            {
                sortedList = new List<SortedPaging>()
                {
                    searchCourseParam.SortField
                };
            }
            var result = await _unitOfWork.CourseRepository.GetCourseByCondition(null, sortedList, predicateFilter);
            return result;
        }

        /// <summary>
        /// Hàm xử lý thêm mới master khóa học
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<RestOutput> CreateCourseMaster(CreateCourseVM createCourseParam)
        {
            var res = new RestOutput();
            var authorCurrent = GetUserAuthen().UserName;
            if (createCourseParam != null)
            {
                if (string.IsNullOrEmpty(createCourseParam.Introduce))
                {
                    res.ErrorEventHandler("Giới thiệu khóa học không được để trống");
                    return res;
                }
                else if (string.IsNullOrEmpty(createCourseParam.CourseName))
                {
                    res.ErrorEventHandler("Tên khóa học không được để trống");
                    return res;
                }

                if (createCourseParam.ImgCourse != null)
                {
                    createCourseParam.ImgCourse = await _fileUlti.SaveFileBase64(createCourseParam.ImgCourseFile);
                }

                // insert khóa học
                var courseInsert = _mapper.Map<Course>(createCourseParam);
                if (Guid.TryParse(authorCurrent, out Guid authorId))
                {
                    courseInsert.CreatedBy = authorId;
                    courseInsert.ModifiedBy = authorId;
                }
                
                _unitOfWork.CourseRepository.Create(courseInsert);

                // insert tag và course
                if (createCourseParam.TagList != null && createCourseParam.TagList.Count > 0)
                {
                    var courseTagList = new List<CourseTag>();
                    foreach (var item in createCourseParam.TagList)
                    {
                        var courseTagInsert = new CourseTag()
                        {
                            CourseId = courseInsert.Id,
                            TagId = item
                        };
                        courseTagList.Add(courseTagInsert);
                    }

                    if (courseTagList.Count > 0)
                    {
                        _unitOfWork.CourseTagRepository.CreateRange(courseTagList);
                    }
                }

                await _unitOfWork.CommitAsync();

                res.SuccessEventHandler(courseInsert.Id);
            }
            res.SuccessEventHandler();

            return res;

        }

        /// <summary>
        /// Hàm kiểm tra user có quyền xem khóa học không
        /// </summary>
        /// <returns></returns>
        public RestOutput CheckUserHasPermissionCourse(Guid courseId)
        {
            var res = new RestOutput();
            var currentAcc = GetUserAuthen()?.AccoutantId;
            if (currentAcc != null)
            {
                // tìm trong bảng nhiều nhiều có kèm 2 điều kiện là success
                var rowPermission = _unitOfWork.CourseAccountsRepository.FirstOrDefault(item => item.AccountId == currentAcc && item.CourseId == courseId);
                if (rowPermission != null)
                {
                    res.SuccessEventHandler(true);
                    return res;
                }
            }
            res.SuccessEventHandler(false);
            return res;
        }

        #region Private Method

        #endregion
    }
}
