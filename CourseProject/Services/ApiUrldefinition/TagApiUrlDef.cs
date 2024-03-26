namespace CourseProject.Services.ApiUrldefinition
{
    public class TagApiUrlDef
    {
        private static readonly string pathController = "/api/Tag";

        /// <summary>
        /// Tạo url lấy tất cả các tag
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public static string GetAllTag()
        {
            return @$"{pathController}/GetAllTag";
        }
    }
}
