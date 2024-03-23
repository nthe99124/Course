﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.API.Common.Repository;

public partial class CourseContext : DbContext
{
    private IConfiguration _configuration;

    public CourseContext(DbContextOptions<CourseContext> options)
        : base(options)
    {

    }

    #region Các model hứng dữ liệu
    
    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

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
    /// CreatedBy ntthe 25.02.2024
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
    /// CreatedBy ntthe 25.02.2024
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
