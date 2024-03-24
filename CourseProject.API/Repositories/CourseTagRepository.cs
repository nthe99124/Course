using CourseProject.API.Common.Repository;
using CourseProject.API.Repositories.Base;
using CourseProject.Model.BaseEntity;

namespace CourseProject.API.Repositories
{
    public interface ICourseTagRepository : IBaseRepository<CourseTag>
    {
        
    }
    public class CourseTagRepository : BaseRepository<CourseTag>, ICourseTagRepository
    {
        public CourseTagRepository(CourseContext context) : base(context)
        {

        }
    }
}
