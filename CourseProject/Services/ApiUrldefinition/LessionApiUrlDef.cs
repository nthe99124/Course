namespace CourseProject.Services.ApiUrldefinition
{
    public class LessionApiUrlDef
    {
        private static readonly string pathController = "/api/Lession";

        /// <summary>
        /// Tạo url thêm mới bài học
        /// CreatedBy ntthe 27.03.2024
        /// </summary>
        /// <returns></returns>
        public static string CreateLession()
        {
            return @$"{pathController}/CreateLession";
        }

        /// <summary>
        /// Tạo url sửa bài học
        /// CreatedBy ntthe 27.03.2024
        /// </summary>
        /// <returns></returns>
        public static string EditLession()
        {
            return @$"{pathController}/EditLession";
        }

        /// <summary>
        /// Tạo url xóa bài học
        /// CreatedBy ntthe 27.03.2024
        /// </summary>
        /// <returns></returns>
        public static string DeleteLession()
        {
            return @$"{pathController}/DeleteLession";
        }

        /// <summary>
        /// Tạo url sửa tên bài học
        /// CreatedBy ntthe 27.03.2024
        /// </summary>
        /// <returns></returns>
        public static string EditLessionName()
        {
            return @$"{pathController}/EditLessionName";
        }
    }
}
