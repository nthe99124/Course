namespace CourseProject.Services.ApiUrldefinition
{
    public class AccountApiUrlDef
    {
        private static readonly string pathController = "/api/Account";

        /// <summary>
        /// Tạo url đăng nhập
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public static string Login()
        {
            return @$"{pathController}/Login";
        }

        /// <summary>
        /// Tạo url đăng ký
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public static string Register()
        {
            return @$"{pathController}/Register";
        }

        /// <summary>
        /// Lấy thông tin user
        /// </summary>
        public static string GetUserInforGeneric()
        {
            return @$"{pathController}/GetUserInforGeneric";
        }

        /// <summary>
        /// Lấy thông tin user
        /// </summary>
        public static string UpdateUserInfor()
        {
            return @$"{pathController}/UpdateUserInfor";
        }

        /// <summary>
        /// Cập nhật mật khẩu
        /// </summary>
        public static string ChangePassword()
        {
            return @$"{pathController}/ChangePassword";
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        public static string Logout()
        {
            return @$"{pathController}/Logout";
        }

        /// <summary>
        /// Lấy url danh sách giảng viên
        /// </summary>
        /// <returns></returns>
        public static string GetTeacherList()
        {
            return @$"{pathController}/GetTeacherList";
        }
        
    }
}
