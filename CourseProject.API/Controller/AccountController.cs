using CourseProject.API.Services;
using Microsoft.AspNetCore.Mvc;
using CourseProject.API.Common.Attribute;
using CourseProject.API.Common.Constant;
using CourseProject.Model.ViewModel;
using CourseProject.Model.ViewModel.Accountant;

namespace CourseProject.API.Controller.Base
{
    public class AccountController : BaseController
    {
        private IAccountService _accountService;
        public AccountController(IRestOutput res, IHttpContextAccessor httpContextAccessor,
                                    IAccountService accountService) : base(res, httpContextAccessor)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Hàm xử lý đăng nhập
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <param name="loginInfor"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginInfor)
        {
            try
            {
                var userResult = await _accountService.Login(loginInfor.Email, loginInfor.Password);
                if (userResult != null && !string.IsNullOrEmpty(userResult.Token))
                {
                    _res.SuccessEventHandler(userResult);
                }
            }
            catch (Exception ex)
            {
                _res.ErrorEventHandler(null, ex.Message);
            }

            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý đăng xuất
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            _accountService.Logout();
            _res.SuccessEventHandler("Đăng xuất thành công");
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý đăng ký
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register(AccountRegister acc)
        {
            if (acc != null)
            {
                var isSuccess = await _accountService.Register(acc);
                if (isSuccess)
                {
                    _res.SuccessEventHandler("Đăng ký thành công");
                }
                else
                {
                    _res.ErrorEventHandler();
                }
                
            }
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý lấy thông tin user
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [HttpGet("GetUserInforGeneric")]
        [Roles(RoleConstant.Customer, RoleConstant.Teacher)]
        public async Task<IActionResult> GetUserInforGeneric()
        {
            var userData = await _accountService.GetUserInforGeneric();
            if (userData != null)
            {
                _res.SuccessEventHandler(userData);
            }
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý cập nhật thông tin user
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [HttpPost("UpdateUserInfor")]
        [Roles(RoleConstant.Customer, RoleConstant.Teacher)]
        public async Task<IActionResult> UpdateUserInfor(AccountUpdate acc)
        {
            _res = await _accountService.UpdateUserInfor(acc);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý cập nhật password
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [HttpPost("ChangePassword")]
        [Roles(RoleConstant.Customer, RoleConstant.Teacher)]
        public async Task<IActionResult> ChangePassword(ChangPasswordVM password)
        {
            _res = await _accountService.ChangePassword(password.NewPassword, password.OldPassword);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lấy thông tin user
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [HttpPost("GetUserInfor")]
        [Roles(RoleConstant.Customer, RoleConstant.Teacher)]
        public IActionResult GetUserInfor()
        {
            var userInfor = _accountService.GetUserInfor();
            _res.SuccessEventHandler(userInfor);
            return Ok(_res);
        }
    }
}
