﻿namespace CourseProject.Services.ApiUrldefinition
{
    public class CourseApiUrlDef
    {
        private static readonly string pathController = "/api/Course";

        /// <summary>
        /// Tạo url lấy top 10 khóa học hằng đầu (tính theo lượt đánh giá và lượt người đánh giá cao nhất)
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public static string GetTop10GoodCourse()
        {
            return @$"{pathController}/GetTop10GoodCourse";
        }

        /// <summary>
        /// Tạo url lấy top 10 khóa học mới nhất
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public static string GetTop10NewCourse()
        {
            return @$"{pathController}/GetTop10NewCourse";
        }

        /// <summary>
        /// Tạo url lấy tất cả khóa học user đã đăng ký
        /// CreatedBy ntthe 24.03.2024
        /// </summary>
        /// <returns></returns>
        public static string GetListCourseByUser()
        {
            return @$"{pathController}/GetListCourseByUser";
        }


    }
}