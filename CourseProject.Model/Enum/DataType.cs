using System.ComponentModel;

namespace CourseProject.Model.Enum
{
    public class DataType
    {
        /// <summary>
        /// Loại thanh toán
        /// </summary>
        public enum TypeOfPurchase : short
        {
            [Description("Nạp tiền")]
            Free,
            [Description("Trả phí")]
            Paid,
            [Description("Không bán lẻ (Bán combo hoặc membership)")]
            NotForRetail,
        }

        /// <summary>
        /// Giới tính
        /// </summary>
        public enum GenderType : short
        {
            [Description("Nam")]
            Male,
            [Description("Nữ")]
            Female,
            [Description("Khác")]
            Other,
        }

        /// <summary>
        /// Trạng thái học
        /// </summary>
        public enum CourseAccountStatus : short
        {
            [Description("Chưa học")]
            NotStarted,
            [Description("Đang học")]
            InProgress,
            [Description("Đã học xong")]
            Completed,
        }

    }
}
