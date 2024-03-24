using CourseProject.API.Common.Repository;
using CourseProject.API.Repositories.Base;
using CourseProject.Model.BaseEntity;

namespace CourseProject.API.Repositories
{
    public interface IRoleAccountRepository : IBaseRepository<RoleAccount>
    {
        
    }
    public class RoleAccountRepository : BaseRepository<RoleAccount>, IRoleAccountRepository
    {
        public RoleAccountRepository(CourseContext context) : base(context)
        {

        }
    }
}
