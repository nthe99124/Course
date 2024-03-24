using CourseProject.Model.BaseEntity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.API.Common.Repository;

public partial class CourseContext : DbContext
{
    public CourseContext(DbContextOptions<CourseContext> options)
        : base(options)
    {

    }

    #region Các model hứng dữ liệu
    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseTag> CourseTags { get; set; }

    public virtual DbSet<Lession> Lessions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleAccount> RoleAccountants { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<CourseAccount> CourseAccounts { get; set; }
    
    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.ToTable("Chapter");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Course).WithMany(p => p.Chapters)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Chapter_Course");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Language)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PriceAfterDiscount).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Course_Accounts");
        });

        modelBuilder.Entity<CourseTag>(entity =>
        {
            entity.ToTable("CourseTag");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Course).WithMany(p => p.CourseTags)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseTag_Course");

            entity.HasOne(d => d.Tag).WithMany(p => p.CourseTags)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("FK_CourseTag_Tag");
        });

        modelBuilder.Entity<Lession>(entity =>
        {
            entity.ToTable("Lession");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Chapter).WithMany(p => p.Lessions)
                .HasForeignKey(d => d.ChapterId)
                .HasConstraintName("FK_Lession_Chapter");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<RoleAccount>(entity =>
        {
            entity.ToTable("RoleAccount");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Account).WithMany(p => p.RoleAccountAccounts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_RoleAccount_Accounts");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RoleAccountCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_RoleAccountant_Accounts1");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleAccounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_RoleAccountant_Role");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tag");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });
        OnModelCreatingPartial(modelBuilder);
    }

    #region override custom savechanges
    public override int SaveChanges()
    {
        var validModel = ValidateModel();
        if (!validModel.IsValid && !string.IsNullOrEmpty(validModel.ErrorMessage))
        {
            // Ném ra một exception với thông điệp lỗi
            throw new InvalidOperationException(validModel.ErrorMessage);
        }
        TrimStringPropertype();
        return base.SaveChanges();
    }

    /// <summary>
    /// Xử lý trim dữ liệu trước khi lưu
    /// CreatedBy ntthe 24.03.2024
    /// </summary>
    private void TrimStringPropertype()
    {
        var entities = ChangeTracker.Entries()
                        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
        foreach (var item in entities)
        {
            var properties = item.Properties.Where(p => p.CurrentValue is string).Select(p => p);
            foreach (var property in properties)
            {
                var currentValue = property.CurrentValue?.ToString();
                if (currentValue != null)
                {
                    property.CurrentValue = currentValue.Trim();
                }
            }
        }
    }

    /// <summary>
    /// Kiểm tra dữ liệu null có phù hợp kiểu dữ liệu không trước khi lưu (chỉ xảy ra với string)
    /// CreatedBy ntthe 24.03.2024
    /// </summary>
    public (bool IsValid, string ErrorMessage) ValidateModel()
    {
        var entities = ChangeTracker.Entries()
                        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).Select(e => e.Entity);
        var validationContext = new ValidationContext(entities);
        var validationResults = new List<ValidationResult>();

        // Kiểm tra tính hợp lệ của model
        bool isValid = Validator.TryValidateObject(entities, validationContext, validationResults, true);
        if (!isValid)
        {
            // Lặp qua các lỗi và tạo thông điệp lỗi
            string errorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
            return (false, errorMessage);
        }

        return (true, "");
    }
    #endregion

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
