using AutoMapper;
using CourseProject.Model.BaseEntity;
using CourseProject.Model.ViewModel.Account;
using CourseProject.Model.ViewModel.Course;

namespace CourseProject.API.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountRegister, Account>();
            CreateMap<CreateCourseVM, Course>();
        }
    }
}
