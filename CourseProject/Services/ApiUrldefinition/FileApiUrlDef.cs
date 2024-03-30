namespace CourseProject.Services.ApiUrldefinition
{
    public class FileApiUrlDef
    {
        private static readonly string pathController = "/api/File";

        /// <summary>
        /// Tạo url upload video
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public static string UploadVideo()
        {
            return @$"{pathController}/video/upload";
        }

        /// <summary>
        /// Tạo url xóa video
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public static string DeleteVideo(string videoName)
        {
            return @$"{pathController}/video/{videoName}";
        }
    }
}
