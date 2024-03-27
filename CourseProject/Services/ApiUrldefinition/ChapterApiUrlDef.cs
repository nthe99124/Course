namespace CourseProject.Services.ApiUrldefinition
{
    public class ChapterApiUrlDef
    {
        private static readonly string pathController = "/api/Chapter";

        /// <summary>
        /// Tạo url thêm mới chương
        /// CreatedBy ntthe 27.03.2024
        /// </summary>
        /// <returns></returns>
        public static string CreateChapter()
        {
            return @$"{pathController}/CreateChapter";
        }

        /// <summary>
        /// Tạo url đổi tên chương
        /// CreatedBy ntthe 27.03.2024
        /// </summary>
        /// <returns></returns>
        public static string EditChapter()
        {
            return @$"{pathController}/EditChapter";
        }
    }
}
