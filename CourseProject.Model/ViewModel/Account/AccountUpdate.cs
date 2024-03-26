using static CourseProject.Model.Enum.DataType;

namespace CourseProject.Model.ViewModel.Account
{
    public class AccountUpdate
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public GenderType? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Introduce { get; set; }
        public string ImgAvatar { get; set; }
    }
}
