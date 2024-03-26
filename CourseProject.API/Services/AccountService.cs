using AutoMapper;
using CourseProject.API.Common.Cache;
using CourseProject.API.Common.Constant;
using CourseProject.API.Common.Repository;
using CourseProject.API.Common.Ulti;
using CourseProject.API.Services.Base;
using CourseProject.Model.BaseEntity;
using CourseProject.Model.DTO.Account;
using CourseProject.Model.ViewModel;
using CourseProject.Model.ViewModel.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace CourseProject.API.Services
{
    public interface IAccountService
    {
        Task<LoginResponse> Login(string email, string password);
        Task<bool> Register(AccountRegister account);
        void Logout();
        AccountGenericDTO GetUserInfor();
        Task<RestOutput> UpdateUserInfor(AccountUpdate account);
        Task<RestOutput> ChangePassword(string newPassword, string oldPassword);
        Task<AccountUpdate> GetUserInforGeneric();
    }
    public class AccountService: BaseService, IAccountService
    {
        private readonly IConfiguration _configuration;
        public AccountService(IHttpContextAccessor httpContextAccessor, IDistributedCacheCustom cache, IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper) : base(httpContextAccessor, cache, unitOfWork, mapper)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Hàm xử lý login
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<LoginResponse> Login(string email, string password)
        {
            var account = await _unitOfWork.AccountRepository.GetUserByUserNameAndPass(email, password);
            if (account != null)
            {
                // lấy role
                var listRole = _unitOfWork.AccountRepository.GetListRoleByAccId(account.Id);
                var token = HandleSignInAndGenerateToken(account, listRole);

                if (token != null)
                {
                    // ghi token vào cookie
                    _httpContextAccessor?.HttpContext?.Response.Cookies.Append("access_token", token, new CookieOptions
                    {
                        HttpOnly = true, // Set HttpOnly to true for security
                        Secure = true,   // Set Secure to true if your site uses HTTPS
                        SameSite = SameSiteMode.Strict, // Set SameSite to Strict for added security
                        Expires = DateTime.UtcNow.AddDays(1) // Set the expiration time
                    });
                    var result = new LoginResponse();
                    result.Token = token;
                    result.Email = account.Email;
                    result.FullName = account.LastName + " " + account.FirstName;
                    result.RoleList = listRole.Select(item => item.RoleName).ToList();
                    return result;
                }
            }
            else
            {
                throw new Exception("Sai tài khoản hoặc mật khẩu");
            }
            return null;
        }

        /// <summary>
        /// Hàm xử lý đăng kí
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <param name="account"></param>
        /// <param name="hasLogin"></param>
        /// <returns></returns>
        public async Task<bool> Register(AccountRegister account)
        {
            var acc = await _unitOfWork.AccountRepository.GetUserByUserNameAndPass(account.Email, account.Password);
            if (acc == null)
            {
                // encode pass trước khi cất
                account.Password = HashCodeUlti.EncodePassword(account.Password);
                var accountInsert = _mapper.Map<Account>(account);
                // cất
                _unitOfWork.AccountRepository.Create(accountInsert);

                // thêm role
                var customer = await _unitOfWork.RoleRepository.FirstOrDefault(f => f.RoleName == "Customer");
                _unitOfWork.RoleAccountRepository.Create(new RoleAccount
                {
                    RoleId = customer.Id,
                    AccountId = accountInsert.Id,
                });
                _unitOfWork.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Hàm xử lý đăng xuất
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public void Logout()
        {
            // SignOut
            _httpContextAccessor?.HttpContext?.Response?.Cookies.Delete("access_token");
        }

        /// <summary>
        /// Hàm lấy thông tin user theo authen
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public AccountGenericDTO GetUserInfor()
        {
            return GetUserAuthen();
        }

        /// <summary>
        /// Hàm lấy thông tin user
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<AccountUpdate> GetUserInforGeneric()
        {
            var userName = GetUserAuthen()?.UserName;
            if (!string.IsNullOrEmpty(userName))
            {
                var userFull = await _unitOfWork.AccountRepository.FirstOrDefault(item => item.UserName == userName);
                if (userFull != null)
                {
                    return new AccountUpdate
                    {
                        UserName = userFull.UserName,
                        FirstName = userFull.FirstName,
                        LastName = userFull.LastName,
                        Email = userFull.Email,
                        Gender = userFull.Gender,
                        DateOfBirth = userFull.DateOfBirth,
                        Introduce = userFull.Introduce,
                        ImgAvatar = userFull.ImgAvatar
                    };
                }
            }
            return null;
        }

        /// <summary>
        /// Hàm xử lý cập nhật thông tin user
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <param name="account"></param>
        /// <param name="hasLogin"></param>
        /// <returns></returns>
        public async Task<RestOutput> UpdateUserInfor(AccountUpdate account)
        {
            var res = new RestOutput();
            if (account != null)
            {
                var isExistUser = await _unitOfWork.AccountRepository.CheckExitsByCondition(item => item.UserName == account.UserName);
                if (isExistUser)
                {
                    res.ErrorEventHandler("Username đã tồn tại");
                }
                else
                {
                    var userNameCurrent = GetUserAuthen()?.UserName;
                    var accUserUpdate = await _unitOfWork.AccountRepository.FirstOrDefault(item => item.UserName == userNameCurrent);
                    if (accUserUpdate != null)
                    {
                        // update từng field của user
                        accUserUpdate.UserName = account.UserName;
                        accUserUpdate.FirstName = account.FirstName;
                        accUserUpdate.LastName = account.LastName;
                        accUserUpdate.Email = account.Email;
                        accUserUpdate.Gender = account.Gender;
                        accUserUpdate.DateOfBirth = account.DateOfBirth;
                        accUserUpdate.Introduce = account.Introduce;
                        accUserUpdate.ImgAvatar = account.ImgAvatar;

                        // lưu acc
                        _unitOfWork.Commit();
                        var dataResponse = new UserInforGeneric
                        {
                            Email = accUserUpdate.Email,
                            ImgAvatar = accUserUpdate.ImgAvatar,
                        };
                        res.SuccessEventHandler(dataResponse);
                    }
                }
            }
            
            return res;
            
        }

        /// <summary>
        /// Hàm xử lý cập nhật password
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <param name="account"></param>
        /// <param name="hasLogin"></param>
        /// <returns></returns>
        public async Task<RestOutput> ChangePassword(string newPassword, string oldPassword)
        {
            var res = new RestOutput();
            if (!string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(oldPassword))
            {
                // nếu trùng là chửi
                if (newPassword.Trim() == oldPassword.Trim())
                {
                    res.ErrorEventHandler("Password mới trùng với password cũ");
                }
                else
                {
                    var userNameCurrent = GetUserAuthen()?.UserName;
                    var accUserUpdate = await _unitOfWork.AccountRepository.FirstOrDefault(item => item.UserName == userNameCurrent);

                    var oldPasswordHash = HashCodeUlti.EncodePassword(oldPassword);

                    if (accUserUpdate != null && accUserUpdate.Password != oldPasswordHash)
                    {
                        res.ErrorEventHandler("Password cũ không chính xác");
                    }
                    else
                    {
                        var passWordHash = HashCodeUlti.EncodePassword(newPassword);
                        // update từng field của user
                        accUserUpdate.Password = passWordHash;

                        // lưu acc
                        _unitOfWork.Commit();
                        res.SuccessEventHandler(true);
                    }
                }
            }
            else
            {
                res.ErrorEventHandler("Password không được để trống");
            }

            return res;
        }

        #region Private Method

        #region phần đăng nhập sử dụng BearToken
        /// <summary>
        /// Hàm ghi claim và gen AccessToken
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private string HandleSignInAndGenerateToken(Account account, IEnumerable<Role> listRole)
        {
            if (_configuration != null)
            {
                var jwtToken = new JwtSecurityTokenHandler();
                var secretKeyByte = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt").GetSection("SecretKey").Value);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(JwtRegisteredClaimsNamesConstant.Sid, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimsNamesConstant.Sub, account.Email),
                        new Claim(JwtRegisteredClaimsNamesConstant.AccId, account.Id.ToString()),
                        new Claim(JwtRegisteredClaimsNamesConstant.Jti, Guid.NewGuid().ToString()),
                    }),
                    //Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyByte), SecurityAlgorithms.HmacSha256)
                };

                // xử lý add Role

                if (listRole != null && listRole.Count() > 0)
                {
                    foreach (var role in listRole)
                    {
                        if (role != null && !string.IsNullOrEmpty(role.RoleName))
                        {
                            tokenDescription?.Subject.AddClaim(new Claim(JwtRegisteredClaimsNamesConstant.Role, role.RoleName));
                            tokenDescription?.Subject.AddClaim(new Claim(JwtRegisteredClaimsNamesConstant.RoleInfor, JsonSerializer.Serialize(role)));
                        }
                    }
                }

                var token = jwtToken.CreateToken(tokenDescription);
                return jwtToken.WriteToken(token);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Luồng đăng nhập với cookie
        /// <summary>
        /// Hàm xử lý sign cho đăng nhập với loại đăng nhập là cookie
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [Obsolete]
        private async Task<AccountGenericDTO> HandleSignInWithCookieSign(Account account)
        {
            var authenObject = SignClaimWriteToken(account);
            if (authenObject != null)
            {
                await _httpContextAccessor?.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                                                            new ClaimsPrincipal(authenObject.ClaimsIdentity), 
                                                            authenObject.AuthProperties);
                var data = new AccountGenericDTO
                {
                    UserName = account.UserName,
                };
                return data;
            }

            return null;
        }

        /// <summary>
        /// Hàm ghi thông tin vào claim
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [Obsolete]
        private AccountAuthenDTO SignClaimWriteToken(Account account)
        {
            var listClaim = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimsNamesConstant.Sid, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimsNamesConstant.Sub, account.UserName),
                    new Claim(JwtRegisteredClaimsNamesConstant.Jti, Guid.NewGuid().ToString())
                };

            //if (account.IsAdmin)
            //{
            //    listClaim.Add(new Claim(ClaimTypes.Role, RoleConstant.Admin));
            //}
            //else
            //{
            //    listClaim.Add(new Claim(ClaimTypes.Role, RoleConstant.Customer));
            //}

            var claimsIdentity = new ClaimsIdentity(listClaim, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                // Thông tin cấu hình cookie, ví dụ như thời gian hết hạn
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                IsPersistent = false, // Đặt true nếu bạn muốn cookie "nhớ đăng nhập" qua các session
                AllowRefresh = false
            };

            return new AccountAuthenDTO
            {
                ClaimsIdentity = claimsIdentity,
                AuthProperties = authProperties,
                UserName = account.UserName
            };
        
        }

        /// <summary>
        /// Hàm xử lý đăng xuất
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task LogoutCookie()
        {
            // SignOut
            await _httpContextAccessor?.HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _httpContextAccessor?.HttpContext?.Session.Clear();
        }

        
        #endregion
        #endregion
    }
}
