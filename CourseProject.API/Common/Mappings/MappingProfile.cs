﻿using AutoMapper;
using CourseProject.Model.BaseEntity;
using CourseProject.Model.ViewModel.Account;

namespace CourseProject.API.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountRegister, Account>();
        }
    }
}
