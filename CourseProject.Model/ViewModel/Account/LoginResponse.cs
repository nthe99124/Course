namespace CourseProject.Model.ViewModel.Account
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public List<string> RoleList { get; set; }
        public string ImgAvatar { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
