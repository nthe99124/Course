﻿namespace CourseProject.Model.DTO.Account
{
    public class AccountGenericDTO
    {
        public Guid AccoutantId { get; set; }
        public string UserName { get; set; }
        public long Coin { get; set; }
        public string Language { get; set; } = "vi-VN";
        public List<string> RoleList { get; set; }
    }
}
