using CourseProject.API.Common.Repository;
using CourseProject.API.Repositories.Base;
using CourseProject.Model.BaseEntity;

namespace CourseProject.API.Repositories
{
    public interface ICourseAccountsRepository : IBaseRepository<CourseAccount>
    {
        
    }
    public class CourseAccountsRepository : BaseRepository<CourseAccount>, ICourseAccountsRepository
    {
        public CourseAccountsRepository(CourseContext context) : base(context)
        {

        }
    }
}
