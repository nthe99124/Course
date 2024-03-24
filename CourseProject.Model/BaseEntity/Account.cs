using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static CourseProject.Model.Enum.DataType;

namespace CourseProject.Model.BaseEntity;

public partial class Account
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("UserName")]
    public string UserName { get; set; }

    [Description("Password")]
    public string Password { get; set; }

    [Description("Ngày tạo")]
    public DateTime? CreatedDate { get; set; }

    [Description("Ngày sinh nhật")]
    public DateTime? DateOfBirth { get; set; }

    [Description("Email")]
    public string Email { get; set; }

    [Description("Giới tính")]
    public GenderType? Gender { get; set; }

    [Description("Link ảnh avatar")]
    public string ImgAvatar { get; set; }

    [Description("Giới thiệu")]
    public string Introduce { get; set; }

    [Description("Tên")]
    public string FirstName { get; set; }

    [Description("Họ")]
    public string LastName { get; set; }

    [Description("Số điện thoại")]
    public string PhoneNumber { get; set; }

    [Description("Link Facebook")]
    public string FacebookLink { get; set; }

    [Description("Link Twitter")]
    public string TwitterLink { get; set; }

    [Description("Link Linkedin")]
    public string LinkedinLink { get; set; }
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<RoleAccount> RoleAccountAccounts { get; set; } = new List<RoleAccount>();

    public virtual ICollection<RoleAccount> RoleAccountCreatedByNavigations { get; set; } = new List<RoleAccount>();

    public virtual ICollection<CourseAccount> CourseAccountAccounts { get; set; } = new List<CourseAccount>();

}
