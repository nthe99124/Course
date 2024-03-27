using CourseProject.Common.Cache;
using CourseProject.Model.ViewModel;
using CourseProject.Model.ViewModel.Account;
using CourseProject.Services.ApiUrldefinition;
using CourseProject.Services.Base;
using Microsoft.JSInterop;

namespace CourseProject.Services
{
    public interface IAccountService : IBaseService
    {
        Task<ResponseOutput<LoginResponse>> Login(LoginViewModel loginViewModel);
        Task<ResponseOutput<string>> Register(AccountRegister loginViewModel);
        Task<AccountUpdate> GetUserInforGeneric();
        Task<ResponseOutput<UserInforGeneric>> UpdateUserInfor(AccountUpdate account);
        Task<ResponseOutput<string>> ChangePassword(ChangPasswordVM password);
        Task<ResponseOutput<string>> Logout();
        Task<Dictionary<Guid, string>> GetTeacherList();
    }

    public class AccountService : BaseService, IAccountService
    {
        public AccountService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory, IConfiguration config, IJSRuntime js) : base(cache, httpClientFactory, config, js)
        {

        }
        /// <summary>
        /// Hàm xử lý login
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<LoginResponse>> Login(LoginViewModel loginViewModel)
        {
            var url = AccountApiUrlDef.Login();
            var res = await RequestFullPostAsync<LoginResponse>(url, loginViewModel);
            return res;
        }

        /// <summary>
        /// Hàm xử lý đăng ký
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> Register(AccountRegister loginViewModel)
        {
            var url = AccountApiUrlDef.Register();
            return await RequestFullPostAsync<string>(url, loginViewModel);
        }

        /// <summary>
        /// Lấy thông tin user
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        public async Task<AccountUpdate> GetUserInforGeneric()
        {
            var url = AccountApiUrlDef.GetUserInforGeneric();
            return await RequestAuthenGetAsync<AccountUpdate>(url);
        }

        /// <summary>
        /// Cập nhật thông tin user
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<ResponseOutput<UserInforGeneric>> UpdateUserInfor(AccountUpdate account)
        {
            var url = AccountApiUrlDef.UpdateUserInfor();
            return await RequestFullAuthenPostAsync<UserInforGeneric>(url, account);
        }

        /// <summary>
        /// Cập nhật thông tin user
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> ChangePassword(ChangPasswordVM password)
        {
            var url = AccountApiUrlDef.ChangePassword();
            return await RequestFullAuthenPostAsync<string>(url, password);
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> Logout()
        {
            var url = AccountApiUrlDef.Logout();
            return await RequestFullPostAsync<string>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách giảng viên
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<Dictionary<Guid, string>> GetTeacherList()
        {
            var url = AccountApiUrlDef.GetTeacherList();
            return await RequestAuthenGetAsync<Dictionary<Guid, string>>(url);
        }
    }
}
